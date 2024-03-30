#region

using ITC.Application.ViewModels.ManageRole;

#endregion

namespace ITC.Service.API.Models.ManageRole;

/// <summary>
/// </summary>
public class ManageRoleIndexViewModel
{
#region Properties

    /// <summary>
    /// </summary>
    public string Keyword { get; set; }

    // public IPagedList<ApplicationRole> PagedRoles { get; set; }
    /// <summary>
    /// </summary>
    public ApplicationRoleViewModel Role { get; set; }

#endregion
}