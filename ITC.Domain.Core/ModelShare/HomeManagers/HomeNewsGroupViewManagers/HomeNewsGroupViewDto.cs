using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using NCore.Actions;
using NCore.Modals;

namespace ITC.Domain.Core.ModelShare.HomeManagers.HomeNewsGroupViewManagers;

/// <summary>
///     [Model] Liên kết với nhóm tin
/// </summary>
public class HomeNewsGroupViewDetailModel
{
    /// <summary>
    ///     Mã dữ liệu
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    ///     Tên nội dung
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Mã nhóm cha
    /// </summary>
    public string ParentId { get; set; }

    /// <summary>
    ///     Tên nhóm cha
    /// </summary>
    public string ParentName { get; set; }
}

/// <summary>
///     Modal phân trang danh sách nhóm tin hiển thị trên trang chủ
/// </summary>
public class HomeNewsGroupViewPagingDto
{
    public              Guid                       Id          { get; set; }
    public              string                     Name        { get; set; }
    public              string                     Description { get; set; }
    public              string                     OwnerId     { get; set; }
    [JsonIgnore] public int                        StatusId    { get; set; }
    public              string                     StatusName  => ActionStatusEnum.GetById(StatusId)?.Name;
    public              string                     StatusColor => ActionStatusColorEnum.GetById(StatusId)?.Name;
    public              List<ActionAuthorityModel> Actions     { get; set; }
    [JsonIgnore] public int                        TotalRecord { get; set; }
}