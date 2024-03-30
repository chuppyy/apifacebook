#region

using ITC.Domain.Core.Models;

#endregion

namespace ITC.Infra.CrossCutting.Identity.Models;

public class FunctionRole : EntityString
{
#region Constructors

    //public ApplicationRole IdentityRole { get; private set; }
    public FunctionRole()
    {
    }

    public FunctionRole(string roleId)
    {
        RoleId = roleId;
    }

#endregion

#region Properties

    public virtual Function Function { get; set; }

    /// <summary>
    ///     Mã chức năng
    /// </summary>
    public string FunctionId { get; }

    /// <summary>
    ///     Mã vai trò
    /// </summary>
    public string RoleId { get; }

#endregion
}