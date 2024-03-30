#region

using System;
using ITC.Domain.Core.Models;

#endregion

namespace ITC.Infra.CrossCutting.Identity.Models;

public class Portal : EntityInt
{
#region Constructors

    public Portal()
    {
        CreateDate = DateTime.UtcNow;
        Identity   = Guid.NewGuid().ToString();
    }

    public Portal(string name, bool isDepartmentOfEducation) : this()
    {
        Name                    = name;
        IsDepartmentOfEducation = isDepartmentOfEducation;
    }

#endregion

#region Properties

    public DateTime CreateDate              { get; }
    public string   Identity                { get; }
    public bool     IsDeleted               { get; private set; }
    public bool     IsDepartmentOfEducation { get; }
    public string   Name                    { get; private set; }

#endregion

#region Methods

    public void SetDelete(bool isDelete)
    {
        IsDeleted = isDelete;
    }

    public void UpdatePortal(string name)
    {
        Name = name;
    }

#endregion
}