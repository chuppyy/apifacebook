using System;
using NCore.Modals;

namespace ITC.Domain.Core.ModelShare.SaleProductManagers.SlideManager;

/// <summary>
///     [Model] nhận dữ liệu slide từ FE
/// </summary>
public class SlideManagerEventModel : PublishModal
{
    public Guid Id { get; set; }

    /// <summary>
    ///     Tên slide
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Mô tả slide
    /// </summary>
    public string Content { get; set; }

    /// <summary>
    ///     Link website
    /// </summary>
    public string UrlLink { get; set; }

    /// <summary>
    ///     Ảnh đại diện
    /// </summary>
    public Guid AvatarId { get; set; }

    /// <summary>
    ///     Loại hiển thị
    /// </summary>
    public int TypeViewId { get; set; }

    /// <summary>
    ///     Loại dữ liệu bài viết
    /// </summary>
    public int UrlType { get; set; }
}

/// <summary>
///     [Model] Trả về SlideManagerGetByIdModel
/// </summary>
public class SlideManagerGetByIdModel : SlideManagerEventModel
{
    public bool IsLocal { get; set; }
}