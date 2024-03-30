using System;

namespace ITC.Domain.Core.ModelShare.SaleProductManagers.AboutManager;

/// <summary>
///     Modal phân trang giới thiệu - hình ảnh
/// </summary>
public class AboutAttackManagerPagingDto
{
    public Guid   Id       { get; set; }
    public string Name     { get; set; }
    public string Content  { get; set; }
    public Guid   AvatarId { get; set; }
    public bool   IsLocal  { get; set; }
}