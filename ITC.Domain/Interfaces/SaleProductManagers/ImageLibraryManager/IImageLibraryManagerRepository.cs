using System;
using System.Threading.Tasks;

namespace ITC.Domain.Interfaces.SaleProductManagers.ImageLibraryManager;

/// <summary>
///     Lớp interface repository slide
/// </summary>
public interface IImageLibraryManagerRepository : IRepository<Models.SaleProductManagers.ImageLibraryManager>
{
    /// <summary>
    ///     Trả về vị trí lớn nhất
    /// </summary>
    /// <returns></returns>
    Task<int> GetMaxPosition();

    /// <summary>
    ///     Lấy dữ liệu theo SecrectKey
    /// </summary>
    /// <param name="projectId"></param>
    /// <param name="secrectKey"></param>
    /// <returns></returns>
    Task<Models.SaleProductManagers.ImageLibraryManager> GetBySecrectKey(Guid projectId, string secrectKey);
}