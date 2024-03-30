#region

using System.Collections.Generic;
using System.Threading.Tasks;
using ITC.Domain.Core.Pagination;
using ITC.Infra.CrossCutting.Identity.Authorization;
using ITC.Infra.CrossCutting.Identity.Models.QueryModel;
using ITC.Infra.CrossCutting.Identity.Repository;

#endregion

namespace ITC.Infra.CrossCutting.Identity.Queries;

public interface IManageRoleQueries
{
#region Methods

    Task<int> CountFunctionAsync(UrlQuery urlQuery);
    Task<int> CountModuleAsync(UrlQuery urlQuery);
    Task<int> CountPortalAsync(UrlQuery urlQuery);
    Task<int> CountUserTypeAsync(ManageRoleQueryViewModel urlQuery, string userId = null);
    Task DeleteFunctionsByModuleIdAsync(string moduleId, List<int> weights, List<string> roleIds);
    Task DeleteFunctionsByUserTypeIdAsync(string userTypeId, List<string> functions);
    Task DeleteModulesByUserTypeIdAsync(string userTypeId, List<string> modules);
    Task DeleteRolesByModuleIdAsync(string moduleId, List<string> roleIds);
    Task EditAccountsWhenDeleteUserType(string userTypeIdOld, string userTypeIdNew, string userId = null);
    Task<string> Excecute(string query);
    Task<CustomUserTypeModel> GetAccountInfoByUserIdAsync(string userId);
    Task<List<FunctionModel>> GetFunctionsAsync(UrlQuery urlQuery);
    Task<List<TypeAudit>> GetFunctionsByModuleIdAsync(string moduleId);
    Task<List<CustomFunctionDecentralizationModel>> GetFunctionsByUserTypeIdAsync(string userTypeId);
    Task<List<ModuleModel>> GetModulesAsync(UrlQuery urlQuery);
    Task<List<ModuleModel>> GetModulesAsync();
    Task<List<CustomModuleDecentralizationModel>> GetModulesByUserTypeIdAsync(string userTypeId);
    Task<List<TreeViewNode>> GetPermissionsAsync();
    Task<UserTypeDetailModel> GetPermissionsByUserIdAsync(string userId);
    Task<UserTypeDetailModel> GetPermissionsByUserIdGroupByModuleGroupAsync(string userId);
    Task<CustomUserTypeModel> GetPermissionsByUserTypeAsync(string userId, string userTypeId);
    Task<List<PortalModel>> GetPortalsAsync(UrlQuery urlQuery);
    Task<List<PortalModel>> GetPortalsAsync(bool isDepartmentOfEducation = true);
    Task<List<string>> GetRolesByModuleIdAsync(string moduleId);
    Task<UserTypeDetailModel> GetUserTypeByIdAsync(string userTypeId, string userId = null);
    Task<UserTypeModel> GetUserTypeByRoleIdentityAsync(string roleIdentity);
    Task<List<UserTypeModel>> GetUserTypesAsync(ManageRoleQueryViewModel urlQuery, string userId = null);
    Task<List<UserTypeModel>> GetUserTypesPaginationAsync(ManageRoleQueryViewModel urlQuery, string userId = null);
    Task<List<UserTypeModel>> GetUserTypesAsync(string userId = null);
    Task<List<UserTypeModel>> GetUserTypesExceptByUserTypeIdAsync(string userTypeId, string userId = null);

    /// <summary>
    ///     Trả về danh sách các quyền của người dùng
    /// </summary>
    /// <param name="userId">Mã người dùng</param>
    /// <param name="userTypeId">Kiểu người dùng</param>
    /// <param name="defaultRoleId">Quyền mặc định</param>
    /// <returns></returns>
    Task<IEnumerable<UserTypeByUserIdDto>> GetUserTypeAndFeatureByUserIdAsync(
        string userId, string userTypeId, string defaultRoleId);

    // /// <summary>
    // /// Trả về danh sách các RoleId
    // /// </summary>
    // /// <param name="userId">Mã người dùng</param>
    // /// <param name="userTypeId">Kiểu người dùng</param>
    // /// <returns></returns>
    // Task<IEnumerable<Combobox2Param>> GetRoleByUserIdAsync(string userId, string userTypeId);
    /// <summary>
    ///     Trả về danh sách quyền của User
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<IEnumerable<AuthorityByUserDto>> GetAuthorityByUser(string userId);

#endregion
}