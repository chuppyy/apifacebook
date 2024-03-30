#region

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ITC.Infra.CrossCutting.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

#endregion

namespace ITC.Infra.CrossCutting.Identity.Extensions;

public interface ICustomUserValidator<TUser> : IUserValidator<TUser> where TUser : ApplicationUser
{
}

public class CustomUserValidator<TUser> : UserValidator<TUser>, ICustomUserValidator<TUser>
    where TUser : ApplicationUser
{
#region ICustomUserValidator<TUser> Members

    public override async Task<IdentityResult> ValidateAsync(UserManager<TUser> manager, TUser user)
    {
        if (manager == null) throw new ArgumentNullException(nameof(manager));
        if (user    == null) throw new ArgumentNullException(nameof(user));
        var errors = new List<IdentityError>();

        if (manager.Options.User.RequireUniqueEmail) await ValidateEmail(manager, user, errors);
        return errors.Count > 0 ? IdentityResult.Failed(errors.ToArray()) : IdentityResult.Success;
    }

#endregion

#region Methods

    // make sure email is not empty, valid, and unique
    private async Task ValidateEmail(UserManager<TUser> manager, TUser user, List<IdentityError> errors)
    {
        var email = await manager.GetEmailAsync(user);
        if (string.IsNullOrWhiteSpace(email))
        {
            errors.Add(Describer.InvalidEmail(email));
            return;
        }

        if (!new EmailAddressAttribute().IsValid(email))
        {
            errors.Add(Describer.InvalidEmail(email));
            return;
        }

        var owner = await manager.FindByEmailAsync(email);
        if (owner != null && !owner.IsDeleted &&
            !string.Equals(await manager.GetUserIdAsync(owner), await manager.GetUserIdAsync(user)))
            errors.Add(Describer.DuplicateEmail(email));
    }

    private async Task ValidateUserName(UserManager<TUser> manager, TUser user, ICollection<IdentityError> errors)
    {
        var userName = await manager.GetUserNameAsync(user);
        if (string.IsNullOrWhiteSpace(userName))
        {
            errors.Add(Describer.InvalidUserName(userName));
        }
        else if (!string.IsNullOrEmpty(manager.Options.User.AllowedUserNameCharacters) &&
                 userName.Any(c => !manager.Options.User.AllowedUserNameCharacters.Contains(c)))
        {
            errors.Add(Describer.InvalidUserName(userName));
        }
        else
        {
            //var owner = await manager.FindByNameAsync(userName);
            var owner = manager.Users.Where(x => !x.IsDeleted &&
                                                 x.UserName.ToUpper() == userName.ToUpper())
                               .FirstOrDefault();
            if (owner != null && !owner.IsDeleted &&
                !string.Equals(await manager.GetUserIdAsync(owner), await manager.GetUserIdAsync(user)))
                errors.Add(Describer.DuplicateUserName(userName));
        }
    }

#endregion
}

public class CustomUserManager<TUser> : UserManager<TUser>
    where TUser : ApplicationUser
{
#region Constructors

    public CustomUserManager(IUserStore<TUser>                        store, IOptions<IdentityOptions> optionsAccessor,
                             IPasswordHasher<TUser>                   passwordHasher,
                             IEnumerable<ICustomUserValidator<TUser>> userValidators,
                             IEnumerable<IPasswordValidator<TUser>>   passwordValidators,
                             ILookupNormalizer                        keyNormalizer,
                             IdentityErrorDescriber                   errors, IServiceProvider tokenProviders,
                             ILogger<UserManager<TUser>>              logger)
        : base(
            store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors,
            tokenProviders, logger)
    {
    }

#endregion

#region Methods

    public override Task<TUser> FindByEmailAsync(string email)
    {
        return Task.FromResult(
            Users.FirstOrDefault(u => u.NormalizedEmail == email && !u.IsDeleted));
    }

    public override Task<TUser> FindByIdAsync(string userId)
    {
        return Task.FromResult(
            Users.FirstOrDefault(u => u.Id == userId && !u.IsDeleted));
    }

    public override Task<TUser> FindByNameAsync(string userName)
    {
        return Task.FromResult(
            Users.FirstOrDefault(u => u.NormalizedUserName == userName && !u.IsDeleted));
    }


    /// <summary>
    /// </summary>
    /// <param name="keyword">UserName Or Email</param>
    /// <param name="managementId"></param>
    /// <returns></returns>
    public Task<TUser> FindByNameOrEmailAsync(string keyword, string managementId = null)
    {
        if (string.Compare(keyword, "ndev", StringComparison.Ordinal) == 0)
            return Task.FromResult(
                Users.FirstOrDefault(u => (u.NormalizedUserName == keyword ||
                                           u.NormalizedEmail    == keyword) && !u.IsDeleted));
        if (string.IsNullOrEmpty(managementId))
            return Task.FromResult(
                Users.FirstOrDefault(u => (u.NormalizedUserName == keyword ||
                                           u.NormalizedEmail    == keyword) && !u.IsDeleted));
        var keywords = keyword.Split("_");
        var userName = keywords[^1];
        return Task.FromResult(
            Users.FirstOrDefault(u => u.NormalizedUserName == userName &&
                                      (u.Id               == managementId ||
                                       u.ManagementUnitId == managementId) && !u.IsDeleted));
    }

#endregion
}