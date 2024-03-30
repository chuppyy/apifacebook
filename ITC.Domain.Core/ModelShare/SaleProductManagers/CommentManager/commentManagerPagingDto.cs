using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using NCore.Actions;
using NCore.Modals;

namespace ITC.Domain.Core.ModelShare.SaleProductManagers.CommentManager;

/// <summary>
///     Modal phân trang comment
/// </summary>
public class CommentManagerPagingDto
{
    public              Guid                       Id          { get; set; }
    public              string                     Name        { get; set; }
    public              string                     Email       { get; set; }
    public              string                     Content     { get; set; }
    public              string                     Phone       { get; set; }
    public              DateTime                   StartDate   { get; set; }
    public              string                     UserAgree   { get; set; }
    public              DateTime                   StartAgree  { get; set; }
    public              int                        StatusId    { get; set; }
    public              string                     StatusName  => ActionStatusEnum.GetById(StatusId)?.Name;
    public              string                     StatusColor => ActionStatusColorEnum.GetById(StatusId)?.Name;
    [JsonIgnore] public int                        TotalRecord { get; set; }
    public              List<ActionAuthorityModel> Actions     { get; set; }
}