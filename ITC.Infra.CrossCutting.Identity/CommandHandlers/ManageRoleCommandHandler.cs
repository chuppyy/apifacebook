#region

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ITC.Domain.Commands.ManageRole;
using ITC.Domain.Core.Bus;
using ITC.Domain.Core.Models;
using ITC.Domain.Core.Notifications;
using ITC.Domain.Extensions;
using ITC.Domain.Interfaces;
using ITC.Infra.CrossCutting.Identity.Commands;
using ITC.Infra.CrossCutting.Identity.Commands.Module;
using ITC.Infra.CrossCutting.Identity.Commands.Portal;
using ITC.Infra.CrossCutting.Identity.Extensions;
using ITC.Infra.CrossCutting.Identity.Interfaces;
using ITC.Infra.CrossCutting.Identity.Models;
using ITC.Infra.CrossCutting.Identity.Models.QueryModel;
using ITC.Infra.CrossCutting.Identity.Queries;
using ITC.Infra.CrossCutting.Identity.UoW;
using MediatR;

#endregion

namespace ITC.Infra.CrossCutting.Identity.CommandHandlers;

public class ManageRoleCommandHandler : CommandIdentityHandler,
                                        IRequestHandler<AddModuleCommand, bool>,
                                        IRequestHandler<UpdateModuleCommand, bool>,
                                        IRequestHandler<RemoveModuleCommand, bool>,
                                        IRequestHandler<AddFunctionCommand, bool>,
                                        IRequestHandler<UpdateFunctionCommand, bool>,
                                        IRequestHandler<RemoveFunctionCommand, bool>,
                                        IRequestHandler<AddUserTypeCommand, bool>,
                                        IRequestHandler<AddUserTypeUnitCommand, bool>,
                                        IRequestHandler<UpdateUserTypeCommand, bool>,
                                        IRequestHandler<UpdateUserTypeUnitCommand, bool>,
                                        IRequestHandler<RemoveUserTypeCommand, bool>,
                                        IRequestHandler<UpdatePortalCommand, bool>,
                                        IRequestHandler<RemovePortalCommand, bool>,
                                        IRequestHandler<AddModuleGroupCommand, bool>,
                                        IRequestHandler<UpdateModuleGroupCommand, bool>,
                                        IRequestHandler<RemoveModuleGroupCommand, bool>
{
#region Constructors

    public ManageRoleCommandHandler(IManageRoleRepository                    manageRoleRepository,
                                    IUnitOfWorkIdentity                      uow,
                                    IMediatorHandler                         bus,
                                    IUser                                    user,
                                    IManageRoleQueries                       manageRoleQueries,
                                    IAccountRepository                       accountRepository,
                                    IModuleGroupRepository                   moduleGroupRepository,
                                    IUnitOfWork                              unitOfWork,
                                    INotificationHandler<DomainNotification> notifications) :
        base(uow, bus, notifications)
    {
        _manageRoleRepository  = manageRoleRepository;
        Bus                    = bus;
        _user                  = user;
        _manageRoleQueries     = manageRoleQueries;
        _accountRepository     = accountRepository;
        _moduleGroupRepository = moduleGroupRepository;
        _uow                   = unitOfWork;
    }

#endregion

#region IRequestHandler<AddFunctionCommand,bool> Members

    public Task<bool> Handle(AddFunctionCommand message, CancellationToken cancellationToken)
    {
        if (!message.IsValid())
        {
            NotifyValidationErrors(message);
            return Task.FromResult(false);
        }

        if (_manageRoleRepository.IsExistFunctionByModuleId(message.ModuleId, message.Weight))
        {
            Bus.RaiseEvent(new DomainNotification(message.MessageType, "The function has already been taken."));
            return Task.FromResult(false);
        }

        var function = new Function(message.Name, message.Weight, message.Description, message.ModuleId);


        _manageRoleRepository.AddFunction(function);

        if (Commit())
        {
            //Bus.RaiseEvent(new FunctionAddEvent(function.Id, message.Name, message.Weight, message.Description, message.ModuleId));
        }

        return Task.FromResult(true);
    }

#endregion

#region IRequestHandler<AddModuleCommand,bool> Members

    public Task<bool> Handle(AddModuleCommand message, CancellationToken cancellationToken)
    {
        if (!message.IsValid())
        {
            NotifyValidationErrors(message);
            return Task.FromResult(false);
        }

        if (_manageRoleRepository.IsExistModuleByIdentity(message.Identity))
        {
            Bus.RaiseEvent(new DomainNotification(message.MessageType,
                                                  "The module identity has already been taken."));
            return Task.FromResult(false);
        }

        var module = new Module(message.Name, message.Identity, message.GroupId, message.Description, true);

        var moduleRoles = new List<ModuleRole>();
        message.RoleIds.ForEach(x => moduleRoles.Add(new ModuleRole(x)));
        module.AddModuleRoles(moduleRoles);


        var functions = new List<Function>();
        message.Weights.ForEach(x => functions.Add(new Function(x.ToString(), (int)x, null)));

        foreach (var roleId in message.RoleIds)
            foreach (var function in functions)
            {
                var functionRoles = new List<FunctionRole>();
                functionRoles.Add(new FunctionRole(roleId));
                function.AddFunctionRoles(functionRoles);
            }


        module.AddFunctions(functions);

        _manageRoleRepository.Add(module);

        if (Commit())
        {
            // Bus.RaiseEvent(new ModuleAddEvent(module.Id, message.Name, message.Identity, message.Description, message.RoleIds));
        }

        return Task.FromResult(true);
    }

#endregion

#region IRequestHandler<AddModuleGroupCommand,bool> Members

    public Task<bool> Handle(AddModuleGroupCommand message, CancellationToken cancellationToken)
    {
        if (!message.IsValid())
        {
            NotifyValidationErrors(message);
            return Task.FromResult(false);
        }

        var module = new ModuleGroup(message.Name, message.Description);
        _moduleGroupRepository.Add(module);
        if (Commit()) return Task.FromResult(true);
        return Task.FromResult(false);
    }

#endregion

#region IRequestHandler<AddUserTypeCommand,bool> Members

    public Task<bool> Handle(AddUserTypeCommand message, CancellationToken cancellationToken)
    {
        if (!message.IsValid())
        {
            NotifyValidationErrors(message);
            return Task.FromResult(false);
        }

        var cur        = _user.GetIdentityUser();
        var userTarget = new UserTarget(cur.UserId, cur.UserId, DateTime.UtcNow, DateTime.UtcNow);
        var userType   = new UserType(message.Name, message.Description, message.RoleId, userTarget, cur.PortalId);

        return AddUserType(userType, message);
    }

#endregion

#region IRequestHandler<AddUserTypeUnitCommand,bool> Members

    public Task<bool> Handle(AddUserTypeUnitCommand message, CancellationToken cancellationToken)
    {
        if (!message.IsValid())
        {
            NotifyValidationErrors(message);
            return Task.FromResult(false);
        }

        var cur        = _user.GetIdentityUser();
        var userTarget = new UserTarget(cur.UserId, cur.UserId, DateTime.UtcNow, DateTime.UtcNow);
        var userType = new UserType(message.Name, message.Description, message.RoleId, userTarget, cur.PortalId,
                                    cur.UnitUserId);

        return AddUserType(userType, message);
    }

#endregion

#region IRequestHandler<RemoveFunctionCommand,bool> Members

    public Task<bool> Handle(RemoveFunctionCommand message, CancellationToken cancellationToken)
    {
        if (!message.IsValid())
        {
            NotifyValidationErrors(message);
            return Task.FromResult(false);
        }

        var function = _manageRoleRepository.GetFunctionById(message.Id);

        if (function == null)
        {
            Bus.RaiseEvent(new DomainNotification(message.MessageType,
                                                  "The module identity has already been taken."));
            return Task.FromResult(false);
        }

        _manageRoleRepository.RemoveFunction(function);

        if (Commit())
        {
            // Bus.RaiseEvent(new FunctionRemovedEvent(function.Id));
        }

        return Task.FromResult(true);
    }

#endregion

#region IRequestHandler<RemoveModuleCommand,bool> Members

    public Task<bool> Handle(RemoveModuleCommand message, CancellationToken cancellationToken)
    {
        if (!message.IsValid())
        {
            NotifyValidationErrors(message);
            return Task.FromResult(false);
        }

        var module = _manageRoleRepository.GetById(message.Id);

        if (module == null)
        {
            Bus.RaiseEvent(new DomainNotification(message.MessageType,
                                                  "The module identity has already been taken."));
            return Task.FromResult(false);
        }

        module.SetDelete(true);
        _manageRoleRepository.Update(module);

        if (Commit())
        {
            //Bus.RaiseEvent(new ModuleRemovedEvent(module.Id));
        }

        return Task.FromResult(true);
    }

#endregion

#region IRequestHandler<RemoveModuleGroupCommand,bool> Members

    public Task<bool> Handle(RemoveModuleGroupCommand message, CancellationToken cancellationToken)
    {
        if (!message.IsValid())
        {
            NotifyValidationErrors(message);
            return Task.FromResult(false);
        }

        var module = _moduleGroupRepository.GetById(message.Id);
        module.Delete();
        _moduleGroupRepository.Update(module);
        if (Commit()) return Task.FromResult(true);
        return Task.FromResult(false);
    }

#endregion

#region IRequestHandler<RemovePortalCommand,bool> Members

    public Task<bool> Handle(RemovePortalCommand message, CancellationToken cancellationToken)
    {
        if (!message.IsValid())
        {
            NotifyValidationErrors(message);
            return Task.FromResult(false);
        }

        var portal = _manageRoleRepository.GetPortalByIdentity(message.Identity);

        if (portal == null)
        {
            Bus.RaiseEvent(new DomainNotification(message.MessageType, "The portal has already been taken."));
            return Task.FromResult(false);
        }

        portal.SetDelete(true);
        _manageRoleRepository.UpdatePortal(portal);

        if (Commit())
        {
            //  Bus.RaiseEvent(new PortalRemovedEvent(portal.Identity));
        }

        return Task.FromResult(true);
    }

#endregion

#region IRequestHandler<RemoveUserTypeCommand,bool> Members

    public Task<bool> Handle(RemoveUserTypeCommand message, CancellationToken cancellationToken)
    {
        if (!message.IsValid())
        {
            NotifyValidationErrors(message);
            return Task.FromResult(false);
        }

        var userType = _manageRoleRepository.GetUserTypeById(message.Id, message.IsUnit ? _user.UnitUserId : null);

        if (userType == null)
        {
            Bus.RaiseEvent(new DomainNotification(message.MessageType, "The user type not found."));
            return Task.FromResult(false);
        }

        if (userType.IsDefault)
        {
            Bus.RaiseEvent(new DomainNotification(message.MessageType, "The user type default."));
            return Task.FromResult(false);
        }

        _manageRoleQueries.EditAccountsWhenDeleteUserType(message.Id, message.UserTypeNewId,
                                                          message.IsUnit ? _user.UnitUserId : null);

        userType.SetDelete(true);
        userType.UpdateUserTager(_user.UserId, DateTime.UtcNow);
        _manageRoleRepository.UpdateUserType(userType);

        if (Commit())
        {
            //Bus.RaiseEvent(new UserTypeRemovedEvent(Domain.Core.Events.StoredEventType.Remove, Guid.Parse(_user.UserId), _user.FullName, Guid.Parse(userType.Id), userType.Name, Guid.Parse(_user.UnitUserId), _user.PortalId));
        }

        return Task.FromResult(true);
    }

#endregion

#region IRequestHandler<UpdateFunctionCommand,bool> Members

    public Task<bool> Handle(UpdateFunctionCommand message, CancellationToken cancellationToken)
    {
        if (!message.IsValid())
        {
            NotifyValidationErrors(message);
            return Task.FromResult(false);
        }

        var function = _manageRoleRepository.GetFunctionById(message.Id);

        if (function == null)
        {
            Bus.RaiseEvent(new DomainNotification(message.MessageType,
                                                  "The module identity has already been taken."));
            return Task.FromResult(false);
        }

        function.UpdateFunction(message.Name, message.Weight, message.Description, message.ModuleId);
        _manageRoleRepository.UpdateFunction(function);

        if (Commit())
        {
            // Bus.RaiseEvent(new FunctionUpdatedEvent(function.Id, function.Name, function.Weight, function.Description, function.ModuleId));
        }

        return Task.FromResult(true);
    }

#endregion

#region IRequestHandler<UpdateModuleCommand,bool> Members

    public Task<bool> Handle(UpdateModuleCommand message, CancellationToken cancellationToken)
    {
        if (!message.IsValid())
        {
            NotifyValidationErrors(message);
            return Task.FromResult(false);
        }

        var module = _manageRoleRepository.GetById(message.Id);

        if (module == null)
        {
            Bus.RaiseEvent(new DomainNotification(message.MessageType,
                                                  "The module identity has already been taken."));
            return Task.FromResult(false);
        }

        if (_manageRoleRepository.IsExistModuleByIdentity(module.Id, message.Identity))
        {
            Bus.RaiseEvent(new DomainNotification(message.MessageType,
                                                  "The module identity has already been taken."));
            return Task.FromResult(false);
        }

        module.UpdateModule(message.Name, message.Identity, message.GroupId, message.Description);

        var rolesRequest = message.RoleIds;
        var rolesOld     = _manageRoleQueries.GetRolesByModuleIdAsync(module.Id).Result;

        var addRoles = rolesRequest.Except(rolesOld);

        var deleteRoles = rolesOld.Except(rolesRequest);
        _manageRoleQueries.DeleteRolesByModuleIdAsync(module.Id, deleteRoles.ToList());
        var roles = new List<ModuleRole>();
        foreach (var role in addRoles) roles.Add(new ModuleRole(role));
        module.AddModuleRoles(roles);

        var functionsRequest = message.Weights.Select(x => x);
        var functionsOld     = _manageRoleQueries.GetFunctionsByModuleIdAsync(module.Id).Result;

        var addFunctions = functionsRequest.Except(functionsOld);

        var deleteFunctions = functionsOld.Except(functionsRequest);
        _manageRoleQueries.DeleteFunctionsByModuleIdAsync(module.Id, deleteFunctions.Select(x => (int)x).ToList(),
                                                          deleteRoles.ToList());
        var functions = new List<Function>();
        addFunctions.ForEach(x => functions.Add(new Function(x.ToString(), (int)x, null)));

        module.AddFunctions(functions);

        foreach (var roleId in message.RoleIds)
            foreach (var function in functions)
            {
                var functionRoles = new List<FunctionRole>();
                functionRoles.Add(new FunctionRole(roleId));
                function.AddFunctionRoles(functionRoles);
            }

        var notDeleteFunctions = functionsOld.Except(deleteFunctions);
        if (notDeleteFunctions.Count() > 0)
            foreach (var roleId in addRoles)
                foreach (var functionView in notDeleteFunctions)
                {
                    var function = module.Functions.FirstOrDefault(x => x.Weight == (int)functionView);
                    if (function != null)
                    {
                        var functionRoles = new List<FunctionRole>();
                        functionRoles.Add(new FunctionRole(roleId));
                        function.AddFunctionRoles(functionRoles);
                    }
                }


        _manageRoleRepository.Update(module);

        if (Commit())
        {
            //Bus.RaiseEvent(new ModuleUpdatedEvent(module.Id, module.Name, module.Identity, module.Description, module.RoleId));
            // Bus.RaiseEvent(new ModuleUpdatedEvent(module.Id, module.Name, module.Identity, module.Description, null));
        }

        return Task.FromResult(true);
    }

#endregion

#region IRequestHandler<UpdateModuleGroupCommand,bool> Members

    public Task<bool> Handle(UpdateModuleGroupCommand message, CancellationToken cancellationToken)
    {
        if (!message.IsValid())
        {
            NotifyValidationErrors(message);
            return Task.FromResult(false);
        }

        var module = _moduleGroupRepository.GetById(message.Id);
        module.Update(message.Name, message.Description);
        _moduleGroupRepository.Update(module);
        if (Commit()) return Task.FromResult(true);
        return Task.FromResult(false);
    }

#endregion

#region IRequestHandler<UpdatePortalCommand,bool> Members

    public Task<bool> Handle(UpdatePortalCommand message, CancellationToken cancellationToken)
    {
        if (!message.IsValid())
        {
            NotifyValidationErrors(message);
            return Task.FromResult(false);
        }

        var portal = _manageRoleRepository.GetPortalById(message.Id);

        if (portal == null)
        {
            Bus.RaiseEvent(new DomainNotification(message.MessageType, "The portal has already been taken."));
            return Task.FromResult(false);
        }

        portal.UpdatePortal(message.Name);
        _manageRoleRepository.UpdatePortal(portal);

        if (Commit())
        {
            //  Bus.RaiseEvent(new PortalUpdatedEvent(portal.Id, portal.Name, portal.Identity));
        }

        return Task.FromResult(true);
    }

#endregion

#region IRequestHandler<UpdateUserTypeCommand,bool> Members

    public Task<bool> Handle(UpdateUserTypeCommand message, CancellationToken cancellationToken)
    {
        if (!message.IsValid())
        {
            NotifyValidationErrors(message);
            return Task.FromResult(false);
        }

        var userType = _manageRoleRepository.GetUserTypeById(message.Id);

        return UpdateUserType(userType, message);
    }

#endregion

#region IRequestHandler<UpdateUserTypeUnitCommand,bool> Members

    public Task<bool> Handle(UpdateUserTypeUnitCommand message, CancellationToken cancellationToken)
    {
        if (!message.IsValid())
        {
            NotifyValidationErrors(message);
            return Task.FromResult(false);
        }

        var userType = _manageRoleRepository.GetUserTypeById(message.Id, _user.UnitUserId);
        userType.Description = message.Description;
        _manageRoleRepository.UpdateUserType(userType);
        return UpdateUserType(userType, message);
    }

#endregion

#region Fields

    private readonly IAccountRepository     _accountRepository;
    private readonly IManageRoleQueries     _manageRoleQueries;
    private readonly IManageRoleRepository  _manageRoleRepository;
    private readonly IModuleGroupRepository _moduleGroupRepository;
    private readonly IUnitOfWork            _uow;
    private readonly IUser                  _user;
    private readonly IMediatorHandler       Bus;

#endregion

#region Methods

    public void Dispose()
    {
        _manageRoleRepository.Dispose();
    }

    private Task<List<string>> AddList(List<string> oldArray, List<string> newArray)
    {
        var news = newArray.Except(oldArray, (x, y) => x == y).ToList();
        return Task.FromResult(news);
    }

    private Task<List<CustomModuleDecentralizationModel>> AddList(List<CustomModuleDecentralizationModel> oldArray,
                                                                  List<CustomModuleDecentralizationModel> newArray)
    {
        var news = newArray.Except(oldArray, (x, y) => x.ModuleId == y.ModuleId && x.RoleId == y.RoleId).ToList();
        return Task.FromResult(news);
    }

    private Task<List<CustomFunctionDecentralizationModel>> AddList(
        List<CustomFunctionDecentralizationModel> oldArray, List<CustomFunctionDecentralizationModel> newArray)
    {
        var news = newArray.Except(oldArray, (x, y) => x.FunctionId == y.FunctionId && x.RoleId == y.RoleId)
                           .ToList();
        return Task.FromResult(news);
    }

    private Task<bool> AddUserType(UserType userType, AddUserTypeCommand message)
    {
        var modules   = new List<ModuleDecentralization>();
        var functions = new List<FunctionDecentralization>();

        foreach (var role in message.Roles)
            foreach (var moduleDTO in role.Modules)
            {
                modules.Add(new ModuleDecentralization(moduleDTO.ModuleId, role.RoleId));
                foreach (var functionDTO in moduleDTO.Functions)
                    functions.Add(new FunctionDecentralization(functionDTO.FunctionId, role.RoleId));
            }

        userType.AddModuleDecentralizations(modules);
        userType.AddFunctionDecentralizations(functions);

        _manageRoleRepository.AddUserType(userType);

        if (Commit())
        {
            //Bus.RaiseEvent(new UserTypeAddEvent(Domain.Core.Events.StoredEventType.Add, Guid.Parse(_user.UserId), _user.FullName, Guid.Parse(userType.Id), userType.Name, Guid.Parse(_user.UnitUserId), _user.PortalId
            //    , message.Name, message.RoleId, message.SelectedItems));
        }

        return Task.FromResult(true);
    }

    private Task<List<CustomModuleDecentralizationModel>> DeleteList(
        List<CustomModuleDecentralizationModel> oldArray, List<CustomModuleDecentralizationModel> newArray)
    {
        var old = oldArray.Except(newArray, (x, y) => x.ModuleId == y.ModuleId && x.RoleId == y.RoleId).ToList();
        return Task.FromResult(old);
    }

    private Task<List<CustomFunctionDecentralizationModel>> DeleteList(
        List<CustomFunctionDecentralizationModel> oldArray, List<CustomFunctionDecentralizationModel> newArray)
    {
        var old = oldArray.Except(newArray, (x, y) => x.FunctionId == y.FunctionId && x.RoleId == y.RoleId)
                          .ToList();
        return Task.FromResult(old);
    }

    private Task<bool> UpdateUserType(UserType userType, UpdateUserTypeCommand message)
    {
        if (userType == null)
        {
            Bus.RaiseEvent(new DomainNotification(message.MessageType, "The user type not found."));
            return Task.FromResult(false);
        }

        userType.UpdateUserType(message.Name, message.RoleId);

        var modulesRequest = message.Roles
                                    .SelectMany(x => x.Modules.Select(y => new CustomModuleDecentralizationModel
                                    {
                                        ModuleId = y.ModuleId, RoleId = x.RoleId
                                    }).ToList()).ToList();
        var modulesOld = _manageRoleQueries.GetModulesByUserTypeIdAsync(userType.Id).Result;
        var addModules = AddList(modulesOld, modulesRequest).Result;

        var deleteModules = DeleteList(modulesOld, modulesRequest).Result;
        _manageRoleQueries.DeleteModulesByUserTypeIdAsync(userType.Id,
                                                          deleteModules.Select(x => x.ModuleId).ToList());
        var modules = new List<ModuleDecentralization>();
        foreach (var module in addModules) modules.Add(new ModuleDecentralization(module.ModuleId, module.RoleId));
        userType.AddModuleDecentralizations(modules);

        var functionsRequest = message.Roles
                                      .SelectMany(x => x.Modules.SelectMany(y => y.Functions
                                                                                .Select(z =>
                                                                                        new
                                                                                            CustomFunctionDecentralizationModel
                                                                                            {
                                                                                                FunctionId =
                                                                                                    z.FunctionId,
                                                                                                RoleId = x
                                                                                                    .RoleId
                                                                                            }).ToList()))
                                      .ToList();
        var functionsOld = _manageRoleQueries.GetFunctionsByUserTypeIdAsync(userType.Id).Result;
        var addFunctions = AddList(functionsOld, functionsRequest).Result;

        var deleteFunctions = DeleteList(functionsOld, functionsRequest).Result;
        _manageRoleQueries.DeleteFunctionsByUserTypeIdAsync(userType.Id,
                                                            deleteFunctions.Select(x => x.FunctionId).ToList());
        var functions = new List<FunctionDecentralization>();
        foreach (var function in addFunctions)
            functions.Add(new FunctionDecentralization(function.FunctionId, function.RoleId));

        userType.AddFunctionDecentralizations(functions);

        userType.UpdateUserTager(_user.UserId, DateTime.UtcNow);

        _manageRoleRepository.UpdateUserType(userType);

        if (Commit())
        {
            //Bus.RaiseEvent(new UserTypeUpdatedEvent(Domain.Core.Events.StoredEventType.Update, Guid.Parse(_user.UserId), _user.FullName, Guid.Parse(userType.Id), userType.Name, Guid.Parse(_user.UnitUserId), _user.PortalId
            //    , message.Name, message.RoleId, message.SelectedItems));
        }

        Debug.WriteLine("UpdateUserType");
        return Task.FromResult(true);
    }

#endregion
}