using System.Threading.Tasks;

namespace ITC.Domain.Interfaces.SaleProductManagers.SlideManager;

/// <summary>
///     Lớp interface repository slide
/// </summary>
public interface ISlideManagerRepository : IRepository<Models.SaleProductManagers.SlideManager>
{
    /// <summary>
    ///     Trả về vị trí lớn nhất
    /// </summary>
    /// <returns></returns>
    Task<int> GetMaxPosition();
}