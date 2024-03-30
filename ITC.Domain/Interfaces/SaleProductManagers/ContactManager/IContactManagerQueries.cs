#region

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ITC.Domain.Core.ModelShare.SaleProductManagers.ContactManager;
using NCore.Modals;

#endregion

namespace ITC.Domain.Interfaces.SaleProductManagers.ContactManager;

/// <summary>
///     Lớp interface query liên hệ
/// </summary>
public interface IContactManagerQueries
{
    /// <summary>
    ///     [Phân trang] Trả về danh sách liên hệ
    /// </summary>
    /// <param name="model">Dữ liệu nhận từ FE</param>
    /// <returns></returns>
    Task<IEnumerable<ContactManagerPagingDto>> GetPaging(PagingModel model);

    /// <summary>
    ///     Xóa nhiều dữ liệu
    /// </summary>
    /// <returns></returns>
    Task<int> DeleteAsync(List<Guid> model);

    /// <summary>
    ///     Cập nhật lại trạng thái của các dữ liệu hiển thị trên trang chủ
    /// </summary>
    /// <param name="newKey"></param>
    /// <param name="projectId"></param>
    /// <returns></returns>
    Task<int> UpdateIsShowHomePage(Guid newKey, Guid projectId);
}