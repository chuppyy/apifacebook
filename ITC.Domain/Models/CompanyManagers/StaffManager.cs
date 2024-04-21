using System;
using System.Collections.Generic;
using ITC.Domain.Core.Models;
using ITC.Domain.Models.SportManagers;
using NCore.Actions;

namespace ITC.Domain.Models.CompanyManagers;

/// <summary>
///     Nhân viên
/// </summary>
public class StaffManager : RootModel
{
    /// <summary>
    ///     Hàm dựng có tham số
    /// </summary>
    public StaffManager(Guid   id,       string name,              string description, Guid companyId, string phone,
                        string address,  string email,             Guid roomManagerId, string userId, Guid authorityId,
                        Guid   avatarId, Guid   userTypeManagerId, Guid projectId, string userCode, 
                        string createdBy = null)
        : base(id, createdBy)
    {
        StatusId = ActionStatusEnum.Active.Id;
        ProjectId = projectId;
        IsOwnerManagement = false;
        BirthDay = DateTime.Now;
        StaffAttackManagers = new List<StaffAttackManager>();
        Update(name, description, companyId, phone, address, email, roomManagerId, userId, authorityId, avatarId,
               userTypeManagerId, userCode, createdBy);
    }

    /// <summary>
    ///     Hàm dựng dùng cho phần quản lý VĐV
    /// </summary>
    public StaffManager(Guid      id,                  string name, string description, Guid companyId, string phone,
                        string    address,             string email, string userId, Guid avatarId, Guid projectId,
                        Guid      positionId,          Guid missionId, string schoolClass, string citizenIdentification,
                        string    identityNumberStudy, string nationPeople, bool isSportAthletics, int sexId,
                        DateTime? birthDay,            string schoolName, string createdBy = null)
        : base(id, createdBy)
    {
        StatusId            = ActionStatusEnum.Active.Id;
        IsSportAthletics    = isSportAthletics;
        ManagementId        = companyId;
        ProjectId           = projectId;
        RoomManagerId       = Guid.Empty;
        AuthorityId         = Guid.Empty;
        UserTypeManagerId   = Guid.Empty;
        UserId              = userId;
        StaffAttackManagers = new List<StaffAttackManager>();
        SportRegisters      = new List<SportRegister>();
        SportUpdate(name,         description, phone,       address, email,                 avatarId,
                    positionId,   missionId,   schoolClass, sexId,   citizenIdentification, identityNumberStudy,
                    nationPeople, birthDay,    schoolName,  createdBy);
    }

    /// <summary>
    ///     Hàm dựng dùng cho phần tài khoản quản trị đơn vị
    /// </summary>
    public StaffManager(Guid   id,          string name,              string description, Guid companyId, string phone,
                        string address,     string email,             string userId,      Guid avatarId, Guid projectId,
                        Guid   authorityId, bool   isOwnerManagement, string userCode,    string createdBy = null)
        : base(id, createdBy)
    {
        StatusId            = ActionStatusEnum.Active.Id;
        IsSportAthletics    = false;
        ProjectId           = projectId;
        RoomManagerId       = Guid.Empty;
        AuthorityId         = Guid.Empty;
        UserTypeManagerId   = Guid.Empty;
        PositionId          = Guid.Empty;
        UserId              = userId;
        IsStaff             = false;
        BirthDay            = DateTime.Now;
        StaffAttackManagers = new List<StaffAttackManager>();
        SportRegisters      = new List<SportRegister>();
        ProjectUpdate(name,     description, phone, address, email, avatarId, authorityId, isOwnerManagement, companyId,
                       createdBy);
    }

    /// <summary>
    ///     Hàm dựng mặc định
    /// </summary>
    protected StaffManager()
    {
    }

    /// <summary>
    ///     Tên
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Mã người dùng
    /// </summary>
    public string UserCode { get; set; }

    /// <summary>
    ///     Ghi chú
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    ///     Mã đơn vị
    /// </summary>
    public Guid ManagementId { get; set; }

    /// <summary>
    ///     Mã phòng ban
    /// </summary>
    public Guid RoomManagerId { get; set; }

    /// <summary>
    ///     Mã kiểu người dùng
    /// </summary>
    public Guid UserTypeManagerId { get; set; }

    /// <summary>
    ///     Mã người dùng hệ thống
    /// </summary>
    public string UserId { get; set; }

    /// <summary>
    ///     Điện thoại
    /// </summary>
    public string Phone { get; set; }

    /// <summary>
    ///     Địa chỉ
    /// </summary>
    public string Address { get; set; }

