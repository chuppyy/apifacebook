#region

using System;

#endregion

namespace ITC.Infra.CrossCutting.Identity.Models.QueryModel;

public class AccountModel
{
#region Properties

    public string          Avatar         { get; set; }
    public string          Email          { get; set; }
    public bool            EmailConfirmed { get; set; }
    public string          FullName       { get; set; }
    public string          Id             { get; set; }
    public DateTimeOffset? LockoutEnd     { get; set; }
    public string          PhoneNumber    { get; set; }

    public int    PortalId     { get; set; }
    public string Role         { get; set; }
    public string UserName     { get; set; }
    public string UserTypeId   { get; set; }
    public string UserTypeName { get; set; }

#endregion
}

public sealed class CustomDepartmentOfEducationModel
{
#region Properties

    public Guid   Id         { get; set; }
    public string Identifier { get; set; }
    public string UserId     { get; set; }

#endregion
}