using System;
using ITC.Domain.Core.Models;

namespace ITC.Domain.Models.HomeManagers;

/// <summary>
///     Menu trang chủ với dữ liệu từ nhóm tin
/// </summary>
public class HomeMenuNewsGroup : RootModel
{
    /// <summary>
    ///     Hàm dựng có tham số
    /// </summary>
    public HomeMenuNewsGroup(Guid   id,
                             Guid   newsGroupId,
                             bool   isViewHomePage,
                             int    historyPosition,
                             string createdBy = null) : base(id, createdBy)
    {
        NewsGroupId     = newsGroupId;
        IsViewHomePage  = isViewHomePage;
        HistoryPosition = historyPosition;
    }

    /// <summary>
    ///     Hàm dựng có tham số
    /// </summary>
    public HomeMenuNewsGroup(Guid   id,
                             Guid   homeMenuId,
                             Guid   newsGroupId,
                             bool   isViewHomePage,
                             int    historyPosition,
                             string createdBy = null) : base(id, createdBy)
    {
        HomeMenuId      = homeMenuId;
        NewsGroupId     = newsGroupId;
        IsViewHomePage  = isViewHomePage;
        HistoryPosition = historyPosition;
    }

    /// <summary>
    ///     Hàm dựng mặc định
    /// </summary>
    protected HomeMenuNewsGroup()
    {
    }

    /// <summary>
    ///     Mã menu trang chủ
    /// </summary>
    public Guid HomeMenuId { get; set; }

    /// <summary>
    ///     Mã nhóm tin
    /// </summary>
    public Guid NewsGroupId { get; set; }

    /// <summary>
    ///     Hiển thị trên trang chủ
    /// </summary>
    public bool IsViewHomePage { get; set; }

    /// <summary>
    ///     Lịch sử vị trí
    /// </summary>
    public int HistoryPosition { get; set; }
}