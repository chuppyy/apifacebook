#region

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ITC.Domain.Commands.NewsManagers.NewsVia;
using ITC.Domain.Core.Bus;
using ITC.Domain.Core.ModelShare.StudyManagers.NewsVia;
using ITC.Domain.Interfaces;
using ITC.Domain.Interfaces.AuthorityManager.AuthorityManagerSystem;
using ITC.Domain.Interfaces.NewsManagers.NewsViaManagers;
using NCore.Actions;
using NCore.Modals;

#endregion

namespace ITC.Application.AppService.NewsManagers.NewsViaManagers;

/// <summary>
///     Class service loại nhóm tin
/// </summary>
public class NewsViaAppService : INewsViaAppService
{
#region Constructors

    /// <summary>
    ///     Hàm dựng có tham số
    /// </summary>
    /// <param name="mapper"></param>
    /// <param name="bus"></param>
    /// <param name="newsViaQueries"></param>
    /// <param name="newsViaRepository"></param>
    /// <param name="authorityManagerQueries"></param>
    /// <param name="user"></param>
    public NewsViaAppService(IMapper                  mapper,
                             IMediatorHandler         bus,
                             INewsViaQueries          newsViaQueries,
                             INewsViaRepository       newsViaRepository,
                             IAuthorityManagerQueries authorityManagerQueries,
                             IUser                    user)
    {
        _mapper                  = mapper;
        _bus                     = bus;
        _newsViaQueries          = newsViaQueries;
        _newsViaRepository       = newsViaRepository;
        _authorityManagerQueries = authorityManagerQueries;
        _user                    = user;
    }

#endregion

    /// <inheritdoc cref="Add" />
    public bool Add(NewsViaEventModel model)
    {
        var addCommand = _mapper.Map<AddNewsViaCommand>(model);
        _bus.SendCommand(addCommand);
        model.Id            = addCommand.Id;
        model.ResultCommand = addCommand.ResultCommand;
        return model.ResultCommand;
    }

    /// <inheritdoc cref="Delete" />
    public bool Delete(DeleteModal model)
    {
        var deleteCommand = new DeleteNewsViaCommand(model.Models);
        _bus.SendCommand(deleteCommand);
        model.ResultCommand = deleteCommand.ResultCommand;
        return model.ResultCommand;
    }

    /// <inheritdoc cref="GetById" />
    public NewsViaEventModel GetById(Guid id)
    {
        var iReturn = _newsViaRepository.GetAsync(id).Result;
        if (iReturn == null) return null;

        return new NewsViaEventModel
        {
            Id      = iReturn.Id,
            Code    = iReturn.Code,
            Content = iReturn.Content,
            Token   = iReturn.Token,
            StaffId = iReturn.StaffId,
            IdTkQc  = iReturn.IdTkQc
        };
    }

    /// <inheritdoc cref="GetByIdQc"/>
    public NewsViaEventModel GetByIdQc(string idQc)
    {
        var iReturn = _newsViaRepository.GetByIdQc(idQc).Result;
        if (iReturn == null) return null;

        return new NewsViaEventModel
        {
            Id      = iReturn.Id,
            Code    = iReturn.Code,
            Content = iReturn.Content,
            Token   = iReturn.Token,
            StaffId = iReturn.StaffId,
            IdTkQc  = iReturn.IdTkQc
        };
    }

    /// <inheritdoc cref="Update" />
    public bool Update(NewsViaEventModel model)
    {
        var addCommand = _mapper.Map<UpdateNewsViaCommand>(model);
        _bus.SendCommand(addCommand);
        model.ResultCommand = addCommand.ResultCommand;
        return model.ResultCommand;
    }

    /// <inheritdoc cref="GetCombobox" />
    public Task<IEnumerable<ComboboxModal>> GetCombobox(string vSearch)
    {
        return _newsViaQueries.GetCombobox(vSearch, Guid.Empty);
    }

    /// <inheritdoc cref="GetPaging" />
    public async Task<IEnumerable<NewsViaPagingDto>> GetPaging(PagingModel model)
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
            var lData = (List<NewsViaPagingDto>)_newsViaQueries.GetPaging(model).Result;
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
    private readonly INewsViaQueries          _newsViaQueries;
    private readonly INewsViaRepository       _newsViaRepository;
    private readonly IAuthorityManagerQueries _authorityManagerQueries;
    private readonly IUser                    _user;
    private readonly IMapper                  _mapper;

#endregion
}