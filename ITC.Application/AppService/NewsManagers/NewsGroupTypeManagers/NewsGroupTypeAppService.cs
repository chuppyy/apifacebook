#region

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ITC.Domain.Commands.NewsManagers.NewsGroupTypeManagers;
using ITC.Domain.Core.Bus;
using ITC.Domain.Core.ModelShare.NewsManagers.NewsGroupTypeManagers;
using ITC.Domain.Interfaces;
using ITC.Domain.Interfaces.AuthorityManager.AuthorityManagerSystem;
using ITC.Domain.Interfaces.NewsManagers.NewsGroupTypeManagers;
using ITC.Domain.Models.NewsManagers;
using NCore.Actions;
using NCore.Modals;

#endregion

namespace ITC.Application.AppService.NewsManagers.NewsGroupTypeManagers;

/// <summary>
///     Class service loại nhóm tin
/// </summary>
public class NewsGroupTypeAppService : INewsGroupTypeAppService
{
#region Constructors

    /// <summary>
    ///     Hàm dựng có tham số
    /// </summary>
    /// <param name="mapper"></param>
    /// <param name="bus"></param>
    /// <param name="newsGroupTypeQueries"></param>
    /// <param name="newsGroupTypeRepository"></param>
    /// <param name="authorityManagerQueries"></param>
    /// <param name="user"></param>
    public NewsGroupTypeAppService(IMapper                  mapper,
                                   IMediatorHandler         bus,
                                   INewsGroupTypeQueries    newsGroupTypeQueries,
                                   INewsGroupTypeRepository newsGroupTypeRepository,
                                   IAuthorityManagerQueries authorityManagerQueries,
                                   IUser                    user)
    {
        _mapper                  = mapper;
        _bus                     = bus;
        _newsGroupTypeQueries    = newsGroupTypeQueries;
        _newsGroupTypeRepository = newsGroupTypeRepository;
        _authorityManagerQueries = authorityManagerQueries;
        _user                    = user;
    }

#endregion

    /// <inheritdoc cref="Add" />
    public bool Add(NewsGroupTypeEventModel model)
    {
        var addCommand = _mapper.Map<AddNewsGroupTypeCommand>(model);
        _bus.SendCommand(addCommand);
        model.Id            = addCommand.Id;
        model.ResultCommand = addCommand.ResultCommand;
        return model.ResultCommand;
    }

    /// <inheritdoc cref="Delete" />
    public bool Delete(DeleteModal model)
    {
        var deleteCommand = new DeleteNewsGroupTypeCommand(model.Models);
        _bus.SendCommand(deleteCommand);
        model.ResultCommand = deleteCommand.ResultCommand;
        return model.ResultCommand;
    }

    /// <inheritdoc cref="GetById" />
    public NewsGroupTypeEventModel GetById(Guid id)
    {
        var iReturn = _newsGroupTypeRepository.GetAsync(id).Result;
        if (iReturn == null) return null;

        return new NewsGroupTypeEventModel
        {
            Id          = iReturn.Id,
            Name        = iReturn.Name,
            CreatedBy   = iReturn.CreatedBy,
            Description = iReturn.Description
        };
    }

    /// <inheritdoc cref="Update" />
    public bool Update(NewsGroupTypeEventModel model)
    {
        var addCommand = _mapper.Map<UpdateNewsGroupTypeCommand>(model);
        _bus.SendCommand(addCommand);
        model.ResultCommand = addCommand.ResultCommand;
        return model.ResultCommand;
    }

    /// <inheritdoc cref="GetCombobox" />
    public Task<IEnumerable<ComboboxModal>> GetCombobox(string vSearch)
    {
        return _newsGroupTypeQueries.GetCombobox(vSearch, _user.ProjectId);
    }

    /// <inheritdoc cref="GetPaging" />
    public async Task<IEnumerable<NewsGroupTypePagingDto>> GetPaging(PagingModel model)
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
            var lData = (List<NewsGroupTypePagingDto>)_newsGroupTypeQueries.GetPaging(model).Result;
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

    /// <inheritdoc cref="GetBySecrect" />
    public async Task<NewsGroupType> GetBySecrect(Guid projectId, string secrectKey)
    {
        return await _newsGroupTypeRepository.GetBySecrect(projectId, secrectKey);
    }

#region Fields

    private readonly IMediatorHandler         _bus;
    private readonly INewsGroupTypeQueries    _newsGroupTypeQueries;
    private readonly INewsGroupTypeRepository _newsGroupTypeRepository;
    private readonly IAuthorityManagerQueries _authorityManagerQueries;
    private readonly IUser                    _user;
    private readonly IMapper                  _mapper;

#endregion
}