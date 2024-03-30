using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using NCore.Actions;
using NCore.Modals;

namespace ITC.Domain.Core.ModelShare.NewsManagers.NewsRecruitmentManagers;

/// <summary>
///     Modal phân trang bài viết
/// </summary>
public class NewsRecruitmentPagingDto
{
    public              Guid                       Id            { get; set; }
    public              string                     Name          { get; set; }
    public              string                     Summary       { get; set; }
    public              DateTime                   DateTimeStart { get; set; }
    [JsonIgnore] public string                     OwnerId       { get; set; }
    public              string                     OwnerName     { get; set; }
    [JsonIgnore] public int                        StatusId      { get; set; }
    public              string                     StatusName    => ActionStatusEnum.GetById(StatusId)?.Name;
    public              string                     StatusColor   => ActionStatusColorEnum.GetById(StatusId)?.Name;
    public              List<ActionAuthorityModel> Actions       { get; set; }
    [JsonIgnore] public int                        TotalRecord   { get; set; }
}

/// <summary>
///     Modal phân trang danh sách combobox
/// </summary>
public class NewsRecruitmentPagingComboboxDto
{
    public              string   Id            { get; set; }
    public              string   Name          { get; set; }
    public              string   Summary       { get; set; }
    public              string   Author        { get; set; }
    public              string   NewsGroupName { get; set; }
    public              DateTime DateTimeStart { get; set; }
    [JsonIgnore] public int      TotalRecord   { get; set; }
}