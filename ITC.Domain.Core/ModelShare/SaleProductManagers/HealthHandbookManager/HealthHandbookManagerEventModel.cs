using System;
using NCore.Modals;

namespace ITC.Domain.Core.ModelShare.SaleProductManagers.HealthHandbookManager;

/// <summary>
///     [Model] nhận dữ liệu giới thiệu từ FE
/// </summary>
public class HealthHandbookManagerEventModel : PublishModal
{
    /// <summary>
    ///     Mã dữ liệu
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    ///     Tên bài viết
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Tóm tắt
    /// </summary>
    public string Summary { get; set; }

    /// <summary>
    ///     Nội dung
    /// </summary>
    public string Content { get; set; }

    /// <summary>
    ///     Tác giả
    /// </summary>
    public string Author { get; set; }

    /// <summary>
    ///     Link đường dẫn gốc
    /// </summary>
    public string UrlRootLink { get; set; }

    /// <summary>
    ///     Từ khóa SEO
    /// </summary>
    public string SeoKeyword { get; set; }

    /// <summary>
    ///     Ảnh đại diện
    /// </summary>
    public Guid AvatarId { get; set; }

    /// <summary>
    ///     Ngày viết bài
    /// </summary>
    public string DateTimeStart { get; set; }

    /// <summary>
    ///     Chế độ hiển thị file đính kèm
    /// </summary>
    public int AttackViewId { get; set; }

    /// <summary>
    ///     Trạng thái
    /// </summary>
    public int StatusId { get; set; }
}

public class HealthHandbookManagerGetByIdModel : PublishModal
{
    /// <summary>
    ///     Mã dữ liệu
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    ///     Tên bài viết
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Tóm tắt
    /// </summary>
    public string Summary { get; set; }

    /// <summary>
    ///     Nội dung
    /// </summary>
    public string Content { get; set; }

    /// <summary>
    ///     Tác giả
    /// </summary>
    public string Author { get; set; }

    /// <summary>
    ///     Link đường dẫn gốc
    /// </summary>
    public string UrlRootLink { get; set; }

    /// <summary>
    ///     Nhóm tin
    /// </summary>
    public Guid NewsGroupId { get; set; }

    /// <summary>
    ///     Từ khóa SEO
    /// </summary>
    public string SeoKeyword { get; set; }

    /// <summary>
    ///     Ảnh đại diện
    /// </summary>
    public Guid AvatarId { get; set; }

    /// <summary>
    ///     Ngày viết bài
    /// </summary>
    public string DateTimeStart { get; set; }

    /// <summary>
    ///     Loại dữ liệu
    /// </summary>
    public Guid NewsGroupTypeId { get; set; }

    /// <summary>
    ///     Chế độ hiển thị file đính kèm
    /// </summary>
    public int AttackViewId { get; set; }

    /// <summary>
    ///     Trạng thái
    /// </summary>
    public int StatusId { get; set; }

    /// <summary>
    ///     Hình ảnh là ở local hoặc link khác
    /// </summary>
    public bool IsLocal { get; set; }
}