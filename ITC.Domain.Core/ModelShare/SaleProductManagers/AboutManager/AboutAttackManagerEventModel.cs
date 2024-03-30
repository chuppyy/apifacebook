using System;
using NCore.Modals;

namespace ITC.Domain.Core.ModelShare.SaleProductManagers.AboutManager;

/// <summary>
///     [Model] nhận dữ liệu giới thiệu - hình ảnh từ FE
/// </summary>
public class AboutAttackManagerEventModel : PublishModal
{
    public Guid Id { get; set; }

    /// <summary>
    ///     Tên
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Nội dung
    /// </summary>
    public string Content { get; set; }

    /// <summary>
    ///     Ảnh đại diện
    /// </summary>
    public Guid AvatarId { get; set; }

    /// <summary>
    ///     Mã giới thiệu
    /// </summary>
    public Guid AboutManagerId { get; set; }
}