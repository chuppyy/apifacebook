#region

using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ITC.Domain.Core.Bus;
using ITC.Infra.CrossCutting.Identity.Authorization.Requirements;
using ITC.Infra.CrossCutting.Identity.Extensions;
using ITC.Infra.CrossCutting.Identity.Models;
using ITC.Infra.CrossCutting.Identity.Models.QueryModel;
using ITC.Infra.CrossCutting.Identity.Queries;
using ITC.Service.API.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

#endregion

namespace ITC.Service.API.Configurations;

/// <summary>
///     IdentitySetup
/// </summary>
public static class IdentitySetup
{
#region Methods

    /// <summary>
    ///     AddAuthSetup
    /// </summary>
    /// <param name="services"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public static void AddAuthSetup(this IServiceCollection services)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));

        services.AddAuthorization(options =>
        {
            options.AddPolicy(RoleIdentity.Administrator,
                              policy => policy.RequireRole(RoleIdentity.Administrator,
                                                           RoleIdentity.DepartmentOfEducation,
                                                           RoleIdentity.EducationDepartment,
                                                           RoleIdentity.Bussiness));
        });

        services.AddAuthorization(options =>
        {
            options.AddPolicy(Policies.Administrator,
                              policy =>
                                  policy.Requirements
                                        .Add(new
                                                 AdministratorRequirement(RoleIdentity.Administrator,
                                                                          true)));
        });
    }

    /// <summary>
    ///     AddIdentitySetup
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public static void AddIdentitySetup(this IServiceCollection services, IConfiguration configuration)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));

        services.AddTransient<ICustomUserValidator<ApplicationUser>, CustomUserValidator<ApplicationUser>>();
        services.AddDefaultIdentity<ApplicationUser>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = true;
                    //options.User.RequireUniqueEmail = true;

                    // Password settings.
                    options.Password.RequireDigit           = false;
                    options.Password.RequiredLength         = 6;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase       = false;
                    options.Password.RequireLowercase       = false;

                    // Lockout settings.
                    options.Lockout.DefaultLockoutTimeSpan  = TimeSpan.FromMinutes(1);
                    options.Lockout.MaxFailedAccessAttempts = 10;
                    options.Lockout.AllowedForNewUsers      = true;
                })
                .AddRoles<ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddClaimsPrincipalFactory<ETCUserClaimsPrincipalFactory>()
                .AddUserManager<CustomUserManager<ApplicationUser>>();

        // JWT Setup

        var appSettingsSection = configuration.GetSection("AppSettings");
        services.Configure<AppSettings>(appSettingsSection);

        var appSettings = appSettingsSection.Get<AppSettings>();
        var key         = Encoding.ASCII.GetBytes(appSettings.Secret);

        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme    = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = true;
            x.SaveToken            = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey         = new SymmetricSecurityKey(key),
                ValidateIssuer           = true,
                ValidateAudience         = true,
                ValidAudience            = appSettings.ValidAt,
                ValidIssuer              = appSettings.Issuer,
                ClockSkew                = TimeSpan.Zero
            };

            x.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    var accessToken = context.Request.Query["access_token"];
                    var path        = context.HttpContext.Request.Path;
                    if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/hubs"))
                        context.Token = accessToken;

                    return Task.CompletedTask;
                }
            };
            // x.Events = new JwtBearerEvents
            // {
            //     OnMessageReceived = context =>
            //                         {
            //                             if(context.HttpContext.WebSockets.IsWebSocketRequest)
            //                                 context.Token = context.Request.Query["access_token"];
            //
            //                             return Task.CompletedTask;
            //                         }
            // };
        });

        services.AddAuthorization(options =>
        {
            var defaultAuthorizationPolicyBuilder =
                new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme);
            defaultAuthorizationPolicyBuilder = defaultAuthorizationPolicyBuilder
                .RequireAuthenticatedUser();
            options.DefaultPolicy = defaultAuthorizationPolicyBuilder.Build();
        });
    }

#endregion
}

/// <inheritdoc />
public class ETCUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser>
{
#region Fields

    private readonly IManageRoleQueries _manageRoleQueries;

#endregion

#region Constructors

    /// <summary>
    /// </summary>
    /// <param name="manageRoleQueries"></param>
    /// <param name="bus"></param>
    /// <param name="userManager"></param>
    /// <param name="optionsAccessor"></param>
    public ETCUserClaimsPrincipalFactory(IManageRoleQueries           manageRoleQueries,
                                         IMediatorHandler             bus,
                                         UserManager<ApplicationUser> userManager,
                                         IOptions<IdentityOptions>    optionsAccessor)
        : base(userManager, optionsAccessor)
    {
        _manageRoleQueries = manageRoleQueries;
    }

#endregion

#region Methods

    /// <summary>
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
    {
        var identity = await base.GenerateClaimsAsync(user);

        identity.AddClaims(new[]
        {
            new Claim(CustomClaimType.UserType, user.UserTypeId),
            new Claim(CustomClaimType.SuperAdministrator, user.IsSuperAdmin.ToString(),
                      ClaimValueTypes.Boolean)
        });
        // identity.AddClaim(new Claim(CustomClaimType.PortalId,       user.PortalId.ToString(), ClaimValueTypes.Integer));
        identity.AddClaim(new Claim(CustomClaimType.ManagementUnit, user.ManagementUnitId ?? user.Id));

        // =====Gán quyền sử dụng của User vào Token======
        var iAuthotiryByUser = (List<AuthorityByUserDto>)await _manageRoleQueries.GetAuthorityByUser(user.Id);
        foreach (var items in iAuthotiryByUser)
            identity.AddClaim(new Claim(items.Code, items.Value.ToString()));

        // var customUserType = await _manageRoleQueries.GetPermissionsByUserTypeAsync(user.Id, user.UserTypeId);
        // if (customUserType != null)
        // {
        //     if (!string.IsNullOrEmpty(customUserType.UnitId))
        //         identity.AddClaim(new Claim(CustomClaimType.Unit, customUserType.UnitId));
        //     identity.AddClaim(new Claim(CustomClaimType.SchoolYear,
        //                                 customUserType.SchoolYear == Guid.Empty ? string.Empty : customUserType.SchoolYear.ToString()));
        //     identity.AddClaim(new Claim(CustomClaimType.RoleDefault, customUserType.RoleIdentity));
        //
        //     identity.AddClaim(new Claim(CustomClaimType.UnitUser, customUserType.UnitUserId ?? user.Id));
        //
        //     if (!string.IsNullOrEmpty(customUserType.ManagementName))
        //         identity.AddClaim(new Claim(CustomClaimType.ManagementName, customUserType.ManagementName));
        //
        //     if (!string.IsNullOrEmpty(customUserType.EducationLevelCode))
        //         identity.AddClaim(new Claim(CustomClaimType.EducationLevelCode, customUserType.EducationLevelCode));
        //
        //     if (!string.IsNullOrEmpty(customUserType.FullName))
        //         identity.AddClaim(new Claim(CustomClaimType.FullName, customUserType.FullName));
        //
        //     if (!string.IsNullOrEmpty(customUserType.Email))
        //         identity.AddClaim(new Claim(CustomClaimType.Email, customUserType.Email));
        //
        //     var claims = new List<Claim>();
        //     foreach (var role in customUserType.Roles) claims.Add(new Claim(ClaimTypes.Role, role));
        //
        //     foreach (var permissions in customUserType.Permissions)
        //         claims.Add(new Claim(CustomClaimType.Permission, JsonConvert.SerializeObject(permissions)));
        //
        //
        //     identity.AddClaims(claims);
        // }

        return identity;
    }

#endregion
}