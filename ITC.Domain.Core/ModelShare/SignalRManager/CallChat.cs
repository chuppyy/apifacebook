using System;

namespace ITC.Domain.Core.ModelShare.SignalRManager;

public class CallChat
{
    public Guid   Id         { get; set; }
    public Guid   Sender     { get; set; }
    public Guid   Receiveder { get; set; }
    public string Content    { get; set; }
}