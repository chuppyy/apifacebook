#region

using System;
using ITC.Domain.Core.Commands;

#endregion

namespace ITC.Domain.Commands.Itphonui.ProjectManager;

/// <summary>
///     Command Quản lý dự án
/// </summary>
public abstract class ProjectManagerCommand : Command
{
    /// <summary>
    ///     Mã ID
    /// </summary>
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

    /// <summary>
    ///     Địa chỉ host
    /// </summary>
    public string HostName { get; set; }

    public string TitleOne         { get; set; }
    public string TitleTwo         { get; set; }
    public string TitleMobileOne   { get; set; }
    public string TitleMobileTwo   { get; set; }
    public string TitleMobileThree { get; set; }

    /// <summary>
    ///     Sử dụng local url đối với dự án này để kiểm tra và phát triển tính năng
    /// </summary>
    public bool IsUseLocalUrl { get; set; }
}