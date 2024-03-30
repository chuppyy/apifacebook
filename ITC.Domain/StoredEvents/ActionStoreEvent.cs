using System;
using ITC.Domain.Core.Events;
using ITC.Domain.Core.ModelShare.SystemManagers.SystemLogManagers;

namespace ITC.Domain.StoredEvents;

public class ActionStoreEvent
{
    /// <summary>
    ///     Event thêm mới
    /// </summary>
    public class RunActionStoreEvent : SystemLogEventModel
    {
    #region Constructors

        public RunActionStoreEvent(int    systemLogType,
                                   string nameFile,
                                   string description,
                                   Guid?  dataId,
                                   string dataOld,
                                   string dataNew,
                                   string userId,
                                   string userName) :
            base(systemLogType, nameFile, description, dataId, dataOld, dataNew, userId, userName)
        {
        }

    #endregion
    }

    /// <summary>
    ///     Event thêm mới
    /// </summary>
    public class AddActionStoreEvent : StoredEvent
    {
    #region Constructors

        public AddActionStoreEvent(StoredEventType eventType,
                                   string          sourceId,
                                   string          sourceName,
                                   string          targetId,
                                   string          targetName,
                                   string          mUId,
                                   int             portalId) : base(eventType, sourceId,
                                                        sourceName, targetId, targetName, mUId,
                                                        portalId)
        {
        }

    #endregion
    }


    /// <summary>
    ///     Event xóa
    /// </summary>
    public class DeleteActionStoreEvent : StoredEvent
    {
    #region Constructors

        public DeleteActionStoreEvent(StoredEventType eventType,
                                      string          sourceId,
                                      string          sourceName,
                                      string          targetId,
                                      string          targetName,
                                      string          mUId,
                                      int             portalId) :
            base(eventType, sourceId, sourceName, targetId, targetName, mUId, portalId)
        {
        }

    #endregion
    }

    public class UpdateActionStoreEvent : StoredEvent
    {
    #region Constructors

        public UpdateActionStoreEvent(StoredEventType eventType,
                                      string          sourceId,
                                      string          sourceName,
                                      string          targetId,
                                      string          targetName,
                                      string          mUId,
                                      int             portalId) :
            base(eventType, sourceId, sourceName, targetId, targetName, mUId, portalId)
        {
        }

    #endregion
    }

    /// <summary>
    ///     Event khôi phục
    /// </summary>
    public class RecoverActionStoreEvent : StoredEvent
    {
    #region Constructors

        public RecoverActionStoreEvent(StoredEventType eventType,
                                       string          sourceId,
                                       string          sourceName,
                                       string          targetId,
                                       string          targetName,
                                       string          mUId,
                                       int             portalId) :
            base(eventType, sourceId, sourceName, targetId, targetName, mUId, portalId)
        {
        }

    #endregion
    }

    /// <summary>
    ///     Event xóa vĩnh viễn
    /// </summary>
    public class PermanentDeleteActionStoreEvent : StoredEvent
    {
    #region Constructors

        public PermanentDeleteActionStoreEvent(StoredEventType eventType,
                                               string          sourceId,
                                               string          sourceName,
                                               string          targetId,
                                               string          targetName,
                                               string          mUId,
                                               int             portalId) :
            base(eventType, sourceId, sourceName, targetId, targetName, mUId, portalId)
        {
        }

    #endregion
    }
}