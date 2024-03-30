#region

using System;
using System.Threading;
using System.Threading.Tasks;
using ITC.Domain.Commands.ManageRole.Account;
using ITC.Domain.Commands.ManageRole.OfficalAccount;
using ITC.Domain.Core.Bus;
using ITC.Domain.Core.Events;
using ITC.Domain.Core.Models;
using ITC.Domain.Core.Notifications;
using ITC.Domain.Interfaces;
using ITC.Domain.StoredEvents.Account;
using ITC.Infra.CrossCutting.Identity.Interfaces;
using ITC.Infra.CrossCutting.Identity.Models;
using ITC.Infra.CrossCutting.Identity.UoW;
using MediatR;
using Microsoft.AspNetCore.Identity;

#endregion

namespace ITC.Infra.CrossCutting.Identity.CommandHandlers;

public class AccountCommandHandler : CommandIdentityHandler,
                                     IRequestHandler<AddAccountCommand, bool>,
                                     IRequestHandler<UpdateAccountCommand, bool>,
                                     IRequestHandler<ActiveAccountCommand, bool>,
                                     IRequestHandler<DisableAccountCommand, bool>,
                                     IRequestHandler<LockAccountCommand, bool>,
                                     IRequestHandler<UnlockAccountCommand, bool>,
                                     IRequestHandler<RemoveAccountCommand, bool>,
                                     IRequestHandler<AccountInfoCommand, bool>,
                                     IRequestHandler<AddEmailAccountCommand, bool>,
                                     //IRequestHandler<ReissuePasswordCommand, bool>,
                                     IRequestHandler<AddDepartmentOfEducationCommand, bool>,
                                     IRequestHandler<UpdateDepartmentOfEducationCommand, bool>,
                                     IRequestHandler<RemoveDepartmentOfEducationCommand, bool>,
                                     IRequestHandler<AddEducationDepartmentCommand, bool>,
                                     IRequestHandler<UpdateEducationDepartmentCommand, bool>,
                                     IRequestHandler<RemoveEducationDepartmentCommand, bool>,
                                     IRequestHandler<AddPersonnelAccountCommand, bool>,
                                     IRequestHandler<UpdatePersonnelAccountCommand, bool>,
                                     IRequestHandler<RemovePersonnelAccountCommand, bool>
{
#region Constructors

    public AccountCommandHandler(IAccountRepository           accountRepository,
                                 IUnitOfWorkIdentity          uow,
                                 IMediatorHandler             bus,
                                 IUser                        user,
                                 UserManager<ApplicationUser> userManager,
                                 //IDepartmentOfEducationRepository soGDRepository,
                                 //IEducationDepartmentRepository educationDepartmentRepository,
                                 IUnitOfWork                              unitOfWork,
                                 INotificationHandler<DomainNotification> notifications) :
        base(uow, bus, notifications)
    {
        _accountRepository = accountRepository;
        _userManager       = userManager;
        _uow               = unitOfWork;
        Bus                = bus;
        _user              = user;
    }

#endregion

#region IRequestHandler<AccountInfoCommand,bool> Members

    /// <summary>
    ///     Cập nhật thông tin cán bộ
    /// </summary>
    /// <param name="message"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<bool> Handle(AccountInfoCommand message, CancellationToken cancellationToken)
    {
        if (!message.IsValid())
        {
            NotifyValidationErrors(message);
            return Task.FromResult(false);
        }

        if (_accountRepository.IsExistAccountByEmail(message.UserId, message.Email))
        {
            Bus.RaiseEvent(new DomainNotification(message.MessageType, "Email đã tồn tại trong hệ thống."));
            return Task.FromResult(false);
        }

        var account = _accountRepository.GetUserByIdAsync(message.UserId).Result;

        if (account == null)
        {
            Bus.RaiseEvent(new DomainNotification(message.MessageType, "Không tìm thấy tài khoản"));
            return Task.FromResult(false);
        }

        account.UpdateApplicationUser(message.Email, message.Name, message.PhoneNumber, account.UserTypeId,
                                      message.Avatar);
        account.UpdateUserTager(_user.UserId, DateTime.UtcNow);

        if (account == null)
        {
            Bus.RaiseEvent(new DomainNotification(message.MessageType, "Không tìm thấy  hồ sơ cán bộ"));
            return Task.FromResult(false);
        }

        //var officalProfile = _hSCanBoRepository.GetById(message.Id);
        //officalProfile.Update(message.Name, message.Workplace, message.BirthDay, message.Gender, message.Address, message.PhoneNumber, message.Email, _user.UserId);
        var result = _accountRepository.UpdateAccountAsync(account).Result;

        if (Commit(result))
        {
        }

        return Task.FromResult(false);
    }

#endregion

#region IRequestHandler<ActiveAccountCommand,bool> Members

    public Task<bool> Handle(ActiveAccountCommand message, CancellationToken cancellationToken)
    {
        if (!message.IsValid())
        {
            NotifyValidationErrors(message);
            return Task.FromResult(false);
        }

        var account = _accountRepository.GetUserByIdAsync(message.Id).Result;

        if (account == null)
        {
            Bus.RaiseEvent(new DomainNotification(message.MessageType, "The account not found."));
            return Task.FromResult(false);
        }

        if (account.IsSuperAdmin)
        {
            Bus.RaiseEvent(new DomainNotification(message.MessageType, "The account super admin."));
            return Task.FromResult(false);
        }

        if (account.EmailConfirmed)
        {
            Bus.RaiseEvent(new DomainNotification(message.MessageType, "The account active."));
            return Task.FromResult(false);
        }

        account.SetActive(true);
        var result = _accountRepository.UpdateAccountAsync(account).Result;

        if (Commit(result))
        {
            //Bus.RaiseEvent(new AccountActivedEvent(StoredEventType.Update, Guid.Parse(_user.UserId), _user.FullName, Guid.Parse(account.Id), account.UserName, Guid.Parse(_user.UnitUserId), _user.PortalId));
        }

        return Task.FromResult(true);
    }

#endregion

#region IRequestHandler<AddAccountCommand,bool> Members

    public Task<bool> Handle(AddAccountCommand message, CancellationToken cancellationToken)
    {
        if (!message.IsValid())
        {
            NotifyValidationErrors(message);
            return Task.FromResult(false);
        }

        if (_accountRepository.IsExistAccountByUserName(message.UserName))
        {
            Bus.RaiseEvent(new DomainNotification(message.MessageType, "The user name has already been taken."));
            return Task.FromResult(false);
        }

        if (_accountRepository.IsExistAccountByEmail(message.Email))
        {
            Bus.RaiseEvent(new DomainNotification(message.MessageType, "The email has already been taken."));
            return Task.FromResult(false);
        }

        var userTarget = new UserTarget(_user.UserId, _user.UserId, DateTime.UtcNow, DateTime.UtcNow);
        var applicationUser = new ApplicationUser(message.UserName, message.FullName, message.Email,
                                                  message.PhoneNumber, message.UserTypeId, message.Avatar,
                                                  userTarget);

        var result = _accountRepository.AddAccountAsync(applicationUser, message.Password).Result;


        if (Commit(result))
        {
            //Bus.RaiseEvent(new AccountAddEvent(applicationUser.Id, message.UserName, message.FullName, message.Email, message.PhoneNumber, message.UserTypeId, message.Avatar));
        }

        return Task.FromResult(true);
    }

#endregion

#region IRequestHandler<AddDepartmentOfEducationCommand,bool> Members

    public Task<bool> Handle(AddDepartmentOfEducationCommand message, CancellationToken cancellationToken)
    {
        //if (!message.IsValid())
        //{
        //    NotifyValidationErrors(message);
        //    return Task.FromResult(false);
        //}

        //if (_accountRepository.IsExistAccountByUserName(message.UserName))
        //{
        //    Bus.RaiseEvent(new DomainNotification(message.MessageType, "The user name has already been taken."));
        //    return Task.FromResult(false);
        //}

        //if (_accountRepository.IsExistAccountByEmail(message.Email))
        //{
        //    Bus.RaiseEvent(new DomainNotification(message.MessageType, "The email has already been taken."));
        //    return Task.FromResult(false);
        //}

        //var cur = _user.GetIdentityUser();

        //var userTarget = new UserTarget(cur.UserId, cur.UserId, DateTime.UtcNow, DateTime.UtcNow);
        //var applicationUser = new ApplicationUser(message.UserName, message.FullName, message.Email, message.PhoneNumber, message.UserTypeId, message.Avatar, cur.IsSuperAdmin ? null : cur.UserId, message.PortalId, userTarget);
        //applicationUser.SetActive(true);
        //var result = _accountRepository.AddAccountAsync(applicationUser, message.Password).Result;

        //if (Commit(result))
        //{
        //    //Bus.RaiseEvent(new AccountAddEvent(applicationUser.Id, message.UserName, message.FullName, message.Email, message.PhoneNumber, message.UserTypeId, message.Avatar));
        //    var departmentOfEducation = new DepartmentOfEducation(Guid.NewGuid(), message.Code, message.Identifier, string.IsNullOrEmpty(applicationUser.ManagementUnitId) ? new Guid() : Guid.Parse(applicationUser.ManagementUnitId), cur.UserId, applicationUser.Id, message.PortalId);
        //    _soGDRepository.Add(departmentOfEducation);
        //    if (Commit(_uow))
        //    {
        //        Bus.RaiseEvent(new DepartmentOfEducationAddEvent(departmentOfEducation.Id.ToString(), departmentOfEducation.Code, applicationUser.Id, message.UserName, message.FullName, message.Email, message.PhoneNumber, message.UserTypeId, message.Avatar));
        //    }
        //}

        return Task.FromResult(true);
    }

#endregion

#region IRequestHandler<AddEducationDepartmentCommand,bool> Members

    public async Task<bool> Handle(AddEducationDepartmentCommand message, CancellationToken cancellationToken)
    {
        //if (!message.IsValid())
        //{
        //    NotifyValidationErrors(message);
        //    return await Task.FromResult(false);
        //}

        //if (_educationDepartmentRepository.CheckExistCode(message.Code, _user.PortalId))
        //{
        //    await Bus.RaiseEvent(new DomainNotification("Code", "Mã đơn vị đã tồn tại."));
        //    return await Task.FromResult(false);
        //}

        //if (_accountRepository.IsExistAccountByUserName(message.UserName))
        //{
        //    await Bus.RaiseEvent(new DomainNotification("UserName", "Tên đăng nhập đã tồn tại"));
        //    return await Task.FromResult(false);
        //}

        //if (_accountRepository.IsExistAccountByEmail(message.Email))
        //{
        //    await Bus.RaiseEvent(new DomainNotification("Email", "Email đã tồn tại"));
        //    return await Task.FromResult(false);
        //}

        //var userTarget = new UserTarget(_user.UserId, _user.UserId, DateTime.UtcNow, DateTime.UtcNow);
        //var applicationUser = new ApplicationUser(message.UserName, message.FullName, message.Email, message.PhoneNumber, message.UserTypeId, message.Avatar, _user.IsSuperAdministrator ? null : _user.UserId, message.PortalId, userTarget);

        //var result = _accountRepository.AddAccountAsync(applicationUser, message.Password).Result;

        //if (Commit(result))
        //{
        //    //Bus.RaiseEvent(new AccountAddEvent(applicationUser.Id, message.UserName, message.FullName, message.Email, message.PhoneNumber, message.UserTypeId, message.Avatar));
        //    var educationDepartment = new EducationDepartment(Guid.NewGuid(), message.Code, message.Identifier, _user.UserId, _user.PortalId, applicationUser.Id);
        //    _educationDepartmentRepository.Add(educationDepartment);
        //    if (Commit(_uow))
        //    {
        //        var applicationUserresult = await _accountRepository.GetUserByIdAsync(_user.UserId);
        //        await Bus.RaiseEvent(new EducationDepartmentAddEvent(educationDepartment.Id.ToString(), educationDepartment.Code, applicationUser.Id, message.UserName, message.FullName, message.Email, message.PhoneNumber, message.UserTypeId, message.Avatar,
        //            ETC.Domain.Core.Events.StoredEventType.Add, Guid.Parse(_user.UserId), applicationUserresult.FullName, educationDepartment.Id, message.FullName, Guid.Parse(applicationUser.Id), applicationUserresult.PortalId));
        //    }
        //}

        return await Task.FromResult(true);
    }

#endregion

#region IRequestHandler<AddEmailAccountCommand,bool> Members

    public Task<bool> Handle(AddEmailAccountCommand message, CancellationToken cancellationToken)
    {
        if (!message.IsValid())
        {
            NotifyValidationErrors(message);
            return Task.FromResult(false);
        }

        var account = _accountRepository.GetUserByIdAsync(message.Id).Result;
        if (_accountRepository.IsExistAccountByEmail(message.Email))
        {
            Bus.RaiseEvent(new DomainNotification(message.MessageType,
                                                  $"Email {message.Email} đã tồn tại trong hệ thống."));
            return Task.FromResult(false);
        }

        if (account == null)
        {
            Bus.RaiseEvent(new DomainNotification(message.MessageType, "không tìm thấy tài khoản."));
            return Task.FromResult(false);
        }

        if (!string.IsNullOrEmpty(account.Email))
        {
            Bus.RaiseEvent(new DomainNotification(message.MessageType, "Tài khoản đã cập nhật email."));
            return Task.FromResult(false);
        }

        account.AddEmail(message.Email);
        var result = _accountRepository.UpdateAccountAsync(account).Result;

        if (Commit(result))
            // Bus.RaiseEvent(new AccountAddEmailEvent(StoredEventType.Update, Guid.Parse(account.Id), account.FullName, Guid.Parse(account.Id), account.FullName, Guid.Parse(_user.UnitUserId), account.PortalId, account.Id));
            return Task.FromResult(true);
        return Task.FromResult(false);
    }

#endregion

#region IRequestHandler<AddPersonnelAccountCommand,bool> Members

    public Task<bool> Handle(AddPersonnelAccountCommand message, CancellationToken cancellationToken)
    {
        if (!message.IsValid())
        {
            NotifyValidationErrors(message);
            return Task.FromResult(false);
        }

        var cur = _user.GetIdentityUser();
        if (message.IsAutoGenUserName)

        {
            foreach (var code in message.StaffRecords)
            {
                // var staffRecord = _officalProfileRepository.GetstaffRecordByCodeAsync(code, cur.UnitUserId).Result;
                // if (staffRecord == null)
                // {
                //     Bus.RaiseEvent(new DomainNotification(message.MessageType, "Tài khoản đã tồn tại."));
                //     continue;
                // }

                // if (_accountRepository.IsExistAccountByUserName(staffRecord.Code))
                // {
                //     Bus.RaiseEvent(new DomainNotification(message.MessageType, $"Tên đăng nhập {staffRecord.Code} đã tồn tại trong hệ thống."));
                //     continue;
                // }
                //
                // if (_accountRepository.IsExistAccountByEmail(staffRecord.Email))
                // {
                //     Bus.RaiseEvent(new DomainNotification(message.MessageType, $"Email {staffRecord.Email} đã tồn tại trong hệ thống."));
                //     continue;
                // }
                //
                // var userTarget = new UserTarget(cur.UserId, cur.UserId, DateTime.UtcNow, DateTime.UtcNow);
                // var applicationUser = new ApplicationUser(staffRecord.Code, staffRecord.Name, staffRecord.Email, staffRecord.PhoneNumber, message.UserTypeId, null, cur.UnitUserId, staffRecord.Id.ToString(), cur.PortalId, userTarget);
                //
                // var result = _accountRepository.AddAccountAsync(applicationUser, message.Password).Result;

                // if (CustomCommit(result)) Bus.RaiseEvent(new PersonalAccountAddEvent(StoredEventType.Add, _user.UserId, _user.FullName, applicationUser.Id, applicationUser.UserName, _user.UnitUserId, _user.PortalId));
            }
        }


        else
        {
            if (message.StaffRecords.Count == 0)
            {
                Bus.RaiseEvent(new DomainNotification(message.MessageType, "Không tìm thấy mã cán bộ."));
                return Task.FromResult(false);
            }

            var code = message.StaffRecords[0];
            // var staffRecord = _officalProfileRepository.GetstaffRecordByCodeAsync(code, cur.UnitUserId).Result;
            // if (staffRecord == null)
            // {
            //     Bus.RaiseEvent(new DomainNotification(message.MessageType, "Tài khoản đã tồn tại."));
            //     return Task.FromResult(false);
            // }
            //
            // if (_accountRepository.IsExistAccountByUserName(message.UserName))
            // {
            //     Bus.RaiseEvent(new DomainNotification(message.MessageType, $"Tên đăng nhập {message.UserName} đã tồn tại trong hệ thống."));
            //     return Task.FromResult(false);
            // }
            //
            // if (_accountRepository.IsExistAccountByEmail(staffRecord.Email))
            // {
            //     Bus.RaiseEvent(new DomainNotification(message.MessageType, $"The email {staffRecord.Email} has already been taken."));
            //     return Task.FromResult(false);
            // }
            //
            // var userTarget = new UserTarget(cur.UserId, cur.UserId, DateTime.UtcNow, DateTime.UtcNow);
            // var applicationUser = new ApplicationUser(message.UserName, staffRecord.Name, staffRecord.Email, staffRecord.PhoneNumber, message.UserTypeId, null, cur.UnitUserId, staffRecord.Id.ToString(), cur.PortalId, userTarget);
            // var result = _accountRepository.AddAccountAsync(applicationUser, message.Password).Result;
            //
            // if (CustomCommit(result))
            // {
            //     Bus.RaiseEvent(new PersonalAccountAddEvent(StoredEventType.Add, _user.UserId, _user.FullName, applicationUser.Id, applicationUser.UserName, _user.UnitUserId, _user.PortalId));
            //     return Task.FromResult(true);
            // }
        }

        return Task.FromResult(true);
    }

#endregion

#region IRequestHandler<DisableAccountCommand,bool> Members

    public Task<bool> Handle(DisableAccountCommand message, CancellationToken cancellationToken)
    {
        if (!message.IsValid())
        {
            NotifyValidationErrors(message);
            return Task.FromResult(false);
        }

        var account = _accountRepository.GetUserByIdAsync(message.Id).Result;

        if (account == null)
        {
            Bus.RaiseEvent(new DomainNotification(message.MessageType, "The account not found."));
            return Task.FromResult(false);
        }

        if (account.IsSuperAdmin)
        {
            Bus.RaiseEvent(new DomainNotification(message.MessageType, "The account super admin."));
            return Task.FromResult(false);
        }

        if (!account.EmailConfirmed)
        {
            Bus.RaiseEvent(new DomainNotification(message.MessageType, "The account disable."));
            return Task.FromResult(false);
        }

        account.SetActive(false);
        var result = _accountRepository.UpdateAccountAsync(account).Result;

        if (Commit(result))
        {
            //Bus.RaiseEvent(new AccountDisabledEvent(account.Id));
        }

        return Task.FromResult(true);
    }

#endregion

#region IRequestHandler<LockAccountCommand,bool> Members

    public Task<bool> Handle(LockAccountCommand message, CancellationToken cancellationToken)
    {
        if (!message.IsValid())
        {
            NotifyValidationErrors(message);
            return Task.FromResult(false);
        }

        var account = _userManager.FindByIdAsync(message.Id).Result;
        //var account = this._accountRepository.GetUserAsync(message.Id).Result;
        if (account == null)
        {
            Bus.RaiseEvent(new DomainNotification(message.MessageType, "The account not found."));
            return Task.FromResult(false);
        }

        if (account.IsSuperAdmin)
        {
            Bus.RaiseEvent(new DomainNotification(message.MessageType, "The account super admin."));
            return Task.FromResult(false);
        }

        //var result = _userManager.SetLockoutEnabledAsync(account, true).Result;
        var result = _userManager.SetLockoutEndDateAsync(account, DateTime.MaxValue).Result;
        //account.SetLock(true, DateTime.MaxValue);
        //var result = _accountRepository.UpdateAccountAsync(account).Result;

        if (Commit(result))
        {
            // Bus.RaiseEvent(new AccountLockedEvent(account.Id));
        }

        return Task.FromResult(true);
    }

#endregion

#region IRequestHandler<RemoveAccountCommand,bool> Members

    public Task<bool> Handle(RemoveAccountCommand message, CancellationToken cancellationToken)
    {
        if (!message.IsValid())
        {
            NotifyValidationErrors(message);
            return Task.FromResult(false);
        }

        foreach (var id in message.Ids)
        {
            var account = _accountRepository.GetUserByIdAsync(id).Result;

            if (account == null)
            {
                Bus.RaiseEvent(new DomainNotification(message.MessageType, "The account not found."));
                return Task.FromResult(false);
            }

            if (account.IsSuperAdmin)
            {
                Bus.RaiseEvent(new DomainNotification(message.MessageType, "The account super admin."));
                return Task.FromResult(false);
            }

            account.SetDelete(true);
            var result = _accountRepository.UpdateAccountAsync(account).Result;
            if (Commit(result))
            {
                //   Bus.RaiseEvent(new AccountRemovedEvent(account.Id));
            }
        }

        return Task.FromResult(true);
    }

#endregion

#region IRequestHandler<RemoveDepartmentOfEducationCommand,bool> Members

    public Task<bool> Handle(RemoveDepartmentOfEducationCommand message, CancellationToken cancellationToken)
    {
        //if (!message.IsValid())
        //{
        //    NotifyValidationErrors(message);
        //    return Task.FromResult(false);
        //}
        //Guid.TryParse(message.Id, out var guid);
        //var departmentOfEducation = _soGDRepository.GetByIdAsync(guid).Result;

        //if (departmentOfEducation == null)
        //{
        //    Bus.RaiseEvent(new DomainNotification(message.MessageType, "The account not found."));
        //    return Task.FromResult(false);
        //}

        //departmentOfEducation.SetIsDelete(true);
        //departmentOfEducation.Update(_user.UserId);
        //_soGDRepository.Update(departmentOfEducation);

        //if (Commit(_uow))
        //{
        //    var account = _accountRepository.GetUserByIdAsync(departmentOfEducation.UserId.ToString()).Result;

        //    if (account == null)
        //    {
        //        Bus.RaiseEvent(new DomainNotification(message.MessageType, "The account not found."));
        //        return Task.FromResult(false);
        //    }
        //    if (account.IsSuperAdmin)
        //    {
        //        Bus.RaiseEvent(new DomainNotification(message.MessageType, "The account super admin."));
        //        return Task.FromResult(false);
        //    }
        //    account.SetDelete(true);
        //    var result = _accountRepository.UpdateAccountAsync(account).Result;

        //    if (Commit(result))
        //    {
        //        Bus.RaiseEvent(new AccountRemovedEvent(account.Id));
        //    }
        //    Bus.RaiseEvent(new DepartmentOfEducationRemovedEvent(departmentOfEducation.Id.ToString()));
        //}

        return Task.FromResult(true);
    }

#endregion

#region IRequestHandler<RemoveEducationDepartmentCommand,bool> Members

    public async Task<bool> Handle(RemoveEducationDepartmentCommand message, CancellationToken cancellationToken)
    {
        //if (!message.IsValid())
        //{
        //    NotifyValidationErrors(message);
        //    return await Task.FromResult(false);
        //}
        //foreach (var item in message.Ids)
        //{

        //    var educationDepartment = _educationDepartmentRepository.GetEducationDepartmentByIdAsync(item).Result;

        //    if (educationDepartment == null)
        //    {
        //        await Bus.RaiseEvent(new DomainNotification(message.MessageType, "The account not found."));
        //        return await Task.FromResult(false);
        //    }

        //    educationDepartment.SetIsDelete(true);
        //    educationDepartment.Update(_user.UserId);
        //    _educationDepartmentRepository.Update(educationDepartment);

        //    if (Commit(_uow))
        //    {
        //        var account = _accountRepository.GetUserByIdAsync(educationDepartment.UserId.ToString()).Result;

        //        if (account == null)
        //        {
        //            await Bus.RaiseEvent(new DomainNotification(message.MessageType, "Tài khoản không tồn tại."));
        //            return await Task.FromResult(false);
        //        }
        //        if (account.IsSuperAdmin)
        //        {
        //            await Bus.RaiseEvent(new DomainNotification(message.MessageType, "Tài khoản là quản trị viên."));
        //            return await Task.FromResult(false);
        //        }
        //        account.SetDelete(true);
        //        var result = _accountRepository.UpdateAccountAsync(account).Result;

        //        if (Commit(result))
        //        {
        //            await Bus.RaiseEvent(new AccountRemovedEvent(account.Id));
        //            var applicationUserresult = await _accountRepository.GetUserByIdAsync(_user.UserId);
        //            await Bus.RaiseEvent(new EducationDepartmentRemovedEvent(ETC.Domain.Core.Events.StoredEventType.Remove, Guid.Parse(applicationUserresult.Id),
        //                applicationUserresult.FullName, educationDepartment.Id, account.FullName,
        //                Guid.Parse(string.IsNullOrEmpty(applicationUserresult.ManagementUnitId) ? applicationUserresult.Id : applicationUserresult.ManagementUnitId), applicationUserresult.PortalId));
        //        }
        //    }
        //}
        return await Task.FromResult(true);
    }

#endregion

#region IRequestHandler<RemovePersonnelAccountCommand,bool> Members

    public Task<bool> Handle(RemovePersonnelAccountCommand message, CancellationToken cancellationToken)
    {
        if (!message.IsValid())
        {
            NotifyValidationErrors(message);
            return Task.FromResult(false);
        }

        foreach (var item in message.Ids)
        {
            var account = _accountRepository.GetUserByIdAsync(item).Result;

            if (account == null)
            {
                Bus.RaiseEvent(new DomainNotification(message.MessageType, "Không tìm thấy tài khoản."));
                return Task.FromResult(false);
            }

            if (account.IsSuperAdmin)
            {
                Bus.RaiseEvent(new DomainNotification(message.MessageType, "Tài khoản ad  min."));
                return Task.FromResult(false);
            }

            var result = _accountRepository.DeleteAccountAsync(account).Result;
            if (Commit(result))
                Bus.RaiseEvent(new PersonalAccountRemovedEvent(StoredEventType.Remove, _user.UserId, _user.FullName,
                                                               account.Id, account.UserName, _user.UnitUserId,
                                                               _user.PortalId));
        }

        return Task.FromResult(true);
    }

#endregion

#region IRequestHandler<UnlockAccountCommand,bool> Members

    public Task<bool> Handle(UnlockAccountCommand message, CancellationToken cancellationToken)
    {
        if (!message.IsValid())
        {
            NotifyValidationErrors(message);
            return Task.FromResult(false);
        }

        var account = _userManager.FindByIdAsync(message.Id).Result;
        //var account = this._accountRepository.GetUserAsync(message.Id).Result;
        if (account == null)
        {
            Bus.RaiseEvent(new DomainNotification(message.MessageType, "The account not found."));
            return Task.FromResult(false);
        }

        if (account.IsSuperAdmin)
        {
            Bus.RaiseEvent(new DomainNotification(message.MessageType, "The account super admin."));
            return Task.FromResult(false);
        }

        var result = _userManager.SetLockoutEndDateAsync(account, null).Result;
        //result = _userManager.SetLockoutEnabledAsync(account, false).Result;
        if (result.Succeeded) result = _userManager.ResetAccessFailedCountAsync(account).Result;

        //account.SetLock(true, DateTime.MaxValue);
        //var result = _accountRepository.UpdateAccountAsync(account).Result;

        if (Commit(result))
        {
            //Bus.RaiseEvent(new AccountUnlockedEvent(account.Id));
        }

        return Task.FromResult(true);
    }

#endregion

#region IRequestHandler<UpdateAccountCommand,bool> Members

    public Task<bool> Handle(UpdateAccountCommand message, CancellationToken cancellationToken)
    {
        if (!message.IsValid())
        {
            NotifyValidationErrors(message);
            return Task.FromResult(false);
        }

        if (_accountRepository.IsExistAccountByEmail(message.Id, message.Email))
        {
            Bus.RaiseEvent(new DomainNotification(message.MessageType, "The email has already been taken."));
            return Task.FromResult(false);
        }

        var account = _accountRepository.GetUserByIdAsync(message.Id).Result;

        if (account == null)
        {
            Bus.RaiseEvent(new DomainNotification(message.MessageType, "The user not found."));
            return Task.FromResult(false);
        }

        account.UpdateApplicationUser(message.Email, message.FullName, message.PhoneNumber, message.UserTypeId,
                                      message.Avatar);
        account.UpdateUserTager(_user.UserId, DateTime.UtcNow);
        var result = _accountRepository.UpdateAccountAsync(account).Result;

        if (Commit(result))
        {
            //    Bus.RaiseEvent(new AccountUpdatedEvent(StoredEventType.Update, Guid.Parse(_user.UserId), _user.FullName, Guid.Parse(account.Id), account.FullName, Guid.Parse(_user.UnitUserId), _user.PortalId,
            //         account.Email, account.FullName, account.PhoneNumber, account.UserTypeId, account.Avatar));
        }

        return Task.FromResult(true);
    }

#endregion

#region IRequestHandler<UpdateDepartmentOfEducationCommand,bool> Members

    public Task<bool> Handle(UpdateDepartmentOfEducationCommand message, CancellationToken cancellationToken)
    {
        //if (!message.IsValid())
        //{
        //    NotifyValidationErrors(message);
        //    return Task.FromResult(false);
        //}


        //Guid.TryParse(message.Id, out var guid);
        //var departmentOfEducation = _soGDRepository.GetByIdAsync(guid).Result;

        //if (departmentOfEducation == null)
        //{
        //    Bus.RaiseEvent(new DomainNotification(message.MessageType, "The user not found."));
        //    return Task.FromResult(false);
        //}

        //if (_accountRepository.IsExistAccountByEmail(departmentOfEducation.UserId.ToString(), message.Email))
        //{
        //    Bus.RaiseEvent(new DomainNotification(message.MessageType, "The email has already been taken."));
        //    return Task.FromResult(false);
        //}

        //departmentOfEducation.Update(_user.UserId);
        //if (Commit(_uow))
        //{
        //    var account = _accountRepository.GetUserByIdAsync(departmentOfEducation.UserId.ToString()).Result;

        //    if (account == null)
        //    {
        //        Bus.RaiseEvent(new DomainNotification(message.MessageType, "The user not found."));
        //        return Task.FromResult(false);
        //    }
        //    account.UpdateApplicationUser(message.Email, message.FullName, message.PhoneNumber, message.UserTypeId, message.Avatar);
        //    account.UpdateUserTager(_user.UserId, DateTime.UtcNow);

        //    var result = _accountRepository.UpdateAccountAsync(account).Result;

        //    if (Commit(result))
        //    {
        //        var unitUser = _accountRepository.GetUserByIdAsync(_user.UnitUserId).Result;
        //        var currentUser = _accountRepository.GetUserByIdAsync(_user.UserId).Result;
        //        Bus.RaiseEvent(new DepartmentOfEducationUpdatedEvent(StoredEventType.Update, Guid.Parse(_user.UserId), currentUser.FullName, Guid.Parse(_user.UnitUserId), unitUser.FullName, Guid.Parse(_user.ManagementUnitId), _user.PortalId, account.Email, departmentOfEducation.Manager, account.PhoneNumber, account.UserTypeId, account.Avatar));
        //    }
        //}


        return Task.FromResult(true);
    }

#endregion

#region IRequestHandler<UpdateEducationDepartmentCommand,bool> Members

    public async Task<bool> Handle(UpdateEducationDepartmentCommand message, CancellationToken cancellationToken)
    {
        //if (!message.IsValid())
        //{
        //    NotifyValidationErrors(message);
        //    return await Task.FromResult(false);
        //}


        //Guid.TryParse(message.Id, out var guid);
        //var educationDepartment = _educationDepartmentRepository.GetEducationDepartmentByIdAsync(guid).Result;

        //if (educationDepartment == null)
        //{
        //    await Bus.RaiseEvent(new DomainNotification(message.MessageType, "The user not found."));
        //    return await Task.FromResult(false);
        //}

        //if (_accountRepository.IsExistAccountByEmail(educationDepartment.UserId.ToString(), message.Email))
        //{
        //    await Bus.RaiseEvent(new DomainNotification(message.MessageType, "The email has already been taken."));
        //    return await Task.FromResult(false);
        //}

        //var account = _accountRepository.GetUserByIdAsync(educationDepartment.UserId.ToString()).Result;
        //if (account == null)
        //{
        //    await Bus.RaiseEvent(new DomainNotification(message.MessageType, "The user not found."));
        //    return await Task.FromResult(false);
        //}
        //account.UpdateApplicationUser(message.Email, message.FullName, message.Address, message.PhoneNumber, message.Fax, message.Website, message.Avatar);
        //account.UpdateUserTager(_user.UserId, DateTime.UtcNow);

        //var result = _accountRepository.UpdateAccountAsync(account).Result;

        //if (Commit(result))
        //{
        //    var applicationUserresult = await _accountRepository.GetUserByIdAsync(_user.UserId);
        //    await Bus.RaiseEvent(new EducationDepartmentUpdatedEvent(educationDepartment.Id.ToString(), account.FullName, account.Email, account.PhoneNumber, account.UserTypeId, account.Avatar,
        //        ETC.Domain.Core.Events.StoredEventType.Update, Guid.Parse(applicationUserresult.Id), applicationUserresult.FullName, educationDepartment.Id, account.FullName,
        //        Guid.Parse(string.IsNullOrEmpty(applicationUserresult.ManagementUnitId) ? applicationUserresult.Id : applicationUserresult.ManagementUnitId), applicationUserresult.PortalId));
        //}

        return await Task.FromResult(true);
    }

#endregion

#region IRequestHandler<UpdatePersonnelAccountCommand,bool> Members

    public Task<bool> Handle(UpdatePersonnelAccountCommand message, CancellationToken cancellationToken)
    {
        if (!message.IsValid())
        {
            NotifyValidationErrors(message);
            return Task.FromResult(false);
        }

        if (_accountRepository.IsExistAccountByEmail(message.Id, message.Email))
        {
            Bus.RaiseEvent(new DomainNotification(message.MessageType, "The email has already been taken."));
            return Task.FromResult(false);
        }


        var account = _accountRepository.GetUserByIdAsync(message.Id).Result;

        if (account == null)
        {
            Bus.RaiseEvent(new DomainNotification(message.MessageType, "The user not found."));
            return Task.FromResult(false);
        }

        account.UpdateApplicationUser(message.Email, message.FullName, message.PhoneNumber, message.UserTypeId,
                                      message.Avatar, message.Address);
        account.UpdateUserTager(_user.UserId, DateTime.UtcNow);
        var result = _accountRepository.UpdateAccountAsync(account).Result;

        if (Commit(result))
            Bus.RaiseEvent(new PersonalAccountUpdatedEvent(StoredEventType.Update, _user.UserId, _user.FullName,
                                                           account.Id, account.UserName, _user.UnitUserId,
                                                           _user.PortalId));

        return Task.FromResult(true);
    }

#endregion

#region Methods

    public void Dispose()
    {
        _accountRepository.Dispose();
        //_soGDRepository.Dispose();
        //_educationDepartmentRepository.Dispose();
        //_hSCanBoRepository.Dispose();
    }

#endregion

#region Fields

    private readonly IAccountRepository _accountRepository;

    //private readonly IDepartmentOfEducationRepository _soGDRepository;
    //  private readonly IEducationDepartmentRepository _educationDepartmentRepository;
    private readonly IUnitOfWork                  _uow;
    private readonly IUser                        _user;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMediatorHandler             Bus;

#endregion


    //public async Task<bool> Handle(ReissuePasswordCommand message, CancellationToken cancellationToken)
    //{
    //    if (!message.IsValid())
    //    {
    //        NotifyValidationErrors(message);
    //        return await Task.FromResult(false);
    //    }
    //    var account = _accountRepository.GetUserByIdAsync(message.UserId).Result;

    //    if (account == null)
    //    {
    //        await Bus.RaiseEvent(new DomainNotification(message.MessageType, "không tìm thấy tài khoản."));
    //        return await Task.FromResult(false);
    //    }

    //    var code = await _userManager.GeneratePasswordResetTokenAsync(account);
    //    var result = await _userManager.ResetPasswordAsync(account, code, message.NewPassword);

    //    if (result.Succeeded)
    //    {
    //        await Bus.RaiseEvent(new ReissusePasswordEvent(StoredEventType.Update, Guid.Parse(_user.UserId), _user.FullName, Guid.Parse(account.Id), account.FullName, Guid.Parse(account.ManagementUnitId), account.PortalId, message.NewPassword));
    //        return await Task.FromResult(true);
    //    }
    //    return await Task.FromResult(false);
    //}
}