#region

using System;
using ITC.Domain.Core.Commands;

#endregion

namespace ITC.Domain.Commands.NewsManagers.NewsVia;

/// <summary>
///     Command danh sách via
/// </summary>
public abstract class NewsViaCommand : Command
{
    public Guid   Id      { get; set; }
    public string Code    { get; set; }
    public string Content { get; set; }
    public string Token   { get; set; }
    public string IdTkQc  { get; set; }
    public Guid   StaffId { get; set; }
}