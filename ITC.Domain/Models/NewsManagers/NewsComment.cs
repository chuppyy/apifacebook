using System;
using ITC.Domain.Core.Models;
using NCore.Actions;

namespace ITC.Domain.Models.NewsManagers;

/// <summary>
///     Comment trong bài viết
/// </summary>
public class NewsComment : RootModel
{
    /// <summary>
    ///     Hàm dựng có tham số
    /// </summary>
    /// <param name="id"></param>
    /// <param name="newsContentId">Mã bài viết</param>
    /// <param name="parentId">Mã cha con</param>
    /// <param name="staffId">Người bình luận</param>
    /// <param name="content">Nội dung bình luận</param>
    /// <param name="createdBy">Người tạo</param>
    public NewsComment(Guid   id, Guid newsContentId, Guid parentId, Guid staffId, string content,
                       string createdBy = null)
        : base(id, createdBy)
    {
        StatusId = ActionStatusEnum.Active.Id;
        Update(newsContentId, parentId, 1, 2, staffId, content, DateTime.Now, 0);
    }

    /// <summary>
    ///     Hàm dựng mặc định
    /// </summary>
    protected NewsComment()
    {
    }

    /// <summary>
    ///     Mã bài viết
    /// </summary>
    public Guid NewsContentId { get; set; }

    /// <summary>
    ///     Mã cha con
    /// </summary>
    public Guid ParentId { get; set; }

    /// <summary>
    ///     Giá trị trái
    /// </summary>
    public int PLeft { get; set; }

    /// <summary>
    ///     Giá trị phải
    /// </summary>
    public int PRight { get; set; }

    /// <summary>
    ///     Mã người bình luận
    /// </summary>
    public Guid StaffId { get; set; }

    /// <summary>
    ///     Nội dung bình luận
    /// </summary>
    public string Content { get; set; }

    /// <summary>
    ///     Thời gian bình luận
    /// </summary>
    public DateTime CommentDateTime { get; set; }

    /// <summary>
    ///     Sao đánh giá
    /// </summary>
    public int NumberOfStar { get; set; }

    /// <summary>
    ///     Cập nhật
    /// </summary>
    /// <param name="newsContentId">Mã bài viết</param>
    /// <param name="parentId">Mã cha con</param>
    /// <param name="pLeft">Giá trị trái</param>
    /// <param name="pRight">Giá trị phải</param>
    /// <param name="staffId">Người bình luận</param>
    /// <param name="content">Nội dung bình luận</param>
    /// <param name="commentDateTime">Thời gian bình luận</param>
    /// <param name="numberOfStar">Số lượt đánh giá</param>
    /// <param name="createdBy">Người tạo</param>
    public void Update(Guid     newsContentId,   Guid parentId,     int pLeft, int pRight, Guid staffId, string content,
                       DateTime commentDateTime, int  numberOfStar, string createdBy = null)
    {
        NewsContentId   = newsContentId;
        ParentId        = parentId;
        PLeft           = pLeft;
        PRight          = pRight;
        StaffId         = staffId;
        Content         = content;
        CommentDateTime = commentDateTime;
        NumberOfStar    = numberOfStar;
        Update(createdBy);
    }
}