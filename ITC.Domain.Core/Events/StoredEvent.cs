#region

using System;

#endregion

namespace ITC.Domain.Core.Events;

public class StoredEvent : Event
{
#region Constructors

    public StoredEvent(StoredEventType eventType,  string sourceId, string sourceName, string targetId,
                       string          targetName, string mUId,     int    portalId)
    {
        Id         = Guid.NewGuid().ToString();
        EventType  = eventType;
        SourceId   = sourceId;
        SourceName = sourceName;
        TargetId   = targetId;
        TargetName = targetName;
        MUId       = mUId;
        PortalId   = portalId;
    }

    // EF Constructor
    protected StoredEvent()
    {
    }

#endregion

#region Properties

    /// <summary>
    ///     Kiểu sự kiện
    /// </summary>
    public StoredEventType EventType { get; }

    public string Id { get; }

    /// <summary>
    ///     Mã định danh đơn vị quản lý
    /// </summary>
    public string MUId { get; }

    /// <summary>
    ///     Mã định danh của Portal
    /// </summary>
    public int PortalId { get; set; }

    /// <summary>
    ///     Mã định danh đối tượng tác động
    /// </summary>
    public string SourceId { get; }

    /// <summary>
    ///     Tên đối tượng tác động
    /// </summary>
    public string SourceName { get; }

    /// <summary>
    ///     Mã định danh đối tượng bị tác động
    /// </summary>
    public string TargetId { get; }

    /// <summary>
    ///     Tên đối tượng bị tác động
    /// </summary>
    public string TargetName { get; }

#endregion
}

/// <summary>
///     Các kiểu tác động
/// </summary>
public enum StoredEventType
{
    /// <summary>
    ///     Truy cập dữ liệu/hệ thống
    /// </summary>
    Access = 1,

    /// <summary>
    ///     Thêm mới tài nguyên
    /// </summary>
    Add = 2,

    /// <summary>
    ///     Cập nhật tài nguyên
    /// </summary>
    Update = 3,

    /// <summary>
    ///     Xóa bỏ tài nguyên
    /// </summary>
    Remove = 4,

    /// <summary>
    ///     Xử lý dữ liệu
    /// </summary>
    Run = 5,

    /// <summary>
    ///     Cập nhật trạng thái
    /// </summary>
    UpdateStatus = 6
}