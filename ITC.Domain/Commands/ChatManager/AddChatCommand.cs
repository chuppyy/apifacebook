using System;

namespace ITC.Domain.Commands.ChatManager;

/// <summary>
///     Command thêm Chat
/// </summary>
public class AddChatCommand : ChatCommand
{
#region Constructors

    /// <summary>
    ///     Hàm dựng với tham số
    /// </summary>
    public AddChatCommand(Guid sender, Guid receiveder, string content, Guid projectId, string createBy)
    {
        Sender     = sender;
        Receiveder = receiveder;
        Content    = content;
        ProjectId  = projectId;
        CreateBy   = createBy;
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