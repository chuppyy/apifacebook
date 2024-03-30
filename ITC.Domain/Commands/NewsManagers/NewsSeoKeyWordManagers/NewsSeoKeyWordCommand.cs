#region

using System;
using ITC.Domain.Core.Commands;

#endregion

namespace ITC.Domain.Commands.NewsManagers.NewsSeoKeyWordManagers;

/// <summary>
///     Command NewsSeoKeyWordCommand
/// </summary>
public abstract class NewsSeoKeyWordCommand : Command
{
    public Guid   Id          { get; set; }
    public string Name        { get; set; }
    public string Description { get; set; }
}