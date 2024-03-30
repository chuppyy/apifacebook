#region

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ITC.Domain.Core.ModelShare;
using ITC.Domain.Core.ModelShare.AuthorityManager;
using ITC.Domain.Core.ModelShare.SystemManagers.SortMenuManagers;
using ITC.Domain.Extensions;
using NCore.Modals;

#endregion

namespace ITC.Domain.Interfaces.AuthorityManager.ProjectMenuManagerSystem;

/// <summary>
///     Lớp interface query danh sách chức năng
/// </summary>
public interface IMenuManagerQueries
{
#region v2022

    /// <summary>
    ///     Trả về vị trí left - right
    /// </summary>
    /// <param name="parentId">Mã cha - con</param>
    /// <param name="managementId">Mã đơn vị</param>
    /// <returns></returns>
    Tuple<int, int, int> GetLeftRightAsync(string parentId, Guid managementId);

    /// <summary>
    ///     Trả về vị trí left - right
    /// </summary>
    /// <param name="parentId">Mã cha - con</param>
    /// <param name="managementId">Mã đơn vị</param>
    /// <returns></returns>
    Tuple<int, int, int> GetLeftRightAsyncWithProject(string parentId, Guid projectId);

    /// <summary>
    ///     Xóa chủ đề và chuyển chủ đề qua nhánh mới
    /// </summary>
    /// <param name="sbBuilder">Câu lệnh sql để xử lý</param>
    /// <returns></returns>
    Task<int> DeleteAsync(StringBuilder sbBuilder);

    /// <summary>
    ///     Xóa chủ đề và chuyển chủ đề qua nhánh mới
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<int> DeleteMenuDetailAsync(List<Guid> model);

    /// <summary>
    ///     Cập nhật chức năng hệ thống
    /// </summary>
    /// <param name="childId"></param>
    /// <param name="childLeft"></param>
    /// <param name="childRight"></param>
    /// <param name="childParentFather"></param>
    /// <param name="projectId"></param>
    /// <param name="childeLeft"></param>
    /// <param name="childParrentFather"></param>
    /// <returns></returns>
    Task<IEnumerable<PLeftRight>> UpdateAsync(string childId,           int  childLeft, int childRight,
                                              string childParentFather, Guid projectId);

    /// <summary>
    ///     [TreeView] Trả về danh sách các chức năng
    /// </summary>
    /// <param name="version"></param>
    /// <param name="projectId"></param>
    /// <returns></returns>
    Task<IEnumerable<TreeViewProjectModel>> GetTreeView(int version);

    /// <summary>
    ///     [Combobox] Danh sách quyền mặc định hệ thống
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<IEnumerable<PermissionDefaultViewModal>> GetPermissionDefault();

    /// <summary>
    ///     Danh sách menu theo quyền sử dụng
    /// </summary>
    /// <param name="authority"></param>
    /// <param name="isAdmin"></param>
    /// <param name="isAddNew"></param>
    /// <returns></returns>
    Task<IEnumerable<MenuByAuthoritiesViewModel>> GetMenuByAuthorities(Guid authority, bool isAdmin, bool isAddNew);

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
    ///     Xóa dữ liệu quyền sử dụng chi tiết
    /// </summary>
    /// <param name="id"></param>
    /// <param name="authoritiesId"></param>
    /// <returns></returns>
    Task<int> DeleteDetailAsync(List<Guid> id, Guid authoritiesId);

    /// <summary>
    ///     Xóa dữ liệu quyền sử dụng
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<int> DeleteAuthoritiesAsync(List<Guid> model);

    /// <summary>
    ///     Danh sách menu theo người dùng
    /// </summary>
    /// <param name="userId">Mã người dùng</param>
    /// <returns></returns>
    Task<IEnumerable<MenuRoleReturnViewModel>> GetMenu(string userId);

    /// <summary>
    ///     [Phân trang] Danh sách menu để xắp xếp
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<SortMenuManagerDto>> GetSortMenu(Guid menuId, Guid parentId, Guid projectId);

#endregion

#region V2023

    /// <summary>
    ///     Danh sách menu theo người dùng
    /// </summary>
    /// <param name="userId">Mã người dùng</param>
    /// <param name="version"></param>
    /// <returns></returns>
    Task<IEnumerable<v3MenuReturnFeModel>> v2023GetMenu(string userId, int version);

    /// <summary>
    ///     Danh sách phiên bản menu
    /// </summary>
    /// <returns></returns>
    Task<List<int>> v2023GetMenuVersion();

    /// <summary>
    ///     Danh sách menu theo quyền sử dụng
    /// </summary>
    /// <param name="authority"></param>
    /// <returns></returns>
    Task<IEnumerable<MenuByAuthoritiesV2023>> V202301GetMenuByAuthorities(Guid authority);

    /// <summary>
    ///     Danh sách menu mặc định
    /// </summary>
    /// <param name="authority"></param>
    /// <returns></returns>
    Task<IEnumerable<MenuByAuthoritiesV2023>> V202301GetMenuDefault(Guid menuId);

#endregion
}