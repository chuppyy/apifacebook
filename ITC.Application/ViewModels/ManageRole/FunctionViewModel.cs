#region

using System.ComponentModel.DataAnnotations;

#endregion

namespace ITC.Application.ViewModels.ManageRole;

public class FunctionViewModel
{
#region Properties

    public string Description  { get; set; }
    public string FunctionName { get; set; }
    public string Id           { get; set; }

    [Required(ErrorMessage = "The ModuleId is Required")]
    public string ModuleId { get; set; }

    public string ModuleName { get; set; }
    public string Name       { get; set; }
    public int    Weight     { get; set; }

#endregion
}