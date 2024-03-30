using System.Threading.Tasks;
using ITC.Domain.Models.HomeManagers;

namespace ITC.Domain.Interfaces.HomeManagers.HomeNewsGroupViewManagers;

/// <summary>
///     Lớp interface repository danh sách các nhóm bài viết hiển thị trên trang chủ
/// </summary>
public interface IHomeNewsGroupViewRepository : IRepository<HomeNewsGroupView>
{
    /// <summary>
    ///     Trả về vị trí lớn nhất
    /// </summary>
    /// <returns></returns>
    Task<int> GetMaxPosition();
}