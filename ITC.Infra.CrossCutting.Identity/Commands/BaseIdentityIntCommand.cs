#region

using System;
using ITC.Domain.Core.Commands;

#endregion

namespace ITC.Infra.CrossCutting.Identity.Commands;

public abstract class BaseIdentityIntCommand : Command
{
#region Properties

    public int Id { get; }

#endregion

#region Methods

    public void SetAggregateId(string identity)
    {
        Guid idParse;
        Guid.TryParse(identity, out idParse);
        AggregateId = idParse;
    }

#endregion
}