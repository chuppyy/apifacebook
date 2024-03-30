using System;

namespace ITC.Domain.Core.ModelShare.NewsManagers.NewsAttackManagers;

/// <summary>
///     Modal phân trang danh sách đính kèm
/// </summary>
public abstract class NewsAttackPagingDto
{
    public Guid     Id             { get; set; }
    public DateTime AttackDateTime { get; set; }
    public bool     IsDownload     { get; set; }
    public string   FileName       { get; set; }
    public bool     IsLocal        { get; set; }
}