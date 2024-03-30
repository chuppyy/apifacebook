#region

using System;
using System.Collections.Generic;
using System.Linq;
using ITC.Domain.Core.Events;
using ITC.Domain.Extensions;
using ITC.Domain.Interfaces;
using ITC.Infra.Data.Context;

#endregion

namespace ITC.Infra.Data.Repositories;

public class EventStoreSqlRepository : IEventStoreRepository
{
#region Fields

    private readonly EventStoreSqlContext _context;

#endregion

#region Constructors

    public EventStoreSqlRepository(EventStoreSqlContext context)
    {
        _context = context;
    }

#endregion

#region Methods

    private bool CheckExist(IList<StoredEventType> types, StoredEventType type)
    {
        foreach (var item in types)
            if (type == item)
                return true;
        return false;
    }

#endregion

#region IEventStoreRepository Members

    public IList<StoredEvent> All(Guid aggregateId)
    {
        return (from e in _context.StoredEvents where e.AggregateId == aggregateId select e).ToList();
    }

    public void Store(StoredEvent theEvent)
    {
        _context.StoredEvents.Add(theEvent);
        _context.SaveChanges();
    }

    /// <summary>
    ///     Lấy dữ liệu
    /// </summary>
    /// <param name="startIndex">Bắt đầu từ phần tử</param>
    /// <param name="numberItems">Số phần tử được lựa chọn</param>
    /// <param name="startTime">Thời gian bắt đầu</param>
    /// <param name="endTime">Thời gian kết thúc</param>
    /// <param name="types">Các kiểu sự kiện</param>
    /// <param name="portalId">Mã định danh Portal</param>
    /// <param name="mUId">Mã định danh đơn vị quản lý</param>
    /// <param name="sourceId">Mã định danh của đối tượng tác động</param>
    /// <param name="targetId">Mã định danh của đối tượng bị tác động</param>
    /// <param name="messageType">Tên sự kiện</param>
    /// <returns></returns>
    public IList<StoredEvent> GetStoreds(int       startIndex = 0,    int numberItems = 20, DateTime? startTime = null,
                                         DateTime? endTime    = null, IList<StoredEventType> types = null,
                                         int?      portalId   = null, string mUId = null, string sourceId = null,
                                         string    targetId   = null, string messageType = null, string query = null,
                                         bool      timeDes    = true, bool isExport = false, bool all = false,
                                         string    role       = "")
    {
        var oderItems = timeDes
                            ? _context.StoredEvents.OrderByDescending(x => x.Timestamp)
                            : _context.StoredEvents.OrderBy(x => x.Timestamp);
        if (!isExport)
            return oderItems.Select(y => new StoredEvent(y.EventType, y.SourceId, y.SourceName,
                                                         y.TargetId, y.TargetName, y.MUId, y.PortalId)
                                        { Timestamp = y.Timestamp, MessageType = y.MessageType }).AsEnumerable()
                            .Where(x => (startTime == null || x.Timestamp >= startTime)
                                        && (endTime == null || x.Timestamp <= endTime)
                                        && (role != RoleIdentity.Administrator
                                                ? (portalId == null || x.PortalId == portalId) && portalId != 1
                                                : x.PortalId >= 0)
                                        && (all == false && role != RoleIdentity.Administrator
                                                ? mUId == null || x.MUId == mUId
                                                : mUId != string.Empty)
                                        && (sourceId == null || x.SourceId == sourceId)
                                        && (targetId == null || x.TargetId == targetId)
                                        && (types    == null || types.Count == 0 || types.Contains(x.EventType))
                                        && (string.IsNullOrWhiteSpace(messageType) || x.MessageType == messageType)
                                        && (string.IsNullOrWhiteSpace(query)                      ||
                                            x.SourceName?.ToLower().IndexOf(query.ToLower()) > -1 ||
                                            x.TargetName?.ToLower().IndexOf(query.ToLower()) > -1))
                            .Skip(startIndex).Take(numberItems).ToList();
        return oderItems.Select(y => new StoredEvent(y.EventType, y.SourceId, y.SourceName,
                                                     y.TargetId, y.TargetName, y.MUId, y.PortalId)
                                    { Timestamp = y.Timestamp, MessageType = y.MessageType }).AsEnumerable()
                        .Where(x => (startTime == null || x.Timestamp >= startTime)
                                    && (endTime == null || x.Timestamp <= endTime)
                                    && (role != RoleIdentity.Administrator
                                            ? (portalId == null || x.PortalId == portalId) && portalId != 1
                                            : x.PortalId >= 0)
                                    && (all == false && role != RoleIdentity.Administrator
                                            ? mUId == null || x.MUId == mUId
                                            : mUId != string.Empty)
                                    && (sourceId == null || x.SourceId == sourceId)
                                    && (targetId == null || x.TargetId == targetId)
                                    && (types    == null || types.Count == 0 || types.Contains(x.EventType))
                                    && (string.IsNullOrWhiteSpace(messageType) || x.MessageType == messageType)
                                    && (string.IsNullOrWhiteSpace(query)                      ||
                                        x.SourceName?.ToLower().IndexOf(query.ToLower()) > -1 ||
                                        x.TargetName?.ToLower().IndexOf(query.ToLower()) > -1))
                        .Take(numberItems).ToList();
    }

    public void Dispose()
    {
        _context.Dispose();
    }

#endregion
}