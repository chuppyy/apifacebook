using System;
using System.Threading.Tasks;

namespace ITC.Domain.Interfaces.SaleProductManagers.AboutManager;

/// <summary>
///     Lớp interface repository giới thiệu
/// </summary>
public interface IAboutManagerRepository : IRepository<Models.SaleProductManagers.AboutManager>
{
    /// <summary>
    ///     Lấy dữ liệu giới thiệu theo loại giới thiệu
    /// </summary>
    /// <param name="typeId"></param>
    /// <param name="projectId"></param>
    /// <returns></returns>
    Task<Models.SaleProductManagers.AboutManager> GetByType(int typeId, Guid projectId);
}