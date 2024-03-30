﻿using System;
using System.Collections.Generic;
using NCore.Modals;

namespace ITC.Domain.Core.ModelShare.SystemManagers.NotificationManagers;

public class NotificationManagerEventModel : PublishModal
{
    public Guid Id { get; set; }

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
    public List<Guid> FileAttackModel { get; set; }

    /// <summary>
    ///     Danh sách ID người dùng gửi thông báo
    /// </summary>
    public List<Guid> UserModels { get; set; }

    /// <summary>
    ///     Gửi cho tất cả mọi người
    /// </summary>
    public bool IsSendAll { get; set; }

    /// <summary>
    ///     Chạy thông báo
    /// </summary>
    public bool IsRun { get; set; }

    /// <summary>
    ///     Thời gian bắt đầu
    /// </summary>
    public string DateStart { get; set; }

    /// <summary>
    ///     Thời gian kết thúc
    /// </summary>
    public string DateEnd { get; set; }

    /// <summary>
    ///     Hiển thị trong giới hạn thời gian
    /// </summary>
    public bool IsLimitedTime { get; set; }

    /// <summary>
    ///     Hiển thị trên trang chủ (Mục văn bản - thông báo)
    /// </summary>
    public bool IsShowMain { get; set; }
}