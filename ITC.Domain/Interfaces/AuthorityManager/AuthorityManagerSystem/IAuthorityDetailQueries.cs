#region

using System;
using System.Threading.Tasks;

#endregion

namespace ITC.Domain.Interfaces.AuthorityManager.AuthorityManagerSystem;

/// <summary>
///     Lớp interface query phân quyền chi tiết
/// </summary>
public interface IAuthorityDetailQueries
{
    /// <summary>
    ///     Xóa dữ liệu
    /// </summary>
    /// <returns></returns>
    Task<int> DeleteAsync(Guid authoritiesId, int historyPosition);
}