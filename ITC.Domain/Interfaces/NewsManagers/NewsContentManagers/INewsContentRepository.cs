using System;
using System.Threading.Tasks;
using ITC.Domain.Core.Models;
using ITC.Domain.Models.NewsManagers;

namespace ITC.Domain.Interfaces.NewsManagers.NewsContentManagers;

/// <summary>
///     Lớp interface repository bài viết
/// </summary>
public interface INewsContentRepository : IRepository<NewsContent>
{
    /// <summary>
    ///     Trả về vị trí lớn nhất
    /// </summary>
    /// <param name="typeId">Loại dữ liệu</param>
    /// <returns></returns>
    Task<int> GetMaxPosition(Guid typeId);

    /// <summary>
    ///     Trả về dữ liệu theo SecrectKey
    /// </summary>
    /// <param name="secretKey">mã dữ liệu</param>
    /// <returns></returns>
    Task<NewsContent> GetBySecretKey(string secretKey);

    /// <summary>
    /// Lấy theo Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<NewsContent> GetByIdAsync(Guid id);

    /// <summary>
    /// Lấy token twitter
    /// </summary>
    /// <returns></returns>
    Task<TokenTwitter> GetTop1TokenTwitterAsync();
}