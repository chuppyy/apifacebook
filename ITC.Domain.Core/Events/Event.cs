#region

using System;
using MediatR;

#endregion

namespace ITC.Domain.Core.Events;

public abstract class Event : Message, INotification
{
#region Constructors

    protected Event()
    {
        Timestamp = DateTime.Now;
    }

#endregion

#region Properties

    public DateTime Timestamp { get; set; }

#endregion
}