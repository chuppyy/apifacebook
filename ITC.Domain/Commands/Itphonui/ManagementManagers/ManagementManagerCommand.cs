#region

using System;
using ITC.Domain.Core.Commands;

#endregion

namespace ITC.Domain.Commands.Itphonui.ManagementManagers;

/// <summary>
///     Command danh sách đơn vị
/// </summary>
public abstract class ManagementManagerCommand : Command
{
    public Guid Id { get; set; }

    /// <summary>
    ///     Tên đơn vị quản lý
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
    ///     Bậc tham gia thi đấu
    /// </summary>
    public int LevelCompetitionId { get; set; }

    /// <summary>
    ///     Ký hiệu
    /// </summary>
    public string Symbol { get; set; }

    /// <summary>
    ///     Tài khoản đăng nhập mặc định
    /// </summary>
    public string AccountDefault { get; set; }
}