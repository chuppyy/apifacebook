using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using NCore.Actions;
using NCore.Modals;

namespace ITC.Domain.Core.ModelShare.NewsManagers.NewsGroupTypeManagers;

/// <summary>
///     Modal phân trang loại nhóm tin
/// </summary>
public class NewsGroupTypePagingDto
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

public class ListVercelDto
{
    public Guid   Id              { get; set; }
    public string Name            { get; set; }
    public string Domain          { get; set; }
    public string DomainVercel    { get; set; }
    public string DomainVercelNew { get; set; }
    public string Checked         { get; set; }
    public string ParentId        { get; set; }
    public int    Number          { get; set; }
}