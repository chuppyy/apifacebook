using System;
using System.Collections.Generic;
using NCore.Modals;

namespace ITC.Domain.Core.ModelShare.SaleProductManagers.ImageLibraryDetailManager;

/// <summary>
///     [Model] nhận dữ liệu slide từ FE
/// </summary>
public class ImageLibraryDetailManagerEventModel : PublishModal
{
    public Guid Id { get; set; }

    /// <summary>
    ///     Tên
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Mô tả
    /// </summary>
    public string Content { get; set; }

    /// <summary>
    ///     Ảnh đại diện
    /// </summary>
    public Guid AvatarId { get; set; }

    /// <summary>
    ///     Mã quản lý thư viện
    /// </summary>
    public Guid ImageId { get; set; }

    /// <summary>
    ///     Danh sách dữ liệu
    /// </summary>
    public List<ImageLibraryDetailManagerContentModel> ContentModels { get; set; }
}

/// <summary>
///     [Model] Trả về ImageLibraryDetailManagerGetByIdModel
/// </summary>
public class ImageLibraryDetailManagerGetByIdModel : ImageLibraryDetailManagerContentModel
{
    public bool   IsLocal  { get; set; }
    public string FilePath { get; set; }
    public string LinkUrl  { get; set; }
    public Guid   AvatarId { get; set; }
}

public class ImageLibraryDetailManagerContentModel
{
    public Guid   Id      { get; set; }
    public string Name    { get; set; }
    public string Content { get; set; }
}