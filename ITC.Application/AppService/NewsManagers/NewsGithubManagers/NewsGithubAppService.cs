﻿#region

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ITC.Domain.Commands.NewsManagers.NewsGithubManagers;
using ITC.Domain.Core.Bus;
using ITC.Domain.Core.ModelShare.NewsManagers.NewsGithubManagers;
using ITC.Domain.Interfaces;
using ITC.Domain.Interfaces.AuthorityManager.AuthorityManagerSystem;
using ITC.Domain.Interfaces.NewsManagers.NewsGithubManagers;
using NCore.Actions;
using NCore.Modals;

#endregion

namespace ITC.Application.AppService.NewsManagers.NewsGithubManagers;

/// <summary>
///     Class service loại nhóm tin
/// </summary>
public class NewsGithubAppService : INewsGithubAppService
{
    #region Constructors

    /// <summary>
    ///     Hàm dựng có tham số
    /// </summary>
    /// <param name="mapper"></param>
    /// <param name="bus"></param>
    /// <param name="newsGithubQueries"></param>
    /// <param name="newsGithubRepository"></param>
    /// <param name="authorityManagerQueries"></param>
    /// <param name="user"></param>
    public NewsGithubAppService(IMapper                  mapper,
                                IMediatorHandler         bus,
                                INewsGithubQueries       newsGithubQueries,
                                INewsGithubRepository    newsGithubRepository,
                                IAuthorityManagerQueries authorityManagerQueries,
                                IUser                    user)
    {
        _mapper                  = mapper;
        _bus                     = bus;
        _newsGithubQueries       = newsGithubQueries;
        _newsGithubRepository    = newsGithubRepository;
        _authorityManagerQueries = authorityManagerQueries;
        _user                    = user;
    }

    #endregion

    /// <inheritdoc cref="Add" />
    public bool Add(NewsGithubEventModel model)
    {
        var addCommand = _mapper.Map<AddNewsGithubCommand>(model);
        _bus.SendCommand(addCommand);
        model.Id            = addCommand.Id;
        model.ResultCommand = addCommand.ResultCommand;
        return model.ResultCommand;
    }

    /// <inheritdoc cref="Delete" />
    public bool Delete(DeleteModal model)
    {
        var deleteCommand = new DeleteNewsGithubCommand(model.Models);
        _bus.SendCommand(deleteCommand);
        model.ResultCommand = deleteCommand.ResultCommand;
        return model.ResultCommand;
    }

    /// <inheritdoc cref="GetById" />
    public NewsGithubEventModel GetById(Guid id)
    {
        var iReturn = _newsGithubRepository.GetAsync(id).Result;
        if (iReturn == null) return null;

        return new NewsGithubEventModel
        {
            Id          = iReturn.Id,
            Name        = iReturn.Name,
            Code        = iReturn.Code,
            Description = iReturn.Description
        };
    }

    /// <inheritdoc cref="Update" />
    public bool Update(NewsGithubEventModel model)
    {
        var addCommand = _mapper.Map<UpdateNewsGithubCommand>(model);
        _bus.SendCommand(addCommand);
        model.ResultCommand = addCommand.ResultCommand;
        return model.ResultCommand;
    }

    /// <inheritdoc cref="GetCombobox" />
    public Task<IEnumerable<ComboboxModal>> GetCombobox(string vSearch)
    {
        return _newsGithubQueries.GetCombobox(vSearch, _user.ProjectId);
    }

    /// <inheritdoc cref="GetPaging" />
    public async Task<IEnumerable<NewsGithubPagingDto>> GetPaging(PagingModel model)
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
            var lData = (List<NewsGithubPagingDto>)_newsGithubQueries.GetPaging(model).Result;
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

    #region Fields

    private readonly IMediatorHandler         _bus;
    private readonly INewsGithubQueries       _newsGithubQueries;
    private readonly INewsGithubRepository    _newsGithubRepository;
    private readonly IAuthorityManagerQueries _authorityManagerQueries;
    private readonly IUser                    _user;
    private readonly IMapper                  _mapper;

    #endregion
}