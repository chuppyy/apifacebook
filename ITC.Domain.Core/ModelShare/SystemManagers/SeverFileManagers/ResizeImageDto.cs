using System;

namespace ITC.Domain.Core.ModelShare.SystemManagers.SeverFileManagers;

/// <summary>
///     Trả về dữ liệu Resize-Image cho FE
/// </summary>
public class ResizeImageDto
{
    public Guid   Id       { get; set; }
    public bool   IsLocal  { get; set; }
    public int    FileType { get; set; }
    public int    TypeId   { get; set; }
    public string Name     { get; set; }
    public string FilePath { get; set; }
}