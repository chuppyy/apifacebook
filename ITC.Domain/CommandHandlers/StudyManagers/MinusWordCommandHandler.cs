using System;
using System.Threading;
using System.Threading.Tasks;
using ITC.Domain.Commands.NewsManagers.MinusWord;
using ITC.Domain.Core.Bus;
using ITC.Domain.Core.Events;
using ITC.Domain.Core.Notifications;
using ITC.Domain.Interfaces;
using ITC.Domain.Interfaces.StudyManagers.MinusWord;
using ITC.Domain.Interfaces.SystemManagers.SystemLogs;
using ITC.Domain.Models.MenuManager;
using ITC.Domain.StoredEvents;
using MediatR;
using NCore.Enums;
using NCore.Helpers;
using Newtonsoft.Json;

namespace ITC.Domain.CommandHandlers.StudyManagers;

#region

#endregion

/// <summary>
///     Command Handler danh sách môn học
/// </summary>
public class MinusWordCommandHandler : CommandHandler,
                                       IRequestHandler<AddMinusWordCommand, bool>,
                                       IRequestHandler<UpdateMinusWordCommand, bool>,
                                       IRequestHandler<DeleteMinusWordCommand, bool>,
                                       IRequestHandler<UpdatePositionMinusWordCommand, bool>
{
#region Constructors

    /// <summary>
    ///     Hàm dựng với tham số
    /// </summary>
    /// <param name="user"></param>
    /// <param name="minusWordRepository"></param>
    /// <param name="minusWordQueries"></param>
    /// <param name="systemLogRepository"></param>
    /// <param name="uow"></param>
    /// <param name="bus"></param>
    /// <param name="notifications"></param>
    public MinusWordCommandHandler(IUser                                    user,
                                   IMinusWordRepository                     minusWordRepository,
                                   IMinusWordQueries                        minusWordQueries,
                                   ISystemLogRepository                     systemLogRepository,
                                   IUnitOfWork                              uow,
                                   IMediatorHandler                         bus,
                                   INotificationHandler<DomainNotification> notifications) :
        base(uow, bus, notifications)
    {
        _user                = user;
        _repository          = minusWordRepository;
        _queries             = minusWordQueries;
        _systemLogRepository = systemLogRepository;
        _bus                 = bus;
    }

#endregion

#region IRequestHandler<AddMinusWordCommand,bool> Members

    /// <summary>
    ///     Handle thêm mới
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> Handle(AddMinusWordCommand command, CancellationToken cancellationToken)
    {
        command.ResultCommand = false;
        if (!command.IsValid())
        {
            NotifyValidationErrors(command);
            return await Task.FromResult(false);
        }

        var iKey         = Guid.NewGuid();
        var iUserCreated = _user.UserId;
        var rAdd = new MinusWord(iKey,
                                 command.Root,
                                 command.Replace,
                                 command.Description,
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
            command.ResultCommand = true;
            return await Task.FromResult(true);
        }

        NotifyValidationErrors(NErrorHelper.them_moi_khong_thanh_cong);
        return await Task.FromResult(false);
    }

#endregion

#region IRequestHandler<DeleteMinusWordCommand,bool> Members

    /// <summary>
    ///     Handle xóa môn học
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> Handle(DeleteMinusWordCommand command, CancellationToken cancellationToken)
    {
        // Gán mặc định giá trị trả về khi xử lý = 0
        command.ResultCommand = false;

        if (!command.IsValid())
        {
            NotifyValidationErrors(command);
            return await Task.FromResult(false);
        }

        var iUserCreated = _user.UserId;
        var iResult      = _queries.DeleteAsync(command.Model).Result;
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
                                                              iUserCreated));
            _systemLogRepository.SaveChanges();
            //==========================================
            return await Task.FromResult(true);
        }

        NotifyValidationErrors(NErrorHelper.xoa_khong_thanh_cong);
        return await Task.FromResult(false);
    }

#endregion

#region IRequestHandler<UpdateMinusWordCommand,bool> Members

    /// <summary>
    ///     Handle cập nhật môn học
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> Handle(UpdateMinusWordCommand command, CancellationToken cancellationToken)
    {
        command.ResultCommand = false;
        if (!command.IsValid())
        {
            NotifyValidationErrors(command);
            return await Task.FromResult(false);
        }

        var iUserCreated = _user.UserId;
        var existing     = _repository.GetAsync(command.Id).Result;
        if (existing != null)
        {
            existing.Update(command.Root,
                            command.Replace,
                            command.Description,
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

#region IRequestHandler<UpdatePositionMinusWordCommand,bool> Members

    /// <summary>
    ///     Handle cập nhật vị trí môn học
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> Handle(UpdatePositionMinusWordCommand command, CancellationToken cancellationToken)
    {
        // Gán mặc định giá trị trả về khi xử lý = 0
        command.ResultCommand = false;

        if (!command.IsValid())
        {
            NotifyValidationErrors(command);
            return await Task.FromResult(false);
        }

        foreach (var items in command.LocationModals)
        {
            var iValue = _repository.GetAsync(items.Id).Result;
            iValue.Position = items.Position;
            _repository.Update(iValue);
        }

        if (Commit())
        {
            command.ResultCommand = true;
            await _bus.RaiseEvent(new ActionStoreEvent.UpdateActionStoreEvent(StoredEventType.Update,
                                                                              _user.UserId,
                                                                              _user.FullName,
                                                                              _user.UserId,
                                                                              _user.FullName,
                                                                              _user.UnitUserId,
                                                                              _user.PortalId));
            return await Task.FromResult(true);
        }

        NotifyValidationErrors(NErrorHelper.cap_nhat_khong_thanh_cong);
        return await Task.FromResult(false);
    }

#endregion

#region Fields

    private new readonly IMediatorHandler     _bus;
    private readonly     IMinusWordRepository _repository;
    private readonly     IMinusWordQueries    _queries;
    private readonly     ISystemLogRepository _systemLogRepository;
    private readonly     IUser                _user;

#endregion
}