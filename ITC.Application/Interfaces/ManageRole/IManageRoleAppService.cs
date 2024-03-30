#region

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ITC.Application.ViewModels.ManageRole;

#endregion

namespace ITC.Application.Interfaces.ManageRole;

public interface IManageRoleAppService : IDisposable
{
#region Methods

    void Add(ModuleViewModel moduleViewModel);

    void AddFunction(FunctionViewModel       functionViewModel);
    void AddModuleGroup(ModuleGroupViewModel model);

    void AddPortal(PortalViewModel portalViewModel);

    // void AddUserType(UserTypeViewModel       userTypeViewModel);
    // void AddUserTypeUnit(UserTypeViewModel   userTypeViewModel);
    ModuleViewModel   GetById(string         id);
    FunctionViewModel GetFunctionById(string id);

    PortalEditViewModel GetPortalById(int id);

    // UserTypeViewModel GetUserTypeById(string id);
    Task<List<ModuleGroupViewModel>> ModuleGroupAsync();

    Task<ModuleGroupViewModel> ModuleGroupByIdAsync(string            id);
    void                       Remove(string                          id);
    void                       RemoveFunction(string                  id);
    void                       RemoveModuleGroup(string               id);
    void                       RemovePortal(string                    id);
    void                       RemoveUserType(string                  id, string userTypeNewId);
    void                       RemoveUserTypeUnit(string              id, string userTypeNewId);
    void                       Update(ModuleViewModel                 moduleViewModel);
    void                       UpdateFunction(FunctionViewModel       functionViewModel);
    void                       UpdateModuleGroup(ModuleGroupViewModel model);

    void UpdatePortal(PortalEditViewModel portalViewModel);
    // void UpdateUserType(UserTypeViewModel                  userTypeViewModel);
    //
    // void UpdateUserTypeUnit(UserTypeViewModel userTypeViewModel);

#endregion
}