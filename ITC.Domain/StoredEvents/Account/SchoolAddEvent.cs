#region

using ITC.Domain.Core.Events;

namespace ITC.Domain.StoredEvents.Account;

#endregion

public class SchoolAddEvent : StoredEvent
{
#region Constructors

    public SchoolAddEvent(StoredEventType eventType,  string sourceId, string sourceName, string targetId,
                          string          targetName, string mUId,     int    portalId)
        : base(eventType, sourceId, sourceName, targetId, targetName, mUId, portalId)
    {
    }

#endregion
}

public class SchoolUpdateEvent : StoredEvent
{
#region Constructors

    public SchoolUpdateEvent(StoredEventType eventType,  string sourceId, string sourceName, string targetId,
                             string          targetName, string mUId,     int    portalId)
        : base(eventType, sourceId, sourceName, targetId, targetName, mUId, portalId)
    {
    }

#endregion
}

public class SchoolRemoveEvent : StoredEvent
{
#region Constructors

    public SchoolRemoveEvent(StoredEventType eventType,  string sourceId, string sourceName, string targetId,
                             string          targetName, string mUId,     int    portalId)
        : base(eventType, sourceId, sourceName, targetId, targetName, mUId, portalId)
    {
    }

#endregion
}