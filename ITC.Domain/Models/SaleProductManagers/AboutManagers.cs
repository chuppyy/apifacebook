using System;
using ITC.Domain.Core.Models;
using NCore.Actions;

namespace ITC.Domain.Models.SaleProductManagers;

/// <summary>
///     Quản lý giới thiệu
/// </summary>
public class AboutManager : RootModel
{
    /// <summary>
    ///     Hàm dựng có tham số
    /// </summary>
    public AboutManager(Guid id, string name, Guid projectId, int typeId, string metaLink, string createdBy = null) :
        base(id, createdBy)
    {
        StatusId  = ActionStatusEnum.Active.Id;
        ProjectId = projectId;
        ViewEye   = 0;
        TypeId    = typeId;
        UpdateMain(name, metaLink, createdBy);
    }

    /// <summary>
    ///     Hàm dựng mặc định
    /// </summary>
    protected AboutManager()
    {
    }

    /// <summary>
    ///     Tên giới thiệu
    /// </summary>
    public string Name { get; set; }

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
    ///     Lượt xem
    /// </summary>
    public int ViewEye { get; set; }

    /// <summary>
    ///     Ảnh đại diện
    /// </summary>
    public Guid AvatarId { get; set; }

    /// <summary>
    ///     Mã dự án
    /// </summary>
    public Guid ProjectId { get; set; }

    /// <summary>
    ///     Loại dữ liệu giới thiệu
    /// </summary>
    public int TypeId { get; set; }

    /// <summary>
    ///     Đường dẫn hiển thị trên trang chủ - dùng chủ yếu trong việc chèn bài viết
    /// </summary>
    public string MetaLink { get; set; }

    /// <summary>
    ///     Cập nhật
    /// </summary>
    /// <param name="name">Tên dữ liệu</param>
    /// <param name="metaLink">Đường dẫn hiển thị trang chủ</param>
    /// <param name="createdBy">Người tạo</param>
    public void UpdateMain(string name, string metaLink, string createdBy = null)
    {
        Name     = name;
        MetaLink = metaLink;
        Update(createdBy);
    }

    /// <summary>
    ///     Cập nhật chi tiết
    /// </summary>
    /// <param name="content">Nội dung</param>
    /// <param name="seoKeyword">Từ khóa SEO</param>
    /// <param name="summary">Tóm tắt</param>
    /// <param name="avatarId">Ảnh đại diện</param>
    /// <param name="createdBy">Người tạo</param>
    public void UpdateDetail(string content, string seoKeyword, string summary, Guid avatarId, string createdBy = null)
    {
        Content    = content;
        SeoKeyword = seoKeyword;
        Summary    = summary;
        AvatarId   = avatarId;
        Update(createdBy);
    }
}