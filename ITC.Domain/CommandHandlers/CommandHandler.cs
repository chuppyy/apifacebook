#region

using System.Linq;
using ITC.Domain.Core.Bus;
using ITC.Domain.Core.Commands;
using ITC.Domain.Core.Notifications;
using ITC.Domain.Interfaces;
using MediatR;

#endregion

namespace ITC.Domain.CommandHandlers;

public class CommandHandler
{
#region Constructors

    public CommandHandler(IUnitOfWork                              uow, IMediatorHandler bus,
                          INotificationHandler<DomainNotification> notifications)
    {
        _uow           = uow;
        _notifications = (DomainNotificationHandler)notifications;
        _bus           = bus;
    }

#endregion

#region Fields

    protected readonly IMediatorHandler          _bus;
    private readonly   DomainNotificationHandler _notifications;
    private readonly   IUnitOfWork               _uow;

#endregion

#region Methods

    public bool Commit()
    {
        if (_notifications.HasNotifications()) return false;
        if (_uow.Commit()) return true;

        _bus.RaiseEvent(new DomainNotification("Commit", "We had a problem during saving your data."));
        return false;
    }

    protected void NotifyValidationErrors(Command message)
    {
        var errors = message?.ValidationResult?.Errors;
        if (errors == null || !errors.Any()) return;
        foreach (var error in message.ValidationResult.Errors)
            _bus.RaiseEvent(new DomainNotification(message.MessageType, error.ErrorMessage));
    }

    protected void NotifyValidationErrors(string error)
    {
        _bus.RaiseEvent(new DomainNotification("Error", error));
    }

#endregion
}