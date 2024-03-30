using System;
using System.Collections.Generic;
using NCore.Modals;

namespace ITC.Domain.Core.ModelShare.NewsManagers.NewsContentManagers;

/// <summary>
///     [Model] Bài viết
/// </summary>
public class NewsContentEventModel : PublishModal
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
    ///     Thay thế từ khóa cấm
    /// </summary>
    public bool IsMinusWord { get; set; }

    public bool   AgreeVia { get; set; }
    public string LinkTree { get; set; }
    /// <summary>
    ///     Mã bí mật
    /// </summary>
    public string SecretKey { get; set; }

    /// <summary>
    ///     Danh sách file đính kèm
    /// </summary>
    public List<NewsContentAttackModel> NewsContentAttackModels { get; set; }

    /// <summary>
    ///     Cấu trúc bài viết
    /// </summary>
    public List<NewsContentContentModel> NewsContentContentModels { get; set; }

    public string AvatarLink { get; set; }
}

public class NewsContentGetByIdModel : NewsContentEventModel
{
    /// <summary>
    ///     Hình ảnh là ở local hoặc link khác
    /// </summary>
    public bool IsLocal { get; set; }

    public string Link { get; set; }
}

public class NewsContentAttackModel
{
    /// <summary>
    ///     Mã NewsAttack
    /// </summary>
    public Guid Id { get; set; }

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
}

public class NewsContentContentModel
{
    public Guid   Id             { get; set; }
    public string Name           { get; set; }
    public string LinkSource     { get; set; }
    public string LinkFull       { get; set; }
    public string Content        { get; set; }
    public int    Config         { get; set; }
    public int    ContentFlagKey { get; set; }
    public int    Position       { get; set; }
}

/// <summary>
///     [Modal] Phân trang bài viết
/// </summary>
public class NewsContentPagingModel : PagingModel
{
    // /// <summary>
    // /// Loại bài viết
    // /// </summary>
    // public Guid NewsGroupTypeId { get; set; }

    public Guid Author { get; set; }

    /// <summary>
    ///     Nhóm bài viết
    /// </summary>
    public string NewsGroupId { get; set; }
}