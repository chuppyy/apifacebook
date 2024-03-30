using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ITC.Domain.Commands.HomeManagers.HomeNewsGroupViewManagers;
using ITC.Domain.Core.Bus;
using ITC.Domain.Core.Events;
using ITC.Domain.Core.Notifications;
using ITC.Domain.Interfaces;
using ITC.Domain.Interfaces.HomeManagers.HomeNewsGroupDetailManagers;
using ITC.Domain.Interfaces.HomeManagers.HomeNewsGroupViewManagers;
using ITC.Domain.Interfaces.SystemManagers.SystemLogs;
using ITC.Domain.Models.HomeManagers;
using ITC.Domain.StoredEvents;
using MediatR;
using NCore.Actions;
using NCore.Enums;
using NCore.Helpers;
using Newtonsoft.Json;

namespace ITC.Domain.CommandHandlers.HomeManagers;

#region

#endregion

/// <summary>
///     Command Handler danh sách các bài viết hiển thị trên trang chủ
/// </summary>
public class HomeNewsGroupViewCommandHandler : CommandHandler,
                                               IRequestHandler<AddHomeNewsGroupViewCommand, bool>,
                                               IRequestHandler<UpdateHomeNewsGroupViewCommand, bool>,
                                               IRequestHandler<DeleteHomeNewsGroupViewCommand, bool>,
                                               IRequestHandler<UpdatePositionHomeNewsGroupViewCommand, bool>
{
#region Constructors

    /// <summary>
    ///     Hàm dựng với tham số
    /// </summary>
    /// <param name="user"></param>
    /// <param name="homeNewsGroupViewRepository"></param>
    /// <param name="homeNewsGroupViewQueries"></param>
    /// <param name="homeNewsGroupViewDetailRepository"></param>
    /// <param name="homeNewsGroupViewDetailQueries"></param>
    /// <param name="systemLogRepository"></param>
    /// <param name="uow"></param>
    /// <param name="bus"></param>
    /// <param name="notifications"></param>
    public HomeNewsGroupViewCommandHandler(
        IUser                                    user,
        IHomeNewsGroupViewRepository             homeNewsGroupViewRepository,
        IHomeNewsGroupViewQueries                homeNewsGroupViewQueries,
        IHomeNewsGroupViewDetailRepository       homeNewsGroupViewDetailRepository,
        IHomeNewsGroupViewDetailQueries          homeNewsGroupViewDetailQueries,
        ISystemLogRepository                     systemLogRepository,
        IUnitOfWork                              uow,
        IMediatorHandler                         bus,
        INotificationHandler<DomainNotification> notifications) :
        base(uow, bus, notifications)
    {
        _user                              = user;
        _repository                        = homeNewsGroupViewRepository;
        _queries                           = homeNewsGroupViewQueries;
        _homeNewsGroupViewDetailRepository = homeNewsGroupViewDetailRepository;
        _homeNewsGroupViewDetailQueries    = homeNewsGroupViewDetailQueries;
        _systemLogRepository               = systemLogRepository;
        _bus                               = bus;
    }

#endregion

#region IRequestHandler<AddHomeNewsGroupViewCommand,bool> Members

    /// <summary>
    ///     Handle thêm mới
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> Handle(AddHomeNewsGroupViewCommand command, CancellationToken cancellationToken)
    {
        command.ResultCommand = false;
        if (!command.IsValid())
        {
            NotifyValidationErrors(command);
            return await Task.FromResult(false);
        }

        var iKey       = Guid.NewGuid();
        var iSecretKey = new NCoreHelper().GeneralSecretKey(iKey);
        var iPosition  = _repository.GetMaxPosition().Result;
        var rAdd = new HomeNewsGroupView(iKey,
                                         command.Name,
                                         command.Description,
                                         Guid.Empty.ToString(),
                                         command.Url,
                                         iPosition,
                                         ActionStatusEnum.Active.Id,
                                         iSecretKey,
                                         _user.UserId);
        rAdd.AddNewsGroup(command.HomeNewsGroupViewModels, 1, _user.UserId);
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
        await _repository.AddAsync(rAdd);
        if (Commit())
        {
            command.ResultCommand = true;
            return await Task.FromResult(true);
        }

        NotifyValidationErrors(NErrorHelper.them_moi_khong_thanh_cong);
        return await Task.FromResult(false);
    }

#endregion

#region IRequestHandler<DeleteHomeNewsGroupViewCommand,bool> Members

    /// <summary>
    ///     Handle xóa menu trang chủ
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> Handle(DeleteHomeNewsGroupViewCommand command, CancellationToken cancellationToken)
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

#region IRequestHandler<UpdateHomeNewsGroupViewCommand,bool> Members

    /// <summary>
    ///     Handle cập nhật menu trang chủ
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> Handle(UpdateHomeNewsGroupViewCommand command, CancellationToken cancellationToken)
    {
        var iUserCreate = _user.UserId;
        command.ResultCommand = false;
        if (!command.IsValid())
        {
            NotifyValidationErrors(command);
            return await Task.FromResult(false);
        }

        var iHistoryPosition = 0;
        var existing         = _repository.GetAsync(command.Id).Result;
        if (existing != null)
        {
            // Lấy giá trị lịch sử vị trí
            iHistoryPosition = _homeNewsGroupViewDetailRepository.GetMaxHistoryPosition(command.Id).Result;
            existing.Update(command.Name,
                            command.Description,
                            command.ParentId,
                            command.Url,
                            existing.Position,
                            existing.StatusId,
                            existing.SecretKey,
                            iUserCreate);
            foreach (var rAdd in command.HomeNewsGroupViewModels
                                        .Select(items =>
                                                    new HomeNewsGroupViewDetail(Guid.NewGuid(),
                                                        command.Id,
                                                        items.Id,
                                                        iHistoryPosition + 1,
                                                        iUserCreate)))
                await _homeNewsGroupViewDetailRepository.AddAsync(rAdd);

            _repository.Update(existing);
        }

        //=================Ghi Log==================
        await _systemLogRepository.AddAsync(new SystemLog(Guid.NewGuid(),
                                                          DateTime.Now,
                                                          SystemLogEnumeration.Update.Id,
                                                          _user.StaffId,
                                                          _user.StaffName,
                                                          GetType().Name,
                                                          "",
                                                          existing?.Id,
                                                          JsonConvert.SerializeObject(command),
                                                          JsonConvert.SerializeObject(existing),
                                                          _user.UserId));
        //==========================================

        if (Commit())
        {
            // Xóa dữ liệu History cũ
            await _homeNewsGroupViewDetailQueries.DeleteHistory(command.Id, iHistoryPosition);

            command.ResultCommand = true;
            return await Task.FromResult(true);
        }

        NotifyValidationErrors(NErrorHelper.cap_nhat_khong_thanh_cong);
        return await Task.FromResult(false);
    }

#endregion

#region IRequestHandler<UpdatePositionHomeNewsGroupViewCommand,bool> Members

    /// <summary>
    ///     Handle cập nhật vị trí menu trang chủ
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> Handle(UpdatePositionHomeNewsGroupViewCommand command,
                                   CancellationToken                      cancellationToken)
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
            // Xóa dữ liệu HomeNewsGroupViewNewsGroup
            //await _menuNewsGroupQueries.DeleteAsync(command.Id);

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

    private new readonly IMediatorHandler                   _bus;
    private readonly     IHomeNewsGroupViewRepository       _repository;
    private readonly     IHomeNewsGroupViewQueries          _queries;
    private readonly     IHomeNewsGroupViewDetailRepository _homeNewsGroupViewDetailRepository;
    private readonly     IHomeNewsGroupViewDetailQueries    _homeNewsGroupViewDetailQueries;
    private readonly     ISystemLogRepository               _systemLogRepository;
    private readonly     IUser                              _user;

#endregion
}