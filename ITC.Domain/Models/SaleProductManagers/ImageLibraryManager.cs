using System;
using ITC.Domain.Core.Models;
using NCore.Actions;

namespace ITC.Domain.Models.SaleProductManagers;

/// <summary>
///     Quản lý thư viện hình ảnh
/// </summary>
public class ImageLibraryManager : RootModel
{
    /// <summary>
    ///     Hàm dựng có tham số
    /// </summary>
    public ImageLibraryManager(Guid id,       string name,      string content, int position, Guid projectId,
                               Guid avatarId, string secretKey, string createdBy = null) :
        base(id, createdBy)
    {
        StatusId  = ActionStatusEnum.Active.Id;
        ProjectId = projectId;
        Position  = position;
        SecretKey = secretKey;
        Update(name, content, avatarId, createdBy);
    }

    /// <summary>
    ///     Hàm dựng mặc định
    /// </summary>
    protected ImageLibraryManager()
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
    ///     Mã dự án
    /// </summary>
    public Guid ProjectId { get; set; }

    /// <summary>
    ///     Vị trí
    /// </summary>
    public int Position { get; set; }

    /// <summary>
    ///     Ảnh đại diện
    /// </summary>
    public Guid AvatarId { get; set; }

    /// <summary>
    ///     Mã bí mật
    /// </summary>
    public string SecretKey { get; set; }

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