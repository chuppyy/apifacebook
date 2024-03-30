#region

using System.Collections.Generic;
using ITC.Domain.Commands.ManageRole;
using ITC.Infra.CrossCutting.Identity.Models.QueryModel;

#endregion

namespace ITC.Infra.CrossCutting.Identity.Commands;

public abstract class UserTypeCommand : BaseIdentityCommand
{
#region Methods

    public void AddRoles(List<RoleDTO> roles)
    {
        Roles.AddRange(roles);
    }

#endregion

#region Constructors

    protected UserTypeCommand()
    {
    }

    protected UserTypeCommand(string        name, string description, string roleId, string selectedItems,
                              List<RoleDTO> roles)
    {
        Description   = description;
        Name          = name;
        RoleId        = roleId;
        SelectedItems = selectedItems;
        Roles         = roles;
    }

#endregion

#region Properties

    public string        Description   { get; set; }
    public string        Name          { get; protected set; }
    public string        RoleId        { get; protected set; }
    public List<RoleDTO> Roles         { get; protected set; }
    public string        SelectedItems { get; protected set; }

#endregion
}