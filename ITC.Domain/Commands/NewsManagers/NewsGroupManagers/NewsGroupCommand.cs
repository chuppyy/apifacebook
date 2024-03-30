#region

using System;
using ITC.Domain.Core.Commands;

#endregion

namespace ITC.Domain.Commands.NewsManagers.NewsGroupManagers;

/// <summary>
///     Command danh sách nhóm tin
/// </summary>
public abstract class NewsGroupCommand : Command
{
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
    public Guid TypeId { get; set; }

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