#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Hangfire;
using ITC.Application.Services.Vercel;
using ITC.Domain.Commands.NewsManagers.NewsDomainManagers;
using ITC.Domain.Commands.NewsManagers.NewsGroupTypeManagers;
using ITC.Domain.Core.Bus;
using ITC.Domain.Core.ModelShare.NewsManagers.NewsDomainManagers;
using ITC.Domain.Core.NCoreLocal;
using ITC.Domain.Core.NCoreLocal.Enum;
using ITC.Domain.Interfaces;
using ITC.Domain.Interfaces.AuthorityManager.AuthorityManagerSystem;
using ITC.Domain.Interfaces.NewsManagers.NewsConfigManagers;
using ITC.Domain.Interfaces.NewsManagers.NewsDomainManagers;
using Microsoft.Extensions.Logging;
using NCore.Actions;
using NCore.Modals;
using Newtonsoft.Json;

#endregion

namespace ITC.Application.AppService.NewsManagers.NewsDomainManagers;

/// <summary>
///     Class service loại nhóm tin
/// </summary>
public class NewsDomainAppService : INewsDomainAppService
{
#region Constructors

    /// <summary>
    ///     Hàm dựng có tham số
    /// </summary>
    /// <param name="mapper"></param>
    /// <param name="bus"></param>
    /// <param name="newsDomainQueries"></param>
    /// <param name="newsDomainRepository"></param>
    /// <param name="authorityManagerQueries"></param>
    /// <param name="vercelService"></param>
    /// <param name="newsConfigRepository"></param>
    /// <param name="logger"></param>
    /// <param name="user"></param>
    public NewsDomainAppService(IMapper                       mapper,
                                IMediatorHandler              bus,
                                INewsDomainQueries            newsDomainQueries,
                                INewsDomainRepository         newsDomainRepository,
                                IAuthorityManagerQueries      authorityManagerQueries,
                                IVercelService                vercelService,
                                INewsConfigRepository         newsConfigRepository,
                                ILogger<NewsDomainAppService> logger,
                                IUser                         user)
    {
        _mapper                  = mapper;
        _bus                     = bus;
        _newsDomainQueries       = newsDomainQueries;
        _newsDomainRepository    = newsDomainRepository;
        _authorityManagerQueries = authorityManagerQueries;
        _vercelService           = vercelService;
        _newsConfigRepository    = newsConfigRepository;
        _logger                  = logger;
        _user                    = user;
    }

#endregion

    /// <inheritdoc cref="Add" />
    public bool Add(NewsDomainEventModel model)
    {
        var addCommand = _mapper.Map<AddNewsDomainCommand>(model);
        _bus.SendCommand(addCommand);
        model.Id            = addCommand.Id;
        model.ResultCommand = addCommand.ResultCommand;
        return model.ResultCommand;
    }

    /// <inheritdoc cref="Delete" />
    public bool Delete(DeleteModal model)
    {
        var deleteCommand = new DeleteNewsDomainCommand(model.Models);
        _bus.SendCommand(deleteCommand);
        model.ResultCommand = deleteCommand.ResultCommand;
        return model.ResultCommand;
    }

    /// <inheritdoc cref="GetById" />
    public NewsDomainEventModel GetById(Guid id)
    {
        var iReturn = _newsDomainRepository.GetAsync(id).Result;
        if (iReturn == null) return null;

        return new NewsDomainEventModel
        {
            Id          = iReturn.Id,
            Name        = iReturn.Name,
            CreatedBy   = iReturn.CreatedBy,
            Description = iReturn.Description
        };
    }

    /// <inheritdoc cref="Update" />
    public bool Update(NewsDomainEventModel model)
    {
        var addCommand = _mapper.Map<UpdateNewsDomainCommand>(model);
        _bus.SendCommand(addCommand);
        model.ResultCommand = addCommand.ResultCommand;
        return model.ResultCommand;
    }

    /// <inheritdoc cref="GetCombobox" />
    public Task<IEnumerable<ComboboxModal>> GetCombobox(string vSearch)
    {
        return _newsDomainQueries.GetCombobox(vSearch, _user.ProjectId);
    }

