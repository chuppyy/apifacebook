#region

using System.ComponentModel.DataAnnotations;

#endregion

namespace ITC.Application.ViewModels.ManageRole;

public class ApplicationRoleViewModel
{
#region Properties

    public string Id { get; set; }

    [Required(ErrorMessage = "The Identity is Required")]
    public string Identity { get; set; }

    [Required(ErrorMessage = "The Name is Required")]
    public string Name { get; set; }

#endregion
}