#region

using System.Collections.Generic;
using ITC.Infra.CrossCutting.Identity.Authorization;
using ITC.Infra.CrossCutting.Identity.Models.QueryModel;
using Newtonsoft.Json;

#endregion

namespace ITC.Service.API.Extensions;

/// <summary>
/// </summary>
public class RouteIdentity
{
#region Static Fields and Constants

    /// <summary>
    /// </summary>
    public const string ManageRoleController = "ManageRole";

    /// <summary>
    /// </summary>
    public const string UserTypeIndex = "UserTypes";

    /// <summary>
    /// </summary>
    public const string AddUserType = ManageRoleController + "/UserTypes/Create";

    /// <summary>
    /// </summary>
    public const string EditUserType = ManageRoleController + "/UserTypes/Edit/{id}";

    /// <summary>
    /// </summary>
    public const string DeleteUserType = ManageRoleController + "/UserTypes/Delete/{id}";

    /// <summary>
    /// </summary>
    public const string RoleIndex = "Roles";

    /// <summary>
    /// </summary>
    public const string ModuleIndex = "Modules";

    /// <summary>
    /// </summary>
    public const string AddModule = ManageRoleController + "/Modules/Create";

    /// <summary>
    /// </summary>
    public const string EditModule = ManageRoleController + "/Modules/Edit/{id}";

    /// <summary>
    /// </summary>
    public const string FunctionIndex = "Functions";

    /// <summary>
    /// </summary>
    public const string AddFuntion = ManageRoleController + "/Functions/Create";

    /// <summary>
    /// </summary>
    public const string EditFuntion = ManageRoleController + "/Functions/Edit/{id}";

    /// <summary>
    /// </summary>
    public const string PortalIndex = "Portals";

    /// <summary>
    /// </summary>
    public const string AddPortal = ManageRoleController + "/Portals/Create";

    /// <summary>
    /// </summary>
    public const string EditPortal = ManageRoleController + "/Portals/Edit/{id}";

    /// <summary>
    /// </summary>
    public const string AccountController = "Account";

    /// <summary>
    /// </summary>
    public const string AccountIndex = "Users";

    /// <summary>
    /// </summary>
    public const string AddAccount = AccountController + "/Users/Create";

    /// <summary>
    /// </summary>
    public const string EditAccount = AccountController + "/Users/Edit/{id}";

    /// <summary>
    /// </summary>
    public const string DeleteAccount = AccountController + "/Users/Delete/{id}";

    /// <summary>
    /// </summary>
    public const string AccountLogin = "/tai-khoan/dang-nhap";

    /// <summary>
    /// </summary>
    public const string AccountNoneEmail = "/tai-khoan-chua-dang-ki-email";

    /// <summary>
    /// </summary>
    public const string ModuleGroup = "ModuleGroups";

    /// <summary>
    /// </summary>
    public const string DepartmentOfEducationIndex = "DepartmentOfEducations";

    /// <summary>
    /// </summary>
    public const string AddDepartmentOfEducation = AccountController + "/DepartmentOfEducations/Create";

    /// <summary>
    /// </summary>
    public const string EditDepartmentOfEducation = AccountController + "/DepartmentOfEducations/Edit/{id}";

    /// <summary>
    /// </summary>
    public const string DeleteDepartmentOfEducation = AccountController + "/DepartmentOfEducations/Delete/{id}";

    /// <summary>
    /// </summary>
    public const string EducationDepartmentIndex = "EducationDepartments";

    /// <summary>
    /// </summary>
    public const string AddEducationDepartment = AccountController + "/EducationDepartments/Create";

    /// <summary>
    /// </summary>
    public const string EditEducationDepartment = AccountController + "/EducationDepartments/Edit/{id}";

    /// <summary>
    /// </summary>
    public const string DeleteEducationDepartment = AccountController + "/EducationDepartments/Delete/{id}";

    /// <summary>
    /// </summary>
    public const string InboxIndex = "tin-nhan";

    /// <summary>
    /// </summary>
    public const string DetailInbox = InboxIndex + "/chi-tiet/{id}";

    /// <summary>
    /// </summary>
    public const string SentIndex = "gui-tin-nhan";

    /// <summary>
    /// </summary>
    public const string DetailSent = "gui-tin-nhan/chi-tiet/{id}";

    /// <summary>
    /// </summary>
    public const string NotificationIndex = "thong-bao";

    /// <summary>
    /// </summary>
    public const string DetailNotification = "thong-bao/chi-tiet/{id}";

#endregion

    // Hệ thống
}

/// <summary>
/// </summary>
public class RoleIdentity
{
#region Static Fields and Constants

    /// <summary>
    /// </summary>
    public const string None = "None";

    /// <summary>
    /// </summary>
    public const string Administrator = "SystemAdministrator";

    /// <summary>
    ///     Sở giáo dục
    /// </summary>
    public const string DepartmentOfEducation = "DepartmentOfEducation";

    /// <summary>
    ///     Phòng giáo cụ
    /// </summary>
    public const string EducationDepartment = "EducationDepartment";

    /// <summary>
    ///     Trường
    /// </summary>
    public const string Bussiness = "Bussiness";

    /// <summary>
    /// </summary>
    public const string Department = "Department";

#endregion
}

//public class ModuleIdentity
//{
//    public const string TestModule = "mdc";
//    public const string Account = "acc";
//    public const string UserType = "uty";
//    public const string ModuleGeT = "mdc";
//    /*Sở giáo dục*/
//    public const string StandardCriteria = "sqltc";
//    public const string SchoolYear = "sqlnk";
//    /*Phòng  giáo dục*/
//    public const string EdInfo = "edinfo";

//    //public const string SchoolINfor = "schoolinfo";
//    public const string OfficalProfile = "officalprofile";
//    public const string OfficalAccount = "officalaccount";
//    public const string Position = "position";
//    public const string Reference = "ptltk";
//    public const string SchoolManagement = "schoolmn";
//    public const string EducationDepartmentManagement = "epmn";
//    public const string DepartmentManagement = "dpmn";
//    public const string ApproverReigister = "approveregister";
//    public const string UnitUserType = "unitusertype";
//    public const string History = "history";
//    public const string Report = "report";
//    public const string Statistical = "statistical";

//    // equiment
//    public const string DeclarationEquipment = "decl";

//}
/// <summary>
/// </summary>
public class AuthorizeHelpers
{
#region Methods

    /// <summary>
    /// </summary>
    /// <param name="value"></param>
    /// <param name="moduleIdentity"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    public static bool HasPermission(IEnumerable<string> value, string moduleIdentity, TypeAudit type)
    {
        //var pers = new List<CustomModuleModel>();
        if (value != null)
            foreach (var item in value)
            {
                var per = JsonConvert.DeserializeObject<CustomModuleModel>(item);
                if (per != null)
                    if (per.Id == moduleIdentity && ((int)type & per.We) == (int)type)
                        return true;
            }

        return false;
    }

#endregion
}