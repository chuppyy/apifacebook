using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using ITC.Domain.Core.NCoreLocal.Enum;
using NCore.Actions;
using NCore.Modals;

namespace ITC.Domain.Core.ModelShare.SaleProductManagers.ContactCustomerManager;

/// <summary>
///     Modal phân trang khách hàng liên hệ
/// </summary>
public class ContactCustomerManagerPagingDto
{
    public              Guid Id { get; set; }
    public              string Name { get; set; }
    public              string OwnerId { get; set; }
    [JsonIgnore] public int StatusId { get; set; }
    public              string StatusName => ActionStatusEnum.GetById(StatusId)?.Name;
    public              string StatusColor => ActionStatusColorEnum.GetById(StatusId)?.Name;
    public              string Phone { get; set; }
    public              string Address { get; set; }
    public              string Email { get; set; }
    public              string Content { get; set; }
    public              DateTime ContactDateTime { get; set; }
    public              int HandlerTypeId { get; set; }
    public              string HandlerTypeName => HandlerContactManagerEnum.GetById(HandlerTypeId)?.Name;
    public              string HandlerUser { get; set; }
    public              string HandlerUserName { get; set; }
    public              string HandlerContent { get; set; }
    public              DateTime HandlerDateTime { get; set; }
    public              List<ActionAuthorityModel> Actions { get; set; }
    [JsonIgnore] public int TotalRecord { get; set; }
}