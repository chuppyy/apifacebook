#region

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ITC.Domain.Core.ModelShare.SaleProductManagers.HealthHandbookManager;
using NCore.Modals;

#endregion

namespace ITC.Domain.Interfaces.SaleProductManagers.HealthHandbookManager;

/// <summary>
///     Lớp interface query bài viết
/// </summary>
public interface IHealthHandbookManagerQueries
{
    /// <summary>
    ///     [Phân trang] Trả về danh sách bài viết
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<HealthHandbookManagerPagingDto>> GetPaging(PagingModel model);

    /// <summary>
    ///     Xóa nhiều dữ liệu
    /// </summary>
    /// <returns></returns>
    Task<int> DeleteAsync(List<Guid> model);
}