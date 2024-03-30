using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ITC.Domain.Commands.SystemManagers.HelperManagers;
using ITC.Domain.Core.Bus;
using ITC.Domain.Core.Events;
using ITC.Domain.Core.Notifications;
using ITC.Domain.Interfaces;
using ITC.Domain.Interfaces.NewsManagers.NewsContentManagers;
using ITC.Domain.Interfaces.NewsManagers.NewsDomainManagers;
using ITC.Domain.Interfaces.NewsManagers.NewsGroupManagers;
using ITC.Domain.Interfaces.SystemManagers.Helpers;
using ITC.Domain.Interfaces.SystemManagers.SystemLogs;
using ITC.Domain.Interfaces.SystemManagers.TableDeleteManagers;
using MediatR;
using NCore.Actions;
using NCore.Enums;
using Newtonsoft.Json;

namespace ITC.Domain.CommandHandlers.SystemManagers;

#region

#endregion

/// <summary>
///     Command Handler Server File
/// </summary>
public class HelperCommandHandler : CommandHandler,
                                    IRequestHandler<UpdateStatusHelperCommand, bool>
{
    #region Constructors

    /// <summary>
    ///     Hàm dựng với tham số
    /// </summary>
    /// <param name="user"></param>
    /// <param name="helperQueries"></param>
    /// <param name="systemLogRepository"></param>
    /// <param name="tableDeleteManagerRepository"></param>
    /// <param name="newsContentRepository"></param>
    /// <param name="newsContentQueries"></param>
    /// <param name="newsGroupRepository"></param>
    /// <param name="newsDomainRepository"></param>
    /// <param name="newsDomainQueries"></param>
    /// <param name="uow"></param>
    /// <param name="bus"></param>
    /// <param name="notifications"></param>
    public HelperCommandHandler(IUser                                    user,
                                IHelperQueries                           helperQueries,
                                ISystemLogRepository                     systemLogRepository,
                                ITableDeleteManagerRepository            tableDeleteManagerRepository,
                                INewsContentRepository                   newsContentRepository,
                                INewsContentQueries                      newsContentQueries,
                                INewsGroupRepository                     newsGroupRepository,
                                INewsDomainRepository                    newsDomainRepository,
                                INewsDomainQueries                       newsDomainQueries,
                                IUnitOfWork                              uow,
                                IMediatorHandler                         bus,
                                INotificationHandler<DomainNotification> notifications) :
        base(uow, bus, notifications)
    {
        _user                         = user;
        _helperQueries                = helperQueries;
        _systemLogRepository          = systemLogRepository;
        _tableDeleteManagerRepository = tableDeleteManagerRepository;
        _newsContentRepository        = newsContentRepository;
        _newsContentQueries           = newsContentQueries;
        _newsGroupRepository          = newsGroupRepository;
        _newsDomainRepository         = newsDomainRepository;
        _newsDomainQueries            = newsDomainQueries;
    }

    #endregion

    #region IRequestHandler<UpdateStatusHelperCommand,bool> Members

    /// <summary>
    ///     Handle cập nhật trạng thái dữ liệu
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> Handle(UpdateStatusHelperCommand command, CancellationToken cancellationToken)
    {
        // Gán mặc định giá trị trả về khi xử lý = 0
        command.ResultCommand = false;

        if (!command.IsValid())
        {
            NotifyValidationErrors(command);
            return await Task.FromResult(false);
        }

        var iTableName = _tableDeleteManagerRepository.GetTableNameByCode(command.TableId).Result;
        var iStatus = command.FlagKey switch
                      {
                          201 => ActionStatusEnum.Active.Id,
                          202 => ActionStatusEnum.Lock.Id,
                          _   => 0
                      };

        var iStatusLog = command.FlagKey switch
                         {
                             201 => SystemLogEnumeration.UnLock.Id,
                             202 => SystemLogEnumeration.Lock.Id,
                             _   => 0
                         };

        var iResult = _helperQueries.UpdateStatus(command.Id, iTableName, command.FlagKey == 102 ? 1000 : iStatus)
                                    .Result;
        if (iResult > 0)
        {
            if (command.TableId == 2 && command.FlagKey == 201)
            {
                // Bảng NewsContent
                // Nếu là chuyển trạng thái từ chờ đăng => đã đăng thì cập nhật lại domain cho nhóm tin 
                var newsContentInfo = _newsContentRepository.GetAsync(command.Id).Result;
                if (newsContentInfo != null)
                {
                    if (string.IsNullOrEmpty(newsContentInfo.LinkTree))
                    {
                        var newsGroupInfo = _newsGroupRepository.GetAsync(newsContentInfo.NewsGroupId).Result;
                        if (newsGroupInfo != null)
                        {
                            if (newsGroupInfo.AgreeVia)
                            {
                                var newsDomainFirst = _newsDomainQueries.GetFirstDomain().Result;
                                if (newsDomainFirst.Any())
                                {
                                    newsGroupInfo.Domain = newsDomainFirst.FirstOrDefault()?.Name;
                                    _newsGroupRepository.Update(newsGroupInfo);

                                    var domainId       = newsDomainFirst.FirstOrDefault()?.Id ?? Guid.Empty;
                                    var newsDomainInfo = _newsDomainRepository.GetAsync(domainId).Result;
                                    newsDomainInfo.IsDeleted = true;
                                    newsDomainInfo.Modified  = DateTime.Now;
                                    _newsDomainRepository.Update(newsDomainInfo);

                                    // // Xóa dữ liệu linktree các bài viết chưa đăng mà có cùng nhóm danh mục như bài viết này !
                                    // var sBuilder = new StringBuilder();
                                    // sBuilder.Append(
                                    //     $@"UPDATE NewsContents SET LinkTree = '' WHERE NewsGroupId = '{newsGroupInfo.Id}' AND StatusId != 3;");
                                    // _ = _newsContentQueries.SaveDomain(sBuilder).Result;
                                }
                            }
                        }
                    }
                }
            }

            command.ResultCommand = true;
            //=================Ghi Log==================
            await _systemLogRepository.AddAsync(new SystemLog(Guid.NewGuid(),
                                                              DateTime.Now,
                                                              iStatusLog,
                                                              _user.StaffId,
                                                              _user.StaffName,
                                                              GetType().Name,
                                                              "Table: " + iTableName,
                                                              command.Id,
                                                              "",
                                                              JsonConvert.SerializeObject(command),
                                                              _user.UserId));
            _systemLogRepository.SaveChanges();
            //==========================================
            return await Task.FromResult(true);
        }

        var iMes = command.FlagKey switch
                   {
                       102 => "Xóa dữ liệu không thành công",
                       201 => "Mở khóa dữ liệu không thành công",
                       202 => "Khóa dữ liệu không thành công",
                       _   => ""
                   };
        NotifyValidationErrors(iMes);
        return await Task.FromResult(false);
    }

    #endregion

    #region Fields

    private readonly IUser                         _user;
    private readonly IHelperQueries                _helperQueries;
    private readonly ISystemLogRepository          _systemLogRepository;
    private readonly ITableDeleteManagerRepository _tableDeleteManagerRepository;
    private readonly INewsContentRepository        _newsContentRepository;
    private readonly INewsContentQueries           _newsContentQueries;
    private readonly INewsGroupRepository          _newsGroupRepository;
    private readonly INewsDomainRepository         _newsDomainRepository;
    private readonly INewsDomainQueries            _newsDomainQueries;

    #endregion
}