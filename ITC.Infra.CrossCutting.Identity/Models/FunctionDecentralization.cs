#region

using ITC.Domain.Core.Models;

#endregion

namespace ITC.Infra.CrossCutting.Identity.Models;

public class FunctionDecentralization : EntityString
{
#region Constructors

    //public ApplicationRole IdentityRole { get; private set; }

    public FunctionDecentralization()
    {
    }

    public FunctionDecentralization(string functionId, string roleId)
    {
        FunctionId = functionId;
        RoleId     = roleId;
    }

    public FunctionDecentralization(string userTypeId, string functionId, string roleId)
    {
        UserTypeId = userTypeId;
        FunctionId = functionId;
        RoleId     = roleId;
    }

#endregion

#region Properties

    /// <summary>
    ///     Mã chức năng
    /// </summary>
    public string FunctionId { get; }

    /// <summary>
    ///     Mã vai trò
    /// </summary>
    public string RoleId { get; }

    /// <summary>
    ///     Mã Kiểu người dùng
    /// </summary>
    public string UserTypeId { get; }

#endregion
}