using System.Threading.Tasks;

namespace ITC.Domain.Interfaces.SaleProductManagers.HealthHandbookManager;

/// <summary>
///     Lớp interface repository giới thiệu
/// </summary>
public interface IHealthHandbookManagerRepository : IRepository<Models.SaleProductManagers.HealthHandbookManager>
{
    /// <summary>
    ///     Trả về dữ liệu theo SecrectKey
    /// </summary>
    /// <param name="secretKey">mã dữ liệu</param>
    /// <returns></returns>
    Task<Models.SaleProductManagers.HealthHandbookManager> GetBySecretKey(string secretKey);
}