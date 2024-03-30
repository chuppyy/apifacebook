#region

using System;
using System.Collections.Generic;

#endregion

namespace ITC.Domain.Commands.SystemManagers.NotificationManagers;

/// <summary>
///     Command xóa thông báo hệ thống
/// </summary>
public class DeleteNotificationManagerCommand : NotificationManagerCommand
{
#region Constructors

    /// <summary>
    ///     Hàm dựng với tham số
    /// </summary>
    /// <param name="model">Danh sách id cần xóa</param>
    public DeleteNotificationManagerCommand(List<Guid> model)
    {
        Model = model;
    }

#endregion

#region Properties

    /// <summary>
    ///     Id cần xóa
    /// </summary>
    public List<Guid> Model { get; set; }

#endregion

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