using System;
using NCore.Modals;

namespace ITC.Domain.Core.ModelShare.NewsManagers.NewsGroupManagers;

/// <summary>
///     [Model] Nhóm tin
/// </summary>
public class NewsGroupEventModel : PublishModal
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
    public Guid NewsGroupTypeId { get; set; }

    /// <summary>
    ///     Domain
    /// </summary>
    public string Domain { get;       set; }
    public string IdViaQc      { get; set; }
    public bool   AgreeVia     { get; set; }
    public string LinkTree     { get; set; }
    public Guid   StaffId      { get; set; }
    public bool   IsShowMain   { get; set; }
    public string DomainVercel { get; set; }
    /// <summary>
    /// Số lần để reset
    /// </summary>
    public int? Amount { get; set; }
    /// <summary>
    /// Số lần đã đăng
    /// </summary>
    public int? AmountPosted { get; set; }
}