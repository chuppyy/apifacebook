#region

using System;
using ITC.Application.AutoMapper.ViewModelToCommands;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace ITC.Service.API.Configurations;

/// <summary>
///     AutoMapperConfig
/// </summary>
public static class AutoMapperConfig
{
#region Methods

    /// <summary>
    ///     AddAutoMapperConfiguration
    /// </summary>
    /// <param name="services"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public static void AddAutoMapperConfiguration(this IServiceCollection services)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));

        services.AddAutoMapper(
            typeof(IdentityMappingProfile)
        );
    }

#endregion
}