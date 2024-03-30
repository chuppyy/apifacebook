#region

using System;
using System.Threading.Tasks;
using AutoMapper;
using ITC.Application.Interfaces.ManageRole;
using ITC.Application.ViewModels.Account;
using ITC.Application.ViewModels.ManageRole;
using ITC.Domain.Commands.ManageRole.Account;
using ITC.Domain.Commands.ManageRole.OfficalAccount;
using ITC.Domain.Core.Bus;
using ITC.Infra.CrossCutting.Identity.Interfaces;
using ITC.Infra.CrossCutting.Identity.Models;
using ITC.Infra.CrossCutting.Identity.Queries;

#endregion

namespace ITC.Application.Services.ManageRole;

public class AccountAppService : IAccountAppService
{
#region Constructors

    public AccountAppService(IMapper               mapper,
                             IManageRoleRepository manageRoleRepository,
                             IAccountRepository    accountRepository,
                             IAccountQueries       accountQueries,
                             IManageRoleQueries    manageRoleQueries,
                             IMediatorHandler      bus)
    {
        _mapper               = mapper;
        _manageRoleRepository = manageRoleRepository;
        _accountRepository    = accountRepository;
        _manageRoleQueries    = manageRoleQueries;
        Bus                   = bus;

        _accountQueries = accountQueries;
    }

#endregion

#region Fields

    private readonly IAccountQueries       _accountQueries;
    private readonly IAccountRepository    _accountRepository;
    private readonly IManageRoleQueries    _manageRoleQueries;
    private readonly IManageRoleRepository _manageRoleRepository;
    private readonly IMapper               _mapper;

    private readonly IMediatorHandler Bus;

#endregion

#region IAccountAppService Members

    public AccountViewModel GetById(string id)
    {
        return _mapper.Map<AccountViewModel>(_accountRepository.GetById(id));
    }

    public AccountEditViewModel GetAccountEditById(string id)
    {
        return _mapper.Map<AccountEditViewModel>(_accountRepository.GetById(id));
    }


    public void Add(AccountViewModel accountViewModel)
    {
        var AddCommand = _mapper.Map<AddAccountCommand>(accountViewModel);
        Bus.SendCommand(AddCommand);
    }

    public void Update(AccountEditViewModel accountViewModel)
    {
        var updateCommand = _mapper.Map<UpdateAccountCommand>(accountViewModel);
        Bus.SendCommand(updateCommand);
    }

    public void Active(string id)
    {
        var activeCommand = new ActiveAccountCommand(id);
        Bus.SendCommand(activeCommand);
    }

    public void Disable(string id)
    {
        var disableCommand = new DisableAccountCommand(id);
        Bus.SendCommand(disableCommand);
    }

    public void Lock(string id)
    {
        var command = new LockAccountCommand(id);
        Bus.SendCommand(command);
    }

    public void Unlock(string id)
    {
        var command = new UnlockAccountCommand(id);
        Bus.SendCommand(command);
    }

    public void Remove(string[] ids)
    {
        var removeCommand = new RemoveAccountCommand(ids);
        Bus.SendCommand(removeCommand);
    }

    public void AddPersonnelAccount(PersonnelAccountViewModel personnelViewModel)
    {
        var AddCommand = _mapper.Map<AddPersonnelAccountCommand>(personnelViewModel);
        Bus.SendCommand(AddCommand);
    }

    public EditPersonnelAccountViewModel GetPersonnelAccountEditById(string id)
    {
        return _mapper.Map<EditPersonnelAccountViewModel>(_accountRepository.GetUserByIdAsync(id).Result);
    }

    public void UpdatePersonnelAccount(EditPersonnelAccountViewModel personnelAccountViewModel)
    {
        var updateCommand = _mapper.Map<UpdatePersonnelAccountCommand>(personnelAccountViewModel);
        Bus.SendCommand(updateCommand);
    }

    public void DeletePersonnelAccount(string id)
    {
        var removeCommand = new RemovePersonnelAccountCommand(id);
        Bus.SendCommand(removeCommand);
    }

