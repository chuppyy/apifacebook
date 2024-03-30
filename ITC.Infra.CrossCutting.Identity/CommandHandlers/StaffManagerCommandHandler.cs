using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ITC.Domain.Commands.CompanyManagers.StaffManager;
using ITC.Domain.Core.Bus;
using ITC.Domain.Core.Events;
using ITC.Domain.Core.Models;
using ITC.Domain.Core.Notifications;
using ITC.Domain.Interfaces;
using ITC.Domain.Interfaces.CompanyManagers.StaffManagers;
using ITC.Domain.Interfaces.SystemManagers.ServerFiles;
using ITC.Domain.Interfaces.SystemManagers.SystemLogs;
using ITC.Domain.Models.CompanyManagers;
using ITC.Domain.Models.SystemManagers;
using ITC.Infra.CrossCutting.Identity.Interfaces;
using ITC.Infra.CrossCutting.Identity.Models;
using ITC.Infra.CrossCutting.Identity.UoW;
using MediatR;
using NCore.Enums;
using NCore.Helpers;
using Newtonsoft.Json;

namespace ITC.Infra.CrossCutting.Identity.CommandHandlers;

public class StaffManagerCommandHandler : CommandIdentityHandler,
                                          IRequestHandler<AddStaffManagerCommand, bool>,
                                          IRequestHandler<UpdateStaffManagerCommand, bool>,
                                          IRequestHandler<DeleteStaffManagerCommand, bool>,
                                          IRequestHandler<AvatarManagerStaffManagerCommand, bool>
{
#region Constructors

    public StaffManagerCommandHandler(IAccountRepository                       accountRepository,
                                      IUnitOfWorkIdentity                      uow,
                                      IMediatorHandler                         bus,
                                      IUser                                    user,
                                      IStaffManagerRepository                  staffManagerRepository,
                                      IStaffManagerQueries                     staffManagerQueries,
                                      IServerFileRepository                    serverFileRepository,
                                      ISystemLogRepository                     systemLogRepository,
                                      INotificationHandler<DomainNotification> notifications) :
        base(uow, bus, notifications)
    {
        _accountRepository      = accountRepository;
        _staffManagerRepository = staffManagerRepository;
        _staffManagerQueries    = staffManagerQueries;
        _serverFileRepository   = serverFileRepository;
        _systemLogRepository    = systemLogRepository;
        _user                   = user;
    }

#endregion

#region IRequestHandler<AddStaffManagerCommand,bool> Members

    public async Task<bool> Handle(AddStaffManagerCommand command, CancellationToken cancellationToken)
    {
        // Gán mặc định giá trị trả về khi xử lý = 0
        command.ResultCommand = false;

        if (!command.IsValid())
        {
            NotifyValidationErrors(command);
            return await Task.FromResult(false);
        }

        var cur        = _user.GetIdentityUser();
        var userTarget = new UserTarget(cur.UserId, cur.UserId, DateTime.Now, DateTime.Now);
        var applicationUser = new ApplicationUser(command.UserName, command.Name, command.Email, command.Phone,
                                                  "54397FD1-5C6B-4006-83D7-0FCE030686A6", "",
                                                  cur.UnitUserId, "", cur.PortalId, userTarget)
        {
            EmailConfirmed = true
        };
        var iPassword = !string.IsNullOrEmpty(command.Password) ? command.Password : "123456";
        var rUser     = _accountRepository.AddAccountAsync(applicationUser, iPassword).Result;
        if (CustomCommit(rUser))
        {
            //Lưu thành công => Lưu dữ liệu hồ sơ nhân viên
            var iKey = Guid.NewGuid();
            var rAdd = new StaffManager(iKey, command.Name, command.Description, _user.ManagementId,
                                        command.Phone, command.Address, command.Email,
                                        command.RoomManagerId, applicationUser.Id, command.AuthorityId,
                                        command.AvatarId, command.UserTypeManagerId, _user.ProjectId, command.UserCode, _user.UserId);
            await _staffManagerRepository.AddAsync(rAdd);
            //=================Ghi Log==================
            await _systemLogRepository.AddAsync(new SystemLog(Guid.NewGuid(),
                                                              DateTime.Now,
                                                              SystemLogEnumeration.AddNew.Id,
                                                              _user.StaffId,
                                                              _user.StaffName,
                                                              GetType().Name,
                                                              "",
                                                              iKey,
                                                              "",
                                                              JsonConvert.SerializeObject(command),
                                                              _user.UserId));
            //==========================================
            if (_staffManagerRepository.SaveChanges() > 0)
            {
                command.Id            = iKey;
                command.ResultCommand = true;
                return await Task.FromResult(true);
            }
        }

        NotifyValidationErrors(NErrorHelper.them_moi_khong_thanh_cong);
        return await Task.FromResult(false);
    }

#endregion

#region IRequestHandler<AvatarManagerStaffManagerCommand,bool> Members

    public async Task<bool> Handle(AvatarManagerStaffManagerCommand command, CancellationToken cancellationToken)
    {
        //Gán mặc định kết quả trả về là false
        command.ResultCommand = false;

        if (!command.IsValid())
        {
            NotifyValidationErrors(command);
            return await Task.FromResult(false);
        }

        var iResult = _staffManagerRepository.GetByUserId(command.Id.ToString()).Result;
        if (iResult == null)
        {
            NotifyValidationErrors(NErrorHelper.du_lieu_khong_ton_tai + ": " + command.Id);
            return await Task.FromResult(false);
        }

        if (iResult.IsDeleted)
        {
            NotifyValidationErrors(NErrorHelper.du_lieu_da_bi_xoa + ": " + command.Id);
            return await Task.FromResult(false);
        }

        if (command.IsDeleteAvatar == 1)
        {
            // Xóa ảnh đại diện
            iResult.AvatarId = Guid.Empty;
        }
        else
        {
            // Cập nhật ảnh đại diện
            var iPosition = _serverFileRepository.GetMaxPosition(_user.UserId).Result;
            // Lưu ảnh dại diện =>
            var serverFileAdd = new ServerFile(new Guid(), command.Base64, iPosition, command.CreateBy);
            await _serverFileRepository.AddAsync(serverFileAdd);
            if (_serverFileRepository.SaveChanges() > 0)
            {
                iResult.AvatarId = serverFileAdd.Id;
            }
            else
            {
                NotifyValidationErrors(NErrorHelper.cap_nhat_khong_thanh_cong);
                command.ResultCommand = false;
                return await Task.FromResult(false);
            }
        }

        _staffManagerRepository.Update(iResult);
        //=================Ghi Log==================
        await _systemLogRepository.AddAsync(new SystemLog(Guid.NewGuid(),
                                                          DateTime.Now,
                                                          SystemLogEnumeration.AddNew.Id,
                                                          _user.StaffId,
                                                          _user.StaffName,
                                                          GetType().Name,
                                                          "",
                                                          iResult.Id,
                                                          command.IsDeleteAvatar == 1
                                                              ? "Xỏa ảnh đại diện"
                                                              : "Thay đổi ảnh đại diện",
                                                          JsonConvert.SerializeObject(command),
                                                          _user.UserId));
        //==========================================
        var iSave = _staffManagerRepository.SaveChanges();
        if (iSave > 0)
        {
            command.ResultCommand = true;
            return await Task.FromResult(true);
        }

        NotifyValidationErrors(NErrorHelper.cap_nhat_khong_thanh_cong);
        return await Task.FromResult(false);
    }

#endregion

#region IRequestHandler<DeleteStaffManagerCommand,bool> Members

    public async Task<bool> Handle(DeleteStaffManagerCommand command, CancellationToken cancellationToken)
    {
        // Gán mặc định giá trị trả về khi xử lý = 0
        command.ResultCommand = false;

        if (!command.IsValid())
        {
            NotifyValidationErrors(command);
            return await Task.FromResult(false);
        }

        var iResult = _staffManagerQueries.DeleteAsync(command.Model).Result;
        if (iResult > 0)
        {
            // Khóa tài khoản đã bị xóa
            foreach (var applicationUser in command.Model
                                                   .Select(zGuid => _accountRepository
                                                                    .GetUserByIdAsync(zGuid.ToString()).Result)
                                                   .Where(applicationUser => applicationUser != null))
                applicationUser.SetActive(false);

            _accountRepository.SaveChanges();

            command.ResultCommand = true;
            //=================Ghi Log==================
            await _systemLogRepository.AddAsync(new SystemLog(Guid.NewGuid(),
                                                              DateTime.Now,
                                                              SystemLogEnumeration.Deleted.Id,
                                                              _user.StaffId,
                                                              _user.StaffName,
                                                              GetType().Name,
                                                              "",
                                                              Guid.Empty,
                                                              "",
                                                              JsonConvert.SerializeObject(command.Model),
                                                              _user.UserId));
            _systemLogRepository.SaveChanges();
            //==========================================
            return await Task.FromResult(true);
        }

        NotifyValidationErrors(NErrorHelper.xoa_khong_thanh_cong);
        return await Task.FromResult(false);
    }

#endregion

#region IRequestHandler<UpdateStaffManagerCommand,bool> Members

    public async Task<bool> Handle(UpdateStaffManagerCommand command, CancellationToken cancellationToken)
    {
        // Gán mặc định giá trị trả về khi xử lý = 0
        command.ResultCommand = false;

        if (!command.IsValid())
        {
            NotifyValidationErrors(command);
            return await Task.FromResult(false);
        }

        var existing = _staffManagerRepository.GetAsync(command.Id).Result;
        if (existing != null)
        {
            existing.Update(command.Name,
                            command.Description,
                            Guid.Empty,
                            command.Phone,
                            command.Address,
                            command.Email,
                            command.RoomManagerId,
                            existing.UserId,
                            command.AuthorityId,
                            string.Compare(command.AvatarId.ToString(), Guid.Empty.ToString(),
                                           StringComparison.Ordinal) == 0
                                ? existing.AvatarId
                                : command.AvatarId,
                            command.UserTypeManagerId,
                            command.UserCode,
                            command.CreateBy);
            _staffManagerRepository.Update(existing);

            if (_staffManagerRepository.SaveChanges() > 0)
            {
                //=================Ghi Log==================
                await _systemLogRepository.AddAsync(new SystemLog(Guid.NewGuid(),
                                                                  DateTime.Now,
                                                                  SystemLogEnumeration.Update.Id,
                                                                  _user.StaffId,
                                                                  _user.StaffName,
                                                                  GetType().Name,
                                                                  "",
                                                                  existing.Id,
                                                                  JsonConvert.SerializeObject(existing),
                                                                  JsonConvert.SerializeObject(command),
                                                                  _user.UserId));
                //==========================================
                var applicationUser = _accountRepository.GetUserByIdAsync(existing.UserId).Result;
                if (applicationUser != null)
                {
                    applicationUser.SetName(command.Name);
                    // Cập nhật mật khẩu
                    if (!string.IsNullOrEmpty(command.Password))
                    {
                       // var identityResult = _accountRepository.AddAccountAsync(applicationUser, command.Password).Result;
                        //_accountRepository.UpdateAccountAsync(applicationUser)
                    }

                    _accountRepository.SaveChanges();
                }
                else
                {
                    _systemLogRepository.SaveChanges();
                }

                command.ResultCommand = true;
                return await Task.FromResult(true);
            }
        }

        NotifyValidationErrors(NErrorHelper.cap_nhat_khong_thanh_cong);
        return await Task.FromResult(false);
    }

#endregion

#region Fields

    private readonly IAccountRepository      _accountRepository;
    private readonly IStaffManagerRepository _staffManagerRepository;
    private readonly IStaffManagerQueries    _staffManagerQueries;
    private readonly IServerFileRepository   _serverFileRepository;
    private readonly ISystemLogRepository    _systemLogRepository;
    private readonly IUser                   _user;

    #endregion
}