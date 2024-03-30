#region

using System.Threading.Tasks;
using ITC.Infra.CrossCutting.Identity.Models;
using Microsoft.AspNetCore.Identity;

#endregion

namespace ITC.Infra.CrossCutting.Identity.Interfaces;

public interface IAccountRepository : IRepository<ApplicationUser>
{
#region Methods

    Task<IdentityResult>  AddAccountAsync(ApplicationUser     obj, string password);
    Task<IdentityResult>  ChangePasswordAsync(ApplicationUser obj, string currentPassword, string newPassword);
    Task<IdentityResult>  DeleteAccountAsync(ApplicationUser  obj);
    Task<ApplicationUser> GetCurrentUserAsync();

    Task<ApplicationUser> GetUserByEmailAsync(string              email);
    Task<ApplicationUser> GetUserByUserNameAsync(string           email);
    Task<ApplicationUser> GetUserByEmailAndManagementAsync(string email, string managementId);

    //Task<ApplicationUser> GetUserAsync(string id);
    Task<ApplicationUser> GetUserByIdAsync(string                    id);
    Task<ApplicationUser> GetUserByOfficalProfileAsync(string        id, string managementId);
    Task<ApplicationUser> GetUserRecoverByOfficalProfileAsync(string id, string managementId);
    bool                  IsExistAccountByEmail(string               email);
    bool                  IsExistAccountByEmail(string               id, string email);

    bool                 IsExistAccountByUserName(string userName);
    bool                 IsExistAccountByUserName(string id, string userName);
    bool                 IsExistAccountByUserName(string id, string userName, string managementUnitId);
    bool                 IsExistAccountByUserNameAndManagementUnitId(string userName, string managementUnitId);
    Task<IdentityResult> RemoveAccountAsync(ApplicationUser obj);
    Task<IdentityResult> UpdateAccountAsync(ApplicationUser obj);

#endregion
}