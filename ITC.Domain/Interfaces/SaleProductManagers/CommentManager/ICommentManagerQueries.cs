#region

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ITC.Domain.Core.ModelShare.SaleProductManagers.CommentManager;
using NCore.Modals;

#endregion

namespace ITC.Domain.Interfaces.SaleProductManagers.CommentManager;

/// <summary>
///     Lớp interface query comment
/// </summary>
public interface ICommentManagerQueries
{
    /// <summary>
    ///     [Phân trang] Trả về danh sách comment
    /// </summary>
    /// <param name="model">Dữ liệu nhận từ FE</param>
    /// <returns></returns>
    Task<IEnumerable<CommentManagerPagingDto>> GetPaging(PagingModel model);

    /// <summary>
    ///     Xóa nhiều dữ liệu
    /// </summary>
    /// <returns></returns>
    Task<int> DeleteAsync(List<Guid> model);
}