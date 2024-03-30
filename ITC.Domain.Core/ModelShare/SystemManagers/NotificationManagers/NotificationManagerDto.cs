using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using NCore.Actions;
using NCore.Modals;

namespace ITC.Domain.Core.ModelShare.SystemManagers.NotificationManagers;

/// <summary>
///     Modal phân trang thông báo
/// </summary>
public class NotificationManagerPagingDto
{
    public              Guid                       Id            { get; set; }
    public              string                     Name          { get; set; }
    public              string                     Content       { get; set; }
    public              string                     OwnerId       { get; set; }
    public              bool                       IsLimitedTime { get; set; }
    public              bool                       IsRun         { get; set; }
    public              bool                       IsSendAll     { get; set; }
    public              DateTime                   DateStart     { get; set; }
    public              DateTime                   DateEnd       { get; set; }
    public              int                        TotalFile     { get; set; }
    public              int                        TotalUser     { get; set; }
    [JsonIgnore] public int                        StatusId      { get; set; }
    public              string                     StatusName    => ActionStatusEnum.GetById(StatusId)?.Name;
    public              string                     StatusColor   => ActionStatusColorEnum.GetById(StatusId)?.Name;
    public              List<ActionAuthorityModel> Actions       { get; set; }
    [JsonIgnore] public int                        TotalRecord   { get; set; }
}