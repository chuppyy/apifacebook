using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ITC.Domain.Models.HomeManagers;

namespace ITC.Domain.Interfaces.HomeManagers.HomeMenuManagers;

/// <summary>
///     Lớp interface repository danh sách menu trang chủ
/// </summary>
public interface IHomeMenuRepository : IRepository<HomeMenu>
{
    /// <summary>
    ///     Xử lý left - right
    /// </summary>
    /// <param name="model"></param>
    /// <param name="left"></param>
    /// <param name="parentId"></param>
    /// <param name="projectId"></param>
    /// <returns></returns>
    int DeQuyLeftRight(List<HomeMenu> model, int left, string parentId, Guid projectId);

    /// <summary>
    ///     Trả về vị trí lớn nhất
    /// </summary>
    /// <returns></returns>
    Task<int> GetMaxPosition();
}