    /// <summary>
    ///     Email
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    ///     Quyền sử dụng
    /// </summary>
    public Guid AuthorityId { get; set; }

    /// <summary>
    ///     Ảnh đại diện
    /// </summary>
    public Guid AvatarId { get; set; }

    /// <summary>
    ///     Là dữ liệu thuộc bảng StaffManager
    /// </summary>
    public bool IsStaff { get; set; }

    /// <summary>
    ///     Thời gian đăng nhập gần nhất
    /// </summary>
    public DateTime? TimeConnectStart { get; set; }

    /// <summary>
    ///     Thời gian đăng xuất gần nhất
    /// </summary>
    public DateTime? TimeConnectEnd { get; set; }

    /// <summary>
    ///     Đang online// Sửa lại nếu là tk quản trị
    /// </summary>
    public bool IsOnline { get; set; }

    /// <summary>
    ///     Mã kết nối hệ thống
    /// </summary>
    public string ConnectionId { get; set; }

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
    ///     Là tài khoản quản trị cho đơn vị
    /// </summary>
    public bool IsOwnerManagement { get; set; }

    /// <summary>
    ///     Ngày sinh
    /// </summary>
    public DateTime? BirthDay { get; set; }

    /// <summary>
    ///     Liên kết khóa ngoại
    /// </summary>
    public virtual List<StaffAttackManager> StaffAttackManagers { get; set; }

    /// <summary>
    ///     Liên kết khóa ngoại
    /// </summary>
    public virtual List<SportRegister> SportRegisters { get; set; }

    public Guid? OwerId { get; set; }

    public double? Ratio { get; set; }

    public void Update(string name, string description, Guid company, string phone, string address, string email,
                       Guid   roomManagerId, string userId, Guid authorityId, Guid avatarId, Guid userTypeManagerId,
                       string userCode, string createdBy)
    {
        Name              = name;
        Description       = description;
        ManagementId      = company;
        Phone             = phone;
        Address           = address;
        Email             = email;
        RoomManagerId     = roomManagerId;
        UserId            = userId;
        AuthorityId       = authorityId;
        AvatarId          = avatarId;
        IsStaff           = true;
        UserTypeManagerId = userTypeManagerId;
        UserCode          = userCode;
        Update(createdBy);
    }

    /// <summary>
    ///     Hàm cập nhật dùng cho dự án VDV thể thao
    /// </summary>
    /// <param name="name"></param>
    /// <param name="description"></param>
    /// <param name="phone"></param>
    /// <param name="address"></param>
    /// <param name="email"></param>
    /// <param name="avatarId"></param>
    /// <param name="positionId"></param>
    /// <param name="missionId"></param>
    /// <param name="schoolClass"></param>
    /// <param name="sexId"></param>
    /// <param name="citizenIdentification"></param>
    /// <param name="identityNumberStudy"></param>
    /// <param name="nationPeople"></param>
    /// <param name="birthDay"></param>
    /// <param name="schoolName"></param>
    /// <param name="createdBy"></param>
    public void SportUpdate(string    name, string description, string phone, string address, string email,
                            Guid      avatarId, Guid positionId, Guid missionId, string schoolClass, int sexId,
                            string    citizenIdentification, string identityNumberStudy, string nationPeople,
                            DateTime? birthDay, string schoolName, string createdBy = null)
    {
        Name                  = name;
        Description           = description;
        Phone                 = phone;
        Address               = address;
        Email                 = email;
        AvatarId              = avatarId;
        IsStaff               = true;
        PositionId            = positionId;
        MissionId             = missionId;
        SchoolClass           = schoolClass;
        CitizenIdentification = citizenIdentification;
        IdentityNumberStudy   = identityNumberStudy;
        NationPeople          = nationPeople;
        SexId                 = sexId;
        BirthDay              = birthDay;
        SchoolName            = schoolName;
        SchoolName            = schoolName;
        Update(createdBy);
    }

    /// <summary>
    ///     Hàm cập nhật dùng cho quản trị dự án
    /// </summary>
    public void ProjectUpdate(string name,     string description, string phone, string address, string email,
                              Guid   avatarId, Guid   authorityId, bool   isOwnerManagement, Guid companyId,
                              string createdBy = null)
    {
        Name              = name;
        Description       = description;
        Phone             = phone;
        Address           = address;
        Email             = email;
        AvatarId          = avatarId;
        IsStaff           = true;
        AuthorityId       = authorityId;
        IsOwnerManagement = isOwnerManagement;
        ManagementId      = companyId;
        Update(createdBy);
    }
}