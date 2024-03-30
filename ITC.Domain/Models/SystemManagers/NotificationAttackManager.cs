using System;
using ITC.Domain.Core.Models;
using NCore.Actions;

namespace ITC.Domain.Models.SystemManagers;

/// <summary>
///     Thông báo hệ thống - file đính kèm
/// </summary>
public class NotificationAttackManager : RootModel
{
    /// <summary>
    ///     Hàm dựng mặc định
    /// </summary>
    protected NotificationAttackManager()
    {
    }

    /// <summary>
    ///     Hàm dựng có tham số
    /// </summary>
    /// <param name="id">Mã thông báo</param>
    /// <param name="notificationManagerId">Mã thông báo</param>
    /// <param name="fileId">Mã file</param>
    /// <param name="createBy">Người tạo</param>
    public NotificationAttackManager(Guid id, Guid notificationManagerId, Guid fileId, string createBy = null) :
        base(id, createBy)
    {
        Update(notificationManagerId, fileId, createBy);
    }

    /// <summary>
    ///     Hàm dựng có tham số
    /// </summary>
    /// <param name="id">Mã thông báo</param>
    /// <param name="fileId">Mã file</param>
    /// <param name="createBy">Người tạo</param>
    public NotificationAttackManager(Guid id, Guid fileId, string createBy = null) : base(id, createBy)
    {
        FileId   = fileId;
        StatusId = ActionStatusEnum.Active.Id;
    }

    /// <summary>
    ///     Mã thông báo
    /// </summary>
    public Guid NotificationManagerId { get; set; }

    /// <summary>
    ///     Mã file đính kèm
    /// </summary>
    public Guid FileId { get; set; }

    /// <summary>
    ///     Cập nhật
    /// </summary>
    /// <param name="notificationManagerId">Mã thông báo</param>
    /// <param name="fileId">Mã file</param>
    /// <param name="createBy">Người tạo</param>
    public void Update(Guid notificationManagerId, Guid fileId, string createBy = null)
    {
        NotificationManagerId = notificationManagerId;
        FileId                = fileId;
        Update(createBy);
    }
}