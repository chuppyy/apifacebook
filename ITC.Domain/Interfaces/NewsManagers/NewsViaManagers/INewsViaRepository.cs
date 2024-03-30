#region

#endregion

using System.Threading.Tasks;
using ITC.Domain.Models.NewsManagers;

namespace ITC.Domain.Interfaces.NewsManagers.NewsViaManagers;

/// <summary>
///     Lớp interface repository loại nhóm tin
/// </summary>
public interface INewsViaRepository : IRepository<NewsVia>
{
    /// <summary>
    ///     Trả về vị trí lớn nhất
    /// </summary>
    /// <returns></returns>
    Task<int> GetMaxPosition();

    /// <summary>
    /// Trả về dữ liệu NewsVia theo IdQc
    /// </summary>
    /// <param name="idQc">Mã IdQc</param>
    /// <returns></returns>
    Task<NewsVia> GetByIdQc(string idQc);
}