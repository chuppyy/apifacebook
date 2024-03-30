#region

#endregion

using System.Threading.Tasks;
using ITC.Domain.Models.NewsManagers;

namespace ITC.Domain.Interfaces.NewsManagers.NewsGithubManagers;

/// <summary>
///     Lớp interface repository loại nhóm tin
/// </summary>
public interface INewsGithubRepository : IRepository<NewsGithub>
{
    /// <summary>
    ///     Trả về vị trí lớn nhất
    /// </summary>
    /// <returns></returns>
    Task<int> GetMaxPosition();
}