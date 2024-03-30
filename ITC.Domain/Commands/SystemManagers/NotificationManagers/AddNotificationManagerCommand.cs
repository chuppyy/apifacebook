using System;
using System.Collections.Generic;
using ITC.Domain.Core.ModelShare.SystemManagers.NotificationManagers;

namespace ITC.Domain.Commands.SystemManagers.NotificationManagers;

/// <summary>
///     Command thêm thông báo hệ thống
/// </summary>
public class AddNotificationManagerCommand : NotificationManagerCommand
{
#region Constructors

    /// <summary>
    ///     Hàm dựng với tham số
    /// </summary>
    public AddNotificationManagerCommand(NotificationManagerEventModel model)
    {
        Name             = model.Name;
        CreateBy         = model.CreatedBy;
        Content          = model.Content;
        Name             = model.Name;
        IsRun            = model.IsRun;
        IsSendAll        = model.IsSendAll;
        DateStart        = model.DateStart;
        DateEnd          = model.DateEnd;
        IsLimitedTime    = model.IsLimitedTime;
        FileAttackModels = model.FileAttackModel;
        UserModels       = model.UserModels;
        IsShowMain       = model.IsShowMain;
    }

#endregion

    /// <summary>
    ///     Danh sách ID file đính kèm
    /// </summary>
    public List<Guid> FileAttackModels { get; set; }

    /// <summary>
    ///     Danh sách ID người dùng gửi thông báo
    /// </summary>
    public List<Guid> UserModels { get; set; }

#region Methods

    /// <summary>
    ///     Kiểm tra valid
    /// </summary>
    /// <returns></returns>
    public override bool IsValid()
    {
        return true;
    }

#endregion
}