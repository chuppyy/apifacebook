#region

using System.Collections.Generic;
using ITC.Infra.CrossCutting.Identity.Authorization;

#endregion

namespace ITC.Service.API.Models.ManageRole;

/// <summary>
/// </summary>
public class FunctionData
{
#region Constructors

    /// <summary>
    /// </summary>
    public FunctionData()
    {
        Functions = new List<DropdowListItem>();
        Functions.Add(new DropdowListItem(TypeAudit.View,     TypeAudit.View.ToString()));
        Functions.Add(new DropdowListItem(TypeAudit.Add,      $"{TypeAudit.Add.ToString()}"));
        Functions.Add(new DropdowListItem(TypeAudit.Edit,     $"{TypeAudit.Edit.ToString()}"));
        Functions.Add(new DropdowListItem(TypeAudit.Delete,   $"{TypeAudit.Delete.ToString()}"));
        Functions.Add(new DropdowListItem(TypeAudit.Approved, $"{TypeAudit.Approved.ToString()}"));
    }

#endregion

#region Properties

    /// <summary>
    /// </summary>
    public List<DropdowListItem> Functions { get; set; }

#endregion
}

/// <summary>
/// </summary>
public class DropdowListItem
{
#region Constructors

    /// <summary>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="name"></param>
    public DropdowListItem(TypeAudit id, string name)
    {
        Id   = id;
        Name = name;
    }

#endregion

#region Properties

    /// <summary>
    /// </summary>
    public TypeAudit Id { get; set; }

    /// <summary>
    /// </summary>
    public string Name { get; set; }

#endregion
}