using System;
using NCore.Modals;

namespace ITC.Domain.Core.ModelShare.SystemManagers.SeverFileManagers;

/// <summary>
///     [Event-Modal] Thư mục
/// </summary>
public class FolderServerFileEvent : PublishModal
{
    public Guid   Id          { get; set; }
    public string Name        { get; set; }
    public string ParentId    { get; set; }
    public string Description { get; set; }
}