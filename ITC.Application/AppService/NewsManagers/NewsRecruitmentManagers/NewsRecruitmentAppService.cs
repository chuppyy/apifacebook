#region

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ITC.Application.AppService.SystemManagers.ServerFileManagers;
using ITC.Domain.Commands.NewsManagers.NewsRecruitmentManagers;
using ITC.Domain.Core.Bus;
using ITC.Domain.Core.ModelShare.NewsManagers.NewsRecruitmentManagers;
using ITC.Domain.Interfaces;
using ITC.Domain.Interfaces.AuthorityManager.AuthorityManagerSystem;
using ITC.Domain.Interfaces.NewsManagers.NewsRecruitmentManagers;
using ITC.Domain.Models.NewsManagers;
using NCore.Actions;
using NCore.Modals;

#endregion

namespace ITC.Application.AppService.NewsManagers.NewsRecruitmentManagers;

/// <summary>
///     Class service bài viết
/// </summary>
public class NewsRecruitmentAppService : INewsRecruitmentAppService
{
#region Constructors

    /// <summary>
    ///     Hàm dựng có tham số
    /// </summary>
    /// <param name="mapper"></param>
    /// <param name="queries"></param>
    /// <param name="repository"></param>
    /// <param name="authorityManagerQueries"></param>
    /// <param name="serverFileAppService"></param>
    /// <param name="bus"></param>
    /// <param name="user"></param>
    public NewsRecruitmentAppService(IMapper                    mapper,
                                     INewsRecruitmentQueries    queries,
                                     INewsRecruitmentRepository repository,
                                     IAuthorityManagerQueries   authorityManagerQueries,
                                     IServerFileAppService      serverFileAppService,
                                     IMediatorHandler           bus,
                                     IUser                      user)
    {
        _mapper                  = mapper;
        _queries                 = queries;
        _repository              = repository;
        _authorityManagerQueries = authorityManagerQueries;
        _serverFileAppService    = serverFileAppService;
        _bus                     = bus;
        _user                    = user;
    }

#endregion

#region Fields

    private readonly IMediatorHandler           _bus;
    private readonly IUser                      _user;
    private readonly IMapper                    _mapper;
    private readonly INewsRecruitmentQueries    _queries;
    private readonly INewsRecruitmentRepository _repository;
    private readonly IAuthorityManagerQueries   _authorityManagerQueries;
    private readonly IServerFileAppService      _serverFileAppService;

#endregion

#region INewsRecruitmentAppService Members

    /// <summary>
    ///     Thêm bài viết
    /// </summary>
    /// <param name="model"></param>
    public async Task<bool> Add(NewsRecruitmentEventModel model)
    {
        var addCommand = _mapper.Map<AddNewsRecruitmentCommand>(model);
        await _bus.SendCommand(addCommand);
        model.Id            = addCommand.Id;
        model.ResultCommand = addCommand.ResultCommand;
        return model.ResultCommand;
    }

    /// <summary>
    ///     Xóa bài viết
    /// </summary>
    /// <param name="model"></param>
    public bool Delete(DeleteModal model)
    {
        var deleteCommand = new DeleteNewsRecruitmentCommand(model.Models);
        _bus.SendCommand(deleteCommand);
        model.ResultCommand = deleteCommand.ResultCommand;
        return model.ResultCommand;
    }

    /// <summary>
    ///     Lấy theo Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<NewsRecruitmentGetByIdModel> GetById(Guid id)
    {
        return await Task.Run(async () =>
        {
            var iValue = await _repository.GetAsync(id);
            if (iValue == null) return null;

            //=========================================Lấy dữ liệu từ ServerFile====================================
            var iServerFileInfo = _serverFileAppService.ViewFile(iValue.AvatarId).Result;
            //======================================================================================================

            return new NewsRecruitmentGetByIdModel
            {
                Id            = iValue.Id,
                Name          = iValue.Name,
                CreatedBy     = iValue.CreatedBy,
                Content       = iValue.Content,
                Summary       = iValue.Summary,
                AvatarId      = iValue.AvatarId,
                ResultCommand = true,
                SeoKeyword    = iValue.SeoKeyword,
                StatusId      = iValue.StatusId,
                DateTimeStart = Convert.ToDateTime(iValue.DateTimeStart).ToString("dd - MM - yyyy HH:mm"),
                IsLocal       = iServerFileInfo?.IsLocal ?? false
            };
        });
    }

    /// <inheritdoc cref="Update" />
    public async Task<bool> Update(NewsRecruitmentEventModel model)
    {
        var updateCommand = _mapper.Map<UpdateNewsRecruitmentCommand>(model);
        await _bus.SendCommand(updateCommand);
        model.ResultCommand = updateCommand.ResultCommand;
        return model.ResultCommand;
    }

    /// <inheritdoc cref="GetPaging" />
    public async Task<IEnumerable<NewsRecruitmentPagingDto>> GetPaging(PagingModel model, int type)
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
            var lData = (List<NewsRecruitmentPagingDto>)_queries.GetPaging(model, type).Result;
            // Trả về Actions
            foreach (var item in lData)
                item.Actions =
                    ActionPagingWorkManager.ActionAuthoritiesNews(iPermission,
                                                                  item.OwnerId,
                                                                  _user.UserId,
                                                                  true,
                                                                  item.StatusId);
            return lData;
        });
    }

    /// <inheritdoc cref="GetBySecrect" />
    public async Task<NewsRecruitment> GetBySecrect(Guid projectId, string secrectKey)
    {
        return await _repository.GetBySecretKey(secrectKey);
    }

#endregion
}