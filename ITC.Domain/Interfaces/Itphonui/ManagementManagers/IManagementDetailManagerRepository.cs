using System;
using System.Threading.Tasks;
using ITC.Domain.Models.Itphonui;

namespace ITC.Domain.Interfaces.Itphonui.ManagementManagers;

/// <summary>
///     Lớp interface repository quản lý đơn vị chi tiết
/// </summary>
public interface IManagementDetailManagerRepository : IRepository<ManagementDetailManager>
{
    /// <summary>
    ///     Trả về dữ liệu theo ManagementId
    /// </summary>
    /// <param name="managementId"></param>
    /// <param name="projectId"></param>
    /// <returns></returns>
    Task<ManagementDetailManager> GetByManagementId(Guid managementId, Guid projectId);
}