#region

using System.Collections.Generic;

#endregion

namespace ITC.Infra.CrossCutting.Identity.Models.QueryModel;

public class TreeViewNode
{
#region Properties

    public IEnumerable<TreeViewNode> children { get; set; }
    public string                    id       { get; set; }
    public string                    key      { get; set; }
    public State                     state    { get; set; }
    public string                    text     { get; set; }
    public string                    valueId  { get; set; }

    public int weight { get; set; }

#endregion
}

public class TreeViewNodeParent : TreeViewNode
{
#region Properties

    public string       parent  { get; set; }
    public List<string> parents { get; set; }

#endregion
}

public class State
{
#region Properties

    public bool opened   { get; set; }
    public bool selected { get; set; }

#endregion
}

/// <summary>
///     Vai trò
/// </summary>
public class RoleDTO
{
#region Properties

    /// <summary>
    ///     Danh sách module
    /// </summary>
    public List<ModuleDTO> Modules { get; set; }

    /// <summary>
    ///     Mã vai trò
    /// </summary>
    public string RoleId { get; set; }

#endregion
}

/// <summary>
///     Module
/// </summary>
public class ModuleDTO
{
#region Properties

    /// <summary>
    ///     Danh sách chức năng
    /// </summary>
    public List<FunctionDTO> Functions { get; set; }

    /// <summary>
    ///     Mã module
    /// </summary>
    public string ModuleId { get; set; }

#endregion
}

/// <summary>
///     Chức năng
/// </summary>
public class FunctionDTO
{
#region Properties

    /// <summary>
    ///     Mã chức năng
    /// </summary>
    public string FunctionId { get; set; }

#endregion
}