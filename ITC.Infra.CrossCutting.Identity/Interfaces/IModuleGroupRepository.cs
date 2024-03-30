#region

using System.Collections.Generic;
using System.Threading.Tasks;
using ITC.Infra.CrossCutting.Identity.Models;

#endregion

namespace ITC.Infra.CrossCutting.Identity.Interfaces;

public interface IModuleGroupRepository : IRepository<ModuleGroup>
{
#region Methods

    Task<List<ModuleGroup>> GetAllAsync();
    Task<ModuleGroup>       GetByIdAsync(string id);

#endregion
}