#region

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using ITC.Domain.Core.ModelShare;
using ITC.Domain.Core.ModelShare.AuthorityManager;
using ITC.Domain.Core.ModelShare.SystemManagers.SortMenuManagers;
using ITC.Domain.Extensions;
using ITC.Domain.Interfaces.AuthorityManager.ProjectMenuManagerSystem;
using Microsoft.Data.SqlClient;
using NCore.Helpers;
using NCore.Modals;

#endregion

namespace ITC.Infra.Data.FunctionRepositoryQueries.AuthorityManager.ProjectMenuManagerSystem;

public class MenuManagerQueries : IMenuManagerQueries
{
#region Fields

    private readonly string _connectionString;

#endregion

#region Constructors

    public MenuManagerQueries(string connectionString)
    {
        _connectionString = connectionString;
    }

#endregion

#region V2022

    /// <inheritdoc />
    public Tuple<int, int, int> GetLeftRightAsync(string parentId, Guid managementId)
    {
        using var connection = new SqlConnection(_connectionString);
        var       sbBuilder  = new StringBuilder();
        sbBuilder.Append("WITH dMaxRight (MRight) AS ( ");
        if (parentId == "0")
            sbBuilder.Append("SELECT MAX(a.[PRight]) FROM [dbo].MenuManager a "           +
                             $"WHERE a.IsDeleted = 0 AND a.ProjectId = '{managementId}' " +
                             $"AND a.ParentId = '{parentId}' ");
        else
            sbBuilder.Append("SELECT MAX(a.[PRight]) FROM [dbo].MenuManager a "           +
                             $"WHERE a.IsDeleted = 0 AND a.ProjectId = '{managementId}' " +
                             $"AND a.Id = '{parentId}' ");

        sbBuilder.Append(") ");
        sbBuilder.Append("SELECT MRight FROM dMaxRight ");
        return SqlLeftRightHelper.ReturnLeftRight(parentId,
                                                  connection.QueryFirstOrDefault<int?>(sbBuilder.ToString()) ?? 0);
    }

    /// <inheritdoc cref="GetLeftRightAsyncWithProject" />
    public Tuple<int, int, int> GetLeftRightAsyncWithProject(string parentId, Guid projectId)
    {
        using var connection = new SqlConnection(_connectionString);
        var       sbBuilder  = new StringBuilder();
        sbBuilder.Append("WITH dMaxRight (MRight) AS ( ");
        if (parentId == "0")
            sbBuilder.Append("SELECT MAX(a.[MRight]) FROM [dbo].MenuManager a "        +
                             $"WHERE a.IsDeleted = 0 AND a.ProjectId = '{projectId}' " +
                             $"AND a.ParentId = '{parentId}' ");
        else
            sbBuilder.Append("SELECT MAX(a.[MRight]) FROM [dbo].MenuManager a "        +
                             $"WHERE a.IsDeleted = 0 AND a.ProjectId = '{projectId}' " +
                             $"AND a.Id = '{parentId}' ");

        sbBuilder.Append(") ");
        sbBuilder.Append("SELECT MRight FROM dMaxRight ");
        return SqlLeftRightHelper.ReturnLeftRight(parentId,
                                                  connection.QueryFirstOrDefault<int?>(sbBuilder.ToString()) ??
                                                  0);
    }

    /// <inheritdoc />
    public Task<int> DeleteAsync(StringBuilder sbBuilder)
    {
        using var connection = new SqlConnection(_connectionString);
        return connection.ExecuteAsync(sbBuilder.ToString());
    }

    /// <inheritdoc cref="DeleteAuthoritiesAsync" />
    public async Task<int> DeleteMenuDetailAsync(List<Guid> model)
    {
        return await SqlHelper.RunDapperExecuteAsync(_connectionString,
                                                     SqlHelper.DeleteListAsync("AuthorityDetail",
                                                                               model));
    }

    /// <inheritdoc />
    public async Task<IEnumerable<PLeftRight>> UpdateAsync(string childId, int childLeft, int childRight,
                                                           string childParentFather,
                                                           Guid   projectId)
    {
        await using var connection = new SqlConnection(_connectionString);
        var             sbBuilder  = new StringBuilder();
        sbBuilder.Append(SqlLeftRightHelper.UpdateLeftRightAsyncWithProject("MenuManager", childId, childLeft,
                                                                            childRight, childParentFather,
                                                                            projectId, 0));
        return await connection.QueryAsync<PLeftRight>(sbBuilder.ToString());
    }

