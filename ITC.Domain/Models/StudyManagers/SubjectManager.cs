using System;
using ITC.Domain.Core.Models;

namespace ITC.Domain.Models.StudyManagers;

/// <summary>
///     Môn học
/// </summary>
public class SubjectManager : RootModel
{
    /// <summary>
    ///     Hàm dựng có tham số
    /// </summary>
    public SubjectManager(Guid   id,        string name,      string description,        string parentId,
                          int    position,  string secretKey, int    statusId,           Guid   subjectTypeManagerId,
                          Guid   projectId, int    typeId,    bool   isShowMenuHomePage, Guid   avatarId,
                          string metaTitle, string createdBy = null)
        : base(id, createdBy)
    {
        ProjectId = projectId;
        TypeId    = typeId;
        PLeft     = 1;
        PRight    = 2;
        Update(name,     description, parentId, position, secretKey, statusId, subjectTypeManagerId, isShowMenuHomePage,
               avatarId, metaTitle,   createdBy);
    }

    /// <summary>
    ///     Hàm dựng mặc định
    /// </summary>
    protected SubjectManager()
    {
    }

    /// <summary>
    ///     Tên nhóm
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Mô tả
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    ///     Mã cha - con
    /// </summary>
    public string ParentId { get; set; }

    /// <summary>
    ///     Giá trị trái
    /// </summary>
    public int PLeft { get; set; }

    /// <summary>
    ///     Giá trị phải
    /// </summary>
    public int PRight { get; set; }

    /// <summary>
    ///     Vị trí hiển thị
    /// </summary>
    public int Position { get; set; }

    /// <summary>
    ///     Mã bí mật
    /// </summary>
    public string SecretKey { get; set; }

    /// <summary>
    ///     Mã dự án
    /// </summary>
    public Guid ProjectId { get; set; }

    /// <summary>
    ///     Loại nhóm dữ liệu
    /// </summary>
    public Guid SubjectTypeManagerId { get; set; }

    /// <summary>
    ///     Loại dữ liệu
    /// </summary>
    public int TypeId { get; set; }

    /// <summary>
    ///     Hiển thị trên danh sách menu trang chủ
    /// </summary>
    public bool IsShowMenuHomePage { get; set; }

    /// <summary>
    ///     Ảnh đại diện
    /// </summary>
    public Guid AvatarId { get; set; }

    /// <summary>
    ///     Tiêu đề
    /// </summary>
    public string MetaTitle { get; set; }

    /// <summary>
    ///     Cập nhật
    /// </summary>
    /// <param name="name">Tên menu</param>
    /// <param name="description">Mô tả</param>
    /// <param name="parentId">Mã cha - con</param>
    /// <param name="position">Vị trí hiển thị</param>
    /// <param name="secretKey">Mã bí mật</param>
    /// <param name="statusId">Trạng thái</param>
    /// <param name="subjectTypeManagerId">Loại dữ liệu</param>
    /// <param name="isShowMenuHomePage">Hiển thị trên danh sách menu trang chủ</param>
    /// <param name="avatarId">Ảnh đại diện</param>
    /// <param name="metaTitle">Tiêu đề</param>
    /// <param name="createdBy">Người tạo</param>
    public void Update(string name,      string description, string parentId,             int  position,
                       string secretKey, int    statusId,    Guid   subjectTypeManagerId, bool isShowMenuHomePage,
                       Guid   avatarId,  string metaTitle,   string createdBy = null)
    {
        Name                 = name;
        Description          = description;
        ParentId             = parentId;
        Position             = position;
        SecretKey            = secretKey;
        StatusId             = statusId;
        SubjectTypeManagerId = subjectTypeManagerId;
        IsShowMenuHomePage   = isShowMenuHomePage;
        AvatarId             = avatarId;
        MetaTitle            = metaTitle;
        Update(createdBy);
    }
}