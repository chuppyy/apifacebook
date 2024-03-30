#region

using Microsoft.AspNetCore.Identity;

#endregion

namespace ITC.Infra.CrossCutting.Identity.Models;

public class ApplicationRole : IdentityRole
{
#region Properties

    public string Identity { get; set; }

#endregion

#region Constructors

    public ApplicationRole()
    {
    }

    public ApplicationRole(string name, string identity) : base(name)
    {
        Identity = identity;
    }

#endregion
}