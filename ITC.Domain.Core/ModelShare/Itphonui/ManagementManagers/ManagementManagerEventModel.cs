using System;
using System.Collections.Generic;
using NCore.Modals;

namespace ITC.Domain.Core.ModelShare.Itphonui.ManagementManagers;

/// <summary>
///     [Model] Quản lý đơn vị
/// </summary>
public class ManagementManagerEventModel : PublishModal
{
    /// <summary>
    ///     Mã dữ liệu
    /// </summary>
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

/// <summary>
///     [Model] Thêm đơn vị vào dự án
/// </summary>
public class ManagementDetailManagerEventModel : PublishModal
{
    public List<Guid> Models    { get; set; }
    public Guid       ProjectId { get; set; }
}