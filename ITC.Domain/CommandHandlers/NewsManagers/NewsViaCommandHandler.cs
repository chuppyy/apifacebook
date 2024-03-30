using System;
using System.Threading;
using System.Threading.Tasks;
using ITC.Domain.Commands.NewsManagers.NewsVia;
using ITC.Domain.Core.Bus;
using ITC.Domain.Core.Events;
using ITC.Domain.Core.Notifications;
using ITC.Domain.Interfaces;
using ITC.Domain.Interfaces.NewsManagers.NewsViaManagers;
using ITC.Domain.Interfaces.SystemManagers.SystemLogs;
using ITC.Domain.Models.NewsManagers;
using MediatR;
using NCore.Enums;
using NCore.Helpers;
using Newtonsoft.Json;

namespace ITC.Domain.CommandHandlers.NewsManagers;

/// <summary>
///     Command Handler loại nhóm tin
/// </summary>
public class NewsViaCommandHandler : CommandHandler,
                                     IRequestHandler<AddNewsViaCommand, bool>,
                                     IRequestHandler<UpdateNewsViaCommand, bool>,
                                     IRequestHandler<DeleteNewsViaCommand, bool>
{
#region Constructors

    /// <summary>
    ///     Hàm dựng với tham số
    /// </summary>
    /// <param name="user"></param>
    /// <param name="newsViaRepository"></param>
    /// <param name="newsViaQueries"></param>
    /// <param name="systemLogRepository"></param>
    /// <param name="uow"></param>
    /// <param name="bus"></param>
    /// <param name="notifications"></param>
    public NewsViaCommandHandler(IUser                                    user,
                                 INewsViaRepository                       newsViaRepository,
                                 INewsViaQueries                          newsViaQueries,
                                 ISystemLogRepository                     systemLogRepository,
                                 IUnitOfWork                              uow,
                                 IMediatorHandler                         bus,
                                 INotificationHandler<DomainNotification> notifications) :
        base(uow, bus, notifications)
    {
        _user                = user;
        _repository          = newsViaRepository;
        _queries             = newsViaQueries;
        _systemLogRepository = systemLogRepository;
    }

#endregion

#region IRequestHandler<AddNewsViaCommand,bool> Members

    /// <summary>
    ///     Handle thêm mới
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> Handle(AddNewsViaCommand command, CancellationToken cancellationToken)
    {
        // Gán mặc định giá trị trả về khi xử lý = 0
        command.ResultCommand = false;

        if (!command.IsValid())
        {
            NotifyValidationErrors(command);
            return await Task.FromResult(false);
        }

        var iPosition   = _repository.GetMaxPosition().Result;
        var iKey        = Guid.NewGuid();
        var userCreated = _user.UserId;
        var rAdd = new NewsVia(iKey,
                               command.Code,
                               command.Content,
                               command.Token,
                               command.IdTkQc,
                               command.StaffId,
                               iPosition,
                               userCreated);
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
                                                          JsonConvert.SerializeObject(rAdd),
                                                          userCreated));
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

#region IRequestHandler<DeleteNewsViaCommand,bool> Members

    /// <summary>
    ///     Handle xóa
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> Handle(DeleteNewsViaCommand command, CancellationToken cancellationToken)
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

#region IRequestHandler<UpdateNewsViaCommand,bool> Members

    /// <summary>
    ///     Handle cập nhật
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> Handle(UpdateNewsViaCommand command, CancellationToken cancellationToken)
    {
        // Gán mặc định giá trị trả về khi xử lý = 0
        command.ResultCommand = false;

        if (!command.IsValid())
        {
            NotifyValidationErrors(command);
            return await Task.FromResult(false);
        }
        
        var userCreated = _user.UserId;
        var existing    = _repository.GetAsync(command.Id).Result;
        if (existing != null)
        {
            existing.Update(command.Code,
                            command.Content,
                            command.Token,
                            command.IdTkQc,
                            command.StaffId,
                            userCreated);
            _repository.Update(existing);
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
                                                              userCreated));
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

    private readonly     INewsViaRepository   _repository;
    private readonly     INewsViaQueries      _queries;
    private readonly     ISystemLogRepository _systemLogRepository;
    private readonly     IUser                _user;

#endregion
}