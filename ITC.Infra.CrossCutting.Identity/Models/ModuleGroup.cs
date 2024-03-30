#region

using ITC.Domain.Core.Models;

#endregion

namespace ITC.Infra.CrossCutting.Identity.Models;

public class ModuleGroup : EntityString
{
#region Constructors

    public ModuleGroup(string name, string description) : this()
    {
        Name        = name;
        Description = description;
    }

    protected ModuleGroup()
    {
    }

#endregion

#region Properties

    public string Description { get; set; }
    public bool   IsDeleted   { get; set; }
    public string Name        { get; set; }

#endregion

#region Methods

    public void Delete()
    {
        IsDeleted = true;
    }

    public void Update(string name, string decription)
    {
        Name        = name;
        Description = decription;
    }

#endregion
}