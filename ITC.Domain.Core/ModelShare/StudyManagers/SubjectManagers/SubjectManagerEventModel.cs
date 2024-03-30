using System;
using NCore.Modals;

namespace ITC.Domain.Core.ModelShare.StudyManagers.SubjectManagers;

/// <summary>
///     [Model] Môn học
/// </summary>
public class SubjectManagerEventModel : PublishModal
{
    /// <summary>
    ///     Mã dữ liệu
    /// </summary>
    public Guid Id { get; set; }

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
    ///     Vị trí hiển thị
    /// </summary>
    public int Position { get; set; }

    /// <summary>
    ///     Loại dữ liệu
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
}

/// <summary>
///     Trả về dữ liệu truy vấn GetById
/// </summary>
public class SubjectManagerGetByIdModel : SubjectManagerEventModel
{
    public bool IsLocal { get; set; }
}