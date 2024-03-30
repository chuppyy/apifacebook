#region

#endregion

using System;
using System.Threading.Tasks;
using ITC.Domain.Models.NewsManagers;

namespace ITC.Domain.Interfaces.NewsManagers.NewsGroupTypeManagers;

/// <summary>
///     Lớp interface repository loại nhóm tin
/// </summary>
public interface INewsGroupTypeRepository : IRepository<NewsGroupType>
{
    /// <summary>
    ///     Trả về vị trí lớn nhất
    /// </summary>
    /// <returns></returns>
    Task<int> GetMaxPosition();

    /// <summary>
    ///     Danh sách dữ liệu theo SecrectId
    /// </summary>
    /// <returns></returns>
    Task<NewsGroupType> GetBySecrect(Guid projectId, string secrectKey);
}