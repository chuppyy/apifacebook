#region

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ITC.Domain.Core.ModelShare.AuthorityManager.AuthorityManagerSystems;
using NCore.Modals;

#endregion

namespace ITC.Application.AppService.AuthorityManager.AuthorityManagerSystem;

/// <summary>
///     Class interface service phân quyền
/// </summary>
public interface IAuthorityManagerAppService
{
    /// <summary>
    ///     Thêm mới Quyền sử dụng
    /// </summary>
    /// <param name="model"></param>
    Task<bool> Add(AuthorityManagerSystemEventModel model);

    /// <summary>
    ///     Xóa Quyền sử dụng
    /// </summary>
    /// <param name="model"></param>
    Task<bool> Delete(DeleteModal model);

    /// <summary>
    ///     Lấy theo Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    AuthorityManagerSystemEventModel GetById(Guid id);

    /// <summary>
    ///     Cập nhật Quyền sử dụng
    /// </summary>
    /// <param name="model"></param>
    Task<bool> Update(AuthorityManagerSystemEventModel model);

    /// <summary>
    ///     [Combobox] Danh sách Quyền sử dụng
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<IEnumerable<AuthorityManagerSystemPagingDto>> GetPaging(PagingModel model);

    /// <summary>
    ///     [Combobox] Danh sách Phân quyền
    /// </summary>
    /// <param name="vSearch"></param>
    /// <param name="projectId"></param>
    /// <returns></returns>
    Task<IEnumerable<ComboboxModal>> GetCombobox(string vSearch, Guid? projectId);

    /// <summary>
    ///     Cập nhật Quyền sử dụng cho menu
    /// </summary>
    /// <param name="model"></param>
    Task<bool> UpdatePermissionMenu(AuthorityManagerSystemUpdatePermissionEventModel model);
}