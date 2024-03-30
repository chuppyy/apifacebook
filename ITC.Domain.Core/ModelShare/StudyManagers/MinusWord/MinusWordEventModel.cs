using System;
using NCore.Modals;

namespace ITC.Domain.Core.ModelShare.StudyManagers.MinusWord;

/// <summary>
///     [Model] Môn học
/// </summary>
public class MinusWordEventModel : PublishModal
{
    public Guid   Id          { get; set; }
    public string Root        { get; set; }
    public string Replace     { get; set; }
    public string Description { get; set; }
}