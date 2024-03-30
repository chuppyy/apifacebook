#region

using System;
using ITC.Domain.Core.Commands;

#endregion

namespace ITC.Domain.Commands.ChatManager;

/// <summary>
///     Command Chat
/// </summary>
public abstract class ChatCommand : Command
{
    public Guid     Id           { get; set; }
    public Guid     Sender       { get; set; }
    public Guid     Receiveder   { get; set; }
    public string   Content      { get; set; }
    public Guid     ProjectId    { get; set; }
    public DateTime SendDateTime { get; set; }
}