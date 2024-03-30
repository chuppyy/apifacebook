using System.Threading.Tasks;

namespace ITC.Domain.Interfaces.SaleProductManagers.ContactManager;

/// <summary>
///     Lớp interface repository liên hệ
/// </summary>
public interface IContactManagerRepository : IRepository<Models.SaleProductManagers.ContactManager>
{
    /// <summary>
    ///     Trả về vị trí lớn nhất
    /// </summary>
    /// <returns></returns>
    Task<int> GetMaxPosition();
}