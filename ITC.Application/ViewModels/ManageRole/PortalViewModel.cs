#region

using System.ComponentModel.DataAnnotations;

#endregion

namespace ITC.Application.ViewModels.ManageRole;

public class PortalViewModel
{
#region Properties

    public int Id { get; set; }

    [Required(ErrorMessage = "The Identity is Required")]
    public string Identifier { get; set; }

    public string Identity                { get; set; }
    public bool   IsDepartmentOfEducation { get; set; } = true;

    [Required(ErrorMessage = "The Name is Required")]
    public string Name { get; set; }

    [Required(ErrorMessage = "The UnitCode is Required")]
    public string UnitCode { get; set; }

    public UserViewModel UserViewModel { get; set; }

#endregion
}

public class PortalEditViewModel
{
#region Properties

    public int    Id       { get; set; }
    public string Identity { get; set; }

    [Required(ErrorMessage = "The Name is Required")]
    public string Name { get; set; }

    public AccountEditViewModel UserViewModel { get; set; }

#endregion
}