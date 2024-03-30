#region

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using ITC.Domain.Core.ModelShare.AuthorityManager;
using ITC.Domain.Core.ModelShare.AuthorityManager.AuthorityManagerSystems;
using ITC.Domain.Interfaces.AuthorityManager.AuthorityManagerSystem;
using NCore.Helpers;
using NCore.Modals;

#endregion

namespace ITC.Infra.Data.FunctionRepositoryQueries.AuthorityManager.AuthorityManagerSystem;

/// <summary>
///     Phân quyền queries
/// </summary>
public class AuthorityManagerQueries : IAuthorityManagerQueries
{
#region Fields

    private readonly string _connectionString;

#endregion

#region Constructors

    public AuthorityManagerQueries(string connectionString)
    {
        _connectionString = connectionString;
    }

#endregion

    /// <inheritdoc cref="GetPaging" />
    public async Task<IEnumerable<AuthorityManagerSystemPagingDto>> GetPaging(PagingModel model)
    {
        var sBuilderTemp = new StringBuilder();
        sBuilderTemp.Append(@"SELECT A.Id, A.Name, A.Description, A.CreatedBy AS OwnerId, A.StatusId
                                FROM Authorities A WHERE A.IsDeleted = 0 ");
        if (model.StatusId > 0) sBuilderTemp.Append(" AND A.StatusId = @pStatusId ");

        var sBuilder = new StringBuilder();
        sBuilder.Append(SqlHelper.GeneralSqlBuilder(sBuilderTemp.ToString()));
        sBuilder.Append(SqlHelper.Search(new List<string>
        {
            "Name"
        }, model.Search));
        sBuilder.Append("ORDER BY Id ");
        sBuilder.Append(SqlHelper.Paging(model.PageNumber, model.PageSize));
        var dictionary = new Dictionary<string, object>
        {
            {
                "@pProjectId", model.ProjectId
            },
            {
                "@pStatusId", model.StatusId
            }
        };
        return await SqlHelper.RunDapperQueryAsync<AuthorityManagerSystemPagingDto>(_connectionString,
                   sBuilder,
                   new DynamicParameters(dictionary));
    }

    /// <inheritdoc cref="GetCombobox" />
    public async Task<IEnumerable<ComboboxModal>> GetCombobox(string vSearch, Guid projectId)
    {
        return await SqlHelper
                   .RunDapperQueryAsync<ComboboxModal>(_connectionString,
                                                       SqlHelper.GetComboboxProjectAsync("Authorities",
                                                           vSearch,
                                                           "",
                                                           projectId));
    }

    /// <inheritdoc cref="DeleteAsync" />
    public async Task<int> DeleteAsync(List<Guid> model)
    {
        return await SqlHelper.RunDapperExecuteAsync(_connectionString,
                                                     SqlHelper.DeleteListAsync("Authorities",
                                                                               model));
    }

    /// <inheritdoc cref="GetPermissionByMenuManagerValue" />
    public async Task<int> GetPermissionByMenuManagerValue(string authorityName, string userId)
    {
        var sBuilder = new StringBuilder();
        sBuilder.Append(@"SELECT AD.Value
                                FROM StaffManagers SM
                                         INNER JOIN AuthorityDetail AD ON AD.AuthorityId = SM.AuthorityId AND AD.IsDeleted = 0
                                         INNER JOIN MenuManager MM ON MM.Id = AD.MenuManagerId AND MM.Code = @authorityName AND MM.IsDeleted = 0
                                WHERE SM.UserId = @userId AND SM.IsDeleted = 0");
        var dictionary = new Dictionary<string, object>
        {
            {
                "@authorityName", authorityName
            },
            {
                "@userId", userId
            }
        };
        return await SqlHelper.RunDapperQueryFirstOrDefaultAsync<int>(_connectionString, sBuilder,
                                                                      new DynamicParameters(dictionary));
    }

    /// <inheritdoc cref="GetPermissionByMenuId" />
    public async Task<IEnumerable<MenuByAuthoritiesSaveModel>> GetPermissionByMenuId(Guid authorities)
    {
        var sBuilder = new StringBuilder();
        sBuilder.Append(@"SELECT AD.MenuManagerId AS Id, AD.Value
                            FROM AuthorityDetail AD
                            WHERE AD.IsDeleted = 0 AND AD.AuthorityId = @authorityId ");
        var dictionary = new Dictionary<string, object>
        {
            {
                "@authorityId", authorities
            }
        };
        return await SqlHelper.RunDapperQueryAsync<MenuByAuthoritiesSaveModel>(_connectionString,
                                                                               sBuilder,
                                                                               new DynamicParameters(dictionary));
    }

    /// <inheritdoc cref="PermissionValueByMenuId" />
    public async Task<int> PermissionValueByMenuId(Guid authorities, Guid menuId)
    {
        var sBuilder = new StringBuilder();
        sBuilder.Append(@"select AD.Value
                            from AuthorityDetail AD
                            where AD.AuthorityId = @authorities AND AD.MenuManagerId = @menuId ");
        var dictionary = new Dictionary<string, object>
        {
            {
                "@authorities", authorities
            },
            {
                "@menuId", menuId
            }
        };
        return await SqlHelper.RunDapperQueryFirstOrDefaultAsync<int>(_connectionString,
                                                                      sBuilder,
                                                                      new DynamicParameters(dictionary));
    }
}