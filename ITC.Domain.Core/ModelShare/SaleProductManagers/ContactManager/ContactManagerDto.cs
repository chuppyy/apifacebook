using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using NCore.Actions;
using NCore.Modals;

namespace ITC.Domain.Core.ModelShare.SaleProductManagers.ContactManager;

/// <summary>
///     Modal phân trang liên hệ
/// </summary>
public class ContactManagerPagingDto
{
    public              Guid                       Id             { get; set; }
    public              string                     Name           { get; set; }
    public              string                     OwnerId        { get; set; }
    [JsonIgnore] public int                        StatusId       { get; set; }
    public              string                     StatusName     => ActionStatusEnum.GetById(StatusId)?.Name;
    public              string                     StatusColor    => ActionStatusColorEnum.GetById(StatusId)?.Name;
    public              string                     Phone          { get; set; }
    public              string                     Address        { get; set; }
    public              string                     Email          { get; set; }
    public              string                     Zalo           { get; set; }
    public              string                     Skype          { get; set; }
    public              string                     Hotline        { get; set; }
    public              bool                       IsShowHomePage { get; set; }
    public              List<ActionAuthorityModel> Actions        { get; set; }
    [JsonIgnore] public int                        TotalRecord    { get; set; }
}