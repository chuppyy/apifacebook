using System;
using System.Threading.Tasks;
using ITC.Domain.Models.HomeManagers;

namespace ITC.Domain.Interfaces.HomeManagers.HomeMenuNewsGroupManagers;

/// <summary>
///     Lớp interface repository danh sách menu trang chủ liên kết với nhóm tin
/// </summary>
public interface IHomeMenuNewsGroupRepository : IRepository<HomeMenuNewsGroup>
{
    /// <summary>
    ///     Trả về số lớn nhất của lịch sử vị trí
    /// </summary>
    /// <param name="homeMenuId"></param>
    /// <returns></returns>
    Task<int> GetMaxHistoryPosition(Guid homeMenuId);
}