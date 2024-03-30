namespace ITC.Domain.Commands.ManageRole.Account;

public abstract class EducationDepartmentCommand : BaseIdentityCommand
{
#region Constructors

    protected EducationDepartmentCommand()
    {
    }


    protected EducationDepartmentCommand(string unitCode,   string identifier, string name,  string userName,
                                         string password,   string fullName,   string email, string phoneNumber,
                                         string userTypeId, string avatar,     int    portalId) : this(name, email,
        fullName, phoneNumber, userTypeId, avatar)
    {
        Code       = unitCode;
        Identifier = identifier;
        Password   = password;
        UserName   = userName;
        PortalId   = portalId;
    }

    protected EducationDepartmentCommand(string name,       string email, string fullName, string phoneNumber,
                                         string userTypeId, string avatar)
    {
        UserName    = name;
        Email       = email;
        FullName    = fullName;
        PhoneNumber = phoneNumber;
        UserTypeId  = userTypeId;
        Avatar      = avatar;
    }

#endregion

#region Properties

    public string Address { get; set; }
    public string Avatar  { get; protected set; }

    public string City     { get; set; }
    public string Code     { get; protected set; }
    public string District { get; set; }
    public string Email    { get; protected set; }
    public string Fax      { get; set; }

    public string FullName    { get; protected set; }
    public string Identifier  { get; protected set; }
    public string Password    { get; protected set; }
    public string Phone       { get; set; }
    public string PhoneNumber { get; protected set; }

    public string UserName   { get; protected set; }
    public string UserTypeId { get; protected set; }
    public string Ward       { get; set; }
    public string Website    { get; set; }

#endregion
}