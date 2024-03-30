#region

using System.Collections.Generic;
using ITC.Domain.Commands.ManageRole;
using ITC.Infra.CrossCutting.Identity.Authorization;

#endregion

namespace ITC.Infra.CrossCutting.Identity.Commands.Module;

public abstract class ModuleCommand : BaseIdentityCommand
{
#region Properties

    public string Description { get; protected set; }
    public string GroupId     { get; set; }

    public string          Identity { get; protected set; }
    public string          Name     { get; protected set; }
    public List<string>    RoleIds  { get; protected set; }
    public List<TypeAudit> Weights  { get; protected set; }

#endregion
}