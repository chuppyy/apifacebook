#region

#endregion

using System;
using System.Threading.Tasks;
using ITC.Domain.Models.NewsManagers;

namespace ITC.Domain.Interfaces.NewsManagers.NewsAttackManagers;

/// <summary>
///     Lớp interface repository bài viết đính kèm
/// </summary>
public interface INewsAttackRepository : IRepository<NewsAttack>
{
    /// <summary>
    ///     Trả về vị trí lịch sử
    /// </summary>
    /// <param name="newsContentId">Mã bài viết</param>
    /// <returns></returns>
    Task<int> GetMaxHistoryPosition(Guid newsContentId);
}