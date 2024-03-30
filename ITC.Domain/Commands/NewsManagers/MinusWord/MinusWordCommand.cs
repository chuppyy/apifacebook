#region

using System;
using ITC.Domain.Core.Commands;

#endregion

namespace ITC.Domain.Commands.NewsManagers.MinusWord;

/// <summary>
///     Command danh sách môn học
/// </summary>
public abstract class MinusWordCommand : Command
{
    public Guid   Id          { get; set; }
    public string Root        { get; set; }
    public string Replace     { get; set; }
    public string Description { get; set; }
}