using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ITC.Domain.Commands.AuthorityManager.AuthorityManagerSystem;
using ITC.Domain.Commands.AuthorityManager.MenuManagerSystem;
using ITC.Domain.Core.Bus;
using ITC.Domain.Core.Events;
using ITC.Domain.Core.ModelShare.AuthorityManager;
using ITC.Domain.Core.Notifications;
using ITC.Domain.Interfaces;
using ITC.Domain.Interfaces.AuthorityManager.AuthorityManagerSystem;
using ITC.Domain.Interfaces.SystemManagers.SystemLogs;
using ITC.Domain.Models.AuthorityManager;
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
public class AuthorityManagerSystemCommandHandler :
    CommandHandler,
    IRequestHandler<AddAuthorityManagerSystemCommand, bool>,
    IRequestHandler<UpdateAuthorityManagerSystemCommand, bool>,
    IRequestHandler<DeleteAuthorityManagerSystemCommand, bool>,
    IRequestHandler<UpdatePermissionByAuthoritiesCommand, bool>,
    IRequestHandler<UpdateAuthorityManagerSystemPermissionMenuCommand, bool>


{
#region Constructors

    /// <summary>
    ///     Hàm dựng với tham số
    /// </summary>
    /// <param name="user"></param>
    /// <param name="authorityManagerRepository"></param>
    /// <param name="authorityManagerQueries"></param>
    /// <param name="authorityDetailManagerRepository"></param>
    /// <param name="systemLogRepository"></param>
    /// <param name="uow"></param>
    /// <param name="bus"></param>
    /// <param name="notifications"></param>
    public AuthorityManagerSystemCommandHandler(IUser user,
                                                IAuthorityManagerRepository authorityManagerRepository,
                                                IAuthorityManagerQueries authorityManagerQueries,
                                                IAuthorityDetailRepository authorityDetailManagerRepository,
                                                ISystemLogRepository systemLogRepository,
                                                IUnitOfWork uow,
                                                IMediatorHandler bus,
                                                INotificationHandler<DomainNotification> notifications) :
        base(uow, bus, notifications)
    {
        _user                             = user;
        _authorityManagerRepository       = authorityManagerRepository;
        _authorityManagerQueries          = authorityManagerQueries;
        _authorityDetailManagerRepository = authorityDetailManagerRepository;
        _systemLogRepository              = systemLogRepository;
    }

#endregion

#region IRequestHandler<AddAuthorityManagerSystemCommand,bool> Members

    /// <summary>
    ///     Handle thêm mới
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> Handle(AddAuthorityManagerSystemCommand command, CancellationToken cancellationToken)
    {
        //Gán mặc định kết quả trả về là false
        command.ResultCommand = false;

        if (!command.IsValid())
        {
            NotifyValidationErrors(command);
            return await Task.FromResult(false);
        }

        // Xử lý dữ liệu giá trị quyền cho các menu chức năng cha
        var menuSend     = ReturnMenuByAuthoritiesSaveModels(command.MenuRoleEventViewModels);
        var iKey         = Guid.NewGuid();
        var iUserCreated = _user.UserId;
        var rAdd = new Authority(new Guid(),
                                 Guid.Empty,
                                 command.ProjectId,
                                 command.Name,
                                 command.Description,
                                 iUserCreated);
        rAdd.AddDetail(menuSend, 1, _user.UserId);
        await _authorityManagerRepository.AddAsync(rAdd);
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
                                                          iUserCreated));
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

#region IRequestHandler<DeleteAuthorityManagerSystemCommand,bool> Members

    /// <summary>
    ///     Handle xóa quyền sử dụng
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> Handle(DeleteAuthorityManagerSystemCommand command, CancellationToken cancellationToken)
    {
        //Gán mặc định kết quả trả về là false
        command.ResultCommand = false;

        if (!command.IsValid())
        {
            NotifyValidationErrors(command);
            return await Task.FromResult(false);
        }

        var iResult = _authorityManagerQueries.DeleteAsync(command.Model).Result;
        if (iResult > 0)
        {
            command.ResultCommand = true;
            // await
            //     _bus.RaiseEvent(new ActionStoreEvent.
            //                         DeleteActionStoreEvent(StoredEventType.Remove,
            //                                                _user.UserId,
            //                                                _user.FullName,
            //                                                _user.UserId,
            //                                                _user.FullName,
            //                                                _user.UnitUserId,
            //                                                _user.PortalId));
            return await Task.FromResult(true);
        }

        NotifyValidationErrors(NErrorHelper.xoa_khong_thanh_cong);
        return await Task.FromResult(false);
    }

