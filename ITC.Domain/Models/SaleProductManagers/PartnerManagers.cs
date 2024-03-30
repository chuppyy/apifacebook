using System;
using ITC.Domain.Core.Models;
using NCore.Actions;

namespace ITC.Domain.Models.SaleProductManagers;

/// <summary>
///     Quản lý đối tác - nhà cung cấp
/// </summary>
public class PartnerManager : RootModel
{
    /// <summary>
    ///     Hàm dựng có tham số
    /// </summary>
    public PartnerManager(Guid id,        string name, string content, string urlLink, Guid avatarId, int position,
                          Guid projectId, string createdBy = null) :
        base(id, createdBy)
    {
        StatusId  = ActionStatusEnum.Active.Id;
        ProjectId = projectId;
        Position  = position;
        Update(name, content, urlLink, avatarId, createdBy);
    }

    /// <summary>
    ///     Hàm dựng mặc định
    /// </summary>
    protected PartnerManager()
    {
    }

    /// <summary>
    ///     Tên đối tác - nhà cung cấp
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Mô tả đối tác - nhà cung cấp
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
    ///     Mã dự án
    /// </summary>
    public Guid ProjectId { get; set; }

    /// <summary>
    ///     Vị trí
    /// </summary>
    public int Position { get; set; }

    /// <summary>
    ///     Cập nhật
    /// </summary>
    /// <param name="name">Tên đối tác - nhà cung cấp</param>
    /// <param name="content">Mô tả đối tác - nhà cung cấp</param>
    /// <param name="urlLink">Link website</param>
    /// <param name="avatarId">Ảnh đại diện</param>
    /// <param name="createdBy">Người tạo</param>
    public void Update(string name, string content, string urlLink, Guid avatarId, string createdBy = null)
    {
        Name     = name;
        Content  = content;
        UrlLink  = urlLink;
        AvatarId = avatarId;
        Update(createdBy);
    }
}