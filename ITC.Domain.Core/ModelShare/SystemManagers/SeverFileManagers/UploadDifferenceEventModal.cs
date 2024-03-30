using System;
using NCore.Modals;

namespace ITC.Domain.Core.ModelShare.SystemManagers.SeverFileManagers;

/// <summary>
///     [Event-Modal] Upload từ link khác
/// </summary>
public class UploadDifferenceEventModal : PublishModal
{
    public Guid   Id          { get; set; }
    public string Name        { get; set; }
    public string ParentId    { get; set; }
    public string Description { get; set; }
    public int    FileType    { get; set; }
    public int    VideoType   { get; set; }
    public string Link        { get; set; }
}