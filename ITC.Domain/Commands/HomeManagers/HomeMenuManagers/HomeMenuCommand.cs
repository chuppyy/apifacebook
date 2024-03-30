#region

using System;
using ITC.Domain.Core.Commands;

#endregion

namespace ITC.Domain.Commands.HomeManagers.HomeMenuManagers;

/// <summary>
///     Command danh sách menu trang chủ
/// </summary>
public abstract class HomeMenuCommand : Command
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
    ///     Đường dẫn
    /// </summary>
    public string Url { get; set; }

    /// <summary>
    ///     Hiển thị trên trang chủ
    /// </summary>
    public bool IsViewHomePage { get; set; }
}