using System;
using System.Collections.Generic;
using ITC.Domain.Core.Models;

namespace ITC.Domain.Models.NewsManagers;

/// <summary>
///     Loại nhóm tin
/// </summary>
public class NewsGroupType : RootModel
{
    /// <summary>
    ///     Hàm dựng có tham số
    /// </summary>
    public NewsGroupType(Guid   id,        string name,      string description, int    position, int statusId,
                         string secretKey, string metaTitle, Guid   projectId,   string createdBy = null)
        : base(id, createdBy)
    {
        NewsContents = new List<NewsContent>();
        NewsGroups   = new List<NewsGroup>();
        ProjectId    = projectId;
        Update(name, description, position, statusId, secretKey, metaTitle, createdBy);
    }

    /// <summary>
    ///     Hàm dựng mặc định
    /// </summary>
    protected NewsGroupType()
    {
    }

    /// <summary>
    ///     Tên loại nhóm tin
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Mô tả
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    ///     Vị trí hiển thị
    /// </summary>
    public int Position { get; set; }

    /// <summary>
    ///     Mã bí mật
    /// </summary>
    public string SecretKey { get; set; }

    /// <summary>
    ///     Tiêu đề
    /// </summary>
    public string MetaTitle { get; set; }

    /// <summary>
    ///     Mã dự án
    /// </summary>
    public Guid ProjectId { get; set; }

    /// <summary>
    ///     Liên kết khóa ngoại
    /// </summary>
    public virtual List<NewsContent> NewsContents { get; set; }

    /// <summary>
    ///     Liên kết khóa ngoại
    /// </summary>
    public virtual List<NewsGroup> NewsGroups { get; set; }

    /// <summary>
    ///     Cập nhật
    /// </summary>
    /// <param name="name">Tên menu</param>
    /// <param name="description">Mô tả</param>
    /// <param name="position">Vị trí hiển thị</param>
    /// <param name="statusId">Trạng thái</param>
    /// <param name="secretKey">Mã bí mật</param>
    /// <param name="metaTitle">Tiêu đề</param>
    /// <param name="createdBy">Người tạo</param>
    public void Update(string name,      string description, int position, int statusId, string secretKey,
                       string metaTitle, string createdBy = null)
    {
        Name        = name;
        Description = description;
        Position    = position;
        StatusId    = statusId;
        SecretKey   = secretKey;
        MetaTitle   = metaTitle;
        Update(createdBy);
    }
}