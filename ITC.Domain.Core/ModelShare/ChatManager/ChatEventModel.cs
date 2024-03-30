using System;
using System.Collections.Generic;
using NCore.Actions;
using NCore.Modals;
using Newtonsoft.Json;

namespace ITC.Domain.Core.ModelShare.ChatManager;

/// <summary>
///     Class truyền dữ liệu Chat
/// </summary>
public class ChatEventModel
{
    public Guid     Id             { get; set; }
    public Guid     Sender         { get; set; }
    public Guid     Receiveder     { get; set; }
    public string   Content        { get; set; }
    public DateTime SendDateTime   { get; set; }
    public string   SenderName     { get; set; }
    public string   ReceivederName { get; set; }
}

public class ChatPagingManagerModel : ChatEventModel
{
    public              string                     OwnerId     { get; set; }
    [JsonIgnore] public int                        StatusId    { get; set; }
    public              string                     StatusName  => ActionStatusEnum.GetById(StatusId)?.Name;
    public              string                     StatusColor => ActionStatusColorEnum.GetById(StatusId)?.Name;
    public              List<ActionAuthorityModel> Actions     { get; set; }
    [JsonIgnore] public int                        TotalRecord { get; set; }
}