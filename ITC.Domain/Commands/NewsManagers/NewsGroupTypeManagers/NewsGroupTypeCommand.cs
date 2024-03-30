#region

using System;
using ITC.Domain.Core.Commands;

#endregion

namespace ITC.Domain.Commands.NewsManagers.NewsGroupTypeManagers;

/// <summary>
///     Command NewsGroupTypeCommand
/// </summary>
public abstract class NewsGroupTypeCommand : Command
{
    public Guid   Id          { get; set; }
    public string Name        { get; set; }
    public string Description { get; set; }
    public string MetaTitle   { get; set; }
}