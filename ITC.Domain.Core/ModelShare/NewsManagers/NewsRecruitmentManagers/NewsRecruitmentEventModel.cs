using System;
using NCore.Modals;

namespace ITC.Domain.Core.ModelShare.NewsManagers.NewsRecruitmentManagers;

/// <summary>
///     [Model] Bài viết
/// </summary>
public class NewsRecruitmentEventModel : PublishModal
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
    public int Type { get; set; }

    /// <summary>
    ///     Trạng thái
    /// </summary>
    public int StatusId { get; set; }
}

public class NewsRecruitmentGetByIdModel : NewsRecruitmentEventModel
{
    /// <summary>
    ///     Hình ảnh là ở local hoặc link khác
    /// </summary>
    public bool IsLocal { get; set; }
}