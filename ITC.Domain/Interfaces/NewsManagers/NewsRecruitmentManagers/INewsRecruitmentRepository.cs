#region

#endregion

using System.Threading.Tasks;
using ITC.Domain.Models.NewsManagers;

namespace ITC.Domain.Interfaces.NewsManagers.NewsRecruitmentManagers;

/// <summary>
///     Lớp interface repository bài viết
/// </summary>
public interface INewsRecruitmentRepository : IRepository<NewsRecruitment>
{
    /// <summary>
    ///     Trả về vị trí lớn nhất
    /// </summary>
    /// <param name="typeId">Loại dữ liệu</param>
    /// <returns></returns>
    Task<int> GetMaxPosition(int typeId);

    /// <summary>
    ///     Trả về dữ liệu theo SecrectKey
    /// </summary>
    /// <param name="secretKey">mã dữ liệu</param>
    /// <returns></returns>
    Task<NewsRecruitment> GetBySecretKey(string secretKey);
}