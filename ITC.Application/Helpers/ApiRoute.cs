namespace ITC.Application.Helpers;

public class EducationDepartmentApi
{
#region Static Fields and Constants

    // Education Department
    public const string Route                               = "EducationDepartment/";
    public const string Get                                 = "education-department";
    public const string Post                                = "education-department";
    public const string Put                                 = "education-department";
    public const string Delete                              = "education-department";
    public const string EducationDepartmentByManagement     = "education-department-by-management";
    public const string CountEducationDepartment            = "count-education-department";
    public const string CountAllEducationDepartment         = "count-all-education-department";
    public const string CheckExistedCodeEducationDepartment = "check-existed-code-education-department";
    public const string PutInfo                             = "education-department-info";
    public const string SampleFile                          = "sample-file";

#endregion
}

public class DepartmentApi
{
#region Static Fields and Constants

    // Department
    public const string Route                      = "Department/";
    public const string Get                        = "department";
    public const string Post                       = "department";
    public const string Put                        = "department";
    public const string Delete                     = "department";
    public const string DepartmentByManagement     = "department-by-management";
    public const string CountDepartment            = "count-department";
    public const string CountAllDepartment         = "count-all-department";
    public const string CheckExistedCodeDepartment = "check-existed-code-department";
    public const string PutInfo                    = "department-info";

#endregion
}

public class UnitApi
{
#region Static Fields and Constants

    // Department
    public const string Route    = "Unit/";
    public const string Get      = "unit";
    public const string GetCount = "count-unit";
    public const string GetById  = "get-by-userId";

#endregion
}

public class EducationLevelApi
{
#region Static Fields and Constants

    public const string Route = "EducationLevel/";
    public const string Get   = "get-all";

#endregion
}

public class SchoolApi
{
#region Static Fields and Constants

    public const string Route                  = "School/";
    public const string Get                    = "school";
    public const string Post                   = "school";
    public const string Put                    = "school";
    public const string Delete                 = "school";
    public const string CountSchool            = "count-school";
    public const string CountAllSchool         = "count-all-school";
    public const string CheckExistedCodeSchool = "check-existed-code-school";
    public const string PutInfo                = "school-info";
    public const string SampleFile             = "sample-file";

#endregion
}

public class DOEApi
{
#region Static Fields and Constants

    public const string Route       = "DepartmentOfEducation/";
    public const string Get         = "department-of-education";
    public const string GetByPortal = "department-of-education-by-portal";
    public const string Post        = "department-of-education";
    public const string Put         = "department-of-education";
    public const string Delete      = "department-of-education";
    public const string GetCount    = "count-department-of-education";
    public const string PutInfo     = "department-of-education-info";

#endregion
}

public class UserTypeApi
{
#region Static Fields and Constants

    public const string Route         = "UserType/";
    public const string Get           = "user-type";
    public const string Post          = "user-type";
    public const string Put           = "user-type";
    public const string Delete        = "user-type";
    public const string GetCount      = "count-user-type";
    public const string GetPermission = "permission";
    public const string GetExcept     = "user-type-except";

#endregion
}

public class OfficalAccountApi
{
#region Static Fields and Constants

    public const string Route                   = "OfficalAccount/";
    public const string Get                     = "offical-account";
    public const string GetInfo                 = "offical-account-info";
    public const string GetByOfficalProfile     = "get-by-officalprofile";
    public const string Post                    = "offical-account";
    public const string Put                     = "offical-account";
    public const string PutInfo                 = "offical-account-info";
    public const string Delete                  = "offical-account";
    public const string GetCount                = "count-offical-account";
    public const string GetUserType             = "user-type";
    public const string GetProfileNoneAccount   = "profile-none-account";
    public const string CountProfileNoneAccount = "count-profile-none-account";

#endregion
}

public class AccountApi
{
#region Static Fields and Constants

    public const string Route           = "Account/";
    public const string Get             = "account";
    public const string Post            = "account";
    public const string Put             = "account";
    public const string Delete          = "ccount";
    public const string GetCount        = "count-account";
    public const string GetUserType     = "user-type";
    public const string Active          = "active-account";
    public const string UnActive        = "disable-account";
    public const string Lock            = "lock-account";
    public const string UnLock          = "unlock-account";
    public const string UserType        = "user-type";
    public const string ReissuePassword = "reissue-password";
    public const string AddEmail        = "add-email";
    public const string ConfirmEmail    = "confirm-email";
    public const string SuperManagement = "super-management";
    public const string LockoutEndDate  = "lockout-end-date";

#endregion
}

