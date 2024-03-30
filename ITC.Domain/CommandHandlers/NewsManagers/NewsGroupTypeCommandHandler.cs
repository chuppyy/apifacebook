using System;
using System.Threading;
using System.Threading.Tasks;
using ITC.Domain.Commands.NewsManagers.NewsGroupTypeManagers;
using ITC.Domain.Core.Bus;
using ITC.Domain.Core.Events;
using ITC.Domain.Core.Notifications;
using ITC.Domain.Interfaces;
using ITC.Domain.Interfaces.NewsManagers.NewsGroupTypeManagers;
using ITC.Domain.Interfaces.SystemManagers.SystemLogs;
using ITC.Domain.Models.NewsManagers;
using MediatR;
using NCore.Actions;
using NCore.Enums;
using NCore.Helpers;
using Newtonsoft.Json;

namespace ITC.Domain.CommandHandlers.NewsManagers;

#region

#endregion

/// <summary>
///     Command Handler loại nhóm tin
/// </summary>
public class NewsGroupTypeCommandHandler : CommandHandler,
                                           IRequestHandler<AddNewsGroupTypeCommand, bool>,
                                           IRequestHandler<UpdateNewsGroupTypeCommand, bool>,
                                           IRequestHandler<DeleteNewsGroupTypeCommand, bool>
{
#region Constructors

    /// <summary>
    ///     Hàm dựng với tham số
    /// </summary>
    /// <param name="user"></param>
    /// <param name="newsGroupTypeRepository"></param>
    /// <param name="newsGroupTypeQueries"></param>
    /// <param name="systemLogRepository"></param>
    /// <param name="uow"></param>
    /// <param name="bus"></param>
    /// <param name="notifications"></param>
    public NewsGroupTypeCommandHandler(IUser                                    user,
                                       INewsGroupTypeRepository                 newsGroupTypeRepository,
                                       INewsGroupTypeQueries                    newsGroupTypeQueries,
                                       ISystemLogRepository                     systemLogRepository,
                                       IUnitOfWork                              uow,
                                       IMediatorHandler                         bus,
                                       INotificationHandler<DomainNotification> notifications) :
        base(uow, bus, notifications)
    {
        _user                = user;
        _repository          = newsGroupTypeRepository;
        _queries             = newsGroupTypeQueries;
        _systemLogRepository = systemLogRepository;
        _bus                 = bus;
    }

#endregion

#region IRequestHandler<AddNewsGroupTypeCommand,bool> Members

    /// <summary>
    ///     Handle thêm mới
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> Handle(AddNewsGroupTypeCommand command, CancellationToken cancellationToken)
    {
        // Gán mặc định giá trị trả về khi xử lý = 0
        command.ResultCommand = false;

        if (!command.IsValid())
        {
            NotifyValidationErrors(command);
            return await Task.FromResult(false);
        }

        var iPosition  = _repository.GetMaxPosition().Result;
        var iKey       = Guid.NewGuid();
        var iSecretKey = new NCoreHelper().GeneralSecretKey(iKey);
        var rAdd = new NewsGroupType(iKey,
                                     command.Name,
                                     command.Description,
                                     iPosition,
                                     ActionStatusEnum.Active.Id,
                                     iSecretKey,
                                     command.MetaTitle,
                                     _user.ProjectId,
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

#region IRequestHandler<DeleteNewsGroupTypeCommand,bool> Members

    /// <summary>
    ///     Handle xóa
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> Handle(DeleteNewsGroupTypeCommand command, CancellationToken cancellationToken)
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

#region IRequestHandler<UpdateNewsGroupTypeCommand,bool> Members

    /// <summary>
    ///     Handle cập nhật
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> Handle(UpdateNewsGroupTypeCommand command, CancellationToken cancellationToken)
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
                            command.Description,
                            existing.Position,
                            existing.StatusId,
                            existing.SecretKey,
                            existing.MetaTitle,
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

    private new readonly IMediatorHandler         _bus;
    private readonly     INewsGroupTypeRepository _repository;
    private readonly     INewsGroupTypeQueries    _queries;
    private readonly     ISystemLogRepository     _systemLogRepository;
    private readonly     IUser                    _user;

#endregion
}