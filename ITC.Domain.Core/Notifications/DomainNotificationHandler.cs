#region

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

#endregion

namespace ITC.Domain.Core.Notifications;

public class DomainNotificationHandler : INotificationHandler<DomainNotification>
{
#region Fields

    private List<DomainNotification> _notifications;

#endregion

#region Constructors

    public DomainNotificationHandler()
    {
        _notifications = new List<DomainNotification>();
    }

#endregion

#region INotificationHandler<DomainNotification> Members

    public Task Handle(DomainNotification message, CancellationToken cancellationToken)
    {
        _notifications.Add(message);

        return Task.CompletedTask;
    }

#endregion

#region Methods

    public void Dispose()
    {
        _notifications = new List<DomainNotification>();
    }

    public virtual List<DomainNotification> GetNotifications()
    {
        return _notifications;
    }

    public virtual bool HasNotifications()
    {
        return GetNotifications().Any();
    }

#endregion
}