using System;
using System.Collections.Generic;
using NCore.Modals;

namespace ITC.Domain.Core.ModelShare.CompanyManager.StaffManagers;

/// <summary>
///     Event SportStaffManager
/// </summary>
public class SportStaffManagerEventModel : PublishModal
{
    public Guid   Id          { get; set; }
    public string Name        { get; set; }
    public string Description { get; set; }
    public string Phone       { get; set; }
    public string Address     { get; set; }
    public string Email       { get; set; }
    public Guid   AvatarId    { get; set; }

    /// <summary>
    ///     Gioi tinh
    /// </summary>
    public int SexId { get; set; }

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
    ///     Ngày sinh
    /// </summary>
    public string BirthDay { get; set; }

    /// <summary>
    ///     Danh sach dang ky thi dau
    /// </summary>
    public List<SportStaffRegisterModel> RegisterModels { get; set; }

    /// <summary>
    ///     Danh sách file đính kèm
    /// </summary>
    public List<Guid> AttackModels { get; set; }
}

public class SportStaffManagerGetByIdModal : SportStaffManagerEventModel
{
    public bool IsLocal { get; set; }
}