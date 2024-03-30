using System;
using System.Threading;
using System.Threading.Tasks;
using ITC.Domain.Commands.NewsManagers.NewsGithubManagers;
using ITC.Domain.Core.Bus;
using ITC.Domain.Core.Events;
using ITC.Domain.Core.Notifications;
using ITC.Domain.Interfaces;
using ITC.Domain.Interfaces.NewsManagers.NewsGithubManagers;
using ITC.Domain.Interfaces.SystemManagers.SystemLogs;
using ITC.Domain.Models.NewsManagers;
using MediatR;
using NCore.Enums;
using NCore.Helpers;
using Newtonsoft.Json;

namespace ITC.Domain.CommandHandlers.NewsManagers;

#region

#endregion

/// <summary>
///     Command Handler loại nhóm tin
/// </summary>
public class NewsGithubCommandHandler : CommandHandler,
                                        IRequestHandler<AddNewsGithubCommand, bool>,
                                        IRequestHandler<UpdateNewsGithubCommand, bool>,
                                        IRequestHandler<DeleteNewsGithubCommand, bool>
{
    #region Constructors

    /// <summary>
    ///     Hàm dựng với tham số
    /// </summary>
    /// <param name="user"></param>
    /// <param name="newsGithubRepository"></param>
    /// <param name="newsGithubQueries"></param>
    /// <param name="systemLogRepository"></param>
    /// <param name="uow"></param>
    /// <param name="bus"></param>
    /// <param name="notifications"></param>
    public NewsGithubCommandHandler(IUser                                    user,
                                    INewsGithubRepository                    newsGithubRepository,
                                    INewsGithubQueries                       newsGithubQueries,
                                    ISystemLogRepository                     systemLogRepository,
                                    IUnitOfWork                              uow,
                                    IMediatorHandler                         bus,
                                    INotificationHandler<DomainNotification> notifications) :
        base(uow, bus, notifications)
    {
        _user                = user;
        _repository          = newsGithubRepository;
        _queries             = newsGithubQueries;
        _systemLogRepository = systemLogRepository;
    }

    #endregion

    #region IRequestHandler<AddNewsGithubCommand,bool> Members

    /// <summary>
    ///     Handle thêm mới
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> Handle(AddNewsGithubCommand command, CancellationToken cancellationToken)
    {
        // Gán mặc định giá trị trả về khi xử lý = 0
        command.ResultCommand = false;

        if (!command.IsValid())
        {
            NotifyValidationErrors(command);
            return await Task.FromResult(false);
        }

        var iPosition = _repository.GetMaxPosition().Result;
        var iKey      = Guid.NewGuid();
        var rAdd = new NewsGithub(iKey,
                                  command.Code,
                                  command.Name,
                                  command.Description,
                                  iPosition,
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

    #region IRequestHandler<DeleteNewsGithubCommand,bool> Members

    /// <summary>
    ///     Handle xóa
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> Handle(DeleteNewsGithubCommand command, CancellationToken cancellationToken)
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

    #region IRequestHandler<UpdateNewsGithubCommand,bool> Members

    /// <summary>
    ///     Handle cập nhật
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> Handle(UpdateNewsGithubCommand command, CancellationToken cancellationToken)
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
            existing.Update(command.Code,
                            command.Name,
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

    private readonly INewsGithubRepository _repository;
    private readonly INewsGithubQueries    _queries;
    private readonly ISystemLogRepository  _systemLogRepository;
    private readonly IUser                 _user;

    #endregion
}