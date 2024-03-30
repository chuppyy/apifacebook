using System;
using ITC.Domain.Core.Models;
using NCore.Actions;

namespace ITC.Domain.Models.SaleProductManagers;

/// <summary>
///     Quản lý slide
/// </summary>
public class SlideManager : RootModel
{
    /// <summary>
    ///     Hàm dựng có tham số
    /// </summary>
    public SlideManager(Guid id,        string name,       string content, string urlLink, Guid avatarId, int position,
                        Guid projectId, int    typeViewId, int    urlType, string createdBy = null) :
        base(id, createdBy)
    {
        StatusId  = ActionStatusEnum.Active.Id;
        ProjectId = projectId;
        Position  = position;
        Update(name, content, urlLink, avatarId, typeViewId, urlType, createdBy);
    }

    /// <summary>
    ///     Hàm dựng mặc định
    /// </summary>
    protected SlideManager()
    {
    }

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
    ///     Loại dữ liệu bài viết
    /// </summary>
    public int UrlType { get; set; }

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
    ///     Loại hiển thị
    /// </summary>
    public int TypeViewId { get; set; }

    /// <summary>
    ///     Cập nhật
    /// </summary>
    /// <param name="name">Tên slide</param>
    /// <param name="content">Mô tả slide</param>
    /// <param name="urlLink">Link website</param>
    /// <param name="avatarId">Ảnh đại diện</param>
    /// <param name="typeViewId">Loại hiển thị</param>
    /// <param name="urlType">Loại bài viết liên kết</param>
    /// <param name="createdBy">Người tạo</param>
    public void Update(string name, string content, string urlLink, Guid avatarId, int typeViewId, int urlType,
                       string createdBy = null)
    {
        Name       = name;
        Content    = content;
        UrlLink    = urlLink;
        AvatarId   = avatarId;
        TypeViewId = typeViewId;
        UrlType    = urlType;
        Update(createdBy);
    }
}