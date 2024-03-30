using System;
using System.Threading.Tasks;

namespace ITC.Domain.Interfaces.SystemManagers.Helpers;

/// <summary>
///     Class interface query Helper
/// </summary>
public interface IHelperQueries
{
    /// <summary>
    ///     Cập nhật trạng thái dữ liệu
    /// </summary>
    /// <returns></returns>
    Task<int> UpdateStatus(Guid contentId, string tableName, int statusId);
}