#region

using System.Collections.Generic;

#endregion

namespace ITC.Domain.Commands.ManageRole.OfficalAccount;

public abstract class PersonnelAccountCommand : BaseIdentityCommand
{
#region Constructors

    protected PersonnelAccountCommand()
    {
    }

    protected PersonnelAccountCommand(string password, string userTypeId, List<string> staffRecords,
                                      string userName, bool   isAutoGen)
    {
        Password          = password;
        UserTypeId        = userTypeId;
        StaffRecords      = StaffRecords;
        IsAutoGenUserName = isAutoGen;
        UserName          = userName;
    }

#endregion

#region Properties

    public bool         IsAutoGenUserName { get; set; }
    public string       Password          { get; protected set; }
    public List<string> StaffRecords      { get; set; }

    public string UserName { get; set; }

    public string UserTypeId { get; protected set; }

#endregion
}