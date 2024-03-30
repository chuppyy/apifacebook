#region

using System;
using ITC.Infra.CrossCutting.Identity.Models;
using ITC.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace ITC.Service.API.Configurations;

/// <summary>
///     DatabaseConfig
/// </summary>
public static class DatabaseConfig
{
#region Methods

    /// <summary>
    ///     AddDatabaseSetup
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public static void AddDatabaseSetup(this IServiceCollection services, IConfiguration configuration)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));

        services.AddDbContext<ApplicationDbContext>(options =>
                                                        options.UseSqlServer(configuration
                                                                                 .GetConnectionString(
                                                                                     "DefaultConnection")));
        services.AddDbContext<EQMContext>(options =>
                                              options.UseSqlServer(configuration
                                                                       .GetConnectionString("DefaultConnection")));
        services.AddDbContext<EventStoreSqlContext>(options =>
                                                        options.UseSqlServer(configuration
                                                                                 .GetConnectionString(
                                                                                     "DefaultConnection")));
    }

#endregion
}