using System;
using ITC.Domain.Core.Models;
using NCore.Actions;

namespace ITC.Domain.Models.SaleProductManagers;

/// <summary>
///     Quản lý slide
/// </summary>
public class ImageLibraryDetailManager : RootModel
{
    /// <summary>
    ///     Hàm dựng có tham số
    /// </summary>
    public ImageLibraryDetailManager(Guid   id, string name, string content, Guid avatarId, int position,
                                     Guid   imageLibraryManagerId,
                                     string createdBy = null) :
        base(id, createdBy)
    {
        StatusId              = ActionStatusEnum.Active.Id;
        ImageLibraryManagerId = imageLibraryManagerId;
        Position              = position;
        Update(name, content, avatarId, createdBy);
    }

    /// <summary>
    ///     Hàm dựng mặc định
    /// </summary>
    protected ImageLibraryDetailManager()
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
    ///     Ảnh đại diện
    /// </summary>
    public Guid AvatarId { get; set; }

    /// <summary>
    ///     Vị trí
    /// </summary>
    public int Position { get; set; }

    /// <summary>
    ///     Mã thư viện hình ảnh
    /// </summary>
    public Guid ImageLibraryManagerId { get; set; }

    /// <summary>
    ///     Cập nhật
    /// </summary>
    /// <param name="name">Tên slide</param>
    /// <param name="content">Mô tả slide</param>
    /// <param name="avatarId">Ảnh đại diện</param>
    /// <param name="createdBy">Người tạo</param>
    public void Update(string name, string content, Guid avatarId, string createdBy = null)
    {
        Name     = name;
        Content  = content;
        AvatarId = avatarId;
        Update(createdBy);
    }
}