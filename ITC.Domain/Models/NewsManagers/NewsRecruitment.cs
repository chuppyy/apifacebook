using System;
using ITC.Domain.Core.Models;

namespace ITC.Domain.Models.NewsManagers;

/// <summary>
///     Tuyển dụng
/// </summary>
public class NewsRecruitment : RootModel
{
    /// <summary>
    ///     Hàm dựng có tham số
    /// </summary>
    public NewsRecruitment(Guid     id,            string name,      string summary,    string content,
                           int      position,      string secretKey, string seoKeyword, Guid   avatarId,
                           DateTime dateTimeStart, int    statusId,  Guid   projectId,  int    type,
                           string   createdBy = null)
        : base(id, createdBy)
    {
        ProjectId     = projectId;
        Position      = position;
        SecretKey     = secretKey;
        DateTimeStart = dateTimeStart;
        Type          = type;
        Update(name, summary, content, seoKeyword, avatarId, statusId, createdBy);
    }

    /// <summary>
    ///     Hàm dựng mặc định
    /// </summary>
    protected NewsRecruitment()
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
    ///     Loại dữ liệu bài viết
    /// </summary>
    public int Type { get; set; }

    /// <summary>
    ///     Cập nhật
    /// </summary>
    /// <param name="name">Tên bài viết</param>
    /// <param name="summary">Tóm tắt</param>
    /// <param name="content">Nội dung</param>
    /// <param name="seoKeyword">Từ khóa SEO</param>
    /// <param name="avatarId">Ảnh đại diện</param>
    /// <param name="statusId">Trạng thái</param>
    /// <param name="createdBy">Người tạo</param>
    public void Update(string name,     string summary, string content, string seoKeyword, Guid avatarId,
                       int    statusId, string createdBy = null)
    {
        Name       = name;
        Summary    = summary;
        Content    = content;
        SeoKeyword = seoKeyword;
        AvatarId   = avatarId;
        StatusId   = statusId;
        Update(createdBy);
    }
}