#region

using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

#endregion

namespace ITC.Service.API.Configurations;

/// <summary>
///     SwaggerConfig
/// </summary>
public static class SwaggerConfig
{
#region Methods

    /// <summary>
    ///     AddSwaggerConfiguration
    /// </summary>
    /// <param name="services"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public static void AddSwaggerConfiguration(this IServiceCollection services)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));

        services.AddSwaggerGen(s =>
        {
            s.SwaggerDoc("v1", new OpenApiInfo
            {
                Version     = "v1",
                Title       = "ITPhoNuiINC",
                Description = "",
                Contact = new OpenApiContact
                {
                    Name  = "ITPhonui",
                    Email = "",
                    Url   = new Uri("https://itphonui.com")
                }
            });

            s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description  = "Input the JWT like: Bearer {your token}",
                Name         = "Authorization",
                Scheme       = "Bearer",
                BearerFormat = "JWT",
                In           = ParameterLocation.Header,
                Type         = SecuritySchemeType.ApiKey
            });

            s.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id   = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });

            var filePath =
                Path.Combine(AppContext.BaseDirectory, "ITC.Service.API.xml");
            s.IncludeXmlComments(filePath, true);
        });
    }

    /// <summary>
    ///     UseSwaggerSetup
    /// </summary>
    /// <param name="app"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public static void UseSwaggerSetup(this IApplicationBuilder app)
    {
        if (app == null) throw new ArgumentNullException(nameof(app));

        app.UseSwagger();
        app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1"); });
    }

#endregion
}