#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ITC.Application.Interfaces.ManageRole;
using ITC.Application.ViewModels.ManageRole;
using ITC.Domain.Commands.ManageRole;
using ITC.Domain.Core.Bus;
using ITC.Infra.CrossCutting.Identity.Authorization;
using ITC.Infra.CrossCutting.Identity.Commands;
using ITC.Infra.CrossCutting.Identity.Commands.Module;
using ITC.Infra.CrossCutting.Identity.Commands.Portal;
using ITC.Infra.CrossCutting.Identity.Interfaces;

#endregion

namespace ITC.Application.Services.ManageRole;

public class ManageRoleAppService : IManageRoleAppService
{
#region Constructors

    public ManageRoleAppService(IMapper                mapper,
                                IManageRoleRepository  manageRoleRepository,
                                IMediatorHandler       bus,
                                IModuleGroupRepository moduleGroupRepository
    )
    {
        _mapper                = mapper;
        _manageRoleRepository  = manageRoleRepository;
        Bus                    = bus;
        _moduleGroupRepository = moduleGroupRepository;
    }

#endregion

#region Fields

    private readonly IManageRoleRepository  _manageRoleRepository;
    private readonly IMapper                _mapper;
    private readonly IModuleGroupRepository _moduleGroupRepository;
    private readonly IMediatorHandler       Bus;

#endregion

#region IManageRoleAppService Members

    public ModuleViewModel GetById(string id)
    {
        var obj    = _manageRoleRepository.GetById(id);
        var mapObj = _mapper.Map<ModuleViewModel>(obj);
        mapObj.RoleIds = obj.ModuleRoles.Select(x => x.RoleId).ToList();
        mapObj.Weights = obj.Functions.Select(x => (TypeAudit)x.Weight).ToList();
        return mapObj;
    }

    public void Add(ModuleViewModel customerViewModel)
    {
        var registerCommand = _mapper.Map<AddModuleCommand>(customerViewModel);
        Bus.SendCommand(registerCommand);
    }

    public void Update(ModuleViewModel customerViewModel)
    {
        var updateCommand = _mapper.Map<UpdateModuleCommand>(customerViewModel);
        Bus.SendCommand(updateCommand);
    }

    public void Remove(string id)
    {
        var removeCommand = new RemoveModuleCommand(id);
        Bus.SendCommand(removeCommand);
    }

    public void AddFunction(FunctionViewModel functionViewModel)
    {
        var addCommand = _mapper.Map<AddFunctionCommand>(functionViewModel);
        Bus.SendCommand(addCommand);
    }

    public FunctionViewModel GetFunctionById(string id)
    {
        return _mapper.Map<FunctionViewModel>(_manageRoleRepository.GetFunctionById(id));
    }

    public void UpdateFunction(FunctionViewModel functionViewModel)
    {
        var updateCommand = _mapper.Map<UpdateFunctionCommand>(functionViewModel);
        Bus.SendCommand(updateCommand);
    }

    public void RemoveFunction(string id)
    {
        var removeCommand = new RemoveFunctionCommand(id);
        Bus.SendCommand(removeCommand);
    }

    // public void AddUserType(UserTypeViewModel userTypeViewModel)
    // {
    //     var addCommand = _mapper.Map<AddUserTypeCommand>(userTypeViewModel);
    //     Bus.SendCommand(addCommand);
    // }
    //
    // public void AddUserTypeUnit(UserTypeViewModel userTypeViewModel)
    // {
    //     var addCommand = _mapper.Map<AddUserTypeUnitCommand>(userTypeViewModel);
    //     Bus.SendCommand(addCommand);
    // }
    //
    // public UserTypeViewModel GetUserTypeById(string id)
    // {
    //     return _mapper.Map<UserTypeViewModel>(_manageRoleRepository.GetUserTypeById(id));
    // }
    //
    // public void UpdateUserType(UserTypeViewModel userTypeViewModel)
    // {
    //     var updateCommand = _mapper.Map<UpdateUserTypeCommand>(userTypeViewModel);
    //     Bus.SendCommand(updateCommand);
    // }
    //
    // public void UpdateUserTypeUnit(UserTypeViewModel userTypeViewModel)
    // {
    //     var updateCommand = _mapper.Map<UpdateUserTypeUnitCommand>(userTypeViewModel);
    //     Bus.SendCommand(updateCommand);
    // }

    public void RemoveUserType(string id, string userTypeNewId)
    {
        var removeCommand = new RemoveUserTypeCommand(id, userTypeNewId);
        Bus.SendCommand(removeCommand);
    }

    public void RemoveUserTypeUnit(string id, string userTypeNewId)
    {
        var removeCommand = new RemoveUserTypeCommand(id, userTypeNewId, true);
        Bus.SendCommand(removeCommand);
    }

    public void AddPortal(PortalViewModel portalViewModel)
    {
        var addCommand = _mapper.Map<AddPortalCommand>(portalViewModel);
        Bus.SendCommand(addCommand);
    }

    public void UpdatePortal(PortalEditViewModel portalViewModel)
    {
        var updateCommand = _mapper.Map<UpdatePortalCommand>(portalViewModel);
        Bus.SendCommand(updateCommand);
    }

    public void RemovePortal(string id)
    {
        var removeCommand = new RemovePortalCommand(id);
        Bus.SendCommand(removeCommand);
    }

    public PortalEditViewModel GetPortalById(int id)
    {
        return _mapper.Map<PortalEditViewModel>(_manageRoleRepository.GetPortalById(id));
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    public async Task<List<ModuleGroupViewModel>> ModuleGroupAsync()
    {
        return _mapper.Map<List<ModuleGroupViewModel>>(await _moduleGroupRepository.GetAllAsync());
    }

    public void AddModuleGroup(ModuleGroupViewModel model)
    {
        var command = _mapper.Map<AddModuleGroupCommand>(model);
        Bus.SendCommand(command);
    }

    public void UpdateModuleGroup(ModuleGroupViewModel model)
    {
        var command = _mapper.Map<UpdateModuleGroupCommand>(model);
        Bus.SendCommand(command);
    }


    public void RemoveModuleGroup(string id)
    {
        var command = new RemoveModuleGroupCommand(id);
        Bus.SendCommand(command);
    }

    public async Task<ModuleGroupViewModel> ModuleGroupByIdAsync(string id)
    {
        return _mapper.Map<ModuleGroupViewModel>(await _moduleGroupRepository.GetByIdAsync(id));
    }

#endregion
}