    /// <inheritdoc cref="GetPaging" />
    public async Task<IEnumerable<NewsDomainPagingDto>> GetPaging(PagingModel model)
    {
        return await Task.Run(() =>
        {
            // Lấy dữ liệu quyền của người dùng
            var iPermission =
                _authorityManagerQueries
                    .GetPermissionByMenuManagerValue(model.ModuleIdentity, _user.UserId)
                    .Result;
            // Xử lý dữ liệu
            model.ProjectId = _user.ProjectId;
            var lData = (List<NewsDomainPagingDto>)_newsDomainQueries.GetPaging(model).Result;
            // Trả về Actions
            foreach (var item in lData)
                item.Actions =
                    ActionPagingWorkManager.ActionAuthoritiesNews(iPermission,
                                                                  item.OwnerId,
                                                                  _user.UserId,
                                                                  false,
                                                                  item.StatusId);
            return lData;
        });
    }

    public async Task SchedulerStart()
    {
        var newsConfig        = _newsConfigRepository.GetAllAsync().Result.FirstOrDefault();
        var tokenGit          = newsConfig?.TokenGit          ?? ""; 
        var ownerGit          = newsConfig?.OwnerGit          ?? ""; 
        var projectDefaultGit = newsConfig?.ProjectDefaultGit ?? ""; 
        var teamId            = newsConfig?.TeamId            ?? ""; 
        var tokenVercel       = newsConfig?.TokenVercel       ?? ""; 
        _logger.LogInformation("===Bắt đầu gọi vercel========");

        var domainVercel =
            await _vercelService.CreatedVercel(tokenGit, ownerGit, projectDefaultGit, teamId, tokenVercel);
        _logger.LogInformation("===Kết thúc gọi vercel========");
        _logger.LogInformation(JsonConvert.SerializeObject(domainVercel));
        _logger.LogInformation("===Bắt đầu gửi lưu server========");
        
        var sBuilder = new StringBuilder();
        foreach (var items in domainVercel)
        {
            var dateTimeNow = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd HH:mm:ss.fffffff");
            sBuilder.Append(
                $@"
                    INSERT INTO NewsDomains (Id, Name, DomainNew, Description, IsDeleted, StatusId, Created, Modified)
                    VALUES ('{Guid.NewGuid()}','{items.Name}','{items.IdDomain}',N'',0, 3, '{dateTimeNow}', '{dateTimeNow}');
                ");
        }

        if (domainVercel.Count > 0)
        {
            var _ = _newsDomainQueries.SaveDomain(sBuilder).Result;
            _logger.LogInformation("===Kết thúc lưu server========: " + _);
        }
    }

    /// <inheritdoc cref="GetScheduleConfig"/>
    public Task<int> GetScheduleConfig(string authorities)
    {
        var iPermission =
            _authorityManagerQueries
                .GetPermissionByMenuManagerValue(authorities, _user.UserId)
                .Result;
        return Task.FromResult((iPermission & PermissionEnum.ChayTuDong.Id) != 0
                                   ? new NCoreHelperV2023().ReturnScheduleConfigDomain()
                                   : 0);
    }

    /// <inheritdoc cref="GetScheduleSave"/>
    [Obsolete("Obsolete")]
    public async Task<int> GetScheduleSave(int id)
    {
        return await Task.Run(async () =>
        {
            var cronJobName = "schedulerDomain";
            var fullPath    = new NCoreHelperV2023().ScheduleConfigDomain;
            await File.WriteAllTextAsync(fullPath, id.ToString());
            if (id == 2)
            {
                // Start
            #pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                SchedulerStart();
            #pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                RecurringJob.AddOrUpdate(cronJobName,
                                         () => SchedulerStart(),
                                         Cron.MinuteInterval(59));
            }
            else
            {
                // Remove
                RecurringJob.RemoveIfExists(cronJobName);
            }

            return 1;
        });
    }
    
    #region Fields

    private readonly IMediatorHandler              _bus;
    private readonly INewsDomainQueries            _newsDomainQueries;
    private readonly INewsDomainRepository         _newsDomainRepository;
    private readonly IAuthorityManagerQueries      _authorityManagerQueries;
    private readonly IVercelService                _vercelService;
    private readonly INewsConfigRepository         _newsConfigRepository;
    private readonly ILogger<NewsDomainAppService> _logger;
    private readonly IUser                         _user;
    private readonly IMapper                       _mapper;

#endregion
}