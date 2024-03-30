#region

using System;
using System.Threading.Tasks;
using ITC.Application.ViewModels.Account;
using ITC.Application.ViewModels.ManageRole;
using ITC.Infra.CrossCutting.Identity.Models;

#endregion

namespace ITC.Application.Interfaces.ManageRole;

public interface IAccountAppService : IDisposable
{
#region Methods

    void Active(string        id);
    void Add(AccountViewModel accountViewModel);

    // void AddDepartmentOfEducation(DepartmentOfEducationViewModel departmentOfEducationViewModel);

    // void AddEducationDepartment(EducationDepartmentViewModel educationDepartmentViewModel);
    void AddEmailAccount(AddAccountEmailViewModel model);


    void                   AddPersonnelAccount(PersonnelAccountViewModel personnelViewModel);
    void                   DeletePersonnelAccount(string                 id);
    void                   Disable(string                                id);
    Task<AccountViewModel> GetAccountByEmailAsync(string                 email);

    //void AddSchool(SchoolViewModel school);

    Task<AccountViewModel> GetAccountByIdAsync(string id);
    AccountEditViewModel   GetAccountEditById(string  id);

    Task<CustomerUserTypeViewModel> GetAccountInfoByIdAsync(string userId);

    // DepartmentOfEducationViewModel GetByDepartmentOfEducationId(string id);
    // EducationDepartmentViewModel GetByEducationDepartmentId(string id);
    AccountViewModel GetById(string id);

    Task<AccountViewModel> GetByOfficalProfileAsync(string id, string managementId);

    // DepartmentOfEducationEditViewModel GetDepartmentOfEducationEditById(string id);
    // EducationDepartmentEditViewModel GetEducationDepartmentEditById(string id);
    Task<AccountViewModel>        GetManagementByPortalId(int        portalId);
    EditPersonnelAccountViewModel GetPersonnelAccountEditById(string id);
    void                          Lock(string                        id);
    void                          Remove(string[]                    ids);
    void                          RemoveDepartmentOfEducation(string id);
    void                          RemoveEducationDepartment(string   id);
    void                          Unlock(string                      id);

    void Update(AccountEditViewModel accountViewModel);

    // void UpdateDepartmentOfEducation(DepartmentOfEducationEditViewModel accountViewModel);
    // void UpdateEducationDepartment(EducationDepartmentEditViewModel educationDepartmentEditViewModel);
    void                  UpdatePersonnelAccount(EditPersonnelAccountViewModel accountViewModel);
    Task<ApplicationUser> GetUserByEmail(string                                email, string managementId);

#endregion
}