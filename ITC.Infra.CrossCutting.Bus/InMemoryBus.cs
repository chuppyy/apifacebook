#region

using System.Diagnostics;
using System.Threading.Tasks;
using ITC.Domain.Core.Bus;
using ITC.Domain.Core.Commands;
using ITC.Domain.Core.Events;
using ITC.Domain.Core.ModelShare.SystemManagers.SystemLogManagers;
using MediatR;

#endregion

namespace ITC.Infra.CrossCutting.Bus;

public sealed class InMemoryBus : IMediatorHandler
{
#region Constructors

    public InMemoryBus(IEventStore eventStore, IMediator mediator)
    {
        _eventStore = eventStore;
        _mediator   = mediator;
    }

#endregion

#region Fields

    private readonly IEventStore _eventStore;
    private readonly IMediator   _mediator;

#endregion

#region IMediatorHandler Members

    public Task SendCommand<T>(T command) where T : Command
    {
        return _mediator.Send(command);
    }

    public Task RaiseEventSystemLog<T>(T @event) where T : Event
    {
        // ReSharper disable once SuspiciousTypeConversion.Global
        var storedEvent = @event as SystemLogEventModel;
        if (!@event.MessageType.Equals("DomainNotification") && storedEvent != null)
            _eventStore?.SaveSystemLog(storedEvent);

        return _mediator.Publish(@event);
    }

    public Task RaiseEvent<T>(T @event) where T : Event
    {
        Debug.WriteLine("RaiseEvent");
        if (!@event.MessageType.Equals("DomainNotification") && @event is StoredEvent storedEvent)
            _eventStore?.Save(storedEvent);

        return _mediator.Publish(@event);
    }

#endregion
}