#region

using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace ITC.Infra.CrossCutting.IoC;

public abstract class Registration
{
#region Fields

    protected readonly IServiceCollection Services;

#endregion

#region Constructors

    public Registration(IServiceCollection services)
    {
        Services = services;
    }

#endregion

#region Methods

    public abstract void Register(IConfiguration configuration);

    protected void AddScoped<TService, TImplementation>(
        Func<IServiceProvider, TImplementation> implementationFactory = null)
        where TService : class where TImplementation : class, TService
    {
        if (implementationFactory != null)
            Services.AddScoped<TService, TImplementation>(implementationFactory);
        else
            Services.AddScoped<TService, TImplementation>();
    }

    protected void AddScoped<TService>(Func<IServiceProvider, TService> implementationFactory = null)
        where TService : class
    {
        AddScoped<TService, TService>(implementationFactory);
    }

    protected void AddSingleton<TService, TImplementation>(
        Func<IServiceProvider, TImplementation> implementationFactory = null)
        where TService : class where TImplementation : class, TService
    {
        if (implementationFactory != null)
            Services.AddSingleton<TService, TImplementation>(implementationFactory);
        else
            Services.AddSingleton<TService, TImplementation>();
    }

    protected void AddTransient<TService>(Func<IServiceProvider, TService> implementationFactory = null)
        where TService : class
    {
        AddTransient<TService, TService>(implementationFactory);
    }

    protected void AddTransient<TService, TImplementation>(
        Func<IServiceProvider, TImplementation> implementationFactory = null)
        where TService : class where TImplementation : class, TService
    {
        if (implementationFactory != null)
            Services.AddTransient<TService, TImplementation>(implementationFactory);
        else
            Services.AddTransient<TService, TImplementation>();
    }

#endregion
}