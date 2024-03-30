using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ITC.Domain.Commands.SystemManagers.NotificationManagers;
using ITC.Domain.Core.Bus;
using ITC.Domain.Core.Events;
using ITC.Domain.Core.Notifications;
using ITC.Domain.Interfaces;
using ITC.Domain.Interfaces.CompanyManagers.StaffManagers;
using ITC.Domain.Interfaces.SystemManagers.NotificationManagers;
using ITC.Domain.Interfaces.SystemManagers.SystemLogs;
using ITC.Domain.Models.SystemManagers;
using MediatR;
using NCore.Actions;
using NCore.Enums;
using NCore.Helpers;
using Newtonsoft.Json;

namespace ITC.Domain.CommandHandlers.SystemManagers;

#region

#endregion

/// <summary>
///     Command Handler thông báo hệ thống
/// </summary>
public class NotificationManagerCommandHandler : CommandHandler,
                                                 IRequestHandler<AddNotificationManagerCommand, bool>,
                                                 IRequestHandler<UpdateNotificationManagerCommand, bool>,
                                                 IRequestHandler<DeleteNotificationManagerCommand, bool>,
                                                 IRequestHandler<UpdateReadNotificationManagerCommand, bool>
{
#region Constructors

    /// <summary>
    ///     Hàm dựng với tham số
    /// </summary>
    /// <param name="user"></param>
    /// <param name="notificationManagerRepository"></param>
    /// <param name="notificationManagerQueries"></param>
    /// <param name="notificationUserManagerRepository"></param>
    /// <param name="notificationAttackManagerRepository"></param>
    /// <param name="systemLogRepository"></param>
    /// <param name="staffManagerQueries"></param>
    /// <param name="uow"></param>
    /// <param name="bus"></param>
    /// <param name="notifications"></param>
    public NotificationManagerCommandHandler(
        IUser                                    user,
        INotificationManagerRepository           notificationManagerRepository,
        INotificationManagerQueries              notificationManagerQueries,
        INotificationUserManagerRepository       notificationUserManagerRepository,
        INotificationAttackManagerRepository     notificationAttackManagerRepository,
        ISystemLogRepository                     systemLogRepository,
        IStaffManagerQueries                     staffManagerQueries,
        IUnitOfWork                              uow,
        IMediatorHandler                         bus,
        INotificationHandler<DomainNotification> notifications) :
        base(uow, bus, notifications)
    {
        _user                                = user;
        _repository                          = notificationManagerRepository;
        _queries                             = notificationManagerQueries;
        _notificationUserManagerRepository   = notificationUserManagerRepository;
        _notificationAttackManagerRepository = notificationAttackManagerRepository;
        _systemLogRepository                 = systemLogRepository;
        _staffManagerQueries                 = staffManagerQueries;
        _bus                                 = bus;
    }

#endregion

#region IRequestHandler<AddNotificationManagerCommand,bool> Members

    /// <summary>
    ///     Handle thêm mới
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> Handle(AddNotificationManagerCommand command, CancellationToken cancellationToken)
    {
        // Gán mặc định giá trị trả về khi xử lý = 0
        command.ResultCommand = false;
        var iUserCreateId = _user.UserId;
        var iStaffId      = _user.StaffId;
        var iProjectId    = _user.ProjectId;
        var iManagementId = _user.ManagementId;

        if (!command.IsValid())
        {
            NotifyValidationErrors(command);
            return await Task.FromResult(false);
        }

        //=============================================Xử lý thời gian==============================================
        var iCore          = new NCoreHelper();
        var iDateTimeStart = iCore.ConvertStringToDateTimeFromVueJs(command.DateStart);
        var iDateTimeEnd   = iCore.ConvertStringToDateTimeFromVueJs(command.DateEnd);
        //==========================================================================================================
        //=====================================Danh sách người dùng hệ thống========================================
        var lStaffInSystem =
            _staffManagerQueries.GetStaffInSystem().Result
                                .Select(x => x.Id)
                                .ToList();
        //==========================================================================================================
        var iPosition = _repository.GetMaxPosition().Result;
        var iKey      = Guid.NewGuid();
        var rAdd = new NotificationManager(iKey,
                                           command.Name,
                                           command.Content,
                                           command.FileAttackId,
                                           ActionStatusEnum.Active.Id,
                                           command.IsSendAll,
                                           command.IsRun,
                                           iProjectId,
                                           iManagementId,
                                           iPosition,
                                           iDateTimeStart,
                                           iDateTimeEnd,
                                           command.IsLimitedTime,
                                           command.IsShowMain,
                                           iUserCreateId);
        rAdd.AddUser(command.IsSendAll ? lStaffInSystem : command.UserModels, iUserCreateId);
        rAdd.AddAttack(command.FileAttackModels, iUserCreateId);
        await _repository.AddAsync(rAdd);
        //=================Ghi Log==================
        await _systemLogRepository.AddAsync(new SystemLog(Guid.NewGuid(),
                                                          DateTime.Now,
                                                          SystemLogEnumeration.AddNew.Id,
                                                          iStaffId,
                                                          _user.StaffName,
                                                          GetType().Name,
                                                          "",
                                                          iKey,
                                                          "",
                                                          JsonConvert.SerializeObject(rAdd),
                                                          iUserCreateId));
        //==========================================
        if (Commit())
        {
            command.ResultCommand = true;
            return await Task.FromResult(true);
        }

        NotifyValidationErrors(NErrorHelper.them_moi_khong_thanh_cong);
        return await Task.FromResult(false);
    }

#endregion

#region IRequestHandler<DeleteNotificationManagerCommand,bool> Members

    /// <summary>
    ///     Handle xóa
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> Handle(DeleteNotificationManagerCommand command, CancellationToken cancellationToken)
    {
        // Gán mặc định giá trị trả về khi xử lý = 0
        command.ResultCommand = false;

        if (!command.IsValid())
        {
            NotifyValidationErrors(command);
            return await Task.FromResult(false);
        }

        var iResult = _queries.DeleteAsync(command.Model).Result;
        if (iResult > 0)
        {
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

#region IRequestHandler<UpdateNotificationManagerCommand,bool> Members

    /// <summary>
    ///     Handle cập nhật
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> Handle(UpdateNotificationManagerCommand command, CancellationToken cancellationToken)
    {
        // Gán mặc định giá trị trả về khi xử lý = 0
        command.ResultCommand = false;
        var iUserCreateId = _user.UserId;
        var iStaffId      = _user.StaffId;

        if (!command.IsValid())
        {
            NotifyValidationErrors(command);
            return await Task.FromResult(false);
        }

        //=============================================Xử lý thời gian==============================================
        var iCore          = new NCoreHelper();
        var iDateTimeStart = iCore.ConvertStringToDateTimeFromVueJs(command.DateStart);
        var iDateTimeEnd   = iCore.ConvertStringToDateTimeFromVueJs(command.DateEnd);
        //==========================================================================================================
        var existing = _repository.GetAsync(command.Id).Result;
        if (existing != null)
        {
            existing.Update(command.Name,
                            command.Content,
                            command.FileAttackId,
                            existing.StatusId,
                            command.IsSendAll,
                            command.IsRun,
                            existing.ProjectId,
                            existing.ManagementId,
                            existing.Position,
                            iDateTimeStart,
                            iDateTimeEnd,
                            command.IsLimitedTime,
                            command.IsShowMain,
                            iUserCreateId);
            _repository.Update(existing);

        #region ===========================================Xử lý User===============================================

            var lStaffInSystem =
                _staffManagerQueries.GetStaffInSystem().Result
                                    .Select(x => x.Id)
                                    .ToList();

            var lUserSendFromFe = command.IsSendAll ? lStaffInSystem : command.UserModels;
            var lUserInDb =
                _notificationUserManagerRepository.GetListUserByNotificationId(command.Id).Result;
            var lUserNeedAddNew = new List<Guid>();
            var lUserNeedUpdate = new List<Guid>();
            var lUserNeedDelete = new List<Guid>();
            new NCoreHelper().ReturnListAddUpdateDelete(lUserSendFromFe,
                                                        lUserInDb,
                                                        ref lUserNeedAddNew,
                                                        ref lUserNeedUpdate,
                                                        ref lUserNeedDelete);
            foreach (var items in lUserNeedAddNew)
                await _notificationUserManagerRepository.AddAsync(new NotificationUserManager(Guid.NewGuid(),
                                                                      command.Id,
                                                                      false,
                                                                      items,
                                                                      null,
                                                                      iUserCreateId));

            foreach (var iExist in lUserNeedUpdate
                                   .Select(items => _notificationUserManagerRepository.GetAsync(items).Result)
                                   .Where(iExist => iExist != null))
            {
                iExist.IsRead       = false;
                iExist.DateTimeRead = null;
                iExist.IsDeleted    = false;
                _notificationUserManagerRepository.Update(iExist);
            }

            foreach (var iExist in lUserNeedUpdate
                                   .Select(items => _notificationUserManagerRepository.GetAsync(items).Result)
                                   .Where(iExist => iExist != null))
            {
                iExist.IsDeleted = true;
                _notificationUserManagerRepository.Update(iExist);
            }

        #endregion

        #region ===========================================Xử lý File===============================================

            var lFileAttackSendFromFe = command.FileAttackModels;
            var lFileAttackInDb =
                _notificationAttackManagerRepository.GetListFileByNotificationId(command.Id).Result;
            var lFileAttackNeedAddNew = new List<Guid>();
            var lFileAttackNeedUpdate = new List<Guid>();
            var lFileAttackNeedDelete = new List<Guid>();
            new NCoreHelper().ReturnListAddUpdateDelete(lFileAttackSendFromFe,
                                                        lFileAttackInDb,
                                                        ref lFileAttackNeedAddNew,
                                                        ref lFileAttackNeedUpdate,
                                                        ref lFileAttackNeedDelete);
            foreach (var items in lFileAttackNeedAddNew)
                await _notificationAttackManagerRepository
                    .AddAsync(new NotificationAttackManager(Guid.NewGuid(), command.Id, items, iUserCreateId));

            foreach (var iExist in lFileAttackNeedUpdate
                                   .Select(items => _notificationAttackManagerRepository.GetAsync(items).Result)
                                   .Where(iExist => iExist != null))
            {
                iExist.IsDeleted = false;
                _notificationAttackManagerRepository.Update(iExist);
            }

            foreach (var iExist in lFileAttackNeedUpdate
                                   .Select(items => _notificationAttackManagerRepository.GetAsync(items).Result)
                                   .Where(iExist => iExist != null))
            {
                iExist.IsDeleted = true;
                _notificationAttackManagerRepository.Update(iExist);
            }

        #endregion

            //=================Ghi Log==================
            await _systemLogRepository.AddAsync(new SystemLog(Guid.NewGuid(),
                                                              DateTime.Now,
                                                              SystemLogEnumeration.Update.Id,
                                                              iStaffId,
                                                              _user.StaffName,
                                                              GetType().Name,
                                                              "",
                                                              existing?.Id,
                                                              JsonConvert.SerializeObject(existing),
                                                              JsonConvert.SerializeObject(command),
                                                              iUserCreateId));
            //==========================================
            if (Commit())
            {
                command.ResultCommand = true;
                return await Task.FromResult(true);
            }

            NotifyValidationErrors(NErrorHelper.cap_nhat_khong_thanh_cong);
            return await Task.FromResult(false);
        }

        NotifyValidationErrors(NErrorHelper.du_lieu_khong_ton_tai + ": " + command.Id);
        return await Task.FromResult(false);
    }

#endregion

#region IRequestHandler<UpdateReadNotificationManagerCommand,bool> Members

    /// <summary>
    ///     Handle cập nhật trạng thái đã đọc
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> Handle(UpdateReadNotificationManagerCommand command, CancellationToken cancellationToken)
    {
        if (!command.IsValid())
        {
            NotifyValidationErrors(command);
            return await Task.FromResult(false);
        }

        var existing = _notificationUserManagerRepository.GetAsync(command.Id).Result;
        if (existing != null)
        {
            existing.IsRead       = true;
            existing.DateTimeRead = DateTime.Now;
            _notificationUserManagerRepository.Update(existing);

            //=================Ghi Log==================
            await _systemLogRepository.AddAsync(new SystemLog(Guid.NewGuid(),
                                                              DateTime.Now,
                                                              SystemLogEnumeration.Update.Id,
                                                              _user.StaffId,
                                                              _user.StaffName,
                                                              GetType().Name,
                                                              "",
                                                              existing?.Id,
                                                              JsonConvert.SerializeObject(existing),
                                                              JsonConvert.SerializeObject(command),
                                                              _user.UserId));
            //==========================================
            if (Commit())
            {
                command.ResultCommand = true;
                return await Task.FromResult(true);
            }

            NotifyValidationErrors(NErrorHelper.cap_nhat_khong_thanh_cong);
            return await Task.FromResult(false);
        }

        NotifyValidationErrors(NErrorHelper.du_lieu_khong_ton_tai + ": " + command.Id);
        return await Task.FromResult(false);
    }

#endregion

#region Fields

    private new readonly IMediatorHandler                     _bus;
    private readonly     INotificationManagerRepository       _repository;
    private readonly     INotificationManagerQueries          _queries;
    private readonly     INotificationUserManagerRepository   _notificationUserManagerRepository;
    private readonly     INotificationAttackManagerRepository _notificationAttackManagerRepository;
    private readonly     ISystemLogRepository                 _systemLogRepository;
    private readonly     IStaffManagerQueries                 _staffManagerQueries;
    private readonly     IUser                                _user;

#endregion
}