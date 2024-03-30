using ITC.Domain.Core.ModelShare.SystemManagers.SystemLogManagers;

namespace ITC.Domain.Core.Events;

public interface IEventStore
{
#region Methods

    void Save<T>(T theEvent) where T : StoredEvent;

    void SaveSystemLog<T>(T theEvent) where T : SystemLogEventModel;

#endregion
}