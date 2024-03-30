using System;

namespace ITC.Domain.Core.ModelShare.SystemManagers.NotificationManagers;

/// <summary>
///     Trả dữ liệu thông báo chạy slide cho FE
/// </summary>
public class NotificationManagerSlideRunModel
{
    public Guid   Id      { get; set; }
    public string Name    { get; set; }
    public string Content { get; set; }
}

/// <summary>
///     Trả dữ liệu thông báo của người dùng cho FE
/// </summary>
public class NotificationManagerSendUserModel : NotificationManagerSlideRunModel
{
    public DateTime DateSend { get; set; }
    public bool     IsRead   { get; set; }
}