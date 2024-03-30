#region

using ITC.Domain.Commands.ManageRole.Account;

#endregion

namespace ITC.Infra.CrossCutting.Identity.Commands.Portal;

public abstract class PortalCommand : BaseIdentityIntCommand
{
#region Constructors

    protected PortalCommand()
    {
    }

    protected PortalCommand(string name)
    {
        Name = name;
    }

    protected PortalCommand(string      name, bool isDepartmentOfEducation, string unitCode, string identifier,
                            UserCommand userCommand)
    {
        Name = name;

        IsDepartmentOfEducation = isDepartmentOfEducation;
        UnitCode                = unitCode;
        Identifier              = identifier;
        UserCommand             = userCommand;
    }

    protected PortalCommand(string name, string identity)
    {
        Name     = name;
        Identity = identity;
    }

#endregion

#region Properties

    public string BudgetRelationCode { get; set; }
    public string Identifier         { get; protected set; }
    public string Identity           { get; protected set; }

    public bool   IsDepartmentOfEducation { get; protected set; }
    public string Name                    { get; protected set; }
    public string UnitCode                { get; protected set; }

    /// <summary>
    ///     Loại hình đơn vị
    /// </summary>
    public string UnitType { get; set; }

    public UserCommand UserCommand { get; protected set; }

#endregion
}