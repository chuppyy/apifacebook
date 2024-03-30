#region

using ITC.Application.AppService.AuthorityManager.MenuManagerSystem;
using ITC.Application.Interfaces.ManageRole;
using ITC.Application.Services.ManageRole;
using ITC.Application.Services.Notifications;
using ITC.Domain.Commands.ManageRole;
using ITC.Domain.Commands.ManageRole.Account;
using ITC.Domain.Commands.ManageRole.OfficalAccount;
using ITC.Domain.Interfaces;
using ITC.Domain.Interfaces.AuthorityManager.ProjectMenuManagerSystem;
using ITC.Domain.Interfaces.Notification;
using ITC.Infra.CrossCutting.Identity.Authorization;
using ITC.Infra.CrossCutting.Identity.Authorization.Handlers;
using ITC.Infra.CrossCutting.Identity.CommandHandlers;
using ITC.Infra.CrossCutting.Identity.Commands;
using ITC.Infra.CrossCutting.Identity.Commands.Module;
using ITC.Infra.CrossCutting.Identity.Commands.Portal;
using ITC.Infra.CrossCutting.Identity.Interfaces;
using ITC.Infra.CrossCutting.Identity.Models;
using ITC.Infra.CrossCutting.Identity.Queries;
using ITC.Infra.CrossCutting.Identity.Repository;
using ITC.Infra.CrossCutting.Identity.UoW;
using ITC.Infra.Data.FunctionRepositoryQueries.AuthorityManager.ProjectMenuManagerSystem;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace ITC.Infra.CrossCutting.IoC;

public class IdentityRegistration : Registration
{
#region Constructors

    public IdentityRegistration(IServiceCollection services) : base(services)
    {
    }

#endregion

#region Methods

#region Public Methods

