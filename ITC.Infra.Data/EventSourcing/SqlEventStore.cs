#region

using System;
using ITC.Domain.Core.Events;
using ITC.Domain.Core.ModelShare.SystemManagers.SystemLogManagers;
using ITC.Domain.Interfaces;
using ITC.Domain.Interfaces.SystemManagers.SystemLogs;

#endregion

namespace ITC.Infra.Data.EventSourcing;

public class SqlEventStore : IEventStore
{
#region Constructors

    public SqlEventStore(IEventStoreRepository eventStoreRepository,
                         ISystemLogRepository  systemLogRepository)
    {
        _eventStoreRepository = eventStoreRepository;
        _systemLogRepository  = systemLogRepository;
    }

#endregion

#region Fields

    private readonly IEventStoreRepository _eventStoreRepository;
    private readonly ISystemLogRepository  _systemLogRepository;

#endregion

#region IEventStore Members

    public void Save<T>(T storedEvent) where T : StoredEvent
    {
        _eventStoreRepository.Store(storedEvent);
    }

    public void SaveSystemLog<T>(T theEvent) where T : SystemLogEventModel
    {
        _systemLogRepository.Store(new SystemLog(Guid.NewGuid(),
                                                 DateTime.Now,
                                                 theEvent.SystemLogType,
                                                 theEvent.UserCreateId,
                                                 theEvent.UserCreateName,
                                                 theEvent.NameFile,
                                                 theEvent.Description,
                                                 theEvent.DataId,
                                                 theEvent.DataOld,
                                                 theEvent.DataNew,
                                                 theEvent.UserCreateId));
    }

#endregion
}