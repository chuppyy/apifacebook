using System;
using ITC.Domain.Core.Models;

namespace ITC.Domain.Models.HomeManagers;

/// <summary>
///     Danh sách chi tiết các nhóm bài viết sẽ lấy dữ liệu hiển thị trên trang chủ
/// </summary>
public class HomeNewsGroupViewDetail : RootModel
{
    /// <summary>
    ///     Hàm dựng có tham số
    /// </summary>
    public HomeNewsGroupViewDetail(Guid   id,
                                   Guid   newsGroupId,
                                   int    historyPosition,
                                   string createdBy = null) : base(id, createdBy)
    {
        NewsGroupId     = newsGroupId;
        HistoryPosition = historyPosition;
    }

    /// <summary>
    ///     Hàm dựng có tham số
    /// </summary>
    public HomeNewsGroupViewDetail(Guid   id,
                                   Guid   homeNewsGroupView,
                                   Guid   newsGroupId,
                                   int    historyPosition,
                                   string createdBy = null) : base(id, createdBy)
    {
        HomeNewsGroupViewId = homeNewsGroupView;
        NewsGroupId         = newsGroupId;
        HistoryPosition     = historyPosition;
    }

    /// <summary>
    ///     Hàm dựng mặc định
    /// </summary>
    protected HomeNewsGroupViewDetail()
    {
    }

    /// <summary>
    ///     Mã menu trang chủ
    /// </summary>
    public Guid HomeNewsGroupViewId { get; set; }

    /// <summary>
    ///     Mã nhóm tin
    /// </summary>
    public Guid NewsGroupId { get; set; }

    /// <summary>
    ///     Lịch sử vị trí
    /// </summary>
    public int HistoryPosition { get; set; }
}