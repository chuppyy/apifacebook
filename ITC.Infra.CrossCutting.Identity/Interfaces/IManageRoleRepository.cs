#region

using ITC.Infra.CrossCutting.Identity.Models;

#endregion

namespace ITC.Infra.CrossCutting.Identity.Interfaces;

public interface IManageRoleRepository : IRepository<Module>
{
#region Methods

    void AddFunction(Function obj);


    Portal AddPortal(Portal obj);


    void     AddUserType(UserType   obj);
    Function GetFunctionById(string id);
    Function GetFunctionById(string id, int weight);

    Portal   GetPortalById(int          id);
    Portal   GetPortalByIdentity(string identity);
    UserType GetUserTypeById(string     id, string userId = null);

    bool IsExistFunctionByModuleId(string moduleId, int weight);

    bool IsExistModuleByIdentity(string identity);
    bool IsExistModuleByIdentity(string moduleId, string identity);
    void RemoveFunction(Function        obj);
    void RemovePortal(Portal            obj);

    void UpdateFunction(Function obj);
    void UpdatePortal(Portal     obj);

    void UpdateUserType(UserType obj);

#endregion
}