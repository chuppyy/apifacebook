#region

using ITC.Domain.Core.Events;

#endregion

namespace ITC.Domain.StoredEvents.Account;

public class PersonalAccountAddEvent : StoredEvent
{
#region Constructors

    public PersonalAccountAddEvent(StoredEventType eventType,  string sourceId, string sourceName, string targetId,
                                   string          targetName, string mUId,     int    portalId)
        : base(eventType, sourceId, sourceName, targetId, targetName, mUId, portalId)
    {
    }

#endregion
}

public class PersonalAccountUpdatedEvent : StoredEvent
{
#region Constructors

    public PersonalAccountUpdatedEvent(StoredEventType eventType, string sourceId,   string sourceName,
                                       string          targetId,  string targetName, string mUId, int portalId)
        : base(eventType, sourceId, sourceName, targetId, targetName, mUId, portalId)
    {
    }

#endregion
}

public class PersonalAccountRemovedEvent : StoredEvent
{
#region Constructors

    public PersonalAccountRemovedEvent(StoredEventType eventType, string sourceId,   string sourceName,
                                       string          targetId,  string targetName, string mUId, int portalId)
        : base(eventType, sourceId, sourceName, targetId, targetName, mUId, portalId)
    {
    }

#endregion
}

public class PersonalAccountRecoverEvent : StoredEvent
{
#region Constructors

    public PersonalAccountRecoverEvent(StoredEventType eventType, string sourceId,   string sourceName,
                                       string          targetId,  string targetName, string mUId, int portalId)
        : base(eventType, sourceId, sourceName, targetId, targetName, mUId, portalId)
    {
    }

#endregion
}

public class PersonalAccountPermanentDeleteEvent : StoredEvent
{
#region Constructors

    public PersonalAccountPermanentDeleteEvent(StoredEventType eventType, string sourceId,   string sourceName,
                                               string          targetId,  string targetName, string mUId, int portalId)
        : base(eventType, sourceId, sourceName, targetId, targetName, mUId, portalId)
    {
    }

#endregion
}

public class PersonalAccountPermanentAvatarManagerEvent : StoredEvent
{
#region Constructors

    public PersonalAccountPermanentAvatarManagerEvent(StoredEventType eventType, string sourceId, string sourceName,
                                                      string targetId, string targetName, string mUId, int portalId)
        : base(eventType, sourceId, sourceName, targetId, targetName, mUId, portalId)
    {
    }

#endregion
}