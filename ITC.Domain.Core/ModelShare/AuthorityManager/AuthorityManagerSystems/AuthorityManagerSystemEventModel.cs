using System;
using System.Collections.Generic;
using NCore.Modals;

namespace ITC.Domain.Core.ModelShare.AuthorityManager.AuthorityManagerSystems;

public class AuthorityManagerSystemEventModel : PublishModal
{
    public Guid                             Id                      { get; set; }
    public string                           Name                    { get; set; }
    public string                           Description             { get; set; }
    public Guid                             ProjectId               { get; set; }
    public List<MenuByAuthoritiesSaveModel> MenuRoleEventViewModels { get; set; }
}