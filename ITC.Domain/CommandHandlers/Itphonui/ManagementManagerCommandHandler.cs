using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ITC.Domain.Commands.Itphonui.ManagementManagers;
using ITC.Domain.Core.Bus;
using ITC.Domain.Core.Events;
using ITC.Domain.Core.Notifications;
using ITC.Domain.Interfaces;
using ITC.Domain.Interfaces.Itphonui.ManagementManagers;
using ITC.Domain.Interfaces.SystemManagers.SystemLogs;
using ITC.Domain.Models.Itphonui;
using MediatR;
using NCore.Enums;
using NCore.Helpers;
using Newtonsoft.Json;

namespace ITC.Domain.CommandHandlers.Itphonui;

#region

#endregion

/// <summary>
///     Command Handler quản lý đơn vị
/// </summary>
public class ManagementManagerCommandHandler : CommandHandler,
                                               IRequestHandler<AddManagementManagerCommand, bool>,
                                               IRequestHandler<AddManagementDetailManagerCommand, bool>,
                                               IRequestHandler<UpdateManagementManagerCommand, bool>,
                                               IRequestHandler<DeleteManagementManagerCommand, bool>
{
#region Constructors

    /// <summary>
    ///     Hàm dựng với tham số
    /// </summary>
    /// <param name="user"></param>
    /// <param name="managementManagerRepository"></param>
    /// <param name="managementManagerQueries"></param>
    /// <param name="managementDetailManagerRepository"></param>
    /// <param name="systemLogRepository"></param>
    /// <param name="uow"></param>
    /// <param name="bus"></param>
    /// <param name="notifications"></param>
    public ManagementManagerCommandHandler(
        IUser                                    user,
        IManagementManagerRepository             managementManagerRepository,
        IManagementManagerQueries                managementManagerQueries,
        IManagementDetailManagerRepository       managementDetailManagerRepository,
        ISystemLogRepository                     systemLogRepository,
        IUnitOfWork                              uow,
        IMediatorHandler                         bus,
        INotificationHandler<DomainNotification> notifications) :
        base(uow, bus, notifications)
    {
        _user                              = user;
        _repository                        = managementManagerRepository;
        _queries                           = managementManagerQueries;
        _managementDetailManagerRepository = managementDetailManagerRepository;
        _systemLogRepository               = systemLogRepository;
        _bus                               = bus;
    }

#endregion

#region IRequestHandler<AddManagementDetailManagerCommand,bool> Members

    /// <summary>
    ///     Handle thêm đơn vị vào dự án
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> Handle(AddManagementDetailManagerCommand command, CancellationToken cancellationToken)
    {
        command.ResultCommand = false;
        if (!command.IsValid())
        {
            NotifyValidationErrors(command);
            return await Task.FromResult(false);
        }

        var iUserCreated = _user.UserId;
        foreach (var rAdd in command.Models.Select(items => new ManagementDetailManager(Guid.NewGuid(),
                                                       command.ProjectId,
                                                       items,
                                                       iUserCreated)))
            await _managementDetailManagerRepository.AddAsync(rAdd);

        //=================Ghi Log==================
        await _systemLogRepository.AddAsync(new SystemLog(Guid.NewGuid(),
                                                          DateTime.Now,
                                                          SystemLogEnumeration.AddNew.Id,
                                                          _user.StaffId,
                                                          _user.StaffName,
                                                          GetType().Name,
                                                          "",
                                                          Guid.Empty,
                                                          "",
                                                          JsonConvert.SerializeObject(command),
                                                          iUserCreated));
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

#region IRequestHandler<AddManagementManagerCommand,bool> Members

    /// <summary>
    ///     Handle thêm mới
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> Handle(AddManagementManagerCommand command, CancellationToken cancellationToken)
    {
        command.ResultCommand = false;
        if (!command.IsValid())
        {
            NotifyValidationErrors(command);
            return await Task.FromResult(false);
        }

        command.ParentId = string.IsNullOrEmpty(command.ParentId) ? Guid.Empty.ToString() : command.ParentId;
        var iKey         = Guid.NewGuid();
        var iProjectId   = _user.ProjectId;
        var iUserCreated = _user.UserId;
        var iPosition    = _repository.GetMaxPosition(iProjectId).Result;
        var rAdd = new ManagementManager(iKey,
                                         iProjectId,
                                         command.Name,
                                         command.Description,
                                         command.ParentId,
                                         iPosition,
                                         command.LevelCompetitionId,
                                         command.Symbol,
                                         command.AccountDefault,
                                         iUserCreated);
        await _repository.AddAsync(rAdd);
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
                                                          iUserCreated));
        //==========================================
        if (Commit())
        {
            var lModal = _repository.GetAllAsync().Result
                                    .Where(x => x.IsDeleted == false && x.ProjectId == iProjectId).ToList();
            _repository.DeQuyLeftRight(lModal, 1, Guid.Empty.ToString());
            _repository.SaveChanges();

            command.ResultCommand = true;
            return await Task.FromResult(true);
        }

        NotifyValidationErrors(NErrorHelper.them_moi_khong_thanh_cong);
        return await Task.FromResult(false);
    }

#endregion

#region IRequestHandler<DeleteManagementManagerCommand,bool> Members

    /// <summary>
    ///     Handle xóa môn học
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> Handle(DeleteManagementManagerCommand command, CancellationToken cancellationToken)
    {
        // Gán mặc định giá trị trả về khi xử lý = 0
        command.ResultCommand = false;

        if (!command.IsValid())
        {
            NotifyValidationErrors(command);
            return await Task.FromResult(false);
        }

        var iProjectId = _user.ProjectId;
        var iResult    = _queries.DeleteAsync(command.Model).Result;
        if (iResult > 0)
        {
            var lModal =
                _repository.GetAllAsync().Result
                           .Where(x => x.IsDeleted == false && x.ProjectId == iProjectId)
                           .ToList();
            _repository.DeQuyLeftRight(lModal, 1, Guid.Empty.ToString());
            _repository.SaveChanges();

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

#region IRequestHandler<UpdateManagementManagerCommand,bool> Members

    /// <summary>
    ///     Handle cập nhật
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> Handle(UpdateManagementManagerCommand command, CancellationToken cancellationToken)
    {
        command.ResultCommand = false;
        if (!command.IsValid())
        {
            NotifyValidationErrors(command);
            return await Task.FromResult(false);
        }

        command.ParentId = string.IsNullOrEmpty(command.ParentId) ? Guid.Empty.ToString() : command.ParentId;
        var iUserCreated = _user.UserId;

        var existing = _repository.GetAsync(command.Id).Result;
        if (existing != null)
        {
            existing.Update(command.Name,
                            command.Description,
                            command.ParentId,
                            existing.Position,
                            command.LevelCompetitionId,
                            command.Symbol,
                            command.AccountDefault,
                            iUserCreated);
            _repository.Update(existing);
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
                                                              iUserCreated));
            //==========================================
        }

        if (Commit())
        {
            command.ResultCommand = true;
            return await Task.FromResult(true);
        }

        NotifyValidationErrors(NErrorHelper.cap_nhat_khong_thanh_cong);
        return await Task.FromResult(false);
    }

#endregion

#region Fields

    private new readonly IMediatorHandler                   _bus;
    private readonly     IManagementManagerRepository       _repository;
    private readonly     IManagementManagerQueries          _queries;
    private readonly     IManagementDetailManagerRepository _managementDetailManagerRepository;
    private readonly     ISystemLogRepository               _systemLogRepository;
    private readonly     IUser                              _user;

#endregion
}