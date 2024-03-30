#region

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ITC.Domain.Core.ModelShare.CompanyManager.CustomerManagers;
using NCore.Modals;

#endregion

namespace ITC.Domain.Interfaces.CompanyManagers.CustomerManagers;

/// <summary>
///     Lớp interface query khách hàng
/// </summary>
public interface ICustomerManagerQueries
{
    /// <summary>
    ///     [Phân trang] Trả về danh sách khách hàng
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<CustomerManagerPagingDto>> GetPaging(PagingModel model);

    /// <summary>
    ///     Xóa nhiều dữ liệu
    /// </summary>
    /// <returns></returns>
    Task<int> DeleteAsync(List<Guid> model);

    /// <summary>
    ///     [Combobox] Trả về danh sách khách hàng
    /// </summary>
    /// <param name="vSearch"></param>
    /// <returns></returns>
    Task<IEnumerable<ComboboxModal>> GetCombobox(string vSearch);
}