    // public void AddDepartmentOfEducation(DepartmentOfEducationViewModel departmentOfEducationViewModel)
    // {
    //     var command = _mapper.Map<AddDepartmentOfEducationCommand>(departmentOfEducationViewModel);
    //     Bus.SendCommand(command);
    // }
    //
    // public DepartmentOfEducationViewModel GetByDepartmentOfEducationId(string id)
    // {
    //     Guid.TryParse(id, out var guid);
    //     // return _mapper.Map<DepartmentOfEducationViewModel>(_soGDRepository.Get(guid));
    //     return null;
    // }

    // public DepartmentOfEducationEditViewModel GetDepartmentOfEducationEditById(string id)
    // {
    //     Guid.TryParse(id, out var guid);
    //     // return _mapper.Map<DepartmentOfEducationEditViewModel>(_soGDRepository.Get(guid));
    //     return null;
    // }
    //
    // public void UpdateDepartmentOfEducation(DepartmentOfEducationEditViewModel departmentOfEducationEditViewModel)
    // {
    //     var updateCommand = _mapper.Map<UpdateDepartmentOfEducationCommand>(departmentOfEducationEditViewModel);
    //     Bus.SendCommand(updateCommand);
    // }

    public void RemoveDepartmentOfEducation(string id)
    {
        var removeCommand = new RemoveDepartmentOfEducationCommand(id);
        Bus.SendCommand(removeCommand);
    }

    // public void AddEducationDepartment(EducationDepartmentViewModel educationDepartmentViewModel)
    // {
    //     var command = _mapper.Map<AddEducationDepartmentCommand>(educationDepartmentViewModel);
    //     Bus.SendCommand(command);
    // }
    //
    // public EducationDepartmentViewModel GetByEducationDepartmentId(string id)
    // {
    //     Guid.TryParse(id, out var guid);
    //     return null;
    //     //return _mapper.Map<EducationDepartmentViewModel>(_educationDepartmentRepository.Get(guid));
    // }
    //
    // public EducationDepartmentEditViewModel GetEducationDepartmentEditById(string id)
    // {
    //     Guid.TryParse(id, out var guid);
    //     return null;
    //     // return _mapper.Map<EducationDepartmentEditViewModel>(_educationDepartmentRepository.Get(guid));
    // }
    //
    // public void UpdateEducationDepartment(EducationDepartmentEditViewModel educationDepartmentEditViewModel)
    // {
    //     var updateCommand = _mapper.Map<UpdateEducationDepartmentCommand>(educationDepartmentEditViewModel);
    //     Bus.SendCommand(updateCommand);
    // }

    public void RemoveEducationDepartment(string id)
    {
        var removeCommand = new RemoveEducationDepartmentCommand(id);
        Bus.SendCommand(removeCommand);
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    public async Task<AccountViewModel> GetManagementByPortalId(int portalId)
    {
        return _mapper.Map<AccountViewModel>(await _accountQueries.GetManagementByPortalId(portalId));
    }

    public void AddEmailAccount(AddAccountEmailViewModel model)
    {
        var _command = _mapper.Map<AddEmailAccountCommand>(model);
        Bus.SendCommand(_command);
    }

    public async Task<AccountViewModel> GetAccountByIdAsync(string id)
    {
        return _mapper.Map<AccountViewModel>(await _accountRepository.GetUserByIdAsync(id));
    }

    public async Task<AccountViewModel> GetAccountByEmailAsync(string email)
    {
        return _mapper.Map<AccountViewModel>(await _accountRepository.GetUserByEmailAsync(email));
    }


    public async Task<CustomerUserTypeViewModel> GetAccountInfoByIdAsync(string userId)
    {
        return _mapper.Map<CustomerUserTypeViewModel>(await _manageRoleQueries.GetAccountInfoByUserIdAsync(userId));
    }

    public async Task<AccountViewModel> GetByOfficalProfileAsync(string code, string managementId)
    {
        return _mapper.Map<AccountViewModel>(await _accountQueries.GetByOfficalProfile(code, managementId));
    }

    public async Task<ApplicationUser> GetUserByEmail(string email, string managementId)
    {
        return await _accountRepository.GetUserByEmailAndManagementAsync(email, managementId);
    }

#endregion

    //public void AddSchool(SchoolViewModel school)
    //{
    //    var command = _mapper.Map<AddSchoolCommand>(school);
    //    Bus.SendCommand(command);
    //}
}