using System;
using System.Collections.Generic;
using ITC.Domain.Core.Models;

namespace ITC.Domain.Models.SystemManagers;

/// <summary>
///     Thông báo hệ thống
/// </summary>
public class NotificationManager : RootModel
{
    /// <summary>
    ///     Hàm dựng mặc định
    /// </summary>
    protected NotificationManager()
    {
    }

    /// <summary>
    ///     Hàm dựng có tham số
    /// </summary>
    /// <param name="id">Mã thông báo</param>
    /// <param name="name">Tên thông báo</param>
    /// <param name="content">Nội dung thông báo</param>
    /// <param name="fileAttackId">Mã file đính kèm</param>
    /// <param name="statusId">Trạng thái</param>
    /// <param name="isSendAll">Gửi cho tất cả mọi người</param>
    /// <param name="isRun">Chạy slide thông báo </param>
    /// <param name="projectId">Mã dự án</param>
    /// <param name="managementId">Mã đơn vị quản lý</param>
    /// <param name="position">Vị trí</param>
    /// <param name="dateStart">Thời gian bắt đầu</param>
    /// <param name="dateEnd">Thời gian kết thúc</param>
    /// <param name="isLimitedTime">Hiển thị theo giới hạn thời gian</param>
    /// <param name="isShowMain">Hiển thị trên trang chủ</param>
    /// <param name="createBy">Người tạo</param>
    public NotificationManager(Guid     id,        string   name,    string content, Guid fileAttackId, int statusId,
                               bool     isSendAll, bool     isRun,   Guid   projectId, Guid managementId, int position,
                               DateTime dateStart, DateTime dateEnd, bool   isLimitedTime, bool isShowMain,
                               string   createBy = null) :
        base(id, createBy)
    {
        NotificationUserManagers   = new List<NotificationUserManager>();
        NotificationAttackManagers = new List<NotificationAttackManager>();
        Update(name,      content, fileAttackId,  statusId,   isSendAll, isRun, projectId, managementId, position,
               dateStart, dateEnd, isLimitedTime, isShowMain, createBy);
    }

    /// <summary>
    ///     Tên thông báo
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Nội dung thông báo
    /// </summary>
    public string Content { get; set; }

    /// <summary>
    ///     Mã file đính kèm
    /// </summary>
    public Guid FileAttackId { get; set; }

    /// <summary>
    ///     Gửi cho tất cả mọi người
    /// </summary>
    public bool IsSendAll { get; set; }

    /// <summary>
    ///     Chạy thông báo
    /// </summary>
    public bool IsRun { get; set; }

    /// <summary>
    ///     Mã dự án
    /// </summary>
    public Guid ProjectId { get; set; }

    /// <summary>
    ///     Mã đơn vị quản lý
    /// </summary>
    public Guid ManagementId { get; set; }

    /// <summary>
    ///     Vị trí
    /// </summary>
    public int Position { get; set; }

    /// <summary>
    ///     Thời gian bắt đầu
    /// </summary>
    public DateTime DateStart { get; set; }

    /// <summary>
    ///     Thời gian kết thúc
    /// </summary>
    public DateTime DateEnd { get; set; }

    /// <summary>
    ///     Hiển thị trong giới hạn thời gian
    /// </summary>
    public bool IsLimitedTime { get; set; }

    /// <summary>
    ///     Hiển thị trên trang chủ (Mục văn bản - thông báo)
    /// </summary>
    public bool IsShowMain { get; set; }

    /// <summary>
    ///     Liên kết khóa ngoại
    /// </summary>
    public virtual List<NotificationUserManager> NotificationUserManagers { get; set; }

    /// <summary>
    ///     Liên kết khóa ngoại
    /// </summary>
    public virtual List<NotificationAttackManager> NotificationAttackManagers { get; set; }

    /// <summary>
    ///     Cập nhật
    /// </summary>
    /// <param name="name">Tên thông báo</param>
    /// <param name="content">Nội dung thông báo</param>
    /// <param name="fileAttackId">Mã file đính kèm</param>
    /// <param name="statusId">Trạng thái</param>
    /// <param name="isSendAll">Gửi cho tất cả mọi người</param>
    /// <param name="isRun">Chạy slide thông báo </param>
    /// <param name="projectId">Mã dự án</param>
    /// <param name="managementId">Mã đơn vị quản lý</param>
    /// <param name="position">Vị trí</param>
    /// <param name="dateStart">Thời gian bắt đầu</param>
    /// <param name="dateEnd">Thời gian kết thúc</param>
    /// <param name="isLimitedTime">Hiển thị theo giới hạn thời gian</param>
    /// <param name="isShowMain">Hiển thị trên trang chủ</param>
    /// <param name="createBy">Người tạo</param>
    public void Update(string   name,    string content,       Guid fileAttackId, int    statusId, bool     isSendAll,
                       bool     isRun,   Guid   projectId,     Guid managementId, int    position, DateTime dateStart,
                       DateTime dateEnd, bool   isLimitedTime, bool isShowMain,   string createBy = null)
    {
        Name          = name;
        Content       = content;
        FileAttackId  = fileAttackId;
        StatusId      = statusId;
        IsSendAll     = isSendAll;
        IsRun         = isRun;
        ProjectId     = projectId;
        ManagementId  = managementId;
        Position      = position;
        DateStart     = dateStart;
        DateEnd       = dateEnd;
        IsLimitedTime = isLimitedTime;
        Update(createBy);
    }

    /// <summary>
    ///     Thêm file đính kèm
    /// </summary>
    /// <param name="model"></param>
    /// <param name="userCreated"></param>
    public void AddAttack(List<Guid> model, string userCreated)
    {
        foreach (var items in model)
            NotificationAttackManagers.Add(new NotificationAttackManager(Guid.NewGuid(), items, userCreated));
    }

    /// <summary>
    ///     Thêm người dùng nhận thông báo
    /// </summary>
    /// <param name="model"></param>
    /// <param name="userCreated"></param>
    public void AddUser(List<Guid> model, string userCreated)
    {
        foreach (var items in model)
            NotificationUserManagers.Add(
                new NotificationUserManager(Guid.NewGuid(), false, items, null, userCreated));
    }
}