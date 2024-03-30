using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using NCore.Actions;
using NCore.Modals;

namespace ITC.Domain.Core.ModelShare.NewsManagers.NewsDomainManagers;

/// <summary>
///     Modal phân trang loại nhóm tin
/// </summary>
public class NewsDomainPagingDto
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