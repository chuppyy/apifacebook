using System;
using System.Threading.Tasks;
using ITC.Domain.Models.HomeManagers;

namespace ITC.Domain.Interfaces.HomeManagers.HomeNewsGroupDetailManagers;

/// <summary>
///     Lớp interface repository danh sách các bài viết hiển thị trên trang chủ liên kết với nhóm tin
/// </summary>
public interface IHomeNewsGroupViewDetailRepository : IRepository<HomeNewsGroupViewDetail>
{
    /// <summary>
    ///     Trả về số lớn nhất của lịch sử vị trí
    /// </summary>
    /// <param name="homeMenuId"></param>
    /// <returns></returns>
    Task<int> GetMaxHistoryPosition(Guid homeMenuId);
}