#region

#endregion

using System.Threading.Tasks;
using ITC.Domain.Models.SystemManagers;

namespace ITC.Domain.Interfaces.SystemManagers.NotificationManagers;

/// <summary>
///     Lớp interface repository thông báo hệ thống
/// </summary>
public interface INotificationManagerRepository : IRepository<NotificationManager>
{
    /// <summary>
    ///     Trả về vị trí lớn nhất
    /// </summary>
    /// <returns></returns>
    Task<int> GetMaxPosition();
}