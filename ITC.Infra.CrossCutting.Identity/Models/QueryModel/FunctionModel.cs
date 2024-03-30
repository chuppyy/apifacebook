namespace ITC.Infra.CrossCutting.Identity.Models.QueryModel;

public class FunctionModel
{
#region Properties

    /// <summary>
    ///     Mô tả
    /// </summary>
    public string Description { get; set; }

    public string FunctionName { get; set; }
    public string Id           { get; set; }


    /// <summary>
    ///     Trạng thái sử dụng
    /// </summary>
    public bool IsActivation { get; set; }

    public string ModuleId   { get; set; }
    public string ModuleName { get; set; }

    /// <summary>
    ///     Tên
    /// </summary>
    public string Name { get; set; }

    public int Weight { get; set; }

#endregion
}