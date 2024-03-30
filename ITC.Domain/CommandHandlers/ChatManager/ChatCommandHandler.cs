#region

using System;
using System.Threading;
using System.Threading.Tasks;
using ITC.Domain.Commands.ChatManager;
using ITC.Domain.Core.Bus;
using ITC.Domain.Core.Events;
using ITC.Domain.Core.Notifications;
using ITC.Domain.Interfaces;
using ITC.Domain.Interfaces.ChatManager;
using ITC.Domain.Interfaces.SystemManagers.SystemLogs;
using MediatR;
using NCore.Enums;
using NCore.Helpers;
using Newtonsoft.Json;

#endregion

namespace ITC.Domain.CommandHandlers.ChatManager;

#region

#endregion

/// <summary>
///     Command Handler chat
/// </summary>
public class ChatCommandHandler : CommandHandler,
                                  IRequestHandler<DeleteChatCommand, bool>
{
#region Constructors

    /// <summary>
    ///     Hàm dựng với tham số
    /// </summary>
    /// <param name="user"></param>
    /// <param name="chatQueries"></param>
    /// <param name="systemLogRepository"></param>
    /// <param name="uow"></param>
    /// <param name="bus"></param>
    /// <param name="notifications"></param>
    public ChatCommandHandler(IUser                                    user,
                              IChatQueries                             chatQueries,
                              ISystemLogRepository                     systemLogRepository,
                              IUnitOfWork                              uow,
                              IMediatorHandler                         bus,
                              INotificationHandler<DomainNotification> notifications) :
        base(uow, bus, notifications)
    {
        _user                = user;
        _chatQueries         = chatQueries;
        _systemLogRepository = systemLogRepository;
    }

#endregion

#region IRequestHandler<DeleteChatCommand,bool> Members

    /// <summary>
    ///     Handle xóa
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> Handle(DeleteChatCommand command, CancellationToken cancellationToken)
    {
        // Gán mặc định giá trị trả về khi xử lý = 0
        command.ResultCommand = false;

        if (!command.IsValid())
        {
            NotifyValidationErrors(command);
            return await Task.FromResult(false);
        }

        var iResult = _chatQueries.DeleteAsync(command.ListModel).Result;
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
                                                              JsonConvert.SerializeObject(command.ListModel),
                                                              _user.UserId));
            //==========================================
            return await Task.FromResult(true);
        }

        NotifyValidationErrors(NErrorHelper.xoa_khong_thanh_cong);
        return await Task.FromResult(false);
    }

#endregion

#region Fields

    private readonly IChatQueries         _chatQueries;
    private readonly ISystemLogRepository _systemLogRepository;
    private readonly IUser                _user;

#endregion
}