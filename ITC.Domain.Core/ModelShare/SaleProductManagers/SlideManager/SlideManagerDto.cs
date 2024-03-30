using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using ITC.Domain.Core.NCoreLocal.Enum;
using NCore.Actions;
using NCore.Modals;

namespace ITC.Domain.Core.ModelShare.SaleProductManagers.SlideManager;

/// <summary>
///     Modal phân trang slide
/// </summary>
public class SlideManagerPagingDto
{
    public              Guid                       Id           { get; set; }
    public              string                     Name         { get; set; }
    public              string                     Content      { get; set; }
    public              string                     OwnerId      { get; set; }
    [JsonIgnore] public int                        StatusId     { get; set; }
    public              string                     StatusName   => ActionStatusEnum.GetById(StatusId)?.Name;
    public              string                     StatusColor  => ActionStatusColorEnum.GetById(StatusId)?.Name;
    [JsonIgnore] public int                        TypeViewId   { get; set; }
    public              string                     TypeViewName => SlideTypeViewEnum.GetById(TypeViewId)?.Name;
    public              bool                       IsLocal      { get; set; }
    public              string                     FilePath     { get; set; }
    public              List<ActionAuthorityModel> Actions      { get; set; }
    [JsonIgnore] public int                        TotalRecord  { get; set; }
}