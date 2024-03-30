using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ITC.Domain.Models.Itphonui;

namespace ITC.Domain.Interfaces.Itphonui.ManagementManagers;

/// <summary>
///     Lớp interface repository quản lý đơn vị
/// </summary>
public interface IManagementManagerRepository : IRepository<ManagementManager>
{
    /// <summary>
    ///     Xử lý left - right
    /// </summary>
    /// <param name="model"></param>
    /// <param name="left"></param>
    /// <param name="parentId"></param>
    /// <returns></returns>
    int DeQuyLeftRight(List<ManagementManager> model, int left, string parentId);

    /// <summary>
    ///     Trả về vị trí lớn nhất
    /// </summary>
    /// <param name="typeId">Loại dữ liệu</param>
    /// <returns></returns>
    Task<int> GetMaxPosition(Guid typeId);

    /// <summary>
    ///     Danh sách ID theo điều kiê Left - Right
    /// </summary>
    /// <param name="vLeft">Giá trị trái</param>
    /// <param name="vRight">Giá trị phải</param>
    /// <param name="projectId"></param>
    /// <returns></returns>
    Task<List<Guid>> GetListIdFromLeftRight(int vLeft, int vRight, Guid projectId);
}