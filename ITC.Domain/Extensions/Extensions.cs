#region

using System.Collections.Generic;

#endregion

namespace ITC.Domain.Extensions;

public static class Extensions
{
#region Methods

    public static bool AddRange<T>(this HashSet<T> @this, IEnumerable<T> items)
    {
        var allAdded                         = true;
        foreach (var item in items) allAdded &= @this.Add(item);
        return allAdded;
    }

#endregion
}

public class RoleIdentity
{
#region Static Fields and Constants

    public const string Administrator         = "SystemAdministrator";
    public const string DepartmentOfEducation = "DepartmentOfEducation";
    public const string EducationDepartment   = "EducationDepartment";
    public const string Department            = "Department";
    public const string Bussiness             = "Bussiness";

#endregion
}