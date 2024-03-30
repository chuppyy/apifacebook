using System;
using NCore.Modals;

namespace ITC.Domain.Core.ModelShare.StudyManagers.NewsVia;

/// <summary>
///     [Model] Môn học
/// </summary>
public class NewsViaEventModel : PublishModal
{
    public Guid   Id      { get; set; }
    public string Code    { get; set; }
    public string Content { get; set; }
    public string Token   { get; set; }
    public string IdTkQc  { get; set; }
    public Guid   StaffId { get; set; }
}