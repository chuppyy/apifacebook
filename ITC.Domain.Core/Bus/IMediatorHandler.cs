#region

using System.Threading.Tasks;
using ITC.Domain.Core.Commands;
using ITC.Domain.Core.Events;

#endregion

namespace ITC.Domain.Core.Bus;

public interface IMediatorHandler
{
#region Methods

    Task RaiseEvent<T>(T          @event) where T : Event;
    Task SendCommand<T>(T         command) where T : Command;
    Task RaiseEventSystemLog<T>(T @event) where T : Event;

#endregion
}