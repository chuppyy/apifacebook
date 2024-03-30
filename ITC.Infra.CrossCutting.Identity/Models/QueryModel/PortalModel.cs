namespace ITC.Infra.CrossCutting.Identity.Models.QueryModel;

public class PortalModel
{
#region Properties

    /// <summary>
    ///     Mã
    /// </summary>
    public int Id { get; set; }

    public string Identity { get; set; }

    public bool IsDepartmentOfEducation { get; set; }

    /// <summary>
    ///     Tên
    /// </summary>
    public string Name { get; set; }

#endregion
}