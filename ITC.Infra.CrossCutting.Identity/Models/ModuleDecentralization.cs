#region

using ITC.Domain.Core.Models;

#endregion

namespace ITC.Infra.CrossCutting.Identity.Models;

public class ModuleDecentralization : EntityString
{
#region Constructors

    //public ApplicationRole IdentityRole { get; private set; }
    public ModuleDecentralization()
    {
    }

    public ModuleDecentralization(string moduleId, string roleId) : this()
    {
        ModuleId = moduleId;
        RoleId   = roleId;
    }

    public ModuleDecentralization(string userTypeId, string moduleId, string roleId) : this()
    {
        UserTypeId = userTypeId;
        ModuleId   = moduleId;
        RoleId     = roleId;
    }

#endregion

#region Properties

    /// <summary>
    ///     Mã module
    /// </summary>
    public string ModuleId { get; }

    /// <summary>
    ///     Mã vai trò
    /// </summary>
    public string RoleId { get; }

    /// <summary>
    ///     Mã kiểu người dùng
    /// </summary>
    public string UserTypeId { get; }

#endregion
}