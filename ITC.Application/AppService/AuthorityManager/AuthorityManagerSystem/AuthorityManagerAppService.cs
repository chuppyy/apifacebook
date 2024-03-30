#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ITC.Domain.Commands.AuthorityManager.AuthorityManagerSystem;
using ITC.Domain.Core.Bus;
using ITC.Domain.Core.ModelShare.AuthorityManager.AuthorityManagerSystems;
using ITC.Domain.Interfaces;
using ITC.Domain.Interfaces.AuthorityManager.AuthorityManagerSystem;
using NCore.Actions;
using NCore.Modals;

#endregion

namespace ITC.Application.AppService.AuthorityManager.AuthorityManagerSystem;

/// <summary>
///     Class service danh mục
/// </summary>
public class AuthorityManagerAppService : IAuthorityManagerAppService
{
#region Constructors

    /// <summary>
    ///     Hàm dựng có tham số
    /// </summary>
    /// <param name="mapper"></param>
    /// <param name="authorityManagerQueries"></param>
    /// <param name="authorityManagerRepository"></param>
    /// <param name="user"></param>
    /// <param name="bus"></param>
    public AuthorityManagerAppService(IMapper                     mapper,
                                      IAuthorityManagerQueries    authorityManagerQueries,
                                      IAuthorityManagerRepository authorityManagerRepository,
                                      IUser                       user,
                                      IMediatorHandler            bus)
    {
        _mapper                     = mapper;
        _authorityManagerQueries    = authorityManagerQueries;
        _authorityManagerRepository = authorityManagerRepository;
        _user                       = user;
        _bus                        = bus;
    }

#endregion

    /// <inheritdoc cref="Add" />
    public async Task<bool> Add(AuthorityManagerSystemEventModel model)
    {
        var addCommand = _mapper.Map<AddAuthorityManagerSystemCommand>(model);
        await _bus.SendCommand(addCommand);
        model.Id            = addCommand.Id;
        model.ResultCommand = addCommand.ResultCommand;
        return model.ResultCommand;
    }

    /// <inheritdoc cref="Delete" />
    public async Task<bool> Delete(DeleteModal model)
    {
        var deleteCommand = new DeleteAuthorityManagerSystemCommand(model.Models);
        await _bus.SendCommand(deleteCommand);
        model.ResultCommand = deleteCommand.ResultCommand;
        return model.ResultCommand;
    }

    /// <inheritdoc cref="GetById" />
    public AuthorityManagerSystemEventModel GetById(Guid id)
    {
        var authorityInfo = _authorityManagerRepository.GetAsync(id).Result;
        if (authorityInfo == null) return null;

        return new AuthorityManagerSystemEventModel
        {
            Id                      = authorityInfo.Id,
            Description             = authorityInfo.Description,
            Name                    = authorityInfo.Name,
            ProjectId               = authorityInfo.ProjectId,
            MenuRoleEventViewModels = _authorityManagerQueries.GetPermissionByMenuId(authorityInfo.Id).Result.ToList()
        };
    }

    /// <inheritdoc cref="Update" />
    public async Task<bool> Update(AuthorityManagerSystemEventModel model)
    {
        var addCommand = _mapper.Map<UpdateAuthorityManagerSystemCommand>(model);
        await _bus.SendCommand(addCommand);
        model.ResultCommand = addCommand.ResultCommand;
        return model.ResultCommand;
    }

    /// <inheritdoc cref="GetPaging" />
    public async Task<IEnumerable<AuthorityManagerSystemPagingDto>> GetPaging(PagingModel model)
    {
        return await Task.Run(() =>
        {
            // Lấy dữ liệu quyền của người dùng
            var iPermission =
                _authorityManagerQueries
                    .GetPermissionByMenuManagerValue(model.ModuleIdentity, _user.UserId)
                    .Result;
            // Xử lý dữ liệu
            //2. Xuất dữ liệu NewsContent theo các điều kiện đã đưa vào
            var lData =
                (List<AuthorityManagerSystemPagingDto>)_authorityManagerQueries.GetPaging(model).Result;
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

    /// <inheritdoc cref="GetCombobox" />
    public async Task<IEnumerable<ComboboxModal>> GetCombobox(string vSearch, Guid? projectId)
    {
        return await _authorityManagerQueries.GetCombobox(vSearch, projectId ?? _user.ProjectId);
    }

    /// <inheritdoc cref="UpdatePermissionMenu" />
    public async Task<bool> UpdatePermissionMenu(AuthorityManagerSystemUpdatePermissionEventModel model)
    {
        var addCommand = _mapper.Map<UpdateAuthorityManagerSystemPermissionMenuCommand>(model);
        await _bus.SendCommand(addCommand);
        model.ResultCommand = addCommand.ResultCommand;
        return model.ResultCommand;
    }

#region Fields

    private readonly IMediatorHandler            _bus;
    private readonly IMapper                     _mapper;
    private readonly IAuthorityManagerQueries    _authorityManagerQueries;
    private readonly IAuthorityManagerRepository _authorityManagerRepository;
    private readonly IUser                       _user;

#endregion
}