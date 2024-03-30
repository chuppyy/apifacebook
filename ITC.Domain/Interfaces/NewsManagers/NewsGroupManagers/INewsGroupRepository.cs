using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ITC.Domain.Models.NewsManagers;

namespace ITC.Domain.Interfaces.NewsManagers.NewsGroupManagers;

/// <summary>
///     Lớp interface repository danh sách nhóm tin
/// </summary>
public interface INewsGroupRepository : IRepository<NewsGroup>
{
    /// <summary>
    ///     Xử lý left - right
    /// </summary>
    /// <param name="model"></param>
    /// <param name="left"></param>
    /// <param name="parentId"></param>
    /// <returns></returns>
    int DeQuyLeftRight(List<NewsGroup> model, int left, string parentId);

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
    /// <returns></returns>
    Task<List<Guid>> GetListIdFromLeftRight(int vLeft, int vRight);

    // <summary>
    /// Lấy dữ liệu theo SecrectKey
    /// </summary>
    /// <param name="projectId"></param>
    /// <param name="secrectKey"></param>
    /// <returns></returns>
    Task<NewsGroup> GetBySecrectKey(Guid projectId, string secrectKey);

    Task<NewsGroup> GetByIdAsync(Guid id);
}