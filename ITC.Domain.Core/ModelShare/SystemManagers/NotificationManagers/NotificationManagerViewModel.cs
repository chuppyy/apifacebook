using System;
using System.Collections.Generic;

namespace ITC.Domain.Core.ModelShare.SystemManagers.NotificationManagers;

/// <summary>
///     Trả dữ liệu thông báo chi tiết
/// </summary>
public class NotificationManagerViewModel
{
    public Guid                             Id         { get; set; }
    public string                           Name       { get; set; }
    public string                           Content    { get; set; }
    public DateTime                         DateSend   { get; set; }
    public string                           AuthorName { get; set; }
    public List<KeyValuePair<Guid, string>> FileModels { get; set; }
}