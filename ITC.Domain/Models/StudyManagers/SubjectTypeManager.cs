using System;
using System.Collections.Generic;
using ITC.Domain.Core.Models;

namespace ITC.Domain.Models.StudyManagers;

/// <summary>
///     Loại môn học
/// </summary>
public class SubjectTypeManager : RootModel
{
    /// <summary>
    ///     Hàm dựng có tham số
    /// </summary>
    public SubjectTypeManager(Guid id,       string name,      string description, int  position,
                              int  statusId, string secretKey, int    typeId,      Guid projectId,
                              Guid avatarId, string createdBy = null)
        : base(id, createdBy)
    {
        ProjectId       = projectId;
        SubjectManagers = new List<SubjectManager>();
        Update(name, description, position, statusId, secretKey, typeId, avatarId, createdBy);
    }

    /// <summary>
    ///     Hàm dựng mặc định
    /// </summary>
    protected SubjectTypeManager()
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
    ///     Loại dữ liệu
    /// </summary>
    public int TypeId { get; set; }

    /// <summary>
    ///     Mã dự án
    /// </summary>
    public Guid ProjectId { get; set; }

    /// <summary>
    ///     Ảnh đại diện
    /// </summary>
    public Guid AvatarId { get; set; }

    /// <summary>
    ///     Liên kết khóa ngoại
    /// </summary>
    public virtual List<SubjectManager> SubjectManagers { get; set; }

    /// <summary>
    ///     Cập nhật
    /// </summary>
    /// <param name="name">Tên menu</param>
    /// <param name="description">Mô tả</param>
    /// <param name="position">Vị trí hiển thị</param>
    /// <param name="statusId">Trạng thái</param>
    /// <param name="secretKey">Mã bí mật</param>
    /// <param name="typeId">Loại nhóm dữ liệu</param>
    /// <param name="avatarId">Ảnh đại diện</param>
    /// <param name="createdBy">Người tạo</param>
    public void Update(string name,     string description, int position, int statusId, string secretKey, int typeId,
                       Guid   avatarId, string createdBy = null)
    {
        Name        = name;
        Description = description;
        Position    = position;
        StatusId    = statusId;
        SecretKey   = secretKey;
        TypeId      = typeId;
        AvatarId    = avatarId;
        Update(createdBy);
    }
}