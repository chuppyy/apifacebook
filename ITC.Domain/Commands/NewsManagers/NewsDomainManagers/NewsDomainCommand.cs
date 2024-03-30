#region

using System;
using ITC.Domain.Core.Commands;

#endregion

namespace ITC.Domain.Commands.NewsManagers.NewsDomainManagers;

/// <summary>
///     Command NewsDomainCommand
/// </summary>
public abstract class NewsDomainCommand : Command
{
    public Guid   Id        { get; set; }
    public string Name      { get; set; }
    public string DomainNew { get; set; }
    public string Description { get; set; }
}