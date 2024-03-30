#region

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ITC.Infra.CrossCutting.Identity.Authorization;

#endregion

namespace ITC.Application.ViewModels.ManageRole;

public class ModuleViewModel
{
#region Properties

    /// <summary>
    ///     Ngày tạo
    /// </summary>
    public DateTime CreateDate { get; }

    public string Description { get; set; }

    [Required(ErrorMessage = "The Name is Required")]

    public string GroupId { get; set; }

    public string Id { get; set; }

    [Required(ErrorMessage = "The Name is Required")]
    [MaxLength(255)]
    public string Identity { get; set; }

    [Required(ErrorMessage = "The Name is Required")]
    [MaxLength(255)]
    public string Name { get; set; }

    [Required(ErrorMessage = "The RoleIds is Required")]
    public List<string> RoleIds { get; set; }

    public List<CustomRoleViewModel> Roles   { get; set; }
    public List<TypeAudit>           Weights { get; set; }

#endregion
}

public class CustomRoleViewModel : ApplicationRoleViewModel
{
#region Properties

    public string ModuleId { get; set; }

#endregion
}