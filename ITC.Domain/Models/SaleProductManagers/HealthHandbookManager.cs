using System;
using ITC.Domain.Core.Models;

namespace ITC.Domain.Models.SaleProductManagers;

/// <summary>
///     e
///     Cẩm nang sức khỏe
/// </summary>
public class HealthHandbookManager : RootModel
{
    /// <summary>
    ///     Hàm dựng có tham số
    /// </summary>
    public HealthHandbookManager(Guid   id,         string name,      string   summary, string content,
                                 int    position,   string secretKey, string   author,  string urlRootLink,
                                 string seoKeyword, Guid   avatarId,  DateTime dateTimeStart,
                                 int    statusId,   Guid   projectId, string   createdBy = null)
        : base(id, createdBy)
    {
        ProjectId = projectId;
        Update(name,          summary,  content, position, secretKey, author, urlRootLink, seoKeyword, avatarId,
               dateTimeStart, statusId, createdBy);
    }

    /// <summary>
    ///     Hàm dựng mặc định
    /// </summary>
    protected HealthHandbookManager()
    {
    }

    /// <summary>
    ///     Tên bài viết
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Tóm tắt
    /// </summary>
    public string Summary { get; set; }

    /// <summary>
    ///     Nội dung
    /// </summary>
    public string Content { get; set; }

    /// <summary>
    ///     Vị trí hiển thị
    /// </summary>
    public int Position { get; set; }

    /// <summary>
    ///     Mã bí mật
    /// </summary>
    public string SecretKey { get; set; }

    /// <summary>
    ///     Tác giả
    /// </summary>
    public string Author { get; set; }

    /// <summary>
    ///     Link đường dẫn gốc
    /// </summary>
    public string UrlRootLink { get; set; }

    /// <summary>
    ///     Từ khóa SEO
    /// </summary>
    public string SeoKeyword { get; set; }

    /// <summary>
    ///     Ảnh đại diện
    /// </summary>
    public Guid AvatarId { get; set; }

    /// <summary>
    ///     Ngày viết bài
    /// </summary>
    public DateTime DateTimeStart { get; set; }

    /// <summary>
    ///     Lượt xem
    /// </summary>
    public int ViewEye { get; set; }

    /// <summary>
    ///     Mã dự án
    /// </summary>
    public Guid ProjectId { get; set; }

    /// <summary>
    ///     Cập nhật
    /// </summary>
    /// <param name="name">Tên bài viết</param>
    /// <param name="summary">Tóm tắt</param>
    /// <param name="content">Nội dung</param>
    /// <param name="position">Vị trí</param>
    /// <param name="secretKey">Mã bí mật</param>
    /// <param name="author">Tác giả bài viết</param>
    /// <param name="urlRootLink">Đường dẫn gốc</param>
    /// <param name="seoKeyword">Từ khóa SEO</param>
    /// <param name="avatarId">Ảnh đại diện</param>
    /// <param name="dateTimeStart">Ngày viết bài</param>
    /// <param name="statusId">Trạng thái</param>
    /// <param name="createdBy">Người tạo</param>
    public void Update(string name,       string summary,  string   content, int position,
                       string secretKey,  string author,   string   urlRootLink,
                       string seoKeyword, Guid   avatarId, DateTime dateTimeStart, int statusId,
                       string createdBy = null)
    {
        Name          = name;
        Summary       = summary;
        Content       = content;
        Position      = position;
        SecretKey     = secretKey;
        Author        = author;
        UrlRootLink   = urlRootLink;
        SeoKeyword    = seoKeyword;
        AvatarId      = avatarId;
        DateTimeStart = dateTimeStart;
        StatusId      = statusId;
        Update(createdBy);
    }
}