#region

using ITC.Domain.Core.Events;

#endregion

namespace ITC.Domain.StoredEvents.Account;

public class AccountAddEmailEvent : StoredEvent
{
#region Constructors

    public AccountAddEmailEvent(StoredEventType eventType,  string sourceId, string sourceName, string targetId,
                                string          targetName, string mUId,     int    portalId)
        : base(eventType, sourceId, sourceName, targetId, targetName, mUId, portalId)
    {
    }

#endregion
}