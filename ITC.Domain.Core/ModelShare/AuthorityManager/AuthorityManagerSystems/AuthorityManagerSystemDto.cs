using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using NCore.Actions;
using NCore.Modals;

namespace ITC.Domain.Core.ModelShare.AuthorityManager.AuthorityManagerSystems;

/// <summary>
///     Modal phân trang quyền sử dụng
/// </summary>
public class AuthorityManagerSystemPagingDto
{
    public              Guid                       Id          { get; set; }
    public              string                     Name        { get; set; }
    public              string                     Description { get; set; }
    public              string                     ProjectName { get; set; }
    [JsonIgnore] public string                     OwnerId     { get; set; }
    public              int                        StatusId    { get; set; }
    public              string                     StatusName  => ActionStatusEnum.GetById(StatusId)?.Name;
    public              List<ActionAuthorityModel> Actions     { get; set; }
    [JsonIgnore] public int                        TotalRecord { get; set; }
}