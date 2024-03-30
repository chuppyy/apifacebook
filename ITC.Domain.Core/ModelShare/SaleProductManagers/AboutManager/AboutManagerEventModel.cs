using System;
using System.Collections.Generic;
using NCore.Modals;

namespace ITC.Domain.Core.ModelShare.SaleProductManagers.AboutManager;

/// <summary>
///     [Model] nhận dữ liệu giới thiệu từ FE
/// </summary>
public class AboutManagerEventModel : PublishModal
{
    public Guid Id { get; set; }

    /// <summary>
    ///     Tên giới thiệu
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Loại dữ liệu giới thiệu
    /// </summary>
    public int TypeId { get; set; }

    /// <summary>
    ///     Đường dẫn hiển thị trên trang chủ - dùng chủ yếu trong việc chèn bài viết
    /// </summary>
    public string MetaLink { get; set; }
}

/// <summary>
///     [Model] nhận dữ liệu giới thiệu từ FE
/// </summary>
public class AboutManagerEventContentModel : PublishModal
{
    public Guid Id { get; set; }

    /// <summary>
    ///     Nội dung giới thiệu
    /// </summary>
    public string Content { get; set; }

    /// <summary>
    ///     Từ khóa SEO
    /// </summary>
    public string SeoKeyword { get; set; }

    /// <summary>
    ///     Tóm tắt
    /// </summary>
    public string Summary { get; set; }

    /// <summary>
    ///     Ảnh đại diện
    /// </summary>
    public Guid AvatarId { get; set; }
}

/// <summary>
///     [Model] Trả về AboutManagerGetByIdModel
/// </summary>
public class AboutManagerGetByIdModel : AboutManagerEventContentModel
{
    public string                 Name              { get; set; }
    public bool                   IsLocal           { get; set; }
    public List<ImageAttackModel> ImageAttackModels { get; set; }
}

public class ImageAttackModel
{
    public string Name     { get; set; }
    public string Content  { get; set; }
    public bool   IsLocal  { get; set; }
    public string FilePath { get; set; }
    public string LinkUrl  { get; set; }
}