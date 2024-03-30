using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ITC.Domain.Models.SystemManagers;

namespace ITC.Domain.Interfaces.SystemManagers.NotificationManagers;

/// <summary>
///     Lớp interface repository thông báo người dùng
/// </summary>
public interface INotificationUserManagerRepository : IRepository<NotificationUserManager>
{
    /// <summary>
    ///     Danh sách ID theo mã thông báo
    /// </summary>
    /// <param name="notificationId">mã thông báo</param>
    /// <returns></returns>
    Task<List<Guid>> GetListUserByNotificationId(Guid notificationId);
}