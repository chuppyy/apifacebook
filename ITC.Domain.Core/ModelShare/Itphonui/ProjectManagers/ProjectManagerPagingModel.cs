using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using NCore.Actions;
using NCore.Modals;

namespace ITC.Domain.Core.ModelShare.Itphonui.ProjectManagers;

/// <summary>
///     [Phân trang] Danh sách dự án
/// </summary>
public class ProjectManagerPagingModel
{
    public              Guid                       Id             { get; set; }
    public              string                     Name           { get; set; }
    public              string                     Description    { get; set; }
    public              DateTime                   StartDate      { get; set; }
    public              DateTime                   EndDate        { get; set; }
    public              int                        NumberOfExtend { get; set; }
    [JsonIgnore] public string                     OwnerId        { get; set; }
    public              string                     OwnerName      { get; set; }
    public              int                        StatusId       { get; set; }
    public              string                     StatusName     => ActionStatusEnum.GetById(StatusId)?.Name;
    public              List<ActionAuthorityModel> Actions        { get; set; }
    [JsonIgnore] public int                        TotalRecord    { get; set; }
}