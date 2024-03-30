#region

using ITC.Domain.Core.Models;

#endregion

namespace ITC.Infra.CrossCutting.Identity.Models;

public class ModuleRole : EntityString
{
#region Constructors

    public ModuleRole()
    {
    }

    public ModuleRole(string roleId)
    {
        RoleId = roleId;
    }

#endregion

#region Properties

    //public ApplicationRole IdentityRole { get; private set; }
    public Module Module { get; }

    /// <summary>
    ///     Mã module
    /// </summary>
    public string ModuleId { get; }

    /// <summary>
    ///     Mã vai trò
    /// </summary>
    public string RoleId { get; }

#endregion
}