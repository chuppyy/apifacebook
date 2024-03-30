#region

using System.Collections.Generic;
using System.Threading.Tasks;
using ITC.Domain.Core.Pagination;
using ITC.Infra.CrossCutting.Identity.Models;
using ITC.Infra.CrossCutting.Identity.Models.QueryModel;

#endregion

namespace ITC.Infra.CrossCutting.Identity.Queries;

public interface IAccountQueries
{
#region Methods

    // Task<int> CountAccountAsync(AccountQueryModel urlQuery);

    Task<int>              CountPersonnelAccountAsync(UrlQuery urlQuery, string managementUnitId);
    Task<AccountInfoModel> GetAccountInfoAsync(string          userId);

    // Task<List<AccountModel>> GetAccountsAsync(AccountQueryModel urlQuery);
    Task<ApplicationUser> GetByOfficalProfile(string  code, string managementId);
    Task<ApplicationUser> GetManagementByPortalId(int portalId);

    // Task<List<PersonalAccountModel>> GetPersonnelAccountsAsync(UrlQuery urlQuery, string managementUnitId);

    Task<string>                GetUnitIdByUserName(string userName);
    Task<ApplicationUser>       GetUnitUser(string         userId);
    Task<List<ApplicationUser>> GetUsersAsync(List<string> users);

    Task<List<AccountModel>> GetUsersAsyncByPortalId(int portalId);

#endregion
}