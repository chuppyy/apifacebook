#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ITC.Domain.Commands.NewsManagers.NewsGroupManagers;
using ITC.Domain.Core.Bus;
using ITC.Domain.Core.ModelShare.NewsManagers.NewsGroupManagers;
using ITC.Domain.Core.ModelShare.NewsManagers.NewsGroupTypeManagers;
using ITC.Domain.Core.ModelShare.PublishManagers;
using ITC.Domain.Interfaces;
using ITC.Domain.Interfaces.AuthorityManager.AuthorityManagerSystem;
using ITC.Domain.Interfaces.NewsManagers.NewsDomainManagers;
using ITC.Domain.Interfaces.NewsManagers.NewsGroupManagers;
using ITC.Domain.Models.NewsManagers;
using Microsoft.Extensions.Logging;
using NCore.Helpers;
using NCore.Modals;

#endregion

namespace ITC.Application.AppService.NewsManagers.NewsGroupManagers;

/// <summary>
///     Class service nhóm tin
/// </summary>
public class NewsGroupAppService : INewsGroupAppService
{
    #region Constructors

    /// <summary>
    ///     Hàm dựng có tham số
    /// </summary>
    /// <param name="mapper"></param>
    /// <param name="queries"></param>
    /// <param name="repository"></param>
    /// <param name="authorityManagerQueries"></param>
    /// <param name="newsDomainRepository"></param>
    /// <param name="logger"></param>
    /// <param name="bus"></param>
    /// <param name="user"></param>
    /// <param name="newsConfigRepository"></param>
    public NewsGroupAppService(IMapper                      mapper,
                               INewsGroupQueries            queries,
                               INewsGroupRepository         repository,
                               IAuthorityManagerQueries     authorityManagerQueries,
                               INewsDomainRepository        newsDomainRepository,
                               ILogger<NewsGroupAppService> logger,
                               IMediatorHandler             bus,
                               IUser                        user)
    {
        _mapper                  = mapper;
        _queries                 = queries;
        _repository              = repository;
        _authorityManagerQueries = authorityManagerQueries;
        _newsDomainRepository    = newsDomainRepository;
        _logger                  = logger;
        _bus                     = bus;
        _user                    = user;
    }

    #endregion

    #region Fields

    private readonly IMediatorHandler             _bus;
    private readonly IUser                        _user;
    private readonly IMapper                      _mapper;
    private readonly INewsGroupQueries            _queries;
    private readonly INewsGroupRepository         _repository;
    private readonly IAuthorityManagerQueries     _authorityManagerQueries;
    private readonly INewsDomainRepository        _newsDomainRepository;
    private readonly ILogger<NewsGroupAppService> _logger;

    #endregion

    #region INewsGroupAppService Members

    /// <summary>
    ///     Thêm nhóm tin
    /// </summary>
    /// <param name="model"></param>
    public async Task<bool> Add(NewsGroupEventModel model)
    {
        var addCommand = _mapper.Map<AddNewsGroupCommand>(model);
        await _bus.SendCommand(addCommand);
        model.Id            = addCommand.Id;
        model.ResultCommand = addCommand.ResultCommand;
        return model.ResultCommand;
    }

    /// <summary>
    ///     Xóa nhóm tin
    /// </summary>
    /// <param name="model"></param>
    public async Task<bool> Delete(DeleteModal model)
    {
        var deleteCommand = new DeleteNewsGroupCommand(model.Models);
        await _bus.SendCommand(deleteCommand);
        model.ResultCommand = deleteCommand.ResultCommand;
        return model.ResultCommand;
    }

    /// <summary>
    ///     Lấy theo Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public NewsGroupEventModel GetById(Guid id)
    {
        var iReturn = _repository.GetAsync(id).Result;
        if (iReturn == null) return null;

        return new NewsGroupEventModel
        {
            Id              = iReturn.Id,
            Name            = iReturn.Name,
            ParentId        = iReturn.ParentId,
            Position        = iReturn.Position,
            CreatedBy       = iReturn.CreatedBy,
            Description     = iReturn.Description,
            NewsGroupTypeId = iReturn.NewsGroupTypeId,
            Domain          = iReturn.Domain,
            AgreeVia        = iReturn.AgreeVia,
            LinkTree        = iReturn.LinkTree,
            IdViaQc         = iReturn.IdViaQc,
            StaffId         = iReturn.StaffId,
            IsShowMain      = iReturn.IsShowMain,
            DomainVercel    = iReturn.DomainVercel,
            Amount = iReturn.Amount,
            AmountPosted = iReturn.AmountPosted
        };
    }

