#region

using System;
using System.Collections.Generic;
using ITC.Domain.Core.Commands;
using ITC.Domain.Core.ModelShare.AuthorityManager;

#endregion

namespace ITC.Domain.Commands.AuthorityManager.AuthorityManagerSystem;

/// <summary>
///     Command AuthorityManagerSystemCommand
/// </summary>
public abstract class AuthorityManagerSystemCommand : Command
{
    public Guid                             Id                      { get; set; }
    public string                           Name                    { get; set; }
    public string                           Description             { get; set; }
    public List<MenuByAuthoritiesSaveModel> MenuRoleEventViewModels { get; set; }
}