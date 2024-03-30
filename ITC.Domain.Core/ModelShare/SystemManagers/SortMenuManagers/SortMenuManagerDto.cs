using System;

namespace ITC.Domain.Core.ModelShare.SystemManagers.SortMenuManagers;

/// <summary>
///     Modal phân trang sắp xếp chức năng
/// </summary>
public class SortMenuManagerDto
{
    public Guid   Id           { get; set; }
    public Guid   MenuId       { get; set; }
    public string Name         { get; set; }
    public string RootName     { get; set; }
    public int    Position     { get; set; }
    public int    RootPosition { get; set; }
}