#region

using System;
using ITC.Domain.Core.Events;

#endregion

namespace ITC.Domain.Core.Notifications;

public class DomainNotification : Event
{
#region Constructors

    public DomainNotification(string key, string value)
    {
        DomainNotificationId = Guid.NewGuid();
        Version              = 1;
        Key                  = key;
        Value                = value;
    }

#endregion

#region Properties

    public Guid   DomainNotificationId { get; }
    public string Key                  { get; }
    public string Value                { get; }
    public int    Version              { get; }

#endregion
}