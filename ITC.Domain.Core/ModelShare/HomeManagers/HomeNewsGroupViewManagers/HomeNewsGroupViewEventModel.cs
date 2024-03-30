using System;
using System.Collections.Generic;
using NCore.Modals;

namespace ITC.Domain.Core.ModelShare.HomeManagers.HomeNewsGroupViewManagers;

/// <summary>
///     [Model] Danh sách các nhóm bài viết hiển thị trên trang chủ
/// </summary>
public class HomeNewsGroupViewEventModel : PublishModal
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
    ///     Dữ liệu liên kết với nhóm tin
    /// </summary>
    public List<HomeNewsGroupViewDetailModel> HomeNewsGroupViewModels { get; set; }
}