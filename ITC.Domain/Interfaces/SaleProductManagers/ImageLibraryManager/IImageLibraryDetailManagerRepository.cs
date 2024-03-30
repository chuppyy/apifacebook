using System.Threading.Tasks;
using ITC.Domain.Models.SaleProductManagers;

namespace ITC.Domain.Interfaces.SaleProductManagers.ImageLibraryManager;

/// <summary>
///     Lớp interface repository slide
/// </summary>
public interface IImageLibraryDetailManagerRepository : IRepository<ImageLibraryDetailManager>
{
    /// <summary>
    ///     Trả về vị trí lớn nhất
    /// </summary>
    /// <returns></returns>
    Task<int> GetMaxPosition();
}