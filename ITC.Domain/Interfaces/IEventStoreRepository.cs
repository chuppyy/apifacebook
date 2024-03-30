#region

using System;
using System.Collections.Generic;
using ITC.Domain.Core.Events;

#endregion

namespace ITC.Domain.Interfaces;

public interface IEventStoreRepository : IDisposable
{
#region Methods

    IList<StoredEvent> All(Guid aggregateId);

    IList<StoredEvent> GetStoreds(int                    startIndex  = 0,
                                  int                    numberItems = 20,
                                  DateTime?              startTime   = null,
                                  DateTime?              endTime     = null,
                                  IList<StoredEventType> types       = null,
                                  int?                   portalId    = null,
                                  string                 mUId        = null,
                                  string                 sourceId    = null,
                                  string                 targetId    = null,
                                  string                 messageType = null,
                                  string                 query       = null,
                                  bool                   timeDes     = true,
                                  bool                   isExport    = false,
                                  bool                   all         = false,
                                  string                 role        = "");

    void Store(StoredEvent theEvent);

#endregion
}