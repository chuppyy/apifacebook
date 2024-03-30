#region

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ITC.Domain.Core.ModelShare;
using ITC.Domain.Core.ModelShare.AuthorityManager;
using NCore.Modals;

#endregion

namespace ITC.Application.AppService.AuthorityManager.MenuManagerSystem;

/// <summary>
///     Class interface service chức năng
/// </summary>
public interface IMenuManagerAppService
{
#region Methods

#region V2

    /// <summary>
    ///     Thêm danh mục
    /// </summary>
    /// <param name="model"></param>
    void Add(MenuManagerEventModel model);

    /// <summary>
    ///     Xóa danh mục
    /// </summary>
    /// <param name="model"></param>
    void Delete(Guid model);

    /// <summary>
    ///     Xóa quyền sủ dụng
    /// </summary>
    /// <param name="model"></param>
    /// <param name="projectId"></param>
    /// <param name="companyId"></param>
    void DeleteAuthorities(string model, Guid projectId, Guid companyId);

    /// <summary>
    ///     Lấy theo Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    MenuManagerEventModel GetById(Guid id);

    /// <summary>
    ///     Cập nhật danh mục
    /// </summary>
    /// <param name="model"></param>
    void Update(MenuManagerEventModel model);

    /// <summary>
    ///     [TreeView] Trả về danh sách các chức năng
    /// </summary>
    /// <param name="version"></param>
    /// <returns></returns>
    Task<IEnumerable<TreeViewProjectModel>> GetTreeView(int version);

    /// <summary>
    ///     [Combobox] Danh sách quyền mặc định hệ thống
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    Task<IEnumerable<PermissionDefaultViewModal>> GetPermissionDefault(int value);

    /// <summary>
    ///     [v2023-Combobox] Danh sách quyền mặc định hệ thống
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    Task<IEnumerable<v2023PermissionDefaultViewModal>> V2023GetPermissionDefault(int value);

    /// <summary>
    ///     Danh sách menu theo quyền sử dụng
    /// </summary>
    /// <param name="authority"></param>
    /// <param name="isAdmin"></param>
    /// <returns></returns>
    Task<IEnumerable<MenuRoleEventViewModel>> GetMenuByAuthorities(Guid authority, bool isAdmin);

    /// <summary>
    ///     Thêm quyền sử dụng
    /// </summary>
    /// <param name="model"></param>
    void AddAuthorities(AuthoritiesMenuManagerEventModel model);

    /// <summary>
    ///     Cập nhật quyền sử dụng
    /// </summary>
    /// <param name="model"></param>
    void UpdateAuthorities(AuthoritiesMenuManagerEventModel model);

    /// <summary>
    ///     Danh sách quyền sử dụng theo companyId, projectId
    /// </summary>
    /// <param name="companyId"></param>
    /// <param name="projectId"></param>
    /// <param name="search"></param>
    /// <param name="pageSize"></param>
    /// <param name="pageNumber"></param>
    /// <returns></returns>
    Task<IEnumerable<AuthoritiesViewModel>> GetAuthoritiesAsync(Guid companyId, Guid projectId, string search,
                                                                int  pageSize,  int  pageNumber);

    /// <summary>
    ///     [Combobox] Danh sách quyền sử dụng
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<KeyValuePair<Guid, string>>> GetAuthoritiesCombobox(Guid companyId, Guid projectId);

    /// <summary>
    ///     Lấy dữ liệu quyền sử dụng theo Id
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<KeyValuePair<Guid, string>>> GetAuthoritiesById(Guid authoritiesId);

    /// <summary>
    ///     Danh sách menu theo người dùng
    /// </summary>
    /// <param name="userId">Mã người dùng</param>
    /// <returns></returns>
    Task<IEnumerable<MenuRoleReturnViewModel>> GetMenu(string userId);

    /// <summary>
    ///     Lấy dữ liệu quyền truy cập theo ID chức năng đã được cấp
    /// </summary>
    /// <param name="authoritiesId">Mã chức năng chi tiết</param>
    /// <returns></returns>
    Task<IEnumerable<PermissionByAuthoritiesModel>> GetPermissionByAuthorities(Guid authoritiesId);

    /// <summary>
    ///     Cập nhật giá trị quyền
    /// </summary>
    /// <param name="model"></param>
    Task<bool> UpdatePermissionByAuthorities(SortMenuPermissionByAuthoritiesModel model);

#endregion

#region v2023

    /// <summary>
    ///     Danh sách menu theo người dùng
    /// </summary>
    /// <param name="userId">Mã người dùng</param>
    /// <returns></returns>
    Task<IEnumerable<v3MenuReturnFeModel>> V2023GetMenu(string userId);

    /// <summary>
    ///     Danh sách các phiên bản menu
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<ComboboxModalInt>> V2023GetMenuVersion();

    /// <summary>
    ///     Danh sách menu theo quyền sử dụng
    /// </summary>
    /// <param name="authority"></param>
    /// <returns></returns>
    Task<IEnumerable<MenuByAuthoritiesV2023>> V202301GetMenuByAuthorities(Guid authority);

    /// <summary>
    ///     Danh sách menu theo quyền sử dụng
    /// </summary>
    /// <param name="authority">Mã quyền</param>
    /// <param name="menuId">Mã chức năng</param>
    /// <returns></returns>
    Task<IEnumerable<MenuByAuthoritiesV2023>> V202301GetFeatureByAuthoritiesParent(Guid authority, Guid menuId);

    /// <summary>
    ///     Trả về PermissionDefault by MenuId
    /// </summary>
    /// <param name="authority"></param>
    /// <param name="menuId">Mã chức năng</param>
    /// <param name="data"></param>
    /// <returns></returns>
    Task<IEnumerable<v2023PermissionDefaultViewModal>> V202301GetPermissionDefaultByMenu(
        Guid authority, Guid menuId, int data);

#endregion

#endregion
}