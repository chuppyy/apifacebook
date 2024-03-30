namespace ITC.Infra.CrossCutting.Identity.Models.QueryModel;

public class RoleModel
{
#region Properties

    public string Id       { get; set; }
    public string Identity { get; set; }
    public string Name     { get; set; }

#endregion
}

public class CustomRoleModel : RoleModel
{
#region Properties

    public string ModuleId { get; set; }

#endregion
}