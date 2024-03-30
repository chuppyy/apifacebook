#region

using ITC.Domain.Core.Bus;
using ITC.Domain.Core.Commands;
using ITC.Domain.Core.Notifications;
using ITC.Domain.Interfaces;
using ITC.Infra.CrossCutting.Identity.UoW;
using MediatR;
using Microsoft.AspNetCore.Identity;

#endregion

namespace ITC.Infra.CrossCutting.Identity.CommandHandlers;

public class CommandIdentityHandler
{
#region Constructors

    public CommandIdentityHandler(IUnitOfWorkIdentity                      uow, IMediatorHandler bus,
                                  INotificationHandler<DomainNotification> notifications)
    {
        _uow           = uow;
        _notifications = (DomainNotificationHandler)notifications;
        _bus           = bus;
    }

#endregion

#region Fields

    private readonly IMediatorHandler          _bus;
    private readonly DomainNotificationHandler _notifications;
    private readonly IUnitOfWorkIdentity       _uow;

#endregion

#region Methods

    public bool Commit()
    {
        if (_notifications.HasNotifications()) return false;
        if (_uow.Commit()) return true;

        _bus.RaiseEvent(new DomainNotification("Commit", "We had a problem during saving your data."));
        return false;
    }

    public bool Commit(IdentityResult identityResult)
    {
        if (_notifications.HasNotifications()) return false;
        if (identityResult.Succeeded) return true;

        _bus.RaiseEvent(new DomainNotification("Commit", "We had a problem during saving your data."));
        return false;
    }

    public bool Commit(IUnitOfWork unitOfWork)
    {
        if (_notifications.HasNotifications()) return false;
        if (unitOfWork.Commit()) return true;

        _bus.RaiseEvent(new DomainNotification("Commit", "We had a problem during saving your data."));
        return false;
    }

    public bool CustomCommit(IdentityResult identityResult)
    {
        if (identityResult.Succeeded) return true;

        _bus.RaiseEvent(new DomainNotification("Commit", "We had a problem during saving your data."));
        return false;
    }

    public bool CustomCommit(IUnitOfWork unitOfWork)
    {
        if (unitOfWork.Commit()) return true;

        _bus.RaiseEvent(new DomainNotification("Commit", "We had a problem during saving your data."));
        return false;
    }

    protected void NotifyValidationErrors(Command message)
    {
        foreach (var error in message.ValidationResult.Errors)
            _bus.RaiseEvent(new DomainNotification(message.MessageType, error.ErrorMessage));
    }

    protected void NotifyValidationErrors(string error)
    {
        _bus.RaiseEvent(new DomainNotification("Error", error));
    }

#endregion
}