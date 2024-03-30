#region

using System;
using ITC.Domain.Core.Commands;

#endregion

namespace ITC.Domain.Commands.CompanyManagers.StaffManager;

/// <summary>
///     Command StaffManagerCommand
/// </summary>
public abstract class StaffManagerCommand : Command
{
    public Guid   Id                { get; set; }
    public string UserCode          { get; set; }
    public string Name              { get; set; }
    public string Description       { get; set; }
    public string Phone             { get; set; }
    public string Address           { get; set; }
    public string Email             { get; set; }
    public Guid   RoomManagerId     { get; set; }
    public string UserName          { get; set; }
    public string Password          { get; set; }
    public Guid   AuthorityId       { get; set; }
    public Guid   AvatarId          { get; set; }
    public Guid   UserTypeManagerId { get; set; }

    /// <summary>
    ///     Lớp học
    /// </summary>
    public string SchoolClass { get; set; }

    /// <summary>
    ///     Trường học
    /// </summary>
    public string SchoolName { get; set; }

    /// <summary>
    ///     Căn cước công dân
    /// </summary>
    public string CitizenIdentification { get; set; }

    /// <summary>
    ///     Là vận động viên
    /// </summary>
    public bool IsSportAthletics { get; set; }

    /// <summary>
    ///     Mã dự án
    /// </summary>
    public Guid ProjectId { get; set; }

    /// <summary>
    ///     Nhiệm vụ
    /// </summary>
    public Guid MissionId { get; set; }

    /// <summary>
    ///     Chức vụ
    /// </summary>
    public Guid PositionId { get; set; }

    /// <summary>
    ///     Mã định danh học sinh
    /// </summary>
    public string IdentityNumberStudy { get; set; }

    /// <summary>
    ///     Dân tộc
    /// </summary>
    public string NationPeople { get; set; }

    /// <summary>
    ///     Gioi tinh
    /// </summary>
    public int SexId { get; set; }

    /// <summary>
    ///     Ngày sinh
    /// </summary>
    public string BirthDay { get; set; }
}