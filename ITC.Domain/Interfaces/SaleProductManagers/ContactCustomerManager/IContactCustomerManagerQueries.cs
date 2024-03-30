#region

using System.Collections.Generic;
using System.Threading.Tasks;
using ITC.Domain.Core.ModelShare.SaleProductManagers.ContactCustomerManager;
using NCore.Modals;

#endregion

namespace ITC.Domain.Interfaces.SaleProductManagers.ContactCustomerManager;

/// <summary>
///     Lớp interface query khách hàng liên hệ
/// </summary>
public interface IContactCustomerManagerQueries
{
    /// <summary>
    ///     [Phân trang] Trả về danh sách liên hệ
    /// </summary>
    /// <param name="model">Dữ liệu nhận từ FE</param>
    /// <returns></returns>
    Task<IEnumerable<ContactCustomerManagerPagingDto>> GetPaging(PagingModel model);
}