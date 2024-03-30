using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using NCore.Actions;
using NCore.Modals;

namespace ITC.Domain.Core.ModelShare.CompanyManager.StaffManagers;

/// <summary>
///     Modal phân trang nhân viên mặc định
/// </summary>
public class StaffManagerPagingDefaultDto
{
    public              Guid                       Id           { get; set; }
    public              string                     Name         { get; set; }
    public              string                     Description  { get; set; }
    public              string                     Phone        { get; set; }
    public              string                     Address      { get; set; }
    public              string                     Email        { get; set; }
    public              string                     UserName     { get; set; }
    public              string                     OwnerId      { get; set; }
    public              DateTime                   OwnerCreated { get; set; }
    [JsonIgnore] public int                        StatusId     { get; set; }
    public              string                     StatusName   => ActionStatusEnum.GetById(StatusId)?.Name;
    public              string                     StatusColor  => ActionStatusColorEnum.GetById(StatusId)?.Name;
    public              List<ActionAuthorityModel> Actions      { get; set; }
    [JsonIgnore] public int                        TotalRecord  { get; set; }
}

/// <summary>
///     Modal phân trang nhân viên
/// </summary>
public class StaffManagerPagingDto : StaffManagerPagingDefaultDto
{
    public string   RoomManagerName      { get; set; }
    public string   AuthorityManagerName { get; set; }
    public string   MissionName          { get; set; }
    public string   PositionName         { get; set; }
    public string   SchoolName           { get; set; }
    public string   SchoolClass          { get; set; }
    public DateTime BirthDay             { get; set; }
    public int      SexId                { get; set; }
    public Guid     AvatarId             { get; set; }
    public bool     IsLocal              { get; set; }
}

/// <summary>
///     Modal phân trang nhân viên
/// </summary>
public class SportStaffManagerPagingDto : StaffManagerPagingDefaultDto
{
    public string   MissionName  { get; set; }
    public string   PositionName { get; set; }
    public string   SchoolName   { get; set; }
    public string   SchoolClass  { get; set; }
    public DateTime BirthDay     { get; set; }
    public int      SexId        { get; set; }
    public Guid     AvatarId     { get; set; }
    public bool     IsLocal      { get; set; }

    /// <summary>
    ///     Danh sach dang ky thi dau
    /// </summary>
    public List<SportStaffRegisterModel> RegisterModels { get; set; }

    /// <summary>
    ///     Danh sach hồ sơ đính kèm
    /// </summary>
    public List<StaffAttackModelPaging> AttackModels { get; set; }
}

/// <summary>
///     Người dùng là tài khoản quản trị
/// </summary>
public class StaffProjectAccountDto : StaffManagerPagingDefaultDto
{
    public string AuthorityManagerName { get; set; }
}

public class StaffAttackModelPaging
{
    public Guid   Id        { get; set; }
    public bool   IsLocal   { get; set; }
    public int    FileType  { get; set; }
    public int    GroupFile { get; set; }
    public string FileName  { get; set; }
}