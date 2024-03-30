using System;
using ITC.Domain.Core.Models;

namespace ITC.Domain.Models.NewsManagers;

/// <summary>
///     Từ khóa SEO trong bài viết
/// </summary>
public class NewsSeoKeyWord : RootModel
{
    /// <summary>
    ///     Hàm dựng có tham số
    /// </summary>
    /// <param name="id"></param>
    /// <param name="name">Tên từ khóa</param>
    /// <param name="description">Mô tả</param>
    /// <param name="statusId">Trạng thái</param>
    /// <param name="projectId">Mã dự án</param>
    /// <param name="createdBy">Người tạo</param>
    public NewsSeoKeyWord(Guid   id, string name, string description, int statusId, Guid projectId,
                          string createdBy = null)
        : base(id, createdBy)
    {
        StatusId    = statusId;
        Name        = name;
        Description = description;
        ProjectId   = projectId;
    }

    /// <summary>
    ///     Hàm dựng mặc định
    /// </summary>
    protected NewsSeoKeyWord()
    {
    }

    /// <summary>
    ///     Tên từ khóa
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Mô tả
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    ///     Mã dự án
    /// </summary>
    public Guid ProjectId { get; set; }

    /// <summary>
    ///     Cập nhật
    /// </summary>
    /// <param name="name">Tên từ khóa</param>
    /// <param name="description">Mô tả</param>
    /// <param name="projectId">Mã dự án</param>
    /// <param name="createdBy">Người tạo</param>
    public void Update(string name, string description, Guid projectId, string createdBy = null)
    {
        Name        = name;
        Description = description;
        ProjectId   = projectId;
        Update(createdBy);
    }
}