public class PortalApi
{
#region Static Fields and Constants

    public const string Route    = "Portal/";
    public const string Get      = "portal";
    public const string Post     = "portal";
    public const string Put      = "portal";
    public const string Delete   = "portal";
    public const string GetCount = "count-portal";

#endregion
}

public class ModuleGroupApi
{
#region Static Fields and Constants

    public const string Route    = "ModuleGroup/";
    public const string Get      = "module-group";
    public const string Post     = "module-group";
    public const string Put      = "module-group";
    public const string Delete   = "module-group";
    public const string GetCount = "count-module-group";

#endregion
}

//Sử dụng tài sản
public class UseEquipmentApi
{
#region Static Fields and Constants

    public const string Route        = "UseEquipment/";
    public const string Get          = "use-equipment";
    public const string Post         = "use-equipment";
    public const string Put          = "use-equipment";
    public const string Delete       = "use-equipment";
    public const string GetCount     = "count-use-equipment";
    public const string GetEquipment = "get-equipment";
    public const string GetYear      = "get-year";

#endregion
}

//Khai thác tài sản
public class ExploitEquipmentApi
{
#region Static Fields and Constants

    public const string Route    = "ExploitEquipment/";
    public const string Get      = "exploit-equipment";
    public const string Post     = "exploit-equipment";
    public const string Put      = "exploit-equipment";
    public const string Delete   = "exploit-equipment";
    public const string GetCount = "count-exploit-equipment";
    public const string Report   = "report";

#endregion
}

//Kế hoạch mua sắm
public class PlanApi
{
#region Static Fields and Constants

    public const string Route        = "Plan/";
    public const string Get          = "plan";
    public const string Post         = "plan";
    public const string Put          = "plan";
    public const string Delete       = "plan";
    public const string GetCount     = "count-plan";
    public const string GetEquipment = "equipment";
    public const string GetYear      = "year";
    public const string GetPlans     = "plans";
    public const string Report       = "report";

#endregion
}

public class GeneralApi
{
#region Static Fields and Constants

    public const string Route   = "General/";
    public const string GetYear = "year";

#endregion
}

//Statistical
public class SchoolStatisticalApi
{
#region Static Fields and Constants

    public const string Route              = "SchoolStatistical/";
    public const string BorrowBySubject    = "borrow-by-subject";
    public const string BorrowByTeacher    = "borrow-by-teacher";
    public const string MinimunEquipment   = "minimun-equipment";
    public const string LessonUseEquipment = "lesson-use-equipment";

#endregion
}

public class DOEStatisticalApi
{
#region Static Fields and Constants

    public const string Route = "DOEStatistical/";
    public const string Room  = "room";

#endregion
}

public class LessonUseEquipmentStatisticalApi
{
#region Static Fields and Constants

    public const string Route  = "LessonUseEquipmentStatistical/";
    public const string Get    = "get";
    public const string Report = "report";

#endregion
}

public class EquipmentProcurementApi
{
#region Static Fields and Constants

    public const string Route  = "EquipmentProcurement/";
    public const string Get    = "equipment-procurement";
    public const string Report = "report";

#endregion
}

public class EquipmentExpiredApi
{
#region Static Fields and Constants

    public const string Route    = "EquipmentExpired/";
    public const string Get      = "equipment-expire";
    public const string GetCount = "count-equipment-expire";
    public const string Report   = "report";

#endregion
}

public class BorrowExpiredApi
{
#region Static Fields and Constants

    public const string Route             = "BorrowExpired/";
    public const string Get               = "borrow-expired";
    public const string GetCount          = "count-borrow-expired";
    public const string GetEquipment      = "borrow-equipment-expired";
    public const string GetCountEquipment = "count-borrow-equipment-expired";
    public const string Report            = "report";

#endregion
}

public class BorrowBySubjectApi
{
#region Static Fields and Constants

    public const string Route  = "BorrowBySubject/";
    public const string Get    = "borrow-by-subject";
    public const string Report = "report";

#endregion
}

public class BorrowByTeacherApi
{
#region Static Fields and Constants

    public const string Route    = "BorrowByTeacher/";
    public const string Overview = "overview/";
    public const string Get      = "borrow-by-teacher";
    public const string Report   = "report";

#endregion
}

public class MinimumEquipmentApi
{
#region Static Fields and Constants

    public const string Route  = " MinimumEquipment/";
    public const string Get    = "minimun-equipment";
    public const string Report = "report";

#endregion
}