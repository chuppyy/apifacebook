#region

using System;
using ITC.Domain.Core.Commands;

#endregion

namespace ITC.Domain.Commands.NewsManagers.NewsContentManagers;

/// <summary>
///     Command NewsContentCommand
/// </summary>
public abstract class NewsContentCommand : Command
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

    public bool   AgreeVia { get; set; }
    public string LinkTree { get; set; }
    public string AvatarLink { get; set; }
    /// <summary>
    ///     Mã bí mật
    /// </summary>
    public string SecretKey { get; set; }
}