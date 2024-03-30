using System;
using NCore.Modals;

namespace ITC.Domain.Core.ModelShare.StudyManagers.SubjectTypeManagers;

/// <summary>
///     [Model] nhận dữ liệu loại môn học từ FE
/// </summary>
public class SubjectTypeManagerEventModel : PublishModal
{
    public Guid   Id          { get; set; }
    public string Name        { get; set; }
    public string Description { get; set; }
    public int    GroupTable  { get; set; }
    public Guid   AvatarId    { get; set; }
}