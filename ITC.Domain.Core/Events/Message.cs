#region

using System;
using MediatR;

#endregion

namespace ITC.Domain.Core.Events;

public abstract class Message : IRequest<bool>
{
#region Constructors

    protected Message()
    {
        MessageType = GetType().Name;
    }

#endregion

#region Properties

    public Guid   AggregateId { get; protected set; }
    public string MessageType { get; set; }

#endregion
}