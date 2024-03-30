using System;
using System.Threading;
using System.Threading.Tasks;
using ITC.Domain.Commands.NewsManagers.NewsDomainManagers;
using ITC.Domain.Commands.NewsManagers.NewsGroupTypeManagers;
using ITC.Domain.Core.Bus;
using ITC.Domain.Core.Events;
using ITC.Domain.Core.Notifications;
using ITC.Domain.Interfaces;
using ITC.Domain.Interfaces.NewsManagers.NewsDomainManagers;
using ITC.Domain.Interfaces.SystemManagers.SystemLogs;
using ITC.Domain.Models.NewsManagers;
using MediatR;
using Microsoft.Extensions.Logging;
using NCore.Enums;
using NCore.Helpers;
using Newtonsoft.Json;

namespace ITC.Domain.CommandHandlers.NewsManagers;

/// <summary>
///     Command Handler loại nhóm tin
/// </summary>
public class NewsDomainCommandHandler : CommandHandler,
                                        IRequestHandler<AddNewsDomainCommand, bool>,
                                        IRequestHandler<UpdateNewsDomainCommand, bool>,
                                        IRequestHandler<DeleteNewsDomainCommand, bool>,
                                        IRequestHandler<SchedulerNewsDomainCommand, bool>
{
#region Constructors

    /// <summary>
    ///     Hàm dựng với tham số
    /// </summary>
    /// <param name="user"></param>
    /// <param name="newsDomainRepository"></param>
    /// <param name="newsDomainQueries"></param>
    /// <param name="systemLogRepository"></param>
    /// <param name="uow"></param>
    /// <param name="bus"></param>
    /// <param name="logger"></param>
    /// <param name="notifications"></param>
    public NewsDomainCommandHandler(IUser                                    user,
                                    INewsDomainRepository                    newsDomainRepository,
                                    INewsDomainQueries                       newsDomainQueries,
                                    ISystemLogRepository                     systemLogRepository,
                                    IUnitOfWork                              uow,
                                    IMediatorHandler                         bus,
                                    ILogger<NewsDomainCommandHandler>        logger,
                                    INotificationHandler<DomainNotification> notifications) :
        base(uow, bus, notifications)
    {
        _user                = user;
        _repository          = newsDomainRepository;
        _queries             = newsDomainQueries;
        _systemLogRepository = systemLogRepository;
        _logger              = logger;
    }

#endregion

#region IRequestHandler<AddNewsDomainCommand,bool> Members

    /// <summary>
    ///     Handle thêm mới
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> Handle(AddNewsDomainCommand command, CancellationToken cancellationToken)
    {
        // Gán mặc định giá trị trả về khi xử lý = 0
        command.ResultCommand = false;

        if (!command.IsValid())
        {
            NotifyValidationErrors(command);
            return await Task.FromResult(false);
        }

        var iKey = Guid.NewGuid();
        var rAdd = new NewsDomain(iKey,
                                  command.Name,
                                  command.DomainNew,
                                  command.Description,
                                  _user.UserId);
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
                                                          _user.UserId));
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

#region IRequestHandler<DeleteNewsDomainCommand,bool> Members

    /// <summary>
    ///     Handle xóa
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> Handle(DeleteNewsDomainCommand command, CancellationToken cancellationToken)
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

#region IRequestHandler<SchedulerNewsDomainCommand,bool> Members

    /// <summary>
    ///     Handle lập lịch
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> Handle(SchedulerNewsDomainCommand command, CancellationToken cancellationToken)
    {
        command.ResultCommand = false;
        foreach (var items in command.DomainVercels)
        {
            _logger.LogInformation("Add vercel: " + items.Name);
            await _repository.AddAsync(new NewsDomain(Guid.NewGuid(),
                                                      items.Name,
                                                      items.IdDomain.ToString(),
                                                      ""));
        }

        _logger.LogInformation("Lưu");
        if (Commit())
        {
            _logger.LogInformation("thành công");
            command.ResultCommand = true;
            return await Task.FromResult(true);
        }

        _logger.LogInformation("thất bại");
        NotifyValidationErrors(NErrorHelper.them_moi_khong_thanh_cong);
        return await Task.FromResult(false);
    }

#endregion

#region IRequestHandler<UpdateNewsDomainCommand,bool> Members

    /// <summary>
    ///     Handle cập nhật
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> Handle(UpdateNewsDomainCommand command, CancellationToken cancellationToken)
    {
        // Gán mặc định giá trị trả về khi xử lý = 0
        command.ResultCommand = false;

        if (!command.IsValid())
        {
            NotifyValidationErrors(command);
            return await Task.FromResult(false);
        }

        var existing = _repository.GetAsync(command.Id).Result;
        if (existing != null)
        {
            existing.Update(command.Name,
                            command.DomainNew,
                            command.Description,
                            _user.UserId);
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

    private readonly INewsDomainRepository             _repository;
    private readonly INewsDomainQueries                _queries;
    private readonly ISystemLogRepository              _systemLogRepository;
    private readonly ILogger<NewsDomainCommandHandler> _logger;
    private readonly IUser                             _user;

#endregion
}