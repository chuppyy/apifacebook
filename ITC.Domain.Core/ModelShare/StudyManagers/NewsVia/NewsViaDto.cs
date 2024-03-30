using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using NCore.Actions;
using NCore.Modals;

namespace ITC.Domain.Core.ModelShare.StudyManagers.NewsVia;

/// <summary>
///     Modal phân trang loại môn học
/// </summary>
public class NewsViaPagingDto
{
    public              Guid                       Id          { get; set; }
    public              string                     Root        { get; set; }
    public              string                     Replace     { get; set; }
    public              string                     Description { get; set; }
    public              string                     Code        { get; set; }
    public              string                     Token       { get; set; }
    public              string                     IdTkQc      { get; set; }
    public              string                     StaffName   { get; set; }
    public              string                     OwnerId     { get; set; }
    [JsonIgnore] public int                        StatusId    { get; set; }
    public              string                     StatusName  => ActionStatusEnum.GetById(StatusId)?.Name;
    public              string                     StatusColor => ActionStatusColorEnum.GetById(StatusId)?.Name;
    public              List<ActionAuthorityModel> Actions     { get; set; }
    [JsonIgnore] public int                        TotalRecord { get; set; }
}