using System;
using System.Collections.Generic;
using NCore.Modals;

namespace ITC.Domain.Core.ModelShare.HomeManagers.HomeMenuManagers;

/// <summary>
///     [Model] Menu trang chủ
/// </summary>
public class HomeMenuEventModel : PublishModal
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

    /// <summary>
    ///     Dữ liệu liên kết với nhóm tin
    /// </summary>
    public List<HomeMenuNewsGroupModel> HomeMenuNewsGroupModels { get; set; }
}