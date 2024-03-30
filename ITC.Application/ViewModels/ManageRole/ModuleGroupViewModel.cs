#region

using System.ComponentModel.DataAnnotations;

#endregion

namespace ITC.Application.ViewModels.ManageRole;

public class ModuleGroupViewModel
{
#region Properties

    public string Description { get; set; }
    public string Id          { get; set; }

    [Required(ErrorMessage = "Tên không để trống")]
    public string Name { get; set; }

#endregion
}