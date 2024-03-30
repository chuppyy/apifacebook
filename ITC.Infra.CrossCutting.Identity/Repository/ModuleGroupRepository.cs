#region

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITC.Domain.Interfaces;
using ITC.Infra.CrossCutting.Identity.Interfaces;
using ITC.Infra.CrossCutting.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

#endregion

namespace ITC.Infra.CrossCutting.Identity.Repository;

public class ModuleGroupRepository : Repository<ModuleGroup>, IModuleGroupRepository
{
#region Constructors

    public ModuleGroupRepository(IUser user, UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        : base(context)
    {
        _user        = user;
        _userManager = userManager;
    }

#endregion

#region Fields

    private readonly IUser                        _user;
    private readonly UserManager<ApplicationUser> _userManager;

#endregion

#region IModuleGroupRepository Members

    public async Task<List<ModuleGroup>> GetAllAsync()
    {
        return await DbSet.Where(x => !x.IsDeleted).ToListAsync();
    }

    public async Task<ModuleGroup> GetByIdAsync(string id)
    {
        return await DbSet.FirstOrDefaultAsync(x => !x.IsDeleted && x.Id == id);
    }

#endregion
}