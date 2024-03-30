#region

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace ITC.Infra.CrossCutting.IoC;

public static class NativeInjectorBootStrapper
{
#region Methods

    public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
    {
        new GeneralRegistration(services).Register(configuration);
        new IdentityRegistration(services).Register(configuration);
        new SystemManagerRegistration(services).Register(configuration);
        new MenuManagerRegistration(services).Register(configuration);
        new CompanyManagerRegistration(services).Register(configuration);
        new AuthorityManagerSystemRegistration(services).Register(configuration);
        new NewsManagerRegistration(services).Register(configuration);
        new StudyManagerRegistration(services).Register(configuration);
        new SaleProductManagerRegistration(services).Register(configuration);
    }

#endregion
}