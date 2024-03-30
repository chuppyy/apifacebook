#region

using System;
using Microsoft.Extensions.DependencyInjection;
using ModelToViewModel = ITC.Application.AutoMapper.ModelToViewModels;
using ModelToCommand = ITC.Application.AutoMapper.ViewModelToCommands;

#endregion

namespace ITC.Service.API.Extensions;

/// <summary>
/// </summary>
public static class AutoMapperSetup
{
#region Methods

    /// <summary>
    /// </summary>
    /// <param name="services"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public static void AddAutoMapperSetup(this IServiceCollection services)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));

        services.AddAutoMapper(
            typeof(ModelToViewModel.IdentityMappingProfile),
            typeof(ModelToCommand.IdentityMappingProfile)
        );
    }

#endregion
}