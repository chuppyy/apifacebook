using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ITC.Domain.Commands.AuthorityManager.MenuManagerSystem;
using ITC.Domain.Core.Bus;
using ITC.Domain.Core.Events;
using ITC.Domain.Core.Notifications;
using ITC.Domain.Extensions;
using ITC.Domain.Interfaces;
using ITC.Domain.Interfaces.AuthorityManager.AuthorityManagerSystem;
using ITC.Domain.Interfaces.AuthorityManager.ProjectMenuManagerSystem;
using ITC.Domain.Interfaces.SystemManagers.SystemLogs;
using ITC.Domain.Models.AuthorityManager;
using ITC.Domain.StoredEvents;
using MediatR;
using NCore.Enums;
using NCore.Helpers;
using Newtonsoft.Json;

namespace ITC.Domain.CommandHandlers.AuthorityManager;

#region

#endregion

/// <summary>
///     Command Handler danh sách chức năng
/// </summary>
public class MenuManagerCommandHandler : CommandHandler,
                                         IRequestHandler<AddMenuManagerCommand, bool>,
                                         IRequestHandler<UpdateMenuManagerCommand, bool>,
                                         IRequestHandler<DeleteMenuManagerCommand, bool>,
                                         IRequestHandler<AddAuthoritiesMenuManagerCommand, bool>,
                                         IRequestHandler<UpdateAuthoritiesMenuManagerCommand, bool>,
                                         IRequestHandler<DeleteAuthoritiesMenuManagerCommand, bool>,
                                         IRequestHandler<SortMenuManagerCommand, bool>,
                                         IRequestHandler<DeleteSortMenuManagerCommand, bool>
{
#region Constructors

    /// <summary>
    ///     Hàm dựng với tham số
    /// </summary>
    /// <param name="user"></param>
    /// <param name="menuManagerRepository"></param>
    /// <param name="menuManagerQueries"></param>
    /// <param name="authorityManagerRepository"></param>
    /// <param name="authorityDetailManagerRepository"></param>
    /// <param name="systemLogRepository"></param>
    /// <param name="uow"></param>
    /// <param name="bus"></param>
    /// <param name="notifications"></param>
    public MenuManagerCommandHandler(IUser                                    user,
                                     IMenuManagerRepository                   menuManagerRepository,
                                     IMenuManagerQueries                      menuManagerQueries,
                                     IAuthorityManagerRepository              authorityManagerRepository,
                                     IAuthorityDetailRepository               authorityDetailManagerRepository,
                                     ISystemLogRepository                     systemLogRepository,
                                     IUnitOfWork                              uow,
                                     IMediatorHandler                         bus,
                                     INotificationHandler<DomainNotification> notifications) :
        base(uow, bus, notifications)
    {
        _user                       = user;
        _repository                 = menuManagerRepository;
        _queries                    = menuManagerQueries;
        _authorityManagerRepository = authorityManagerRepository;
        _authorityDetailRepository  = authorityDetailManagerRepository;
        _systemLogRepository        = systemLogRepository;
        _bus                        = bus;
    }

#endregion

#region IRequestHandler<AddAuthoritiesMenuManagerCommand,bool> Members

    /// <summary>
    ///     Handle thêm mới
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> Handle(AddAuthoritiesMenuManagerCommand command, CancellationToken cancellationToken)
    {
        if (!command.IsValid())
        {
            NotifyValidationErrors(command);
            return await Task.FromResult(false);
        }

        var rAdd = new Authority(Guid.NewGuid(), Guid.Empty, _user.ProjectId, command.Name, _user.UserId);
        rAdd.AddDetail(command.Models, 1, _user.UserId);
        await _authorityManagerRepository.AddAsync(rAdd);
        if (Commit())
        {
            await _bus.RaiseEvent(new ActionStoreEvent.AddActionStoreEvent(StoredEventType.Add,
                                                                           _user.UserId,
                                                                           _user.FullName,
                                                                           rAdd.Id.ToString(),
                                                                           rAdd.Name,
                                                                           _user.UnitUserId,
                                                                           _user.PortalId));
            return await Task.FromResult(true);
        }

        return await Task.FromResult(false);
    }

#endregion

#region IRequestHandler<AddMenuManagerCommand,bool> Members

    /// <summary>
    ///     Handle thêm mới
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> Handle(AddMenuManagerCommand command, CancellationToken cancellationToken)
    {
        if (!command.IsValid())
        {
            NotifyValidationErrors(command);
            return await Task.FromResult(false);
        }

        //3.---Lấy dữ liệu left - right theo parentId-----------
        command.ParentId = string.IsNullOrEmpty(command.ParentId) ? "0" : command.ParentId;
        var rAdd = new MenuManager(Guid.NewGuid(), Guid.NewGuid(), command.ManagerICon, command.Name,
                                   command.MenuGroupId, command.Position, command.Router, command.ParentId,
                                   command.PermissionValue, command.Code, command.CreateBy);
        await _repository.AddAsync(rAdd);
        if (Commit())
        {
            var lModal = _repository.GetAllAsync().Result.Where(x => x.IsDeleted == false).ToList();
            _repository.DeQuyLeftRight(lModal, 1, Guid.Empty.ToString());
            _repository.SaveChanges();
            // await _bus.RaiseEvent(new ActionStoreEvent.AddActionStoreEvent(StoredEventType.Add,
            //                                                                _user.UserId,
            //                                                                _user.FullName,
            //                                                                rAdd.Id.ToString(),
            //                                                                rAdd.Name,
            //                                                                _user.UnitUserId,
            //                                                                _user.PortalId));
            return await Task.FromResult(true);
        }

        NotifyValidationErrors(NErrorHelper.them_moi_khong_thanh_cong);
        return await Task.FromResult(false);
    }

#endregion

#region IRequestHandler<DeleteAuthoritiesMenuManagerCommand,bool> Members

    /// <summary>
    ///     Handle xóa danh mục
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> Handle(DeleteAuthoritiesMenuManagerCommand command, CancellationToken cancellationToken)
    {
        if (!command.IsValid())
        {
            NotifyValidationErrors(command);
            return await Task.FromResult(false);
        }

        var iResult = _queries.DeleteAuthoritiesAsync(command.ListModel)
                              .Result;
        if (iResult > 0)
        {
            await _bus.RaiseEvent(new ActionStoreEvent.DeleteActionStoreEvent(StoredEventType.Add,
                                                                              _user.UserId,
                                                                              _user.FullName,
                                                                              command.ProjectId.ToString(),
                                                                              command.ProjectId.ToString(),
                                                                              _user.UnitUserId, _user.PortalId));
            return await Task.FromResult(true);
        }

        return await Task.FromResult(false);
    }

#endregion

#region IRequestHandler<DeleteMenuManagerCommand,bool> Members

    /// <summary>
    ///     Handle xóa danh mục
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> Handle(DeleteMenuManagerCommand command, CancellationToken cancellationToken)
    {
        if (!command.IsValid())
        {
            NotifyValidationErrors(command);
            return await Task.FromResult(false);
        }

        var existing = _repository.GetAsync(command.Ids).Result;
        if (existing != null)
        {
            //---Xóa danh mục-----------
            existing.Delete(_user.UserId);
            // await _bus.RaiseEvent(new ActionStoreEvent.DeleteActionStoreEvent(StoredEventType.Remove,
            //                           _user.UserId, _user.FullName, existing.Id.ToString(),
            //                           existing.Name, _user.UnitUserId, _user.PortalId));
            _repository.Update(existing);
            if (Commit())
            {
                //---Xóa thành công => cập nhật lại vị trí left - right
                var iTopicNew = existing.ParentId; //Gán mặc định là bằng parrent id cần xóa
                var genSql = new ActionHelper().GeneralDeleteWithLeftRightSqlWithProject("MenuManager",
                    existing.Id.ToString(),
                    existing.MLeft, existing.MRight, iTopicNew,
                    Guid.Empty, false, 0);
                await _queries.DeleteAsync(genSql);

                var lModal = _repository.GetAllAsync().Result.Where(x => x.IsDeleted == false).ToList();
                _repository.DeQuyLeftRight(lModal, 1, Guid.Empty.ToString());
                _repository.SaveChanges();

                return await Task.FromResult(true);
            }
        }

        NotifyValidationErrors(NErrorHelper.cap_nhat_khong_thanh_cong);
        return await Task.FromResult(false);
    }

#endregion

#region IRequestHandler<DeleteSortMenuManagerCommand,bool> Members

    /// <summary>
    ///     Handle xóa DeleteSortMenuManagerCommand
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> Handle(DeleteSortMenuManagerCommand command, CancellationToken cancellationToken)
    {
        // Gán mặc định giá trị trả về khi xử lý = 0
        command.ResultCommand = false;

        if (!command.IsValid())
        {
            NotifyValidationErrors(command);
            return await Task.FromResult(false);
        }

        var iResult = _queries.DeleteAuthoritiesAsync(command.Model).Result;
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

#region IRequestHandler<SortMenuManagerCommand,bool> Members

    /// <summary>
    ///     Handle sắp xếp menu
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> Handle(SortMenuManagerCommand command, CancellationToken cancellationToken)
    {
        command.ResultCommand = false;
        if (!command.IsValid())
        {
            NotifyValidationErrors(command);
            return await Task.FromResult(false);
        }

        foreach (var items in command.Models)
        {
            var iValue = _authorityDetailRepository.GetAsync(items.Id).Result;
            if (iValue != null)
            {
                iValue.Name     = items.Name;
                iValue.Position = items.Position;
                _authorityDetailRepository.Update(iValue);
            }
        }

        if (Commit())
        {
            command.ResultCommand = true;
            return await Task.FromResult(true);
        }

        NotifyValidationErrors("Sắp xếp menu không thành công");
        return await Task.FromResult(false);
    }

#endregion

#region IRequestHandler<UpdateAuthoritiesMenuManagerCommand,bool> Members

    /// <summary>
    ///     Handle Cập nhật
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> Handle(UpdateAuthoritiesMenuManagerCommand command, CancellationToken cancellationToken)
    {
        if (!command.IsValid())
        {
            NotifyValidationErrors(command);
            return await Task.FromResult(false);
        }

        var iUpdate = _authorityManagerRepository.LoadAsync(command.Id).Result;
        // iUpdate.Update(command.CompanyId, command.Name);
        _authorityManagerRepository.Update(iUpdate);
        if (Commit())
        {
            //Xử lý dữ liệu chi tiết
            // var iRunDelete = _queries
            //                  .DeleteDetailAsync(iUpdate.AuthorityDetails.Select(x => x.Id).ToList(), command.Id)
            //                  .Result;
            // foreach (var zModel in command.Models)
            // {
            //     var rAdd2 = new AuthorityDetail(Guid.NewGuid(), command.Id, zModel.Id, zModel.Value, _user.UserId);
            //     await _authorityDetailManagerRepository.AddAsync(rAdd2);
            // }

            Commit();
            await _bus.RaiseEvent(new ActionStoreEvent.AddActionStoreEvent(StoredEventType.Update,
                                                                           _user.UserId,
                                                                           _user.FullName,
                                                                           iUpdate.Id.ToString(),
                                                                           iUpdate.Name,
                                                                           _user.UnitUserId,
                                                                           _user.PortalId));
            return await Task.FromResult(true);
        }

        return await Task.FromResult(false);
    }

#endregion

#region IRequestHandler<UpdateMenuManagerCommand,bool> Members

    /// <summary>
    ///     Handle cập nhật danh mục
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> Handle(UpdateMenuManagerCommand command, CancellationToken cancellationToken)
    {
        if (!command.IsValid())
        {
            NotifyValidationErrors(command);
            return await Task.FromResult(false);
        }

        var existing = _repository.GetAsync(command.Id).Result;
        if (existing != null)
        {
            // var sLeft  = existing.MLeft;
            // var sRight = existing.MRight;

            /*command.ParentId = string.IsNullOrEmpty(command.ParentId) ? "0" : command.ParentId;
            if (existing.ParentId.ToLower() != command.ParentId)
            {
                //Khác nhau => xử lý lại cái left - right
                var sRun =
                    _queries.UpdateAsync(existing.Id.ToString(), existing.MLeft, existing.MRight, command.ParentId,
                                         command.ProjectId).Result;
                if (!sRun.Any()) return await Task.FromResult(false);

                //Lấy lại cái left - right
                sLeft  = sRun.FirstOrDefault().MLeft;
                sRight = sRun.FirstOrDefault().MRight;
            }*/

            existing.Update(command.ManagerICon,
                            command.Name,
                            command.MenuGroupId,
                            command.Router,
                            command.ParentId,
                            command.PermissionValue,
                            command.Code,
                            command.CreateBy);
            _repository.Update(existing);
        }

        if (Commit())
        {
            var lModal = _repository.GetAllAsync().Result.Where(x => x.IsDeleted == false).ToList();
            _repository.DeQuyLeftRight(lModal, 1, Guid.Empty.ToString());
            _repository.SaveChanges();
            // if (existing is { })
            //     await _bus.RaiseEvent(new ActionStoreEvent.UpdateActionStoreEvent(StoredEventType.Update,
            //                               _user.UserId,
            //                               _user.FullName,
            //                               existing.Id.ToString(),
            //                               existing.Name,
            //                               _user.UnitUserId,
            //                               _user.PortalId));
            return await Task.FromResult(true);
        }

        return await Task.FromResult(false);
    }

#endregion

#region Fields

    private new readonly IMediatorHandler            _bus;
    private readonly     IMenuManagerRepository      _repository;
    private readonly     IMenuManagerQueries         _queries;
    private readonly     IAuthorityManagerRepository _authorityManagerRepository;
    private readonly     IAuthorityDetailRepository  _authorityDetailRepository;
    private readonly     ISystemLogRepository        _systemLogRepository;
    private readonly     IUser                       _user;

#endregion
}