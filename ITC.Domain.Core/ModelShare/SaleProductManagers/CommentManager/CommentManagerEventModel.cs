using System;
using NCore.Modals;

namespace ITC.Domain.Core.ModelShare.SaleProductManagers.CommentManager;

/// <summary>
///     [Model] nhận dữ liệu comment
/// </summary>
public class CommentManagerEventModel : PublishModal
{
    public Guid Id { get; set; }

    /// <summary>
    ///     Tên comment
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Mô tả comment
    /// </summary>
    public string Content { get; set; }

    /// <summary>
    ///     Email
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    ///     Mã sản phẩm, bài viết
    /// </summary>
    public string ProductId { get; set; }

    /// <summary>
    ///     Nhóm comment
    /// </summary>
    public int GroupId { get; set; }

    /// <summary>
    ///     Ngày comment
    /// </summary>
    public string ParentId { get; set; }

    /// <summary>
    ///     Số điện thoại
    /// </summary>
    public string Phone { get; set; }

    /// <summary>
    ///     Mã dự án
    /// </summary>
    public Guid ProjectId { get; set; }
}

/// <summary>
///     [Model] Trả về CommentManagerGetByIdModel
/// </summary>
public class CommentManagerGetByIdModel : CommentManagerEventModel
{
    public bool IsLocal { get; set; }
}