    /// <param name="version"></param>
    /// <inheritdoc cref="GetTreeView" />
    public async Task<IEnumerable<TreeViewProjectModel>> GetTreeView(int version)
    {
        await using var connection = new SqlConnection(_connectionString);
        var             sbBuilder  = new StringBuilder();
        sbBuilder.Append(
            "SELECT MM.Id, MM.Name AS Text, MM.ParentId, MM.PermissionValue, MM.MenuGroupId, MM.Position ");
        sbBuilder.Append("FROM MenuManager MM ");
        sbBuilder.Append($"WHERE MM.IsDeleted = 0 AND MM.Version = {version} ");
        sbBuilder.Append("ORDER BY MM.MenuGroupId ASC, MM.Position ASC");
        return await connection.QueryAsync<TreeViewProjectModel>(sbBuilder.ToString());
    }

    /// <inheritdoc cref="GetPermissionDefault" />
    public async Task<IEnumerable<PermissionDefaultViewModal>> GetPermissionDefault()
    {
        await using var connection = new SqlConnection(_connectionString);
        var             sbBuilder  = new StringBuilder();
        sbBuilder.Append("SELECT PDS.Id, PDS.Name, PDS.Value ");
        sbBuilder.Append("FROM PermissionDefaults PDS ");
        return await connection.QueryAsync<PermissionDefaultViewModal>(sbBuilder.ToString());
    }

