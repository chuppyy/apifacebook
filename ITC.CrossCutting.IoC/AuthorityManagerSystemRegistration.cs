using ITC.Application.AppService.AuthorityManager.AuthorityManagerSystem;
using ITC.Domain.CommandHandlers.AuthorityManager;
using ITC.Domain.Commands.AuthorityManager.AuthorityManagerSystem;
using ITC.Domain.Commands.AuthorityManager.MenuManagerSystem;
using ITC.Domain.Interfaces.AuthorityManager.AuthorityManagerSystem;
using ITC.Infra.Data.FunctionRepositoryQueries.AuthorityManager.AuthorityManagerSystem;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ITC.Infra.CrossCutting.IoC;

public class AuthorityManagerSystemRegistration : Registration
{
#region Constructors

    public AuthorityManagerSystemRegistration(IServiceCollection services) : base(services)
    {
    }

    public override void Register(IConfiguration configuration)
    {
        var connection = configuration.GetConnectionString("DefaultConnection");

    #region Authority-Manager-System

        AddScoped<IAuthorityManagerAppService, AuthorityManagerAppService>();
        AddScoped<IRequestHandler<AddAuthorityManagerSystemCommand, bool>, AuthorityManagerSystemCommandHandler>();
        AddScoped<IRequestHandler<UpdateAuthorityManagerSystemCommand, bool>, AuthorityManagerSystemCommandHandler>();
        AddScoped<IRequestHandler<DeleteAuthorityManagerSystemCommand, bool>, AuthorityManagerSystemCommandHandler>();
        AddScoped<IRequestHandler<UpdatePermissionByAuthoritiesCommand, bool>, AuthorityManagerSystemCommandHandler>();
        AddScoped<IRequestHandler<UpdateAuthorityManagerSystemPermissionMenuCommand, bool>,
            AuthorityManagerSystemCommandHandler>();
        AddScoped<IAuthorityManagerRepository, AuthorityManagerRepository>();
        AddScoped<IAuthorityManagerQueries>(_ => new AuthorityManagerQueries(connection));

    #endregion
    }

#endregion
}