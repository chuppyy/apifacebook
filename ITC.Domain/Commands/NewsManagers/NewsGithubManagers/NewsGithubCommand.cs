#region

using System;
using ITC.Domain.Core.Commands;

#endregion

namespace ITC.Domain.Commands.NewsManagers.NewsGithubManagers;

/// <summary>
///     Command NewsGithubCommand
/// </summary>
public abstract class NewsGithubCommand : Command
{
    public Guid   Id          { get; set; }
    public string Code        { get; set; }
    public string Name        { get; set; }
    public string Description { get; set; }
}