#region

#endregion

namespace ITC.Domain.Commands.ManageRole.Account;

public abstract class DepartmentOfEducationCommand : BaseIdentityCommand
{
#region Constructors

    protected DepartmentOfEducationCommand()
    {
    }


    protected DepartmentOfEducationCommand(string code,       string identifier, string name,  string userName,
                                           string password,   string fullName,   string email, string phoneNumber,
                                           string userTypeId, string avatar,     int    portalId) : this(name, email,
        fullName, phoneNumber, userTypeId, avatar)
    {
        Code       = code;
        Identifier = identifier;
        Password   = password;
        UserName   = userName;
        PortalId   = portalId;
    }

    protected DepartmentOfEducationCommand(string name,       string email, string fullName, string phoneNumber,
                                           string userTypeId, string avatar)
    {
        Name        = name;
        Email       = email;
        FullName    = fullName;
        PhoneNumber = phoneNumber;
        UserTypeId  = userTypeId;
        Avatar      = avatar;
    }

#endregion

#region Properties

    public string Avatar { get; protected set; }
    public string Code   { get; protected set; }
    public string Email  { get; protected set; }

    public string FullName   { get; protected set; }
    public string Identifier { get; set; }
    public string Manager    { get; protected set; }
    public string Name       { get; protected set; }
    public string Password   { get; protected set; }

    public string PhoneNumber { get; protected set; }

    // public int PortalId { get; protected set; }
    public string UserName   { get; protected set; }
    public string UserTypeId { get; protected set; }

#endregion
}