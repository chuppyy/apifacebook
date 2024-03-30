using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ITC.Domain.Commands.HomeManagers.HomeMenuManagers;
using ITC.Domain.Core.Bus;
using ITC.Domain.Core.Events;
using ITC.Domain.Core.Notifications;
using ITC.Domain.Interfaces;
using ITC.Domain.Interfaces.HomeManagers.HomeMenuManagers;
using ITC.Domain.Interfaces.HomeManagers.HomeMenuNewsGroupManagers;
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
///     Command Handler danh sách menu trang chủ
/// </summary>
public class HomeMenuCommandHandler : CommandHandler,
                                      IRequestHandler<AddHomeMenuCommand, bool>,
                                      IRequestHandler<UpdateHomeMenuCommand, bool>,
                                      IRequestHandler<DeleteHomeMenuCommand, bool>,
                                      IRequestHandler<UpdatePositionHomeMenuCommand, bool>
{
#region Constructors

    /// <summary>
    ///     Hàm dựng với tham số
    /// </summary>
    /// <param name="user"></param>
    /// <param name="homeMenuRepository"></param>
    /// <param name="homeMenuQueries"></param>
    /// <param name="homeMenuNewsGroupRepository"></param>
    /// <param name="menuNewsGroupQueries"></param>
    /// <param name="systemLogRepository"></param>
    /// <param name="uow"></param>
    /// <param name="bus"></param>
    /// <param name="notifications"></param>
    public HomeMenuCommandHandler(IUser                                    user,
                                  IHomeMenuRepository                      homeMenuRepository,
                                  IHomeMenuQueries                         homeMenuQueries,
                                  IHomeMenuNewsGroupRepository             homeMenuNewsGroupRepository,
                                  IHomeMenuNewsGroupQueries                menuNewsGroupQueries,
                                  ISystemLogRepository                     systemLogRepository,
                                  IUnitOfWork                              uow,
                                  IMediatorHandler                         bus,
                                  INotificationHandler<DomainNotification> notifications) :
        base(uow, bus, notifications)
    {
        _user                        = user;
        _repository                  = homeMenuRepository;
        _queries                     = homeMenuQueries;
        _homeMenuNewsGroupRepository = homeMenuNewsGroupRepository;
        _menuNewsGroupQueries        = menuNewsGroupQueries;
        _systemLogRepository         = systemLogRepository;
        _bus                         = bus;
    }

#endregion

#region IRequestHandler<AddHomeMenuCommand,bool> Members

    /// <summary>
    ///     Handle thêm mới
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> Handle(AddHomeMenuCommand command, CancellationToken cancellationToken)
    {
        command.ResultCommand = false;
        if (!command.IsValid())
        {
            NotifyValidationErrors(command);
            return await Task.FromResult(false);
        }

        var iUserCreated = _user.UserId;
        var iProjectId   = _user.ProjectId;
        command.ParentId = string.IsNullOrEmpty(command.ParentId) ? Guid.Empty.ToString() : command.ParentId;
        var iKey       = Guid.NewGuid();
        var iSecretKey = new NCoreHelper().GeneralSecretKey(iKey);
        var iPosition  = _repository.GetMaxPosition().Result;
        var rAdd = new HomeMenu(iKey,
                                command.Name,
                                command.Description,
                                command.ParentId,
                                command.Url,
                                iPosition,
                                ActionStatusEnum.Active.Id,
                                iSecretKey,
                                command.IsViewHomePage,
                                iProjectId,
                                iUserCreated);
        rAdd.AddNewsGroup(command.HomeMenuNewsGroupModels, 1, iUserCreated);
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
                                                          iUserCreated));
        //==========================================
        await _repository.AddAsync(rAdd);
        if (Commit())
        {
            var lModal = _repository.GetAllAsync().Result.Where(x => x.IsDeleted == false).ToList();
            _repository.DeQuyLeftRight(lModal, 1, Guid.Empty.ToString(), iProjectId);
            _repository.SaveChanges();

            command.ResultCommand = true;
            return await Task.FromResult(true);
        }

        NotifyValidationErrors(NErrorHelper.them_moi_khong_thanh_cong);
        return await Task.FromResult(false);
    }

#endregion

#region IRequestHandler<DeleteHomeMenuCommand,bool> Members

    /// <summary>
    ///     Handle xóa menu trang chủ
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> Handle(DeleteHomeMenuCommand command, CancellationToken cancellationToken)
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
            var lModal = _repository.GetAllAsync().Result.Where(x => x.IsDeleted == false).ToList();
            _repository.DeQuyLeftRight(lModal, 1, Guid.Empty.ToString(), _user.ProjectId);
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

#region IRequestHandler<UpdateHomeMenuCommand,bool> Members

    /// <summary>
    ///     Handle cập nhật menu trang chủ
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> Handle(UpdateHomeMenuCommand command, CancellationToken cancellationToken)
    {
        var iUserCreate = _user.UserId;
        command.ResultCommand = false;
        if (!command.IsValid())
        {
            NotifyValidationErrors(command);
            return await Task.FromResult(false);
        }

        command.ParentId = string.IsNullOrEmpty(command.ParentId) ? Guid.Empty.ToString() : command.ParentId;

        var iHistoryPosition = 0;
        var existing         = _repository.GetAsync(command.Id).Result;
        if (existing != null)
        {
            // Lấy giá trị lịch sử vị trí
            iHistoryPosition = _homeMenuNewsGroupRepository.GetMaxHistoryPosition(command.Id).Result;
            existing.Update(command.Name,
                            command.Description,
                            command.ParentId,
                            command.Url,
                            existing.Position,
                            existing.StatusId,
                            existing.SecretKey,
                            existing.IsViewHomePage,
                            iUserCreate);
            foreach (var rAdd in command.HomeMenuNewsGroupModels
                                        .Select(items =>
                                                    new HomeMenuNewsGroup(Guid.NewGuid(),
                                                                          command.Id,
                                                                          items.Id,
                                                                          true,
                                                                          iHistoryPosition + 1,
                                                                          iUserCreate)))
                await _homeMenuNewsGroupRepository.AddAsync(rAdd);

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
            await _menuNewsGroupQueries.DeleteHistory(command.Id, iHistoryPosition);

            command.ResultCommand = true;
            return await Task.FromResult(true);
        }

        NotifyValidationErrors(NErrorHelper.cap_nhat_khong_thanh_cong);
        return await Task.FromResult(false);
    }

#endregion

#region IRequestHandler<UpdatePositionHomeMenuCommand,bool> Members

    /// <summary>
    ///     Handle cập nhật vị trí menu trang chủ
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> Handle(UpdatePositionHomeMenuCommand command, CancellationToken cancellationToken)
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
            // Xóa dữ liệu HomeMenuNewsGroup
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

    private new readonly IMediatorHandler             _bus;
    private readonly     IHomeMenuRepository          _repository;
    private readonly     IHomeMenuQueries             _queries;
    private readonly     IHomeMenuNewsGroupRepository _homeMenuNewsGroupRepository;
    private readonly     IHomeMenuNewsGroupQueries    _menuNewsGroupQueries;
    private readonly     ISystemLogRepository         _systemLogRepository;
    private readonly     IUser                        _user;

#endregion
}