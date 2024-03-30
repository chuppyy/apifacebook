#region

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ITC.Domain.Core.ModelShare.AuthorityManager;
using ITC.Domain.Core.ModelShare.AuthorityManager.AuthorityManagerSystems;
using NCore.Modals;

#endregion

namespace ITC.Domain.Interfaces.AuthorityManager.AuthorityManagerSystem;

/// <summary>
///     Lớp interface query phân quyền
/// </summary>
public interface IAuthorityManagerQueries
{
    /// <summary>
    ///     [Combobox] Danh sách phòng ban
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<IEnumerable<AuthorityManagerSystemPagingDto>> GetPaging(PagingModel model);

    /// <summary>
    ///     [Combobox] Trả về danh sách phân quyền
    /// </summary>
    /// <param name="vSearch"></param>
    /// <param name="projectId"></param>
    /// <returns></returns>
    Task<IEnumerable<ComboboxModal>> GetCombobox(string vSearch, Guid projectId);

    /// <summary>
    ///     Xóa nhiều dữ liệu
    /// </summary>
    /// <returns></returns>
    Task<int> DeleteAsync(List<Guid> model);

    /// <summary>
    ///     Trả về trạng thái quyền theo tên quyền và người dùng
    /// </summary>
    /// <param name="authorityName">Tên quyền cần lấy dữ liệu</param>
    /// <param name="userId">Mã người dùng</param>
    /// <returns></returns>
    Task<int> GetPermissionByMenuManagerValue(string authorityName, string userId);

    /// <summary>
    ///     Trả về danh sách menu và giá trị quyền của nó
    /// </summary>
    /// <param name="authorities">Mã phân quyền sử dụng</param>
    /// <returns></returns>
    Task<IEnumerable<MenuByAuthoritiesSaveModel>> GetPermissionByMenuId(Guid authorities);

    /// <summary>
    ///     Trả về giá trị của chức năng đã được cấp
    /// </summary>
    /// <param name="authorities">Mã phân quyền sử dụng</param>
    /// <param name="menuId">Mã chức năng</param>
    /// <returns></returns>
    Task<int> PermissionValueByMenuId(Guid authorities, Guid menuId);
}