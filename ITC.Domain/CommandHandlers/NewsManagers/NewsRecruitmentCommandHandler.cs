using System;
using System.Threading;
using System.Threading.Tasks;
using ITC.Domain.Commands.NewsManagers.NewsRecruitmentManagers;
using ITC.Domain.Core.Bus;
using ITC.Domain.Core.Events;
using ITC.Domain.Core.Notifications;
using ITC.Domain.Interfaces;
using ITC.Domain.Interfaces.NewsManagers.NewsAttackManagers;
using ITC.Domain.Interfaces.NewsManagers.NewsRecruitmentManagers;
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
///     Command Handler bài viết
/// </summary>
public class NewsRecruitmentCommandHandler : CommandHandler,
                                             IRequestHandler<AddNewsRecruitmentCommand, bool>,
                                             IRequestHandler<UpdateNewsRecruitmentCommand, bool>,
                                             IRequestHandler<DeleteNewsRecruitmentCommand, bool>
{
#region Constructors

    /// <summary>
    ///     Hàm dựng với tham số
    /// </summary>
    /// <param name="user"></param>
    /// <param name="newsRecruitmentRepository"></param>
    /// <param name="newsRecruitmentQueries"></param>
    /// <param name="newsAttackQueries"></param>
    /// <param name="systemLogRepository"></param>
    /// <param name="uow"></param>
    /// <param name="bus"></param>
    /// <param name="notifications"></param>
    public NewsRecruitmentCommandHandler(IUser                                    user,
                                         INewsRecruitmentRepository               newsRecruitmentRepository,
                                         INewsRecruitmentQueries                  newsRecruitmentQueries,
                                         INewsAttackQueries                       newsAttackQueries,
                                         ISystemLogRepository                     systemLogRepository,
                                         IUnitOfWork                              uow,
                                         IMediatorHandler                         bus,
                                         INotificationHandler<DomainNotification> notifications) :
        base(uow, bus, notifications)
    {
        _user                = user;
        _repository          = newsRecruitmentRepository;
        _queries             = newsRecruitmentQueries;
        _newsAttackQueries   = newsAttackQueries;
        _systemLogRepository = systemLogRepository;
    }

#endregion

#region IRequestHandler<AddNewsRecruitmentCommand,bool> Members

    /// <summary>
    ///     Handle thêm mới
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> Handle(AddNewsRecruitmentCommand command, CancellationToken cancellationToken)
    {
        // Gán mặc định giá trị trả về khi xử lý = 0
        command.ResultCommand = false;

        if (!command.IsValid())
        {
            NotifyValidationErrors(command);
            return await Task.FromResult(false);
        }

        var iCore          = new NCoreHelper();
        var iKey           = Guid.NewGuid();
        var iSecretKey     = iCore.GeneralSecretKey(iKey);
        var iPosition      = _repository.GetMaxPosition(command.Type).Result;
        var iDateTimeStart = iCore.ConvertStringToDateTimeFromVueJs(command.DateTimeStart);
        //======================================================================================================
        var rAdd = new NewsRecruitment(iKey,
                                       command.Name,
                                       command.Summary,
                                       command.Content,
                                       iPosition,
                                       iSecretKey,
                                       command.SeoKeyword,
                                       command.AvatarId,
                                       iDateTimeStart,
                                       command.StatusId,
                                       _user.ProjectId,
                                       command.Type,
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
                                                          JsonConvert.SerializeObject(command),
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

#region IRequestHandler<DeleteNewsRecruitmentCommand,bool> Members

    /// <summary>
    ///     Handle xóa
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> Handle(DeleteNewsRecruitmentCommand command, CancellationToken cancellationToken)
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
            //==========================================
            return await Task.FromResult(true);
        }

        NotifyValidationErrors(NErrorHelper.xoa_khong_thanh_cong);
        return await Task.FromResult(false);
    }

#endregion

#region IRequestHandler<UpdateNewsRecruitmentCommand,bool> Members

    /// <summary>
    ///     Handle cập nhật
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> Handle(UpdateNewsRecruitmentCommand command, CancellationToken cancellationToken)
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
                            command.Summary,
                            command.Content,
                            command.SeoKeyword,
                            command.AvatarId,
                            command.StatusId,
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
                // Xóa dữ liệu NewsAttack cũ
                await _newsAttackQueries.DeleteAsync(command.Id);
                // Trả dữ liệu về FE
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

    private readonly INewsRecruitmentRepository _repository;
    private readonly INewsRecruitmentQueries    _queries;
    private readonly INewsAttackQueries         _newsAttackQueries;
    private readonly ISystemLogRepository       _systemLogRepository;
    private readonly IUser                      _user;

#endregion
}