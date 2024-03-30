#region

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ITC.Domain.Core.ModelShare.SystemManagers.NotificationManagers;
using NCore.Modals;

#endregion

namespace ITC.Domain.Interfaces.SystemManagers.NotificationManagers;

/// <summary>
///     Lớp interface query thông báo hệ thống
/// </summary>
public interface INotificationManagerQueries
{
    /// <summary>
    ///     [Phân trang] Trả về danh sách thông báo hệ thống
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<NotificationManagerPagingDto>> GetPaging(PagingModel model);

    /// <summary>
    ///     Xóa nhiều dữ liệu
    /// </summary>
    /// <returns></returns>
    Task<int> DeleteAsync(List<Guid> model);

    /// <summary>
    ///     [Combobox] Trả về danh sách thông báo hệ thống
    /// </summary>
    /// <param name="vSearch"></param>
    /// <returns></returns>
    Task<IEnumerable<ComboboxModal>> GetCombobox(string vSearch);

    /// <summary>
    ///     [SlideRun] Trả về danh sách thông báo hệ thống
    /// </summary>
    /// <param name="projectId"></param>
    /// <returns></returns>
    Task<IEnumerable<NotificationManagerSlideRunModel>> GetSlideRun(Guid projectId);

    /// <summary>
    ///     [HomeMain] Trả về danh sách thông báo hệ thống
    /// </summary>
    /// <param name="projectId"></param>
    /// <returns></returns>
    Task<IEnumerable<NotificationManagerSlideRunModel>> GetHomeMain(Guid projectId);

    /// <summary>
    ///     [SendUser] Danh sách thông báo hệ thống gửi cho User
    /// </summary>
    /// <param name="staffId">Mã người dùng</param>
    /// <returns></returns>
    Task<IEnumerable<NotificationManagerSendUserModel>> GetWidthUser(Guid staffId);
}