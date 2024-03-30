#region

using System;
using ITC.Domain.Core.Commands;

#endregion

namespace ITC.Domain.Commands.ManageRole;

public abstract class BaseIdentityCommand : Command
{
#region Properties

    public string Id { get; private set; }

#endregion

#region Methods

    public void SetId(string id)
    {
        Id = id;
        Guid idParse;
        Guid.TryParse(id, out idParse);
        AggregateId = idParse;
    }

#endregion
}