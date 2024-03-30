using System;
using NCore.Modals;

namespace ITC.Domain.Core.ModelShare.Itphonui.ProjectManagers;

/// <summary>
///     Class truyền dữ liệu bài viết từ FE
/// </summary>
public class ProjectManagerEventModel : PublishModal
{
    public Guid Id { get; set; }

    /// <summary>
    ///     Tên dự án
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Mô tả
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    ///     Mô tả
    /// </summary>
    public string Nation { get; set; }

    /// <summary>
    ///     Ngày bắt đầu
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    ///     Ngày kết thúc
    /// </summary>
    public DateTime EndDate { get; set; }

    /// <summary>
    ///     Số lần gia hạn
    /// </summary>
    public int NumberOfExtend { get; set; }

    /// <summary>
    ///     Địa chỉ host
    /// </summary>
    public string HostName { get; set; }

    public string TitleOne         { get; set; }
    public string TitleTwo         { get; set; }
    public string TitleMobileOne   { get; set; }
    public string TitleMobileTwo   { get; set; }
    public string TitleMobileThree { get; set; }
    public string Code             { get; set; }

    /// <summary>
    ///     Sử dụng local url đối với dự án này để kiểm tra và phát triển tính năng
    /// </summary>
    public bool IsUseLocalUrl { get; set; }
}

/// <summary>
///     Class view model bài viết
/// </summary>
public class ProjectManagerEventViewModel
{
    public Guid Id { get; set; }

    /// <summary>
    ///     Tên dự án
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Mô tả
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    ///     Mô tả
    /// </summary>
    public string Nation { get; set; }

    /// <summary>
    ///     Ngày bắt đầu
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    ///     Ngày kết thúc
    /// </summary>
    public DateTime EndDate { get; set; }

    /// <summary>
    ///     Số lần gia hạn
    /// </summary>
    public int NumberOfExtend { get; set; }

    /// <summary>
    ///     Trạng thái
    /// </summary>
    public int StatusId { get; set; }

    public string HostName  { get; set; }
    public string CreatedBy { get; set; }

    public string TitleOne         { get; set; }
    public string TitleTwo         { get; set; }
    public string TitleMobileOne   { get; set; }
    public string TitleMobileTwo   { get; set; }
    public string TitleMobileThree { get; set; }
}

/// <summary>
///     Class view model cấu hình dự án
/// </summary>
public class ProjectManagerConfigEventViewModel
{
    /// <summary>
    ///     Mã ID
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    ///     Mã dự án
    /// </summary>
    public Guid ProjectManagerId { get; set; }

    /// <summary>
    ///     Nhóm môn học
    /// </summary>
    public int SubjectGroup { get; set; }

    /// <summary>
    ///     Nhóm chức vụ
    /// </summary>
    public int DutyGroup { get; set; }

    /// <summary>
    ///     Nhóm nhiệm vụ
    /// </summary>
    public int TaskGroup { get; set; }
}

public class ProjectManagerAddFunctionEventModel : RequestBaseModel
{
    /// <summary>
    ///     Mã chức năng
    /// </summary>
    public Guid FunctionId { get; set; }
}