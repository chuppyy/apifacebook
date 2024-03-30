using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ITC.Domain.Models.SystemManagers;

namespace ITC.Domain.Interfaces.SystemManagers.NotificationManagers;

/// <summary>
///     Lớp interface repository thông báo - file đính kèm
/// </summary>
public interface INotificationAttackManagerRepository : IRepository<NotificationAttackManager>
{
    /// <summary>
    ///     Danh sách ID theo mã thông báo
    /// </summary>
    /// <param name="notificationId">mã thông báo</param>
    /// <returns></returns>
    Task<List<Guid>> GetListFileByNotificationId(Guid notificationId);

    /// <summary>
    ///     Danh sách ID File theo mã thông báo
    /// </summary>
    /// <param name="notificationId">mã thông báo</param>
    /// <returns></returns>
    Task<List<Guid>> GetListFileAttackByNotificationId(Guid notificationId);
}