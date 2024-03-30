#region

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ITC.Domain.Commands.Itphonui.ProjectManager;
using ITC.Domain.Core.Bus;
using ITC.Domain.Core.Events;
using ITC.Domain.Core.Notifications;
using ITC.Domain.Interfaces;
using ITC.Domain.Interfaces.Itphonui.ProjectManagers;
using ITC.Domain.Interfaces.SystemManagers.SystemLogs;
using ITC.Domain.Models.Itphonui;
using MediatR;
using NCore.Enums;
using NCore.Helpers;
using Newtonsoft.Json;

#endregion

namespace ITC.Domain.CommandHandlers.Itphonui;

#region

#endregion

/// <summary>
///     Command Handler dự án
/// </summary>
public class ProjectManagerCommandHandler : CommandHandler,
                                            IRequestHandler<AddProjectManagerCommand, bool>,
                                            IRequestHandler<UpdateProjectManagerCommand, bool>,
                                            IRequestHandler<DeleteProjectManagerCommand, bool>
{
#region Constructors

    /// <summary>
    ///     Hàm dựng với tham số
    /// </summary>
    /// <param name="user"></param>
    /// <param name="projectManagerRepository"></param>
    /// <param name="projectManagerQueries"></param>
    /// <param name="systemLogRepository"></param>
    /// <param name="uow"></param>
    /// <param name="bus"></param>
    /// <param name="notifications"></param>
    public ProjectManagerCommandHandler(IUser                                    user,
                                        IProjectManagerRepository                projectManagerRepository,
                                        IProjectManagerQueries                   projectManagerQueries,
                                        ISystemLogRepository                     systemLogRepository,
                                        IUnitOfWork                              uow,
                                        IMediatorHandler                         bus,
                                        INotificationHandler<DomainNotification> notifications) : base(
        uow, bus, notifications)
    {
        _user                = user;
        _repository          = projectManagerRepository;
        _queries             = projectManagerQueries;
        _systemLogRepository = systemLogRepository;
    }

#endregion

#region IRequestHandler<AddProjectManagerCommand,bool> Members

    /// <summary>
    ///     Handle thêm mới
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> Handle(AddProjectManagerCommand command, CancellationToken cancellationToken)
    {
        // Gán mặc định giá trị trả về khi xử lý = 0
        command.ResultCommand = false;

        if (!command.IsValid())
        {
            NotifyValidationErrors(command);
            return await Task.FromResult(false);
        }

        var iKey  = Guid.NewGuid();
        var iCode = Convert.ToInt32(_repository.GetAllAsync().Result.MaxBy(x => x.Created).Code) + 1;
        var rAdd = new ProjectManager(iKey,
                                      command.Name,
                                      command.Description,
                                      command.StartDate,
                                      command.EndDate,
                                      command.NumberOfExtend,
                                      command.HostName,
                                      command.TitleOne,
                                      command.TitleTwo,
                                      command.TitleMobileOne,
                                      command.TitleMobileTwo,
                                      command.TitleMobileThree,
                                      command.IsUseLocalUrl,
                                      iCode < 10 ? "0" + iCode : iCode + "",
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

#region IRequestHandler<DeleteProjectManagerCommand,bool> Members

    /// <summary>
    ///     Handle xóa
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> Handle(DeleteProjectManagerCommand command, CancellationToken cancellationToken)
    {
        // Gán mặc định giá trị trả về khi xử lý = 0
        command.ResultCommand = false;

        if (!command.IsValid())
        {
            NotifyValidationErrors(command);
            return await Task.FromResult(false);
        }

        var iResult = _queries.DeleteAsync(command.ListModel).Result;
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
                                                              JsonConvert.SerializeObject(command.ListModel),
                                                              _user.UserId));
            //==========================================
            return await Task.FromResult(true);
        }

        NotifyValidationErrors(NErrorHelper.xoa_khong_thanh_cong);
        return await Task.FromResult(false);
    }

#endregion

#region IRequestHandler<UpdateProjectManagerCommand,bool> Members

    /// <summary>
    ///     Handle cập nhật danh mục
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> Handle(UpdateProjectManagerCommand command, CancellationToken cancellationToken)
    {
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
                            command.StartDate,
                            command.EndDate,
                            command.NumberOfExtend,
                            command.HostName,
                            command.TitleOne,
                            command.TitleTwo,
                            command.TitleMobileOne,
                            command.TitleMobileTwo,
                            command.TitleMobileThree,
                            command.IsUseLocalUrl,
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

    private readonly IProjectManagerRepository _repository;
    private readonly IProjectManagerQueries    _queries;
    private readonly ISystemLogRepository      _systemLogRepository;
    private readonly IUser                     _user;

#endregion
}