#endregion

#region IRequestHandler<UpdateAuthorityManagerSystemCommand,bool> Members

    /// <summary>
    ///     Handle cập nhật quyền sử dụng
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> Handle(UpdateAuthorityManagerSystemCommand command, CancellationToken cancellationToken)
    {
        //Gán mặc định kết quả trả về là false
        command.ResultCommand = false;
        if (!command.IsValid())
        {
            NotifyValidationErrors(command);
            return await Task.FromResult(false);
        }

        var vInfo = _authorityManagerRepository.GetAsync(command.Id).Result;
        if (vInfo != null)
        {
            vInfo.Name        = command.Name;
            vInfo.Description = command.Description;
            _authorityManagerRepository.Update(vInfo);

            var menuSend  = ReturnMenuByAuthoritiesSaveModels(command.MenuRoleEventViewModels);
            var lMenuInDb = _authorityDetailManagerRepository.GetListAuthorityDetailChild(command.Id).Result;
            //=================Ghi Log==================
            // Thay ghi log trước vì Entity không chấp nhận tạo file JsonConvert sau khi thay đổi dữ liệu bảng
            var iUserCreated = _user.UserId;
            await _systemLogRepository.AddAsync(new SystemLog(Guid.NewGuid(),
                                                              DateTime.Now,
                                                              SystemLogEnumeration.Update.Id,
                                                              _user.StaffId,
                                                              _user.StaffName,
                                                              GetType().Name,
                                                              "",
                                                              vInfo.Id,
                                                              "",
                                                              JsonConvert.SerializeObject(command),
                                                              iUserCreated));
            // Kiểm tra xem có chức năng nào chưa được cập nhật hay không ?
            foreach (var items in menuSend)
            {
                var iCheck = lMenuInDb.FirstOrDefault(x => x.MenuManagerId == items.Id);
                if (iCheck != null)
                {
                    // Có dữ liệu => cập nhật
                    var iUpdate = _authorityDetailManagerRepository.GetAsync(iCheck.Id).Result;
                    iUpdate.Value = items.Value;
                    _authorityDetailManagerRepository.Update(iUpdate);
                }
                else
                {
                    // Không có dữ liệu => thêm mới
                    var addNew = new AuthorityDetail(Guid.NewGuid(),
                                                     command.Id,
                                                     items.Id,
                                                     items.Value,
                                                     1,
                                                     items.Name,
                                                     iUserCreated);
                    await _authorityDetailManagerRepository.AddAsync(addNew);
                }
            }

            //==========================================
            if (Commit())
            {
                command.ResultCommand = true;
                return await Task.FromResult(true);
            }
        }

        NotifyValidationErrors(NErrorHelper.du_lieu_khong_ton_tai + ": " + command.Id);
        return await Task.FromResult(false);
    }

#endregion

