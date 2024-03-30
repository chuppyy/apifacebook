using System;
using ITC.Domain.Core.Models;

namespace ITC.Domain.Models.NewsManagers;

/// <summary>
///     File đính kèm trong bài viết
/// </summary>
public class NewsAttack : RootModel
{
    /// <summary>
    ///     Hàm dựng có tham số
    /// </summary>
    /// <param name="id"></param>
    /// <param name="newsContentId">Mã bài viết</param>
    /// <param name="fileId">Mã file</param>
    /// <param name="attackDateTime">Thời gian đính kèm</param>
    /// <param name="isDownload">Cho phép tải về</param>
    /// <param name="statusId">Trạng thái</param>
    /// <param name="createdBy">Người tạo</param>
    public NewsAttack(Guid id,       Guid   newsContentId, Guid fileId, DateTime attackDateTime, bool isDownload,
                      int  statusId, string createdBy = null)
        : base(id, createdBy)
    {
        NewsContentId  = newsContentId;
        FileId         = fileId;
        AttackDateTime = attackDateTime;
        IsDownload     = isDownload;
        StatusId       = statusId;
    }

    /// <summary>
    ///     Hàm dựng có tham số
    /// </summary>
    /// <param name="id"></param>
    /// <param name="fileId">Mã file</param>
    /// <param name="attackDateTime">Thời gian đính kèm</param>
    /// <param name="isDownload">Cho phép tải về</param>
    /// <param name="statusId">Trạng thái</param>
    /// <param name="historyPosition">Lịch sử cập nhật</param>
    /// <param name="createdBy">Người tạo</param>
    public NewsAttack(Guid id,              Guid   fileId, DateTime attackDateTime, bool isDownload, int statusId,
                      int  historyPosition, string createdBy = null)
        : base(id, createdBy)
    {
        FileId          = fileId;
        AttackDateTime  = attackDateTime;
        IsDownload      = isDownload;
        StatusId        = statusId;
        HistoryPosition = historyPosition;
    }

    /// <summary>
    ///     Hàm dựng mặc định
    /// </summary>
    protected NewsAttack()
    {
    }

    /// <summary>
    ///     Mã bài viết
    /// </summary>
    public Guid NewsContentId { get; set; }

    /// <summary>
    ///     Mã file
    /// </summary>
    public Guid FileId { get; set; }

    /// <summary>
    ///     Thời gian đính kèm
    /// </summary>
    public DateTime AttackDateTime { get; set; }

    /// <summary>
    ///     Cho phép tải về
    /// </summary>
    public bool IsDownload { get; set; }

    /// <summary>
    ///     Vị trí lịch sử
    /// </summary>
    public int HistoryPosition { get; set; }
}