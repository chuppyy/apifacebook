#region

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ITC.Domain.Commands.SystemManagers.HelperManagers;
using ITC.Domain.Core.Bus;
using ITC.Domain.Core.ModelShare.SystemManagers.HelperManagers;
using NCore.Enums;
using NCore.Modals;

#endregion

namespace ITC.Application.AppService.SystemManagers.HelperManagers;

/// <summary>
///     Class service helper
/// </summary>
public class HelperAppService : IHelperAppService
{
#region Constructors

    /// <summary>
    ///     Hàm dựng có tham số
    /// </summary>
    /// <param name="mapper"></param>
    /// <param name="bus"></param>
    public HelperAppService(IMapper          mapper,
                            IMediatorHandler bus)
    {
        _mapper = mapper;
        _bus    = bus;
    }

#endregion

    /// <inheritdoc cref="GetAttackViewCombobox" />
    public async Task<IEnumerable<ComboboxModalInt>> GetAttackViewCombobox()
    {
        return await Task.Run(() =>
        {
            var lData = (List<NewsAttackEnumeration>)NewsAttackEnumeration.GetList();
            return lData.Select(items => new ComboboxModalInt { Id = items.Id, Name = items.Name }).ToList();
        });
    }

    /// <inheritdoc cref="UpdateStatus" />
    public async Task<bool> UpdateStatus(UpdateStatusHelperModal model)
    {
        var updateCommand = _mapper.Map<UpdateStatusHelperCommand>(model);
        await _bus.SendCommand(updateCommand);
        model.ResultCommand = updateCommand.ResultCommand;
        return model.ResultCommand;
    }

    public async Task<bool> CheckTime(CheckTimeModel model)
    {
        var updateCommand = new CheckTimeHelperCommand(model);
        await _bus.SendCommand(updateCommand);
        return updateCommand.ResultCommand;
    }

#region Fields

    private readonly IMediatorHandler _bus;
    private readonly IMapper          _mapper;

#endregion
}