#region IRequestHandler<UpdateAuthorityManagerSystemPermissionMenuCommand,bool> Members

    /// <summary>
    ///     Handle cập nhật quyền sử dụng cho menu
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> Handle(UpdateAuthorityManagerSystemPermissionMenuCommand command,
                                   CancellationToken                                 cancellationToken)
    {
        //Gán mặc định kết quả trả về là false
        command.ResultCommand = false;
        if (!command.IsValid())
        {
            NotifyValidationErrors(command);
            return await Task.FromResult(false);
        }

        foreach (var items in command.Model)
        {
            var vInfo = _authorityDetailManagerRepository.GetAsync(items.Id).Result;
            if (vInfo != null)
            {
                if (command.IsUpdatePermission)
                {
                    vInfo.Value = items.Value;
                }
                else
                {
                    vInfo.Name     = items.Label;
                    vInfo.Position = items.Position;
                }

                _authorityDetailManagerRepository.Update(vInfo);
            }
        }

        if (Commit())
        {
            //=================Ghi Log==================
            await _systemLogRepository.AddAsync(new SystemLog(Guid.NewGuid(),
                                                              DateTime.Now,
                                                              SystemLogEnumeration.Update.Id,
                                                              _user.StaffId,
                                                              _user.StaffName,
                                                              GetType().Name,
                                                              "",
                                                              Guid.Empty,
                                                              null,
                                                              JsonConvert.SerializeObject(command),
                                                              _user.UserId));
            //==========================================
            command.ResultCommand = true;
            return await Task.FromResult(true);
        }
        // var vInfo = _authorityDetailManagerRepository.GetAsync(command.Id).Result;
        // if (vInfo != null)
        // {
        //     vInfo.Value        = command.Value;
        //     _authorityDetailManagerRepository.Update(vInfo);
        //     //=================Ghi Log==================
        //     await _systemLogRepository.AddAsync(new SystemLog(Guid.NewGuid(),
        //                                                       DateTime.Now,
        //                                                       SystemLogEnumeration.Update.Id,
        //                                                       _user.StaffId,
        //                                                       _user.StaffName,
        //                                                       GetType().Name,
        //                                                       "",
        //                                                       vInfo.Id,
        //                                                       JsonConvert.SerializeObject(vInfo),
        //                                                       JsonConvert.SerializeObject(command),
        //                                                       _user.UserId));
        //     //==========================================
        //     if (Commit())
        //     {
        //         command.ResultCommand = true;
        //         return await Task.FromResult(true);
        //     }
        // }

        NotifyValidationErrors(NErrorHelper.du_lieu_khong_ton_tai + ": " + command.Id);
        return await Task.FromResult(false);
    }

#endregion

#region IRequestHandler<UpdatePermissionByAuthoritiesCommand,bool> Members

    /// <summary>
    ///     Handle cập nhật giá trị sử dụng
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> Handle(UpdatePermissionByAuthoritiesCommand command, CancellationToken cancellationToken)
    {
        //Gán mặc định kết quả trả về là false
        command.ResultCommand = false;

        if (!command.IsValid())
        {
            NotifyValidationErrors(command);
            return await Task.FromResult(false);
        }

        var vInfo = _authorityDetailManagerRepository.GetAsync(command.Id).Result;
        if (vInfo != null)
        {
            vInfo.Value = command.Value;
            _authorityDetailManagerRepository.Update(vInfo);
            //=================Ghi Log==================
            await _systemLogRepository.AddAsync(new SystemLog(Guid.NewGuid(),
                                                              DateTime.Now,
                                                              SystemLogEnumeration.Update.Id,
                                                              _user.StaffId,
                                                              _user.StaffName,
                                                              GetType().Name,
                                                              "",
                                                              vInfo.Id,
                                                              JsonConvert.SerializeObject(vInfo),
                                                              JsonConvert.SerializeObject(command),
                                                              _user.UserId));
            //==========================================
            if (Commit())
            {
                command.ResultCommand = true;
                return await Task.FromResult(true);
            }
        }

        NotifyValidationErrors(NErrorHelper.du_lieu_khong_ton_tai + ": " + command.Id);
        return await Task.FromResult(false);
    }

#endregion

    /// <summary>
    ///     Trả về dữ liệu quyền được cấp
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    private List<MenuByAuthoritiesSaveModel> ReturnMenuByAuthoritiesSaveModels(List<MenuByAuthoritiesSaveModel> data)
    {
        var listUpdate = new List<Guid>();
        foreach (var items in data)
        {
            var sumData = data.Where(x => x.ParentId == items.Id)
                              .Sum(x => x.Value);
            if (sumData > 0)
                // Danh sách các nhóm cha phía trên cần cập nhật lại dữ liệu
                listUpdate.Add(items.ParentId);
        }

    xulydulieucha:
        var listUpdateTemp = listUpdate.Distinct().ToList();
        listUpdate = new List<Guid>();
        foreach (var iData in
                 listUpdateTemp.Select(items => data.FirstOrDefault(x => x.Id == items))
                               .Where(iData => iData != null))
        {
            listUpdate.Add(iData.ParentId);
            iData.Value = 1;
        }

        if (listUpdate.Count > 0) goto xulydulieucha;

        return data;
    }

#region Fields

    private readonly IAuthorityManagerRepository _authorityManagerRepository;
    private readonly IAuthorityManagerQueries    _authorityManagerQueries;
    private readonly IAuthorityDetailRepository  _authorityDetailManagerRepository;
    private readonly ISystemLogRepository        _systemLogRepository;
    private readonly IUser                       _user;

#endregion
}