    public override void Register(IConfiguration configuration)
    {
        //string connection = configuration.GetConnectionString("DefaultConnection");
        //#region Service
        ////Account

        //AddScoped<IAccountAppService, AccountAppService>();
        //AddScoped<IManageRoleAppService, ManageRoleAppService>();
        //AddScoped<IAccountRepository, AccountRepository>();
        //AddScoped<IModuleGroupRepository, ModuleGroupRepository>();
        //#endregion

        //// Infra - Identity
        //AddScoped<IUser, AspNetUser>();

        //#region Repositories
        //AddScoped<IManageRoleRepository, ManageRoleRepository>();
        //#endregion
        //#region Quries
        //AddScoped<Identity.Queries.IAccountQueries>(ops => new Identity.Repository.AccountQueries(connection));
        //AddScoped<IManageRoleQueries>(ops => new ManageRoleQueries(connection));
        //#endregion


        var connection = configuration.GetConnectionString("DefaultConnection");
        // ASP.NET Authorization Polices
        AddScoped<IAuthorizationHandler, RolesAuthorizationHandler>();

        AddSingleton<IAuthorizationHandler, AdministratorHandler>();

        //// Replace the default authorization policy provider with our own
        //// custom provider which can return authorization policies for given
        //// policy names (instead of using the default policy provider)
        AddSingleton<IAuthorizationPolicyProvider, CustomModulePolicyProvider>();

        //// As always, handlers must be provided for the requirements of the authorization policies
        AddSingleton<IAuthorizationHandler, CustomAuthorizationHandler>();

        ////Email Setting
        Services.Configure<EmailSetting>(options => configuration.GetSection("EmailConfig").Bind(options));
        AddScoped<IEmailSender, MessageService>();


        //Appication
        AddScoped<ITokenAppService, TokenAppService>();
        AddScoped<IManageRoleAppService, ManageRoleAppService>();
        AddScoped<IAccountAppService, AccountAppService>();
        AddScoped<IMenuManagerAppService, MenuManagerAppService>();

    #endregion Manage Identity

        // Infra - Identity
        AddScoped<IUser, AspNetUser>();
        //AddScoped<IUnitOfWorkIdentity, UnitOfWorkIdentity>();
        AddScoped<IManageRoleRepository, ManageRoleRepository>();
        AddScoped<IAccountRepository, AccountRepository>();
        AddScoped<IModuleGroupRepository, ModuleGroupRepository>();
        AddScoped<IAccountRepository, AccountRepository>();
        AddScoped<IUnitOfWorkIdentity, UnitOfWorkIdentity>();
        AddScoped<IMenuManagerRepository, MenuManagerRepository>();

        //Command-commandhandler
        AddScoped<IRequestHandler<AddAccountCommand, bool>, AccountCommandHandler>();
        AddScoped<IRequestHandler<UpdateAccountCommand, bool>, AccountCommandHandler>();
        AddScoped<IRequestHandler<ActiveAccountCommand, bool>, AccountCommandHandler>();
        AddScoped<IRequestHandler<DisableAccountCommand, bool>, AccountCommandHandler>();
        AddScoped<IRequestHandler<LockAccountCommand, bool>, AccountCommandHandler>();
        AddScoped<IRequestHandler<UnlockAccountCommand, bool>, AccountCommandHandler>();
        AddScoped<IRequestHandler<RemoveAccountCommand, bool>, AccountCommandHandler>();
        AddScoped<IRequestHandler<AddEmailAccountCommand, bool>, AccountCommandHandler>();
        AddScoped<IRequestHandler<AccountInfoCommand, bool>, AccountCommandHandler>();

        //personel account
        AddScoped<IRequestHandler<AddPersonnelAccountCommand, bool>, AccountCommandHandler>();
        AddScoped<IRequestHandler<UpdatePersonnelAccountCommand, bool>, AccountCommandHandler>();
        AddScoped<IRequestHandler<RemovePersonnelAccountCommand, bool>, AccountCommandHandler>();

        AddScoped<IRequestHandler<AddModuleGroupCommand, bool>, ManageRoleCommandHandler>();
        AddScoped<IRequestHandler<UpdateModuleGroupCommand, bool>, ManageRoleCommandHandler>();
        AddScoped<IRequestHandler<RemoveModuleGroupCommand, bool>, ManageRoleCommandHandler>();
        AddScoped<IRequestHandler<AddModuleCommand, bool>, ManageRoleCommandHandler>();
        AddScoped<IRequestHandler<AddUserTypeCommand, bool>, ManageRoleCommandHandler>();
        AddScoped<IRequestHandler<UpdateUserTypeCommand, bool>, ManageRoleCommandHandler>();
        AddScoped<IRequestHandler<RemoveUserTypeCommand, bool>, ManageRoleCommandHandler>();
        AddScoped<IRequestHandler<UpdateModuleCommand, bool>, ManageRoleCommandHandler>();
        AddScoped<IRequestHandler<RemoveModuleCommand, bool>, ManageRoleCommandHandler>();
        AddScoped<IRequestHandler<RemoveFunctionCommand, bool>, ManageRoleCommandHandler>();

        AddScoped<IRequestHandler<AddUserTypeUnitCommand, bool>, ManageRoleCommandHandler>();
        AddScoped<IRequestHandler<UpdateUserTypeUnitCommand, bool>, ManageRoleCommandHandler>();

        // AddScoped<IRequestHandler<AddPortalCommand, bool>, ManageRoleCommandHandler>();
        AddScoped<IRequestHandler<UpdatePortalCommand, bool>, ManageRoleCommandHandler>();
        AddScoped<IRequestHandler<RemovePortalCommand, bool>, ManageRoleCommandHandler>();

        //Doamain-Queries
        AddScoped<IManageRoleQueries>(_ => new ManageRoleQueries(connection));
        AddScoped<IAccountQueries>(_ => new AccountQueries(connection));
        AddScoped<IMenuManagerQueries>(_ => new MenuManagerQueries(connection));
    }

#endregion
}