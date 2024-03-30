using System;
using NCore.Modals;

namespace ITC.Domain.Core.ModelShare.CompanyManager.StaffManagers;

/// <summary>
///     Event StaffManager
/// </summary>
public class StaffManagerEventModel : PublishModal
{
    public Guid   Id                 { get; set; }
    public string UserCode { get;           set; }
    public string Name               { get; set; }
    public string Description        { get; set; }
    public string Phone              { get; set; }
    public string Address            { get; set; }
    public string Email              { get; set; }
    public Guid   RoomManagerId      { get; set; }
    public string UserName           { get; set; }
    public string Password           { get; set; }
    public Guid   AuthorityManagerId { get; set; }
    public Guid   AvatarId           { get; set; }
    public Guid   UserTypeManagerId  { get; set; }
}

/// <summary>
///     Event ProjectAccountManager
/// </summary>
public class ProjectAccountManagerEventModel : PublishModal
{
    public Guid   Id           { get; set; }
    public string Name         { get; set; }
    public string Description  { get; set; }
    public string Phone        { get; set; }
    public string Address      { get; set; }
    public string Email        { get; set; }
    public Guid   AvatarId     { get; set; }
    public string UserName     { get; set; }
    public Guid   ProjectId    { get; set; }
    public Guid   ManagementId { get; set; }
    public Guid   AuthorityId  { get; set; }
}

public class SportStaffRegisterModel
{
    public Guid   Id                { get; set; }
    public Guid   SubjectId         { get; set; }
    public Guid   SubjectDetailId   { get; set; }
    public string Name              { get; set; }
    public string SubjectName       { get; set; }
    public string SubjectDetailName { get; set; }
    public int    LevelId           { get; set; }
    public int    Position          { get; set; }
    public int    SexId             { get; set; }
    public bool   IsTeam            { get; set; }
}