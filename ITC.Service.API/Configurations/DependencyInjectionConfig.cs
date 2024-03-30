#region

using System;
using ITC.Infra.CrossCutting.IoC;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace ITC.Service.API.Configurations;

/// <summary>
/// </summary>
public static class DependencyInjectionConfig
{
#region Methods

    /// <summary>
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public static void AddDependencyInjectionSetup(this IServiceCollection services, IConfiguration configuration)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));

        NativeInjectorBootStrapper.RegisterServices(services, configuration);
    }

#endregion
}