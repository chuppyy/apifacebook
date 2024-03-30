namespace ITC.Domain.Commands.ManageRole.Account;

public abstract class UserCommand : BaseIdentityCommand
{
#region Constructors

    protected UserCommand()
    {
    }

    protected UserCommand(string email, string fullName, string phoneNumber)
    {
        Email       = email;
        FullName    = fullName;
        PhoneNumber = phoneNumber;
    }

    protected UserCommand(string userName, string password, string fullName, string email, string phoneNumber) :
        this(email, fullName, phoneNumber)
    {
        UserName = userName;
        Password = password;
    }

#endregion

#region Properties

    public string Email { get; protected set; }

    public string FullName    { get; protected set; }
    public string Password    { get; protected set; }
    public string PhoneNumber { get; protected set; }
    public string UserName    { get; protected set; }

#endregion
}

public abstract class AccountCommand : UserCommand
{
#region Constructors

    protected AccountCommand()
    {
    }

    protected AccountCommand(string email, string fullName, string phoneNumber, string userTypeId, string avatar) :
        base(email, fullName, phoneNumber)
    {
        UserTypeId = userTypeId;
        Avatar     = avatar;
    }

    protected AccountCommand(string userName,   string password, string fullName, string email, string phoneNumber,
                             string userTypeId, string avatar) : base(userName, password, fullName, email,
                                                                      phoneNumber)
    {
        UserTypeId = userTypeId;
        Avatar     = avatar;
    }

#endregion

#region Properties

    public string Avatar     { get; protected set; }
    public string UserTypeId { get; protected set; }

#endregion
}