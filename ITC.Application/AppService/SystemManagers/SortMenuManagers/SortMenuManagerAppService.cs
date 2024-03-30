#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ITC.Domain.Commands.AuthorityManager.MenuManagerSystem;
using ITC.Domain.Core.Bus;
using ITC.Domain.Core.ModelShare.SystemManagers.SortMenuManagers;
using ITC.Domain.Interfaces;
using ITC.Domain.Interfaces.AuthorityManager.ProjectMenuManagerSystem;
using NCore.Modals;

#endregion

namespace ITC.Application.AppService.SystemManagers.SortMenuManagers;

/// <summary>
///     Class service sắp xếp menu
/// </summary>
public class SortMenuManagerAppService : ISortMenuManagerAppService
{
#region Constructors

    /// <summary>
    ///     Hàm dựng có tham số
    /// </summary>
    /// <param name="mapper"></param>
    /// <param name="bus"></param>
    /// <param name="menuManagerQueries"></param>
    /// <param name="user"></param>
    public SortMenuManagerAppService(IMapper             mapper,
                                     IMediatorHandler    bus,
                                     IMenuManagerQueries menuManagerQueries,
                                     IUser               user)
    {
        _mapper             = mapper;
        _bus                = bus;
        _menuManagerQueries = menuManagerQueries;
        _user               = user;
    }

#endregion

    /// <inheritdoc cref="Update" />
    public async Task<bool> Update(SortMenuManagerEventModel model)
    {
        var addCommand = _mapper.Map<SortMenuManagerCommand>(model);
        await _bus.SendCommand(addCommand);
        model.ResultCommand = addCommand.ResultCommand;
        return model.ResultCommand;
    }

    /// <inheritdoc cref="Delete" />
    public async Task<bool> Delete(DeleteModal model)
    {
        var deleteCommand = new DeleteSortMenuManagerCommand(model.Models);
        await _bus.SendCommand(deleteCommand);
        model.ResultCommand = deleteCommand.ResultCommand;
        return model.ResultCommand;
    }

    /// <inheritdoc cref="GetSortMenu" />
    public async Task<IEnumerable<SortMenuManagerDto>> GetSortMenu(Guid menuId, Guid parentId)
    {
        return await Task.Run(() =>
        {
            var iCount = 1;
            var lData = _menuManagerQueries.GetSortMenu(menuId, parentId, _user.ProjectId).Result.ToList();
            foreach (var items in lData) items.Position = iCount++;
            return lData;
        });
    }

#region Fields

    private readonly IMediatorHandler    _bus;
    private readonly IMenuManagerQueries _menuManagerQueries;
    private readonly IUser               _user;
    private readonly IMapper             _mapper;

#endregion
}