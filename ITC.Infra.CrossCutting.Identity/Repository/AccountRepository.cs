#region

using System.Linq;
using System.Threading.Tasks;
using ITC.Domain.Interfaces;
using ITC.Infra.CrossCutting.Identity.Interfaces;
using ITC.Infra.CrossCutting.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

#endregion

namespace ITC.Infra.CrossCutting.Identity.Repository;

public class AccountRepository : Repository<ApplicationUser>, IAccountRepository
{
#region Constructors

    public AccountRepository(IUser user, UserManager<ApplicationUser> userManager, ApplicationDbContext context) :
        base(context)
    {
        _user        = user;
        _userManager = userManager;
    }

#endregion

#region Fields

    private readonly IUser                        _user;
    private readonly UserManager<ApplicationUser> _userManager;

#endregion

#region IAccountRepository Members

    public async Task<ApplicationUser> GetCurrentUserAsync()
    {
        return await _userManager.FindByIdAsync(_user.UserId);
    }

    public override ApplicationUser GetById(string id)
    {
        return Db.Users.FirstOrDefault(x => x.Id == id && !x.IsDeleted);
    }

    public async Task<ApplicationUser> GetUserByEmailAsync(string email)
    {
        return await Db.Users.FirstOrDefaultAsync(x => x.Email == email && !x.IsDeleted);
    }

    public async Task<ApplicationUser> GetUserByUserNameAsync(string email)
    {
        return await Db.Users.FirstOrDefaultAsync(x => x.UserName == email && !x.IsDeleted);
    }

    public async Task<ApplicationUser> GetUserByIdAsync(string id)
    {
        return await Db.Users.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
    }

    public Task<IdentityResult> AddAccountAsync(ApplicationUser obj, string password)
    {
        return _userManager.CreateAsync(obj, password);
    }

    public Task<IdentityResult> UpdateAccountAsync(ApplicationUser obj)
    {
        return _userManager.UpdateAsync(obj);
    }

    public Task<IdentityResult> RemoveAccountAsync(ApplicationUser obj)
    {
        return _userManager.UpdateAsync(obj);
    }

    public Task<IdentityResult> ChangePasswordAsync(ApplicationUser obj, string currentPassword, string newPassword)
    {
        return _userManager.ChangePasswordAsync(obj, currentPassword, newPassword);
    }


    public Task<IdentityResult> DeleteAccountAsync(ApplicationUser obj)
    {
        return _userManager.DeleteAsync(obj);
    }


    public bool IsExistAccountByUserName(string userName)
    {
        return DbSet.Any(x => x.UserName == userName && !x.IsDeleted);
    }

    public bool IsExistAccountByUserNameAndManagementUnitId(string userName, string managementUnitId)
    {
        return DbSet.Any(x => x.UserName == userName && x.ManagementUnitId == managementUnitId && !x.IsDeleted);
    }


    public bool IsExistAccountByUserName(string id, string userName)
    {
        return DbSet.Any(x => x.Id != id && x.UserName.ToLower().Equals(userName.ToLower()) && !x.IsDeleted);
    }

    public bool IsExistAccountByUserName(string id, string userName, string managementUnitId)
    {
        return DbSet.Any(x => x.Id               != id && x.UserName.ToLower().Equals(userName.ToLower()) &&
                              x.ManagementUnitId == managementUnitId && !x.IsDeleted);
    }

    public bool IsExistAccountByEmail(string email)
    {
        return DbSet.Any(x => x.Email == email && !x.IsDeleted);
    }

    public bool IsExistAccountByEmail(string id, string email)
    {
        return DbSet.Any(x => x.Id != id && x.Email == email && !x.IsDeleted);
    }

    public async Task<ApplicationUser> GetUserByOfficalProfileAsync(string id, string managementId)
    {
        return await DbSet.FirstOrDefaultAsync(x => x.OfficalProfileId == id           &&
                                                    x.ManagementUnitId == managementId && !x.IsDeleted);
        // return await DbSet.FirstOrDefaultAsync(x => x.OfficalProfileId == id && x.ManagementUnitId == managementId && !x.IsDeleted);
    }

    public async Task<ApplicationUser> GetUserRecoverByOfficalProfileAsync(string id, string managementId)
    {
        return await DbSet.FirstOrDefaultAsync(x => x.OfficalProfileId == id           &&
                                                    x.ManagementUnitId == managementId && x.IsDeleted);
    }

#endregion

#region Methods

    public async Task<ApplicationUser> GetUserAsync(string id)
    {
        return await _userManager.FindByIdAsync(id);
    }

    public async Task<ApplicationUser> GetUserByEmailAndManagementAsync(string email, string managementId)
    {
        return await Db.Users.FirstOrDefaultAsync(x => x.Email == email && x.ManagementUnitId == managementId &&
                                                       !x.IsDeleted);
    }

#endregion
}