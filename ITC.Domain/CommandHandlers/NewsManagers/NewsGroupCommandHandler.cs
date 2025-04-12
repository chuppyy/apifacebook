using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ITC.Domain.Commands.NewsManagers.NewsGroupManagers;
using ITC.Domain.Core.Bus;
using ITC.Domain.Core.Events;
using ITC.Domain.Core.Notifications;
using ITC.Domain.Interfaces;
using ITC.Domain.Interfaces.NewsManagers.NewsGroupManagers;
using ITC.Domain.Interfaces.SystemManagers.SystemLogs;
using ITC.Domain.Models.NewsManagers;
using ITC.Domain.StoredEvents;
using MediatR;
using NCore.Actions;
using NCore.Enums;
using NCore.Helpers;
using Newtonsoft.Json;

namespace ITC.Domain.CommandHandlers.NewsManagers;

/// <summary>
///     Command Handler danh sách nhóm tin
/// </summary>
public class NewsGroupCommandHandler : CommandHandler,
                                       IRequestHandler<AddNewsGroupCommand, bool>,
                                       IRequestHandler<UpdateNewsGroupCommand, bool>,
                                       IRequestHandler<DeleteNewsGroupCommand, bool>,
                                       IRequestHandler<UpdatePositionNewsGroupCommand, bool>
{
    #region Constructors

    /// <summary>
    ///     Hàm dựng với tham số
    /// </summary>
    /// <param name="user"></param>
    /// <param name="newsGroupRepository"></param>
    /// <param name="newsGroupQueries"></param>
    /// <param name="systemLogRepository"></param>
    /// <param name="uow"></param>
    /// <param name="bus"></param>
    /// <param name="notifications"></param>
    public NewsGroupCommandHandler(IUser                                    user,
                                   INewsGroupRepository                     newsGroupRepository,
                                   INewsGroupQueries                        newsGroupQueries,
                                   ISystemLogRepository                     systemLogRepository,
                                   IUnitOfWork                              uow,
                                   IMediatorHandler                         bus,
                                   INotificationHandler<DomainNotification> notifications) :
        base(uow, bus, notifications)
    {
        _user                = user;
        _repository          = newsGroupRepository;
        _queries             = newsGroupQueries;
        _systemLogRepository = systemLogRepository;
        _bus                 = bus;
    }

    #endregion

    #region IRequestHandler<AddNewsGroupCommand,bool> Members

    /// <summary>
    ///     Handle thêm mới
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> Handle(AddNewsGroupCommand command, CancellationToken cancellationToken)
    {
        command.ResultCommand = false;
        if (!command.IsValid())
        {
            NotifyValidationErrors(command);
            return await Task.FromResult(false);
        }

        var iCore = new NCoreHelper();
        command.ParentId = string.IsNullOrEmpty(command.ParentId) ? Guid.Empty.ToString() : command.ParentId;
        var iKey       = Guid.NewGuid();
        var iSecretKey = iCore.GeneralSecretKey(iKey);
        var iPosition  = _repository.GetMaxPosition(command.TypeId).Result;
        var iMetaTitle = iCore.create_META_TITLE(command.Name);
        
        var rAdd = new NewsGroup(iKey,
                                 command.Name,
                                 command.Description,
                                 command.ParentId,
                                 iPosition,
                                 iSecretKey,
                                 ActionStatusEnum.Active.Id,
                                 command.TypeId,
                                 _user.ProjectId,
                                 CheckDomain(command.Domain),
                                 iMetaTitle,
                                 command.IdViaQc,
                                 command.AgreeVia,
                                 command.LinkTree,
                                 command.StaffId,
                                 command.IsShowMain,
                                 command.DomainVercel,
                                 _user.UserId,
                                 command.Amount, command.AmountPosted);
        rAdd.TypeId = 2;
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
            var lModal = _repository.GetAllAsync().Result.Where(x => x.IsDeleted == false).ToList();
            _repository.DeQuyLeftRight(lModal, 1, Guid.Empty.ToString());
            _repository.SaveChanges();

            command.ResultCommand = true;
            return await Task.FromResult(true);
        }

        NotifyValidationErrors(NErrorHelper.them_moi_khong_thanh_cong);
        return await Task.FromResult(false);
    }

    #endregion

    #region IRequestHandler<DeleteNewsGroupCommand,bool> Members

    /// <summary>
    ///     Handle xóa nhóm tin
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> Handle(DeleteNewsGroupCommand command, CancellationToken cancellationToken)
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
            _repository.DeQuyLeftRight(lModal, 1, Guid.Empty.ToString());
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

    #region IRequestHandler<UpdateNewsGroupCommand,bool> Members

    /// <summary>
    ///     Handle cập nhật nhóm tin
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> Handle(UpdateNewsGroupCommand command, CancellationToken cancellationToken)
    {
        command.ResultCommand = false;
        if (!command.IsValid())
        {
            NotifyValidationErrors(command);
            return await Task.FromResult(false);
        }

        command.ParentId = string.IsNullOrEmpty(command.ParentId) ? Guid.Empty.ToString() : command.ParentId;
        var iMetaTitle      = new NCoreHelper().create_META_TITLE(command.Name);
        var domainVercelOld = "";
        var existing        = _repository.GetAsync(command.Id).Result;
        if (existing != null)
        {
            domainVercelOld = existing.DomainVercel;
            existing.Update(command.Name,
                            command.Description,
                            command.ParentId,
                            existing.Position,
                            existing.SecretKey,
                            existing.StatusId,
                            existing.NewsGroupTypeId,
                            CheckDomain(command.Domain),
                            iMetaTitle,
                            command.IdViaQc,
                            command.AgreeVia,
                            command.LinkTree, 
                            command.StaffId,
                            command.IsShowMain, 
                            command.DomainVercel,
                            _user.UserId,
                            command.Amount,
                            command.AmountPosted);
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
        }

        if (Commit())
        {
            if (string.Compare(domainVercelOld, command.DomainVercel, StringComparison.Ordinal) != 0)
            {
                // Xóa dữ liệu linktree các bài viết chưa đăng mà có cùng nhóm danh mục như bài viết này !
                var sBuilder = new StringBuilder();
                sBuilder.Append(
                    $@"UPDATE NewsContents SET LinkTree = '' WHERE NewsGroupId = '{command.Id}' AND StatusId != {ActionStatusEnum.Active.Id};");
                _ = _queries.SaveDomain(sBuilder).Result;
            }
            command.ResultCommand = true;
            return await Task.FromResult(true);
        }

        NotifyValidationErrors(NErrorHelper.cap_nhat_khong_thanh_cong);
        return await Task.FromResult(false);
    }

    #endregion

    #region IRequestHandler<UpdatePositionNewsGroupCommand,bool> Members

    /// <summary>
    ///     Handle cập nhật vị trí nhóm tin
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> Handle(UpdatePositionNewsGroupCommand command, CancellationToken cancellationToken)
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

    private string CheckDomain(string value)
    {
        return value;
        // var check = value.Substring(value.Length - 1, 1);
        // if (string.Compare(check, "/", StringComparison.Ordinal) == 0) return value;
        // return value + "/";
    }

    #region Fields

    private new readonly IMediatorHandler     _bus;
    private readonly     INewsGroupRepository _repository;
    private readonly     INewsGroupQueries    _queries;
    private readonly     ISystemLogRepository _systemLogRepository;
    private readonly     IUser                _user;

    #endregion
}