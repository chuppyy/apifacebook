using ITC.Application.AppService.AuthorityManager.AuthorityManagerSystem;
using ITC.Application.AppService.AuthorityManager.MenuManagerSystem;
using ITC.Application.AppService.SystemManagers.HelperManagers;
using ITC.Application.AppService.SystemManagers.SortMenuManagers;
using ITC.Domain.CommandHandlers.AuthorityManager;
using ITC.Domain.CommandHandlers.SystemManagers;
using ITC.Domain.Commands.AuthorityManager.MenuManagerSystem;
using ITC.Domain.Commands.SystemManagers.HelperManagers;
using ITC.Domain.Interfaces.AuthorityManager.AuthorityManagerSystem;
using ITC.Domain.Interfaces.AuthorityManager.ProjectMenuManagerSystem;
using ITC.Domain.Interfaces.SystemManagers.Helpers;
using ITC.Domain.Interfaces.SystemManagers.SystemLogs;
using ITC.Domain.Interfaces.SystemManagers.TableDeleteManagers;
using ITC.Infra.Data.FunctionRepositoryQueries.AuthorityManager.AuthorityManagerSystem;
using ITC.Infra.Data.FunctionRepositoryQueries.AuthorityManager.ProjectMenuManagerSystem;
using ITC.Infra.Data.FunctionRepositoryQueries.SystemManagers.HelperManagers;
using ITC.Infra.Data.FunctionRepositoryQueries.SystemManagers.SystemLogManagers;
using ITC.Infra.Data.FunctionRepositoryQueries.SystemManagers.TableDeleteManagers;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ITC.Infra.CrossCutting.IoC;

public class SystemManagerRegistration : Registration
{
#region Constructors

    public SystemManagerRegistration(IServiceCollection services) : base(services)
    {
    }

    public override void Register(IConfiguration configuration)
    {
        var connection = configuration.GetConnectionString("DefaultConnection");

    #region Menu-manager

        AddScoped<IMenuManagerAppService, MenuManagerAppService>();
        AddScoped<IRequestHandler<AddMenuManagerCommand, bool>, MenuManagerCommandHandler>();
        AddScoped<IRequestHandler<UpdateMenuManagerCommand, bool>, MenuManagerCommandHandler>();
        AddScoped<IRequestHandler<DeleteMenuManagerCommand, bool>, MenuManagerCommandHandler>();
        AddScoped<IRequestHandler<AddAuthoritiesMenuManagerCommand, bool>, MenuManagerCommandHandler>();
        AddScoped<IRequestHandler<UpdateAuthoritiesMenuManagerCommand, bool>, MenuManagerCommandHandler>();
        AddScoped<IRequestHandler<DeleteAuthoritiesMenuManagerCommand, bool>, MenuManagerCommandHandler>();
        AddScoped<IMenuManagerRepository, MenuManagerRepository>();
        AddScoped<IMenuManagerQueries>(_ => new MenuManagerQueries(connection));

    #endregion

    #region Authority-manager

        AddScoped<IAuthorityManagerAppService, AuthorityManagerAppService>();
        AddScoped<IAuthorityManagerRepository, AuthorityManagerRepository>();
        AddScoped<IAuthorityDetailRepository, AuthorityDetailRepository>();
        AddScoped<IAuthorityManagerQueries>(_ => new AuthorityManagerQueries(connection));

        AddScoped<IAuthorityDetailRepository, AuthorityDetailRepository>();
        AddScoped<IAuthorityDetailQueries>(_ => new AuthorityDetailQueries(connection));

    #endregion

    #region Helper-manager

        AddScoped<IHelperAppService, HelperAppService>();
        AddScoped<IHelperQueries>(_ => new HelperQueries(connection));
        AddScoped<IRequestHandler<UpdateStatusHelperCommand, bool>, HelperCommandHandler>();

    #endregion

    #region System-Log

        AddScoped<ISystemLogRepository, SystemLogRepository>();
        AddScoped<ISystemLogQueries>(_ => new SystemLogQueries(connection));

    #endregion

    #region Table-Delete

        AddScoped<ITableDeleteManagerRepository, TableDeleteManagerRepository>();

    #endregion

    #region Sort-menu

        AddScoped<ISortMenuManagerAppService, SortMenuManagerAppService>();
        AddScoped<IRequestHandler<SortMenuManagerCommand, bool>, MenuManagerCommandHandler>();
        AddScoped<IRequestHandler<DeleteSortMenuManagerCommand, bool>, MenuManagerCommandHandler>();

    #endregion
    }

#endregion
}