using ITC.Domain.Core.ModelShare.SystemManagers.NotificationManagers;

namespace ITC.Domain.Commands.SystemManagers.NotificationManagers;

/// <summary>
///     Command cập nhật trạng thái đã đọc thông báo hệ thống
/// </summary>
public class UpdateReadNotificationManagerCommand : NotificationManagerCommand
{
#region Constructors

    /// <summary>
    ///     Hàm dựng với tham số
    /// </summary>
    public UpdateReadNotificationManagerCommand(NotificationManagerEventModel model)
    {
        Id = model.Id;
    }

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