#region

using System;
using System.Text.Json.Serialization;
using Aspose.Words;
using Coravel;
using Hangfire;
using ITC.Domain.Core.Behaviors;
using ITC.Service.API.Configurations;
using ITC.Service.API.Extensions;
using ITC.Service.API.Helpers;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NCore.Systems;
using OfficeOpenXml;
using RateLimitRequest.Middleware;
using RateLimitRequest.Models;
using RateLimitRequest.Store;

#endregion

namespace ITC.Service.API;

/// <summary>
/// </summary>
public class Startup
{
#region Constructors

    //public Startup(IConfiguration configuration)
    //{
    //    Configuration = configuration;
    //}

    /// <summary>
    /// </summary>
    /// <param name="env"></param>
    public Startup(IHostEnvironment env)
    {
        var builder = new ConfigurationBuilder()
                      .SetBasePath(env.ContentRootPath)
                      .AddJsonFile("appsettings.json",                        true, true)
                      .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true);

        //if (env.IsDevelopment())
        //{
        //    builder.AddUserSecrets<Startup>();
        //}

        builder.AddEnvironmentVariables();
        Configuration = builder.Build();
        HistoryHelper.RegisterHistory();
        new License().SetLicense(LicenseAspose.LStream);
        // new Aspose.Pdf.License().SetLicense(LicenseAspose.LStream);
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
    }

#endregion

#region Properties

    ///// <summary>
    /////     Phương thức này được gọi sau <see cref="ConfigureServices(IServiceCollection)" />
    ///// </summary>
    ///// <param name="builder">Autofac ContainerBuilder</param>
    //public void ConfigureContainer(ContainerBuilder builder)
    //{
    //    builder.RegisterGeneric(typeof(ValidatorBehavior<,>))
    //     .As(typeof(IPipelineBehavior<,>))
    //     .InstancePerLifetimeScope();
    //}
    /// <summary>
    /// </summary>
    public IConfiguration Configuration { get; }

#endregion

#region Methods

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    /// <summary>
    /// </summary>
    /// <param name="app"></param>
    /// <param name="env"></param>
    /// <param name="loggerFactory"></param>
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
    {
        if (env.IsDevelopment()) app.UseDeveloperExceptionPage();



        app.UseHttpsRedirection();
        app.UseStaticFiles();

        // app.UseRouting();
        app.UseSession();

        app.UseCors("CorsPolicy");
        app.UseAuthentication();
        app.UseMiddleware<LimitedRequestMiddleware>();

        //app.UseHangfireDashboard(); // Để quản lý lịch
        app.UseHangfireServer();    // Để thực thi lịch
        app.UseHangfireDashboard("/hangFileDashboard");
        app.UseSwaggerSetup();

        app.UseRouting();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            // endpoints.MapHub<ItphonuiHub>("/hubs/itphonuihub", options =>
            // {
            //     options.Transports = HttpTransportType.WebSockets  |
            //                          HttpTransportType.LongPolling |
            //                          HttpTransportType.ServerSentEvents;
            // });
        });
        //Log4net
        loggerFactory.AddLog4Net();
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    /// <summary>
    /// </summary>
    /// <param name="services"></param>
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDistributedMemoryCache();
        //services.AddControllers();
        services.AddSession(options =>
        {
            options.IdleTimeout        = TimeSpan.FromMinutes(60);
            options.Cookie.HttpOnly    = true;
            options.Cookie.IsEssential = true;
        });


        // Setting DBContexts
        services.AddDatabaseSetup(Configuration);

        // Setting Hàngile
        services.AddHangFileSetup(Configuration);

        // ASP.NET Identity Settings
        services.AddIdentitySetup(Configuration);

        // AutoMapper Settings
        services.AddAutoMapperConfiguration();

        // Authentication & Authorization
        services.AddAuthSetup();

        // Adding MediatR for Domain Events and Notifications
        services.AddMediatR(typeof(Startup));

        // ASP.NET HttpContext dependency
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        // .NET Native DI Abstraction
        services.AddDependencyInjectionSetup(Configuration);
        services.AddScoped<IViewRenderService, ViewRenderService>();

        services.AddMvc().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters
                   .Add(new JsonStringEnumConverter());
            //options.JsonSerializerOptions.IgnoreNullValues = true;
        });
        services.Configure<FormOptions>(options =>
        {
            options.ValueCountLimit          = int.MaxValue;
            options.MultipartBodyLengthLimit = int.MaxValue;
        });
        services.AddOptions();

        services.AddSwaggerConfiguration();
        //Rate Limit
        // needed to store rate limit counters and ip rules
        //services.AddMemoryCache();

        services.AddMemoryCache(options =>
        {
            options.SizeLimit = 100; // tối đa 50 bài viết trong cache
        });

        services.AddScheduler();

        //load general configuration from appsettings.json
        services.Configure<ClientRateLimitOptions>(Configuration.GetSection("ClientRateLimiting"));

        // inject counter and rules stores
        services.AddSingleton<IClientPolicyStore, DistributedCacheClientPolicyStore>();
        services.AddSingleton<IRateLimitCounterStore, DistributedCacheRateLimitCounterStore>();
        services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
        services.AddCors(
            o =>
            {
                o.AddPolicy("CorsPolicy", builder =>
                {
                    builder.SetIsOriginAllowed(_ => true)
                           .AllowAnyMethod()
                           .AllowAnyHeader()
                           .AllowCredentials();
                });
            });

        services.AddCors();

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidatorBehavior<,>));
        services.AddHttpClient("seonewsClient", client =>
        {
            client.Timeout = TimeSpan.FromMinutes(1);
        });
        // services.AddSignalR(options =>
        // {
        //     options.EnableDetailedErrors      = true;
        //     options.MaximumReceiveMessageSize = 1024000;
        // });
    }

#endregion
}