    /// <inheritdoc cref="GetMenuByAuthorities" />
    public async Task<IEnumerable<MenuByAuthoritiesViewModel>> GetMenuByAuthorities(
        Guid authority, bool isAdmin, bool isAddNew)
    {
        await using var connection = new SqlConnection(_connectionString);
        var             sbBuilder  = new StringBuilder();

        if (isAdmin)
        {
            sbBuilder.Append(string.Compare(authority.ToString(), Guid.Empty.ToString(),
                                            StringComparison.Ordinal) == 0
                                 ? @"SELECT MM.Id, MM.Name, MM.ParentId, MM.PermissionValue,
                                            MM.PermissionValue AS PermissionValueDefault, MM.MenuGroupId, MM.Position
                                    FROM MenuManager MM WHERE MM.IsDeleted = 0 ORDER BY MM.MenuGroupId, MM.Position "
                                 : @"WITH dAuthorityDetailTemp(Id, Name, Parent, PermissionValue, PermissionValueDefault, MenuGroupId, Position)
                                    AS (
                                        SELECT MM.Id,
                                                MM.Name,
                                                MM.ParentId,
                                                AD.Value,
                                                MM.PermissionValue,
                                                MM.MenuGroupId,
                                                MM.Position
                                            FROM           AuthorityDetail AD
                                                INNER JOIN Authorities A    ON A.Id     = AD.AuthorityId
                                            INNER          JOIN MenuManager MM ON MM.Id = AD.MenuManagerId
                                            WHERE AD.IsDeleted = 0 AND A.Id = @pAuthority
                                        )
                                    SELECT dDAT.Id,
                                            dDAT.Name,
                                            dDAT.Parent AS ParentId,
                                            dDAT.PermissionValue,
                                            dDAT.PermissionValueDefault,
                                            dDAT.MenuGroupId,
                                            dDAT.Position
                                                FROM dAuthorityDetailTemp dDAT
                                            UNION
                                                SELECT MM.Id,
                                            MM.Name,
                                            MM.ParentId,
                                            0 AS PermissionValue,
                                            MM.PermissionValue AS PermissionValueDefault,
                                            MM.MenuGroupId,
                                            MM.Position
                                        FROM MenuManager MM
                                    WHERE MM.Id NOT IN (SELECT dDAT2.Id FROM dAuthorityDetailTemp dDAT2) ");
            return await connection.QueryAsync<MenuByAuthoritiesViewModel>(sbBuilder.ToString(), new
            {
                pAuthority = authority
            });
        }

        sbBuilder.Append(@"SELECT MM.Id,
                                           MM.Name,
                                           MM.ParentId,
                                           MM.PermissionValue,
                                           MM.PermissionValue AS PermissionValueDefault,
                                           MM.MenuGroupId,
                                           AD.Position
                                    FROM AuthorityDetail AD
                                             INNER JOIN MenuManager MM ON AD.MenuManagerId = MM.Id
                                    WHERE MM.IsDeleted = 0 AND AD.IsDeleted = 0 AND MM.IsDeleted = 0 AND AD.Value > 0 AND AD.AuthorityId = @pAuthority
                                    GROUP BY MM.Id, MM.Name, MM.ParentId, MM.PermissionValue, MM.PermissionValue, MM.MenuGroupId, AD.Position
                                    ORDER BY AD.Position ");

        return await connection.QueryAsync<MenuByAuthoritiesViewModel>(sbBuilder.ToString(), new
        {
            pAuthority = authority
        });
    }

    /// <inheritdoc cref="GetAuthoritiesAsync" />
    public async Task<IEnumerable<AuthoritiesViewModel>> GetAuthoritiesAsync(
        Guid companyId, Guid projectId, string search, int pageSize,
        int  pageNumber)
    {
        await using var connection = new SqlConnection(_connectionString);
        var             sbBuilder  = new StringBuilder();

        sbBuilder.Append(SqlHelper.SelectTotalCount("A.Id "));
        sbBuilder.Append("A.Id, A.Name, A.StatusId ");
        sbBuilder.Append("FROM Authorities A ");
        sbBuilder.Append("WHERE A.IsDeleted = 0 AND A.ProjectId = @pProjectId AND A.CompanyId = @pAuthority ");
        sbBuilder.Append(SqlHelper.Search(new List<string>
        {
            "A.Name"
        }, search));
        sbBuilder.Append("ORDER BY A.Name ASC ");
        sbBuilder.Append(SqlHelper.Paging(pageNumber, pageSize));
        return await connection.QueryAsync<AuthoritiesViewModel>(sbBuilder.ToString(), new
        {
            pAuthority = companyId,
            pProjectId = projectId
        });
    }

    /// <inheritdoc cref="GetAuthoritiesCombobox" />
    public async Task<IEnumerable<KeyValuePair<Guid, string>>> GetAuthoritiesCombobox(
        Guid companyId, Guid projectId)
    {
        await using var connection = new SqlConnection(_connectionString);
        var             sbBuilder  = new StringBuilder();
        sbBuilder.Append("SELECT A.Id AS [KEY], A.Name AS [VALUE] ");
        sbBuilder.Append("FROM Authorities A ");
        sbBuilder.Append("WHERE A.IsDeleted = 0 AND A.ProjectId = @pProjectId AND A.CompanyId = @pAuthority ");
        sbBuilder.Append("ORDER BY A.Name ASC ");
        return await connection.QueryAsync<KeyValuePair<Guid, string>>(sbBuilder.ToString(), new
        {
            pAuthority = companyId,
            pProjectId = projectId
        });
    }

    /// <inheritdoc cref="GetAuthoritiesById" />
    public async Task<IEnumerable<KeyValuePair<Guid, string>>> GetAuthoritiesById(Guid authoritiesId)
    {
        await using var connection = new SqlConnection(_connectionString);
        var             sbBuilder  = new StringBuilder();
        sbBuilder.Append("SELECT A.Id AS [KEY], A.Name AS [VALUE] ");
        sbBuilder.Append("FROM Authorities A ");
        sbBuilder.Append("WHERE A.IsDeleted = 0 AND A.Id = @pAuthority ");
        sbBuilder.Append("ORDER BY A.Name ASC ");
        return await connection.QueryAsync<KeyValuePair<Guid, string>>(sbBuilder.ToString(), new
        {
            pAuthority = authoritiesId
        });
    }

    /// <inheritdoc cref="DeleteDetailAsync" />
    public async Task<int> DeleteDetailAsync(List<Guid> model, Guid authoritiesId)
    {
        await using var connection = new SqlConnection(_connectionString);
        var             sbBuilder  = new StringBuilder();
        sbBuilder.Append("DELETE FROM AuthorityDetail WHERE Id IN @pList AND AuthorityId = @pAuthoritiesId ");
        return await connection.ExecuteAsync(sbBuilder.ToString(), new
        {
            pList          = model,
            pAuthoritiesId = authoritiesId
        });
    }

    /// <inheritdoc cref="DeleteAuthoritiesAsync" />
    public async Task<int> DeleteAuthoritiesAsync(List<Guid> model)
    {
        return await SqlHelper.RunDapperExecuteAsync(_connectionString,
                                                     SqlHelper.DeleteListAsync("AuthorityDetail",
                                                                               model));
    }

    /// <inheritdoc cref="GetMenu" />
    public async Task<IEnumerable<MenuRoleReturnViewModel>> GetMenu(string userId)
    {
        await using var connection = new SqlConnection(_connectionString);
        var             sbBuilder  = new StringBuilder();
        sbBuilder.Append(@"SELECT AD.MenuManagerId AS Code,
                                       MM.Code          AS Id,
                                       MM.ManagerICon   AS Icon,
                                       AD.Name          AS Label,
                                       AD.Value,
                                       MM.ParentId,
                                       MM.Router        AS [To],                                       
                                       AD.Position
                                FROM Authorities A
                                         INNER JOIN AuthorityDetail AD ON A.Id = AD.AuthorityId 
                                             AND AD.Value > 0 AND AD.IsDeleted = 0
                                         INNER JOIN MenuManager MM ON MM.Id = AD.MenuManagerId AND MM.IsDeleted = 0
                                         INNER JOIN StaffManagers AU ON AU.AuthorityId = A.Id
                                WHERE AU.UserId = @userId
                                ORDER BY AD.Position ");
        // ORDER BY MM.MenuGroupId, MM.Position");
        return await connection.QueryAsync<MenuRoleReturnViewModel>(sbBuilder.ToString(), new
        {
            userId
        });
    }

    /// <inheritdoc cref="GetSortMenu" />
    public async Task<IEnumerable<SortMenuManagerDto>> GetSortMenu(Guid menuId, Guid parentId, Guid projectId)
    {
        await using var connection = new SqlConnection(_connectionString);
        var             sbBuilder  = new StringBuilder();
        sbBuilder.Append(@"SELECT AD.Id, AD.Name , AD.Position, MM.Name AS RootName, MM.Position AS RootPosition,
                                        MM.Id AS MenuId
                               FROM Authorities A
                                        INNER JOIN AuthorityDetail AD ON A.Id = AD.AuthorityId
                                        INNER JOIN MenuManager MM ON MM.Id = AD.MenuManagerId
                               WHERE A.Id = @menuId
                                 AND A.ProjectId = @projectId
                                 AND AD.IsDeleted = 0
                                 AND MM.ParentId = @parentId
                               ORDER BY AD.Position");
        return await connection.QueryAsync<SortMenuManagerDto>(sbBuilder.ToString(), new
        {
            menuId,
            projectId,
            parentId
        });
    }

#endregion

#region V2023

    /// <inheritdoc cref="v2023GetMenu" />
    public async Task<IEnumerable<v3MenuReturnFeModel>> v2023GetMenu(string userId, int version)
    {
        await using var connection = new SqlConnection(_connectionString);
        var             sbBuilder  = new StringBuilder();
        sbBuilder.Append(@"SELECT AD.MenuManagerId AS Code,
                                       MM.Code          AS Id,
                                       MM.ManagerICon   AS Icon,
                                       AD.Name          AS Label,
                                       AD.Value,
                                       MM.ParentId,
                                       MM.Router        AS [To],                                       
                                       AD.Position
                                FROM Authorities A
                                         INNER JOIN AuthorityDetail AD ON A.Id = AD.AuthorityId 
                                             AND AD.Value > 0 AND AD.IsDeleted = 0
                                         INNER JOIN MenuManager MM ON MM.Id = AD.MenuManagerId
                                         INNER JOIN StaffManagers AU ON AU.AuthorityId = A.Id
                                WHERE AU.UserId = @userId AND MM.Version = @version
                                ORDER BY AD.Position ");
        // ORDER BY MM.MenuGroupId, MM.Position");
        return await connection.QueryAsync<v3MenuReturnFeModel>(sbBuilder.ToString(), new
        {
            userId,
            version
        });
    }

    /// <inheritdoc cref="v2023GetMenuVersion" />
    public async Task<List<int>> v2023GetMenuVersion()
    {
        await using var connection = new SqlConnection(_connectionString);
        var             sbBuilder  = new StringBuilder();
        sbBuilder.Append(@"SELECT DISTINCT MM.Version AS Version FROM MenuManager MM WHERE MM.IsDeleted = 0 ");
        // ORDER BY MM.MenuGroupId, MM.Position");
        return (List<int>)await connection.QueryAsync<int>(sbBuilder.ToString());
    }

    /// <inheritdoc cref="V202301GetMenuByAuthorities" />
    public async Task<IEnumerable<MenuByAuthoritiesV2023>> V202301GetMenuByAuthorities(Guid authority)
    {
        await using var connection = new SqlConnection(_connectionString);
        var             sbBuilder  = new StringBuilder();
        sbBuilder.Append(@"SELECT AD.Id, AD.Value, AD.Name AS Label, AD.Position, AD.Value,
                                    MM.Name AS MenuName, MM.ParentId AS MenuParent, MM.Id AS MenuId
                            FROM AuthorityDetail AD
                                     INNER JOIN MenuManager MM ON AD.MenuManagerId = MM.Id
                            WHERE AD.AuthorityId = @authority
                              AND AD.IsDeleted = 0
                            ORDER BY AD.Position ");
        return await connection.QueryAsync<MenuByAuthoritiesV2023>(sbBuilder.ToString(), new
        {
            authority
        });
    }

    /// <inheritdoc cref="V202301GetMenuDefault" />
    public async Task<IEnumerable<MenuByAuthoritiesV2023>> V202301GetMenuDefault(Guid menuId)
    {
        await using var connection = new SqlConnection(_connectionString);
        var             sbBuilder  = new StringBuilder();
        sbBuilder.Append(@"SELECT MM.Id, MM.PermissionValue AS Value, MM.Name AS MenuName
                            FROM MenuManager MM 
                            WHERE MM.Id = @menuId
                              AND MM.IsDeleted = 0 ");
        return await connection.QueryAsync<MenuByAuthoritiesV2023>(sbBuilder.ToString(), new
        {
            menuId
        });
    }

#endregion
}