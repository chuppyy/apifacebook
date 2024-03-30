using System;
using System.Threading;
using System.Threading.Tasks;
using ITC.Domain.Commands.SystemManagers.RegisterEmailManagers;
using ITC.Domain.Core.Bus;
using ITC.Domain.Core.Events;
using ITC.Domain.Core.Notifications;
using ITC.Domain.Interfaces;
using ITC.Domain.Interfaces.SystemManagers.RegisterEmailManagers;
using ITC.Domain.Interfaces.SystemManagers.SystemLogs;
using ITC.Domain.Models.SystemManagers;
using MediatR;
using NCore.Enums;
using NCore.Helpers;
using Newtonsoft.Json;

namespace ITC.Domain.CommandHandlers.SystemManagers;

#region

#endregion

/// <summary>
///     Command Handler đăng ký email
/// </summary>
public class RegisterEmailCommandHandler : CommandHandler,
                                           IRequestHandler<RegRegisterEmailCommand, bool>
{
#region Constructors

    /// <summary>
    ///     Hàm dựng với tham số
    /// </summary>
    /// <param name="user"></param>
    /// <param name="registerEmailRepository"></param>
    /// <param name="systemLogRepository"></param>
    /// <param name="uow"></param>
    /// <param name="bus"></param>
    /// <param name="notifications"></param>
    public RegisterEmailCommandHandler(IUser                                    user,
                                       IRegisterEmailRepository                 registerEmailRepository,
                                       ISystemLogRepository                     systemLogRepository,
                                       IUnitOfWork                              uow,
                                       IMediatorHandler                         bus,
                                       INotificationHandler<DomainNotification> notifications) :
        base(uow, bus, notifications)
    {
        _user                    = user;
        _registerEmailRepository = registerEmailRepository;
        _systemLogRepository     = systemLogRepository;
    }

#endregion

#region IRequestHandler<RegRegisterEmailCommand,bool> Members

    /// <summary>
    ///     Handle đăng ký email từ trang chủ
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> Handle(RegRegisterEmailCommand command, CancellationToken cancellationToken)
    {
        // Gán mặc định giá trị trả về khi xử lý = 0
        command.ResultCommand = false;

        if (!command.IsValid())
        {
            NotifyValidationErrors(command);
            return await Task.FromResult(false);
        }

        var iKey = Guid.NewGuid();
        var rAdd = new RegisterEmail(iKey,
                                     command.Email,
                                     _user.ProjectId,
                                     _user.UserId);
        //=================Ghi Log==================
        await _systemLogRepository.AddAsync(new SystemLog(Guid.NewGuid(),
                                                          DateTime.Now,
                                                          SystemLogEnumeration.Deleted.Id,
                                                          _user.StaffId,
                                                          _user.StaffName,
                                                          GetType().Name,
                                                          "",
                                                          iKey,
                                                          "",
                                                          JsonConvert.SerializeObject(command),
                                                          _user.UserId));
        //==========================================
        await _registerEmailRepository.AddAsync(rAdd);
        if (Commit())
        {
            command.ResultCommand = true;
            return await Task.FromResult(true);
        }

        NotifyValidationErrors(NErrorHelper.them_moi_khong_thanh_cong);
        return await Task.FromResult(false);
    }

#endregion

#region Fields

    private readonly IUser                    _user;
    private readonly IRegisterEmailRepository _registerEmailRepository;
    private readonly ISystemLogRepository     _systemLogRepository;

#endregion
}