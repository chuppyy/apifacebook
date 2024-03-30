#region

using ITC.Domain.Core.Events;

#endregion

namespace ITC.Domain.StoredEvents.Account;

public class DepartmentOfEducationRemovedEvent : StoredEvent
{
#region Constructors

    public DepartmentOfEducationRemovedEvent(StoredEventType eventType, string sourceId,   string sourceName,
                                             string          targetId,  string targetName, string mUId, int portalId)
        : base(eventType, sourceId, sourceName, targetId, targetName, mUId, portalId)
    {
    }

#endregion
}