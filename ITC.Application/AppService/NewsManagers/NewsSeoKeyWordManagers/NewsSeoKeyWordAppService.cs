#region

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ITC.Domain.Commands.NewsManagers.NewsSeoKeyWordManagers;
using ITC.Domain.Core.Bus;
using ITC.Domain.Core.ModelShare.NewsManagers.NewsSeoKeyWordManagers;
using ITC.Domain.Interfaces;
using ITC.Domain.Interfaces.AuthorityManager.AuthorityManagerSystem;
using ITC.Domain.Interfaces.NewsManagers.NewsSeoKeyWordManagers;
using NCore.Actions;
using NCore.Modals;

#endregion

namespace ITC.Application.AppService.NewsManagers.NewsSeoKeyWordManagers;

/// <summary>
///     Class service từ khóa SEO
/// </summary>
public class NewsSeoKeyWordAppService : INewsSeoKeyWordAppService
{
#region Constructors

    /// <summary>
    ///     Hàm dựng có tham số
    /// </summary>
    /// <param name="mapper"></param>
    /// <param name="bus"></param>
    /// <param name="newsSeoKeyWordQueries"></param>
    /// <param name="newsSeoKeyWordRepository"></param>
    /// <param name="authorityManagerQueries"></param>
    /// <param name="user"></param>
    public NewsSeoKeyWordAppService(IMapper                   mapper,
                                    IMediatorHandler          bus,
                                    INewsSeoKeyWordQueries    newsSeoKeyWordQueries,
                                    INewsSeoKeyWordRepository newsSeoKeyWordRepository,
                                    IAuthorityManagerQueries  authorityManagerQueries,
                                    IUser                     user)
    {
        _mapper                   = mapper;
        _bus                      = bus;
        _newsSeoKeyWordQueries    = newsSeoKeyWordQueries;
        _newsSeoKeyWordRepository = newsSeoKeyWordRepository;
        _authorityManagerQueries  = authorityManagerQueries;
        _user                     = user;
    }

#endregion

    /// <inheritdoc cref="Add" />
    public bool Add(NewsSeoKeyWordEventModel model)
    {
        var addCommand = _mapper.Map<AddNewsSeoKeyWordCommand>(model);
        _bus.SendCommand(addCommand);
        model.Id            = addCommand.Id;
        model.ResultCommand = addCommand.ResultCommand;
        return model.ResultCommand;
    }

    /// <inheritdoc cref="Delete" />
    public bool Delete(DeleteModal model)
    {
        var deleteCommand = new DeleteNewsSeoKeyWordCommand(model.Models);
        _bus.SendCommand(deleteCommand);
        model.ResultCommand = deleteCommand.ResultCommand;
        return model.ResultCommand;
    }

    /// <inheritdoc cref="GetById" />
    public NewsSeoKeyWordEventModel GetById(Guid id)
    {
        var iReturn = _newsSeoKeyWordRepository.GetAsync(id).Result;
        if (iReturn == null) return null;

        return new NewsSeoKeyWordEventModel
        {
            Id          = iReturn.Id,
            Name        = iReturn.Name,
            CreatedBy   = iReturn.CreatedBy,
            Description = iReturn.Description
        };
    }

    /// <inheritdoc cref="Update" />
    public bool Update(NewsSeoKeyWordEventModel model)
    {
        var addCommand = _mapper.Map<UpdateNewsSeoKeyWordCommand>(model);
        _bus.SendCommand(addCommand);
        model.ResultCommand = addCommand.ResultCommand;
        return model.ResultCommand;
    }

    /// <param name="vSearch"></param>
    /// <inheritdoc cref="GetCombobox" />
    public Task<IEnumerable<ComboboxModal>> GetCombobox(string vSearch)
    {
        return _newsSeoKeyWordQueries.GetCombobox(vSearch, _user.ProjectId);
    }

    /// <inheritdoc cref="GetPaging" />
    public async Task<IEnumerable<NewsSeoKeyWordPagingDto>> GetPaging(PagingModel model)
    {
        return await Task.Run(() =>
        {
            // Lấy dữ liệu quyền của người dùng
            var iPermission = model.IsModal
                                  ? 0
                                  : _authorityManagerQueries
                                    .GetPermissionByMenuManagerValue(model.ModuleIdentity, _user.UserId)
                                    .Result;
            // Xử lý dữ liệu
            var lData = (List<NewsSeoKeyWordPagingDto>)_newsSeoKeyWordQueries.GetPaging(model, _user.ProjectId).Result;
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

    private readonly IMediatorHandler          _bus;
    private readonly INewsSeoKeyWordQueries    _newsSeoKeyWordQueries;
    private readonly INewsSeoKeyWordRepository _newsSeoKeyWordRepository;
    private readonly IAuthorityManagerQueries  _authorityManagerQueries;
    private readonly IUser                     _user;
    private readonly IMapper                   _mapper;

#endregion
}