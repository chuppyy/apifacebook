using System;
using ITC.Domain.Core.Models;
using NCore.Actions;

namespace ITC.Domain.Models.SystemManagers;

/// <summary>
///     Thông báo hệ thống gửi tới người dùng
/// </summary>
public class NotificationUserManager : RootModel
{
    /// <summary>
    ///     Hàm dựng mặc định
    /// </summary>
    protected NotificationUserManager()
    {
    }

    /// <summary>
    ///     Hàm dựng có tham số
    /// </summary>
    /// <param name="id">Mã thông báo</param>
    /// <param name="notificationManagerId">Mã thông báo</param>
    /// <param name="isRead">Trạng thái đọc</param>
    /// <param name="staffId">Mã người dùng</param>
    /// <param name="dateTimeRead">Thời gian đọc</param>
    /// <param name="createBy">Người tạo</param>
    public NotificationUserManager(Guid      id,           Guid   notificationManagerId, bool isRead, Guid staffId,
                                   DateTime? dateTimeRead, string createBy = null) :
        base(id, createBy)
    {
        Update(notificationManagerId, isRead, staffId, dateTimeRead, createBy);
    }

    /// <summary>
    ///     Hàm dựng có tham số
    /// </summary>
    /// <param name="id">Mã thông báo</param>
    /// <param name="isRead">Trạng thái đọc</param>
    /// <param name="staffId">Mã người dùng</param>
    /// <param name="dateTimeRead">Thời gian đọc</param>
    /// <param name="createBy">Người tạo</param>
    public NotificationUserManager(Guid id, bool isRead, Guid staffId, DateTime? dateTimeRead, string createBy = null) :
        base(id, createBy)
    {
        IsRead       = isRead;
        StaffId      = staffId;
        DateTimeRead = dateTimeRead;
        StatusId     = ActionStatusEnum.Active.Id;
    }

    /// <summary>
    ///     Mã thông báo
    /// </summary>
    public Guid NotificationManagerId { get; set; }

    /// <summary>
    ///     Trạng thái đọc
    /// </summary>
    public bool IsRead { get; set; }

    /// <summary>
    ///     Mã người dùng
    /// </summary>
    public Guid StaffId { get; set; }

    /// <summary>
    ///     Thời gian đọc
    /// </summary>
    public DateTime? DateTimeRead { get; set; }

    /// <summary>
    ///     Cập nhật
    /// </summary>
    /// <param name="notificationManagerId">Mã thông báo</param>
    /// <param name="isRead">Trạng thái đọc</param>
    /// <param name="staffId">Mã người dùng</param>
    /// <param name="dateTimeRead">Thời gian đọc</param>
    /// <param name="createBy">Người tạo</param>
    public void Update(Guid   notificationManagerId, bool isRead, Guid staffId, DateTime? dateTimeRead,
                       string createBy = null)
    {
        NotificationManagerId = notificationManagerId;
        IsRead                = isRead;
        StaffId               = staffId;
        DateTimeRead          = dateTimeRead;
        Update(createBy);
    }
}