    /// <inheritdoc cref="GetTreeView" />
    public async Task<IEnumerable<TreeViewProjectModel>> GetTreeView(TreeViewPagingModel model)
    {
        return await Task.Run(() =>
        {
            var iPermission = 1;
            if (!string.IsNullOrEmpty(_user.UserId))
            {
                //===============================Quyền người dùng=======================================================

                iPermission = model.IsModal
                    ? 0
                    : _authorityManagerQueries
                        .GetPermissionByMenuManagerValue(model.ModuleIdentity, _user.UserId)
                        .Result;
            }
            
            var lMenu =
                _queries.GetTreeView(model.VSearch, model.NewsGroupTypeId, model.IsAll, _user.ProjectId)
                        .Result.ToList();
            return new TreeViewHelper().GetTreeViewWithTreeSelectVueJs(lMenu, _user.UserId, iPermission).Result;
        });
    }

    /// <inheritdoc cref="Update" />
    public async Task<bool> Update(NewsGroupEventModel model)
    {
        var updateCommand = _mapper.Map<UpdateNewsGroupCommand>(model);
        await _bus.SendCommand(updateCommand);
        model.ResultCommand = updateCommand.ResultCommand;
        return model.ResultCommand;
    }

    /// <inheritdoc cref="UpdateLocation" />
    public async Task<bool> UpdateLocation(UpdatePositionModal model)
    {
        var addCommand = _mapper.Map<UpdatePositionNewsGroupCommand>(model);
        await _bus.SendCommand(addCommand);
        model.ResultCommand = addCommand.ResultCommand;
        return model.ResultCommand;
    }

    /// <inheritdoc cref="GetListIdFromListId" />
    public async Task<List<Guid>> GetListIdFromListId(List<Guid> models)
    {
        return await Task.Run(() =>
        {
            var lReturn = new List<Guid>();
            foreach (var iListId in models.Select(items => _repository.GetAsync(items).Result)
                                          .Select(iNewsGroupInfo => _repository
                                                                    .GetListIdFromLeftRight(iNewsGroupInfo.PLeft - 1,
                                                                        iNewsGroupInfo.PRight                    + 1)
                                                                    .Result).Where(iListId => iListId.Count > 0))
                lReturn.AddRange(iListId);

            return lReturn.Distinct().ToList();
        });
    }

    /// <inheritdoc cref="GetBySecrect" />
    public async Task<NewsGroup> GetBySecrect(Guid projectId, string secrectKey)
    {
        return await _repository.GetBySecrectKey(projectId, secrectKey);
    }

    /// <inheritdoc cref="ListDomainVercel"/>
    public async Task<IEnumerable<ListVercelDto>> ListDomainVercel()
    {
        return await Task.Run(() =>
        {
            var listReturn = new List<ListVercelDto>();
            var listVercel = _queries.ListDomainVercel().Result;
            var group      = listVercel.Where(x => x.ParentId == Guid.Empty.ToString());
            foreach (var groupItems in group)
            {
                listReturn.Add(new ListVercelDto
                {
                    Id           = groupItems.Id,
                    DomainVercel = groupItems.DomainVercel,
                    ParentId     = groupItems.ParentId,
                    Name         = groupItems.Name,
                    Domain       = groupItems.Domain.Substring(0, groupItems.Domain.Length - 1),
                    Checked      = "not_accept",
                    Number       = 0
                });
                var child = listVercel.Where(x => x.ParentId == groupItems.Id.ToString());
                if (child.Any())
                {
                    listReturn.AddRange(child.Select(childItem => new ListVercelDto
                    {
                        Id           = childItem.Id,
                        DomainVercel = childItem.DomainVercel,
                        ParentId     = childItem.ParentId,
                        Name         = childItem.Name,
                        Domain       = childItem.Domain.Substring(0, childItem.Domain.Length - 1),
                        Checked      = "not_accept",
                        Number       = 1
                    }));
                }
            }

            return listReturn;
        });
    }

    /// <inheritdoc cref="ChangeDomainVercel"/>
    public async Task<string> ChangeDomainVercel()
    {
        return await Task.Run(() =>
        {
            var domainInfo = _newsDomainRepository.GetFirt().Result;
            if (domainInfo == null) return "";
            
            _logger.LogInformation("===Domain change====:" + domainInfo.Id);
            domainInfo.IsDeleted = true;
            _newsDomainRepository.SaveChanges();
            return domainInfo.Name;

        });
    }

    #endregion
}