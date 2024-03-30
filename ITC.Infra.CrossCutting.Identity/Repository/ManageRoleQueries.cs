#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Dapper;
using ITC.Domain.Core.Pagination;
using ITC.Domain.Extensions;
using ITC.Infra.CrossCutting.Identity.Authorization;
using ITC.Infra.CrossCutting.Identity.Models.QueryModel;
using ITC.Infra.CrossCutting.Identity.Queries;
using Microsoft.Data.SqlClient;

#endregion

namespace ITC.Infra.CrossCutting.Identity.Repository;

public class ManageRoleQueries : IManageRoleQueries
{
#region Fields

    private readonly string _connectionString;

#endregion

#region Constructors

    public ManageRoleQueries(string connectionString)
    {
        _connectionString = connectionString;
    }

#endregion

#region IManageRoleQueries Members

    public async Task<List<ModuleModel>> GetModulesAsync(UrlQuery urlQuery)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var sb = new StringBuilder();

            sb.Append("SELECT f.[Id], f.[Name], f.[Identity], f.[Description], f.[CreateDate], f.[IsActivation] ");

            sb.Append("FROM [AspNetModules] as f ");
            sb.Append("WHERE f.IsDeleted=0 ");


            if (!string.IsNullOrEmpty(urlQuery.Keyword))
                sb.Append(
                    "AND ((f.[Name] COLLATE Latin1_General_CI_AI LIKE N'%'+@keyWord+'%') OR (f.[Identity] COLLATE Latin1_General_CI_AI LIKE N'%'+@keyWord+'%')) ");

            sb.Append("ORDER BY f.[CreateDate] DESC OFFSET @pageSize  *(@pageNumber - 1) ");
            sb.Append("ROWS FETCH NEXT @pageSize ROWS ONLY  ");
            var query = sb.ToString();

            var result = await connection.QueryAsync<ModuleModel>(query, new
            {
                keyWord    = urlQuery.Keyword,
                pageSize   = urlQuery.PageSize,
                pageNumber = urlQuery.PageNumber
            });
            var modules = result.ToList();
            sb.Clear();
            sb.Append("SELECT Distinct r.[Id], r.[Identity], r.[Name], mr.[ModuleId] ");

            sb.Append("FROM [AspNetModuleRoles] as mr ");
            sb.Append("INNER JOIN [AspNetRoles] as r ON r.[Id]=mr.[RoleId] ");

            sb.Append("WHERE mr.[ModuleId] IN (");
            for (var i = 0; i < modules.Count; i++)
                if (i != modules.Count - 1)
                    sb.Append($"'{modules[i].Id}',");
                else
                    sb.Append($"'{modules[i].Id}') ");
            query = sb.ToString();

            var resultRoles = await connection.QueryAsync<CustomRoleModel>(query);

            modules.ForEach(x => { x.Roles = resultRoles.Where(y => y.ModuleId == x.Id).ToList(); });

            return modules;
        }
    }

    public async Task<int> CountModuleAsync(UrlQuery urlQuery)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var sb = new StringBuilder();

            sb.Append("SELECT Count(f.[Id]) ");

            sb.Append("FROM [AspNetModules] as f ");

            sb.Append("WHERE f.IsDeleted=0 ");

            if (!string.IsNullOrEmpty(urlQuery.Keyword))
                sb.Append(
                    "AND ((f.[Name] COLLATE Latin1_General_CI_AI LIKE N'%'+@keyWord+'%') OR (f.[Identity] COLLATE Latin1_General_CI_AI LIKE N'%'+@keyWord+'%')) ");

            var query = sb.ToString();
            var result = await connection.QueryAsync<int>(query, new
            {
                keyWord = urlQuery.Keyword
            });
            return result.FirstOrDefault();
        }
    }

    public async Task<List<ModuleModel>> GetModulesAsync()
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var sb = new StringBuilder();

            sb.Append(
                "SELECT f.[Id], f.[Name], f.[Identity], f.[Description], f.[CreateDate], f.[IsActivation], mr.[RoleId], (f.[Name] + ' - ' + r.[Name]) as DisplayName ");

            sb.Append("FROM [AspNetModules] as f ");
            sb.Append("INNER JOIN [AspNetModuleRoles] as mr ON mr.[ModuleId]=f.[Id] ");
            sb.Append("INNER JOIN [AspNetRoles] as r ON r.[Id]=mr.[RoleId] ");
            sb.Append("WHERE f.IsDeleted=0 ");
            sb.Append("ORDER BY mr.[RoleId] DESC ");
            var query  = sb.ToString();
            var result = await connection.QueryAsync<ModuleModel>(query);
            return result.ToList();
        }
    }

    public async Task<List<string>> GetRolesByModuleIdAsync(string moduleId)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var sb = new StringBuilder();
            sb.Append("SELECT p.RoleId FROM [AspNetModuleRoles] as p ");
            sb.Append("WHERE p.[ModuleId]=@moduleId ");

            var query = sb.ToString();
            var result = await connection.QueryAsync<string>(query, new
            {
                moduleId
            });
            return result.ToList();
        }
    }

    public async Task DeleteRolesByModuleIdAsync(string moduleId, List<string> roleIds)
    {
        if (roleIds.Count == 0)
            return;

        using (var connection = new SqlConnection(_connectionString))
        {
            var sb = new StringBuilder();
            sb.Append("BEGIN TRANSACTION ");
            sb.Append("DELETE [AspNetModuleRoles] ");
            sb.Append("WHERE ModuleId=@moduleId ");
            sb.Append("AND RoleId IN (");
            for (var i = 0; i < roleIds.Count; i++)
                if (i != roleIds.Count - 1)
                    sb.Append($"'{roleIds[i]}',");
                else
                    sb.Append($"'{roleIds[i]}') ");

            sb.Append("COMMIT TRANSACTION ");
            var query = sb.ToString();
            var result = await connection.ExecuteAsync(query, new
            {
                moduleId
            });
            await Task.CompletedTask;
        }
    }

    public async Task<List<TypeAudit>> GetFunctionsByModuleIdAsync(string moduleId)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var sb = new StringBuilder();
            sb.Append("SELECT p.[Weight] FROM [AspNetFunctions] as p ");
            sb.Append("WHERE p.[ModuleId]=@moduleId ");

            var query = sb.ToString();
            var result = await connection.QueryAsync<TypeAudit>(query, new
            {
                moduleId
            });
            return result.ToList();
        }
    }

    public async Task DeleteFunctionsByModuleIdAsync(string moduleId, List<int> weights, List<string> roleIds)
    {
        if (weights.Count == 0 && roleIds.Count == 0)
            return;

        using (var connection = new SqlConnection(_connectionString))
        {
            var weight = string.Join(",", weights);
            var sb     = new StringBuilder();
            sb.Append("BEGIN TRANSACTION ");
            if (weights.Count > 0)
            {
                sb.Append("DELETE [AspNetFunctions] ");
                sb.Append("WHERE ModuleId=@moduleId ");
                sb.Append($"AND Weight IN ({weight})");
            }

            if (roleIds.Count > 0)
            {
                sb.Append("DELETE fr FROM [AspNetFunctionRoles] fr ");
                sb.Append("INNER JOIN [AspNetFunctions] as f ON f.[Id]=fr.[FunctionId] AND f.[ModuleId]=@moduleId ");
                sb.Append("WHERE RoleId IN (");
                for (var i = 0; i < roleIds.Count; i++)
                    if (i != roleIds.Count - 1)
                        sb.Append($"'{roleIds[i]}',");
                    else
                        sb.Append($"'{roleIds[i]}') ");
                if (weights.Count > 0)
                    sb.Append($"AND f.Weight NOT IN ({weight})");
            }

            sb.Append("COMMIT TRANSACTION ");
            var query = sb.ToString();
            var result = await connection.ExecuteAsync(query, new
            {
                moduleId
            });
            await Task.CompletedTask;
        }
    }

    public async Task<List<FunctionModel>> GetFunctionsAsync(UrlQuery urlQuery)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var sb = new StringBuilder();

            sb.Append(
                "SELECT f.[Id], f.[Name], f.[Weight], f.[Description], f.[IsActivation], f.[ModuleId], m.[Name] as ModuleName ");

            sb.Append("FROM [AspNetFunctions] as f ");
            sb.Append("INNER JOIN [AspNetModules] as m ON m.[Id]=f.[ModuleId] ");


            if (!string.IsNullOrEmpty(urlQuery.Keyword))
                sb.Append(
                    "WHERE ((f.[Name] COLLATE Latin1_General_CI_AI LIKE N'%'+@keyWord+'%') OR (f.[Description] COLLATE Latin1_General_CI_AI LIKE N'%'+@keyWord+'%')) ");

            sb.Append(
                "ORDER BY f.[CreateDate] DESC, f.[ModuleId] DESC, f.[Weight] OFFSET @pageSize  *(@pageNumber - 1) ");
            sb.Append("ROWS FETCH NEXT @pageSize ROWS ONLY  ");
            var query = sb.ToString();
            var result = await connection.QueryAsync<FunctionModel>(query, new
            {
                keyWord    = urlQuery.Keyword,
                pageSize   = urlQuery.PageSize,
                pageNumber = urlQuery.PageNumber
            });
            return result.ToList();
        }
    }

    public async Task<int> CountFunctionAsync(UrlQuery urlQuery)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var sb = new StringBuilder();

            sb.Append("SELECT Count(f.[Id]) ");

            sb.Append("FROM [AspNetFunctions] as f ");
            sb.Append("INNER JOIN [AspNetModules] as m ON m.[Id]=f.[ModuleId] ");

            if (!string.IsNullOrEmpty(urlQuery.Keyword))
                sb.Append(
                    "WHERE ((f.[Name] COLLATE Latin1_General_CI_AI LIKE N'%'+@keyWord+'%') OR (f.[Description] COLLATE Latin1_General_CI_AI LIKE N'%'+@keyWord+'%')) ");

            var query = sb.ToString();
            var result = await connection.QueryAsync<int>(query, new
            {
                keyWord = urlQuery.Keyword
            });
            return result.FirstOrDefault();
        }
    }

    public async Task<List<TreeViewNode>> GetPermissionsAsync()
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var sb = new StringBuilder();

            sb.Append("SELECT f.[Id] as id, f.[Name] as text ");

            sb.Append("FROM [AspNetRoles] as f ");
            sb.Append("ORDER BY f.[Id] DESC ");

            var query = sb.ToString();

            var result = await connection.QueryAsync<TreeViewNode>(query);

            sb.Clear();
            sb.Append("SELECT m.[Id] as id, m.[Name] as text ");

            sb.Append("FROM [AspNetModules] as m ");
            sb.Append("INNER JOIN [AspNetModuleRoles] as mr ON mr.[ModuleId]=m.[Id] AND mr.[RoleId]=@roleId ");
            sb.Append("WHERE m.[IsDeleted]=0 AND m.[IsActivation]=1 ");
            sb.Append("ORDER BY m.[CreateDate] ");
            query = sb.ToString();

            sb.Clear();
            sb.Append("SELECT (fn.[Id]+'_'+fr.[RoleId]) as id, fn.[Name] as text, fn.[Weight] as weight ");

            sb.Append("FROM [AspNetFunctions] as fn ");
            sb.Append("INNER JOIN [AspNetFunctionRoles] as fr ON fr.[FunctionId]=fn.[Id] AND fr.[RoleId]=@roleId ");
            sb.Append("INNER JOIN [AspNetModules] as m ON m.[Id]=fn.[ModuleId] AND m.[Id]=@moduleId ");
            sb.Append("ORDER BY fn.[Weight] ");
            var queryFunction = sb.ToString();

            foreach (var role in result)
            {
                role.children = await connection.QueryAsync<TreeViewNode>(query, new
                {
                    roleId = role.id
                });
                role.key = "Role";
                foreach (var module in role.children)
                {
                    module.key = "Module";
                    module.children = await connection.QueryAsync<TreeViewNode>(queryFunction, new
                    {
                        moduleId = module.id,
                        roleId   = role.id
                    });
                    foreach (var item in module.children) item.key = "Function";
                    module.id = $"{module.id}_{role.id}";
                }
            }

            return result.ToList();
        }
    }

    public async Task<UserTypeDetailModel> GetPermissionsByUserIdAsync(string userId)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var sb = new StringBuilder();


            sb.Append("SELECT ut.[Id], ut.[Name], ut.[IsSuperAdmin], ut.[RoleId], ut.[IsDefault] ");

            sb.Append("FROM [AspNetUserTypes] as ut ");
            sb.Append(
                "INNER JOIN [AspNetUsers] as u ON u.[UserTypeId]=ut.[Id] AND u.[Id]=@userId AND u.[IsDeleted]=0 ");
            sb.Append("WHERE ut.IsDeleted=0 ");

            var query = sb.ToString();
            var result = await connection.QueryAsync<UserTypeDetailModel>(query, new
            {
                userId
            });
            var userType = result.FirstOrDefault();
            if (userType == null) return null;

            sb.Clear();

            //get all roleId
            sb.Append("SELECT DISTINCT [RoleId] as id ");

            sb.Append("FROM [AspNetModuleDecentralizations] ");
            sb.Append("WHERE UserTypeId=@userTypeId ");
            sb.Append("UNION ");

            sb.Append("SELECT DISTINCT [RoleId] as id ");

            sb.Append("FROM [AspNetFunctionDecentralizations] ");
            sb.Append("WHERE UserTypeId=@userTypeId ");
            query = sb.ToString();

            sb.Clear();
            sb.Append("SELECT [Id] as id, [Name] as text ");
            sb.Append("FROM [AspNetRoles] as rl ");

            sb.Append("WHERE rl.Id IN ( " + query + " ) ");

            query = sb.ToString();
            userType.Roles = await connection.QueryAsync<TreeViewNode>(query, new
            {
                userTypeId = userType.Id
            });

            sb.Clear();
            sb.Append("SELECT mr.ModuleId as id, m.[Name] as text ");

            sb.Append("FROM [AspNetModuleDecentralizations] as mr ");
            sb.Append("INNER JOIN [AspNetModules] as m ON m.Id = mr.ModuleId AND m.IsActivation=1 ");
            sb.Append("WHERE mr.RoleId=@roleId ");
            sb.Append("AND mr.UserTypeId=@userTypeId ");
            query = sb.ToString();
            sb.Clear();
            sb.Append("SELECT fr.FunctionId as id, f.[Name] as text ");

            sb.Append("FROM [AspNetFunctionDecentralizations] as fr ");
            sb.Append("INNER JOIN [AspNetFunctions] as f ON f.Id = fr.FunctionId AND f.IsActivation=1 ");
            sb.Append("WHERE fr.[RoleId]=@roleId ");
            sb.Append("AND f.ModuleId=@moduleId ");
            sb.Append("AND fr.UserTypeId=@userTypeId ");
            sb.Append("ORDER BY f.[Weight] ");
            var queryModule = sb.ToString();
            foreach (var permission in userType.Roles)
            {
                permission.key = "Role";
                permission.children = await connection.QueryAsync<TreeViewNode>(query, new
                {
                    userTypeId = userType.Id,
                    roleId     = permission.id
                });
                foreach (var module in permission.children)
                {
                    module.key = "Module";
                    module.children = await connection.QueryAsync<TreeViewNode>(queryModule, new
                    {
                        userTypeId = userType.Id,
                        roleId     = permission.id,
                        moduleId   = module.id
                    });
                    module.children.ForEach(x => x.key = "Function");
                }
            }

            return userType;
        }
    }


    public async Task<CustomUserTypeModel> GetPermissionsByUserTypeAsync(string userId, string userTypeId)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var sb = new StringBuilder();

            sb.Append("SELECT  r.[Name] as RoleIdentity,u.PortalId,u.[FullName],u.Email ");

            sb.Append("FROM [AspNetUsers] u ");
            sb.Append("INNER JOIN UserTypes ut ON ut.Id = u.UserTypeId ");
            sb.Append("INNER JOIN UserTypeFeatures UTF ON ut.Id = UTF.UserTypeId ");
            sb.Append("INNER JOIN Features F ON UTF.FeatureId = F.Id ");
            sb.Append("INNER JOIN DefaultRoles r ON F.RoleId = r.Id ");
            sb.Append("WHERE ut.Id = @userTypeId and u.[IsDeleted]=0 AND u.[Id]=@userId ");
            var query = sb.ToString();
            var result = await connection.QueryAsync<CustomUserTypeModel>(query, new
            {
                userId,
                userTypeId
            });
            var customUserTypeModel = result.FirstOrDefault();
            if (customUserTypeModel == null)
                return null;

            customUserTypeModel.UserId = userId;
            sb.Clear();
            sb.Append("SELECT u.Id as BaseUnitUserId, p.IsDepartmentOfEducation ");
            sb.Append("FROM [AspNetUsers] u ");
            sb.Append("INNER JOIN [DepartmentOfEducation] d ON u.[Id]=d.[UserId] ");
            sb.Append("INNER JOIN [AspNetPortals] p ON u.PortalId=p.Id ");
            sb.Append("WHERE u.[IsDeleted]=0 AND d.[IsDeleted]=0 and u.PortalId=@portalId ");
            query = sb.ToString();
            var baseUnit = await connection.QueryAsync<CustomUserTypeModel>(query, new
            {
                portalId = customUserTypeModel.PortalId
            });
            if (baseUnit.Count() > 0)
            {
                var baseUnitModel = baseUnit.FirstOrDefault();
                customUserTypeModel.BaseUnitUserId = baseUnitModel.BaseUnitUserId;
                if (baseUnitModel.IsDepartmentOfEducation)
                    customUserTypeModel.BaseRoleIdentity = RoleIdentity.DepartmentOfEducation;
                else
                    customUserTypeModel.BaseRoleIdentity = RoleIdentity.EducationDepartment;
            }

            sb.Clear();

            // Lấy sở giáo dục 

            //// Lấy id năm học hiện tại
            //sb.Append("SELECT * FROM [SchoolYear] WHERE [EndTime]=@finalYear and [IsDeleted]=0 and [PortalId]=@portalId");
            //query = sb.ToString();
            //var schoolYears = await connection.QueryAsync<dynamic>(query, new
            //{
            //    finalYear = DateTime.Now.Year,
            //    portalId  = customUserTypeModel.PortalId
            //});

            //if (schoolYears != null && schoolYears.Count() > 0)
            //{
            //    var schoolYear = schoolYears.FirstOrDefault();
            //    customUserTypeModel.SchoolYear = schoolYear.Id;
            //}

            sb.Clear();
            //Unit
            if (RoleIdentity.Administrator != customUserTypeModel.RoleIdentity)
            {
                sb.Append("SELECT g.[Id],u.[FullName] as ManagementName, g.[UserId] as UserId ");
                switch (customUserTypeModel.RoleIdentity)
                {
                    case RoleIdentity.DepartmentOfEducation:
                        sb.Append("FROM [DepartmentOfEducation] g ");
                        break;
                    case RoleIdentity.EducationDepartment:
                        sb.Append("FROM [EducationDepartment] g ");
                        break;
                    case RoleIdentity.Department:
                        sb.Append("FROM [Department] g ");
                        break;
                    case RoleIdentity.Bussiness:
                        sb.Append("FROM [Bussiness] g ");
                        break;
                    default:
                        sb.Append("FROM [Bussiness] g ");
                        break;
                }

                sb.Append("INNER JOIN [AspNetUsers] u ON (u.[Id]=g.[UserId]) AND u.[Id]=@userId ");
                sb.Append("WHERE u.[IsDeleted]=0 ");

                query = sb.ToString();
                var units = await connection.QueryAsync<CustomManagement>(query, new
                {
                    userId
                });

                // tài khoản cấp dưới
                if (units.Count() == 0)
                {
                    sb.Clear();
                    sb.Append("SELECT g.[Id],u.[FullName] as ManagementName, g.[UserId] as UserId ");
                    switch (customUserTypeModel.RoleIdentity)
                    {
                        case RoleIdentity.DepartmentOfEducation:
                            sb.Append("FROM [DepartmentOfEducation] g ");
                            break;
                        case RoleIdentity.EducationDepartment:
                            sb.Append("FROM [EducationDepartment] g ");
                            break;
                        case RoleIdentity.Department:
                            sb.Append("FROM [Department] g ");
                            break;
                        case RoleIdentity.Bussiness:
                            sb.Append("FROM [Bussiness] g ");
                            break;
                        default:
                            sb.Append("FROM [Bussiness] g ");
                            break;
                    }

                    sb.Append("INNER JOIN [AspNetUsers] u ON (u.[ManagementUnitId]=g.[UserId]) AND u.[Id]=@userId ");
                    sb.Append("WHERE u.[IsDeleted]=0 ");

                    query = sb.ToString();
                    units = await connection.QueryAsync<CustomManagement>(query, new
                    {
                        userId
                    });
                }

                var unit = units.FirstOrDefault();
                if (unit != null)
                {
                    customUserTypeModel.UnitId             = unit.Id.ToString();
                    customUserTypeModel.UnitUserId         = unit.UserId;
                    customUserTypeModel.EducationLevelCode = unit.EducationLevelCode;
                    // Lấy tên đơn vị quản lý
                    sb.Clear();
                    sb.Append("SELECT u.[FullName] as ManagementName From [AspNetUsers] u  where u.Id=@id ");
                    query = sb.ToString();
                    var results = await connection.QueryAsync<CustomManagement>(query, new
                    {
                        id = customUserTypeModel.UnitUserId
                    });
                    if (results.Count() > 0)
                    {
                        var mana = results.FirstOrDefault();
                        customUserTypeModel.ManagementName = mana.ManagementName;
                    }
                }

                sb.Clear();
            }

            sb.Append("SELECT DISTINCT [RoleId] ");

            sb.Append("FROM [AspNetModuleDecentralizations] ");
            sb.Append("WHERE UserTypeId=@userTypeId ");
            sb.Append("UNION ");

            sb.Append("SELECT DISTINCT [RoleId] ");

            sb.Append("FROM [AspNetFunctionDecentralizations] ");
            sb.Append("WHERE UserTypeId=@userTypeId ");

            query = sb.ToString();
            sb.Clear();
            sb.Append("SELECT rl.[Identity] ");

            sb.Append("FROM [AspNetRoles] as rl ");
            sb.Append("WHERE Id IN (" + query + ") ");

            query = sb.ToString();

            customUserTypeModel.Roles = await connection.QueryAsync<string>(query, new
            {
                userTypeId
            });

            sb.Clear();

            sb.Append("SELECT m.[Identity] as Id, r.[Identity] as RoleIdentity, SUM(fn.[Weight]) as We ");
            sb.Append("FROM [AspNetFunctionDecentralizations] as fnd ");

            sb.Append("INNER JOIN [AspNetFunctions] as fn ON fn.[Id]=fnd.[FunctionId] ");
            sb.Append("INNER JOIN [AspNetModules] as m ON m.[Id]=fn.[ModuleId] ");
            sb.Append("INNER JOIN [AspNetRoles] as r ON r.[Id]=fnd.[RoleId] ");

            sb.Append("WHERE fnd.UserTypeId=@userTypeId ");
            sb.Append("GROUP BY m.[Identity], r.[Identity] ");

            query = sb.ToString();

            customUserTypeModel.Permissions = await connection.QueryAsync<CustomModuleModel>(query, new
            {
                userTypeId
            });
            return customUserTypeModel;
        }
    }

    public async Task<List<UserTypeModel>> GetUserTypesAsync(ManageRoleQueryViewModel urlQuery,
                                                             string                   userId = null)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var sb = new StringBuilder();

            sb.Append(
                "SELECT [Id],[Description], [Name], [IsSuperAdmin], [RoleId], [IsDefault], [PortalId] , COUNT(Id) OVER () AS TotalRecord ");

            sb.Append("FROM [AspNetUserTypes] as f ");
            sb.Append("WHERE f.IsDeleted=0 ");

            if (!string.IsNullOrEmpty(userId)) sb.Append("AND f.[ManagementUnitId]=@userId ");

            if (!string.IsNullOrEmpty(urlQuery.Name))
                sb.Append("AND (f.[Name] COLLATE Latin1_General_CI_AI LIKE N'%'+@name+'%') ");

            if (!string.IsNullOrEmpty(urlQuery.Description))
                sb.Append("AND (f.[Description] COLLATE Latin1_General_CI_AI LIKE N'%'+@description+'%') ");
            sb.Append("ORDER BY f.[CreateDate] DESC OFFSET @pageSize  *(@pageNumber - 1) ");
            sb.Append("ROWS FETCH NEXT @pageSize ROWS ONLY  ");

            var query = sb.ToString();
            var result = await connection.QueryAsync<UserTypeModel>(query, new
            {
                name        = urlQuery.Name,
                description = urlQuery.Description,
                userId,
                pageSize   = urlQuery.PageSize,
                pageNumber = urlQuery.PageNumber
            });

            sb.Clear();
            sb.Append("SELECT DISTINCT [RoleId] ");

            sb.Append("FROM [AspNetModuleDecentralizations] ");
            sb.Append("WHERE UserTypeId=@userTypeId ");
            sb.Append("UNION ");

            sb.Append("SELECT DISTINCT [RoleId] ");

            sb.Append("FROM [AspNetFunctionDecentralizations] ");
            sb.Append("WHERE UserTypeId=@userTypeId ");

            var rolesQuery = sb.ToString();
            sb.Clear();
            sb.Append("SELECT rl.Id as RoleId ");

            sb.Append("FROM [AspNetRoles] as rl ");
            sb.Append("WHERE Id IN (" + rolesQuery + ") ");

            query = sb.ToString();
            foreach (var item in result)
                item.Roles = await connection.QueryAsync<CustomRoleUserTypeModel>(query, new
                {
                    userTypeId = item.Id
                });

            return result.ToList();
        }
    }

    public async Task<List<UserTypeModel>> GetUserTypesPaginationAsync(
        ManageRoleQueryViewModel urlQuery, string userId = null)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var sb = new StringBuilder();

            sb.Append(
                "SELECT [Id],[Description], [Name], [IsSuperAdmin], [RoleId], [IsDefault], [PortalId] , COUNT(Id) OVER () AS TotalRecord ");

            sb.Append("FROM [AspNetUserTypes] as f ");
            sb.Append("WHERE f.IsDeleted=0 ");

            if (!string.IsNullOrEmpty(userId)) sb.Append("AND f.[ManagementUnitId]=@userId ");

            if (!string.IsNullOrEmpty(urlQuery.Keyword))
                sb.Append(
                    "AND ((f.[Name] COLLATE Latin1_General_CI_AI LIKE N'%'+@keyWord+'%') OR (f.[Description] COLLATE Latin1_General_CI_AI LIKE N'%'+@keyWord+'%')) ");

            sb.Append("ORDER BY f.[CreateDate] DESC OFFSET @pageSize  *(@pageNumber - 1) ");
            sb.Append("ROWS FETCH NEXT @pageSize ROWS ONLY  ");

            var query = sb.ToString();
            var result = await connection.QueryAsync<UserTypeModel>(query, new
            {
                keyWord = urlQuery.Keyword,
                userId,
                pageSize   = urlQuery.PageSize,
                pageNumber = urlQuery.PageNumber
            });

            sb.Clear();
            sb.Append("SELECT DISTINCT [RoleId] ");

            sb.Append("FROM [AspNetModuleDecentralizations] ");
            sb.Append("WHERE UserTypeId=@userTypeId ");
            sb.Append("UNION ");

            sb.Append("SELECT DISTINCT [RoleId] ");

            sb.Append("FROM [AspNetFunctionDecentralizations] ");
            sb.Append("WHERE UserTypeId=@userTypeId ");

            var rolesQuery = sb.ToString();
            sb.Clear();
            sb.Append("SELECT rl.Id as RoleId ");

            sb.Append("FROM [AspNetRoles] as rl ");
            sb.Append("WHERE Id IN (" + rolesQuery + ") ");

            query = sb.ToString();
            foreach (var item in result)
                item.Roles = await connection.QueryAsync<CustomRoleUserTypeModel>(query, new
                {
                    userTypeId = item.Id
                });

            return result.ToList();
        }
    }

    public async Task<int> CountUserTypeAsync(ManageRoleQueryViewModel urlQuery, string userId = null)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var sb = new StringBuilder();

            sb.Append("SELECT Count([Id]) ");

            sb.Append("FROM [AspNetUserTypes] as ut ");
            sb.Append("WHERE IsDeleted=0 ");

            if (!string.IsNullOrEmpty(userId)) sb.Append("AND ut.[ManagementUnitId]=@userId ");

            if (!string.IsNullOrEmpty(urlQuery.Name))
                sb.Append("AND (ut.[Name] COLLATE Latin1_General_CI_AI LIKE N'%'+@name+'%') ");
            if (!string.IsNullOrEmpty(urlQuery.Name))
                sb.Append("AND (ut.[Description] COLLATE Latin1_General_CI_AI LIKE N'%'+@description+'%') ");
            var query = sb.ToString();
            var result = await connection.QueryAsync<int>(query, new
            {
                name        = urlQuery.Name,
                description = urlQuery.Description,
                userId
            });
            return result.FirstOrDefault();
        }
    }

    public async Task<List<UserTypeModel>> GetUserTypesAsync(string userId = null)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var sb = new StringBuilder();

            sb.Append("SELECT [Id], [Name] ");

            sb.Append("FROM [AspNetUserTypes] as f ");
            sb.Append("WHERE f.IsDeleted=0 ");
            if (!string.IsNullOrEmpty(userId)) sb.Append("AND f.[ManagementUnitId]=@userId ");

            var query = sb.ToString();
            var result = await connection.QueryAsync<UserTypeModel>(query, new
            {
                userId
            });
            return result.ToList();
        }
    }

    public async Task<List<UserTypeModel>> GetUserTypesExceptByUserTypeIdAsync(
        string userTypeId, string userId = null)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var sb = new StringBuilder();

            sb.Append("SELECT [Id], [Name], [IsSuperAdmin], [RoleId], [IsDefault] ");

            sb.Append("FROM [AspNetUserTypes] as f ");
            sb.Append("WHERE f.IsDeleted=0 ");
            sb.Append("AND f.[Id] != @userTypeId ");
            if (!string.IsNullOrEmpty(userId))
                sb.Append("AND f.[ManagementUnitId]=@userId ");

            var query = sb.ToString();
            var result = await connection.QueryAsync<UserTypeModel>(query, new
            {
                userTypeId,
                userId
            });
            return result.ToList();
        }
    }

    /// <inheritdoc />
    public async Task<IEnumerable<UserTypeByUserIdDto>> GetUserTypeAndFeatureByUserIdAsync(
        string userId, string userTypeId, string defaultRoleId)
    {
        await using (var connection = new SqlConnection(_connectionString))
        {
            var sBuilder = new StringBuilder();
            sBuilder.Append(
                @"SELECT a.UserName, UT.Name AS UserTypeName, UTF.FeatureId, F.PermissionValue, FD.Code AS FeatureDfName
                                    FROM AspNetUsers a
                                    INNER JOIN UserTypes UT ON a.UserTypeId = UT.Id
                                    INNER JOIN UserTypeFeatures UTF ON UT.Id = UTF.UserTypeId
                                    INNER JOIN Features F ON UTF.FeatureId = F.Id
                                    INNER JOIN FeatureDefaults FD ON F.FeatureDefaultId = FD.Id
                                    WHERE a.Id = @pUserId AND a.UserTypeId = @pUserTypeId AND F.RoleId = @pRoleId");
            return await connection.QueryAsync<UserTypeByUserIdDto>(sBuilder.ToString(), new
            {
                pUserId     = userId,
                pUserTypeId = userTypeId,
                pRoleId     = defaultRoleId
            });
        }
    }

    /// <inheritdoc cref="GetAuthorityByUser" />
    public async Task<IEnumerable<AuthorityByUserDto>> GetAuthorityByUser(string userId)
    {
        await using var connection = new SqlConnection(_connectionString);
        var             sBuilder   = new StringBuilder();
        // sBuilder.Append(@"SELECT MM.Code AS Name, MM.ParentId, AD.Value
        //                         FROM StaffManagers AU
        //                                  INNER JOIN AuthorityDetail AD ON AD.AuthorityId = AU.AuthorityId
        //                                  INNER JOIN MenuManager MM ON MM.Id = AD.MenuManagerId
        //                         WHERE AD.IsDeleted = 0 AND AU.UserId = @userId ");
        sBuilder.Append(@"SELECT AD.Name, MM.Code, MM.ParentId, AD.Value, AD.Position, 
                                    AD.AuthorityId, AU.Id AS StaffId, AU.Name AS StaffName
                                FROM StaffManagers AU
                                         INNER JOIN AuthorityDetail AD ON AD.AuthorityId = AU.AuthorityId
                                         INNER JOIN MenuManager MM ON MM.Id = AD.MenuManagerId
                                WHERE AD.IsDeleted = 0 AND AU.UserId = @userId AND AD.Value > 0
                                ORDER BY AD.Position ");
        return await connection.QueryAsync<AuthorityByUserDto>(sBuilder.ToString(), new
        {
            userId
        });
    }

    // /// <inheritdoc />
    // public async Task<IEnumerable<Combobox2Param>> GetRoleByUserIdAsync(string userId, string userTypeId)
    // {
    //     await using (var connection = new SqlConnection(_connectionString))
    //     {
    //         var sBuilder = new StringBuilder();
    //         sBuilder.Append(@$"SELECT P.Id, P.Name
    //                             FROM AspNetUsers ASU
    //                                      INNER JOIN UserTypes UT ON UT.Id = ASU.UserTypeId
    //                                      INNER JOIN UserTypeFeatures UTF ON UT.Id = UTF.UserTypeId
    //                                      INNER JOIN Features F ON UTF.FeatureId = F.Id
    //                                      INNER JOIN DefaultRoles P ON F.RoleId = P.Id
    //                             WHERE ASU.Id = @pUserId AND ASU.UserTypeId = @pUserTypeId
    //                             GROUP BY P.Id, P.Name ");
    //         return await connection.QueryAsync<Combobox2Param>(sBuilder.ToString(), new
    //         {
    //             pUserId = userId,
    //             pUserTypeId = userTypeId
    //         });
    //     }
    // }

    public async Task<UserTypeDetailModel> GetUserTypeByIdAsync(string userTypeId, string userId = null)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var sb = new StringBuilder();


            sb.Append("SELECT [Id],[Description], [Name], [IsSuperAdmin], [RoleId], [IsDefault] ");

            sb.Append("FROM [AspNetUserTypes] as ut ");
            sb.Append("WHERE IsDeleted=0 ");
            sb.Append("AND Id=@userTypeId ");
            if (!string.IsNullOrEmpty(userId))
                sb.Append("AND [ManagementUnitId]=@userId ");


            var query = sb.ToString();
            var result = await connection.QueryAsync<UserTypeDetailModel>(query, new
            {
                userTypeId,
                userId
            });
            var userType = result.FirstOrDefault();
            if (userType == null) return null;

            sb.Clear();

            //get all roleId
            sb.Append("SELECT DISTINCT [RoleId] as id ");

            sb.Append("FROM [AspNetModuleDecentralizations] ");
            sb.Append("WHERE UserTypeId=@userTypeId ");
            sb.Append("UNION ");

            sb.Append("SELECT DISTINCT [RoleId] as id ");

            sb.Append("FROM [AspNetFunctionDecentralizations] ");
            sb.Append("WHERE UserTypeId=@userTypeId ");
            query = sb.ToString();

            sb.Clear();
            sb.Append("SELECT [Id] as id, [Name] ");
            sb.Append("FROM [AspNetRoles] as rl ");

            sb.Append("WHERE rl.Id IN ( " + query + " ) ");

            query = sb.ToString();
            userType.Roles = await connection.QueryAsync<TreeViewNode>(query, new
            {
                userTypeId = userType.Id
            });

            sb.Clear();
            sb.Append("SELECT (mr.ModuleId) as id, m.[Name] ");

            sb.Append("FROM [AspNetModuleDecentralizations] as mr ");
            sb.Append("INNER JOIN [AspNetModules] as m ON m.Id = mr.ModuleId AND m.IsActivation=1 ");
            sb.Append("WHERE mr.RoleId=@roleId ");
            sb.Append("AND mr.UserTypeId=@userTypeId ");

            query = sb.ToString();

            sb.Clear();
            sb.Append("SELECT (fr.FunctionId +'_'+ fr.[RoleId]) as id, f.[Name] ");

            sb.Append("FROM [AspNetFunctionDecentralizations] as fr ");
            sb.Append("INNER JOIN [AspNetFunctions] as f ON f.Id = fr.FunctionId AND f.IsActivation=1 ");
            sb.Append("WHERE fr.[RoleId]=@roleId ");
            sb.Append("AND f.ModuleId=@moduleId ");
            sb.Append("AND fr.UserTypeId=@userTypeId ");
            var queryModule = sb.ToString();
            foreach (var permission in userType.Roles)
            {
                permission.children = await connection.QueryAsync<TreeViewNode>(query, new
                {
                    userTypeId = userType.Id,
                    roleId     = permission.id
                });
                foreach (var module in permission.children)
                {
                    module.children = await connection.QueryAsync<TreeViewNode>(queryModule, new
                    {
                        userTypeId = userType.Id,
                        roleId     = permission.id,
                        moduleId   = module.id
                    });
                    module.id = $"{module.id}_{permission.id}";
                }
            }

            return userType;
        }
    }

    public async Task<UserTypeModel> GetUserTypeByRoleIdentityAsync(string roleIdentity)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var sb = new StringBuilder();

            sb.Append("SELECT f.[Id], f.[Name] ");

            sb.Append("FROM [AspNetUserTypes] as f ");
            sb.Append("INNER JOIN [AspNetRoles] as r ON r.[Id]=f.[RoleId] AND r.[Identity]=@roleIdentity ");
            sb.Append("WHERE f.IsDeleted=0 AND f.[IsDefault]=1 ");

            var query = sb.ToString();
            var result = await connection.QueryAsync<UserTypeModel>(query, new
            {
                roleIdentity
            });
            return result.FirstOrDefault();
        }
    }

    public async Task<List<CustomModuleDecentralizationModel>> GetModulesByUserTypeIdAsync(string userTypeId)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var sb = new StringBuilder();
            sb.Append("SELECT p.ModuleId, p.RoleId FROM [AspNetModuleDecentralizations] as p ");
            sb.Append("WHERE p.UserTypeId=@userTypeId ");

            var query = sb.ToString();
            var result = await connection.QueryAsync<CustomModuleDecentralizationModel>(query, new
            {
                userTypeId
            });
            return result.ToList();
        }
    }

    public async Task DeleteModulesByUserTypeIdAsync(string userTypeId, List<string> modules)
    {
        if (modules.Count == 0)
            return;

        using (var connection = new SqlConnection(_connectionString))
        {
            var sb = new StringBuilder();
            sb.Append("BEGIN TRANSACTION ");
            sb.Append("DELETE [AspNetModuleDecentralizations] ");
            sb.Append("WHERE UserTypeId=@userTypeId ");
            sb.Append("AND ModuleId IN (");
            for (var i = 0; i < modules.Count; i++)
                if (i != modules.Count - 1)
                    sb.Append($"'{modules[i]}',");
                else
                    sb.Append($"'{modules[i]}') ");

            sb.Append("COMMIT TRANSACTION ");
            var query = sb.ToString();
            var result = await connection.ExecuteAsync(query, new
            {
                userTypeId
            });
            await Task.CompletedTask;
        }
    }

    public async Task<List<CustomFunctionDecentralizationModel>> GetFunctionsByUserTypeIdAsync(string userTypeId)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var sb = new StringBuilder();
            sb.Append("SELECT p.FunctionId, p.RoleId FROM [AspNetFunctionDecentralizations] as p ");
            sb.Append("WHERE p.UserTypeId=@userTypeId ");

            var query = sb.ToString();
            var result = await connection.QueryAsync<CustomFunctionDecentralizationModel>(query, new
            {
                userTypeId
            });
            return result.ToList();
        }
    }

    public async Task DeleteFunctionsByUserTypeIdAsync(string userTypeId, List<string> functions)
    {
        if (functions.Count == 0)
            return;

        using (var connection = new SqlConnection(_connectionString))
        {
            var ids = string.Join(",", functions);
            var sb  = new StringBuilder();
            sb.Append("BEGIN TRANSACTION ");
            sb.Append("DELETE [AspNetFunctionDecentralizations] ");
            sb.Append("WHERE UserTypeId=@userTypeId ");
            sb.Append("AND FunctionId IN (");
            for (var i = 0; i < functions.Count; i++)
                if (i != functions.Count - 1)
                    sb.Append($"'{functions[i]}',");
                else
                    sb.Append($"'{functions[i]}') ");

            sb.Append("COMMIT TRANSACTION ");
            var query = sb.ToString();
            var result = await connection.ExecuteAsync(query, new
            {
                userTypeId
            });
            await Task.CompletedTask;
        }
    }

    public async Task EditAccountsWhenDeleteUserType(string userTypeIdOld, string userTypeIdNew,
                                                     string userId = null)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var sb = new StringBuilder();
            sb.Append("BEGIN TRANSACTION ");
            sb.Append("UPDATE [AspNetUsers] ");
            sb.Append("SET [UserTypeId]=@userTypeIdNew ");

            sb.Append("WHERE [UserTypeId]=@userTypeIdOld ");
            sb.Append("AND [IsDeleted]=0 ");
            if (!string.IsNullOrEmpty(userId)) sb.Append("AND [ManagementUnitId]=@userId ");
            sb.Append("COMMIT TRANSACTION ");
            var query = sb.ToString();
            var result = await connection.ExecuteAsync(query, new
            {
                userTypeIdOld,
                userTypeIdNew
            });
        }
    }

    public async Task<List<PortalModel>> GetPortalsAsync(UrlQuery urlQuery)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var sb = new StringBuilder();

            sb.Append("SELECT f.[Id], f.[Name], f.[Identity], f.[IsDepartmentOfEducation] ");

            sb.Append("FROM [AspNetPortals] as f ");
            sb.Append("WHERE [IsDeleted]=0 ");
            if (!string.IsNullOrEmpty(urlQuery.Keyword))
                sb.Append("AND (f.[Name] COLLATE Latin1_General_CI_AI LIKE N'%'+@keyWord+'%') ");

            sb.Append("ORDER BY f.[Id] DESC OFFSET @pageSize  *(@pageNumber - 1) ");
            sb.Append("ROWS FETCH NEXT @pageSize ROWS ONLY  ");
            var query = sb.ToString();
            var result = await connection.QueryAsync<PortalModel>(query, new
            {
                keyWord    = urlQuery.Keyword,
                pageSize   = urlQuery.PageSize,
                pageNumber = urlQuery.PageNumber
            });
            return result.ToList();
        }
    }

    public async Task<int> CountPortalAsync(UrlQuery urlQuery)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var sb = new StringBuilder();

            sb.Append("SELECT Count(f.[Id]) ");

            sb.Append("FROM [AspNetPortals] as f ");

            sb.Append("WHERE [IsDeleted]=0 ");
            if (!string.IsNullOrEmpty(urlQuery.Keyword))
                sb.Append("AND (f.[Name] COLLATE Latin1_General_CI_AI LIKE N'%'+@keyWord+'%') ");

            var query = sb.ToString();
            var result = await connection.QueryAsync<int>(query, new
            {
                keyWord = urlQuery.Keyword
            });
            return result.FirstOrDefault();
        }
    }

    public async Task<List<PortalModel>> GetPortalsAsync(bool isDepartmentOfEducation = true)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var sb = new StringBuilder();

            sb.Append("SELECT f.[Id], f.[Name], f.[Identity] ");

            sb.Append("FROM [AspNetPortals] as f ");
            sb.Append("WHERE [IsDeleted]=0 ");
            sb.Append("AND [IsDepartmentOfEducation]=@isDepartmentOfEducation ");

            var query = sb.ToString();
            var result = await connection.QueryAsync<PortalModel>(query, new
            {
                isDepartmentOfEducation
            });
            return result.ToList();
        }
    }

    public async Task<UserTypeDetailModel> GetPermissionsByUserIdGroupByModuleGroupAsync(string userId)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var sb = new StringBuilder();


            sb.Append("SELECT ut.[Id], ut.[Name], ut.[IsSuperAdmin], ut.[RoleId], ut.[IsDefault] ");

            sb.Append("FROM [AspNetUserTypes] as ut ");
            sb.Append(
                "INNER JOIN [AspNetUsers] as u ON u.[UserTypeId]=ut.[Id] AND u.[Id]=@userId AND u.[IsDeleted]=0 ");
            sb.Append("WHERE ut.IsDeleted=0 ");

            var query = sb.ToString();
            var result = await connection.QueryAsync<UserTypeDetailModel>(query, new
            {
                userId
            });
            var userType = result.FirstOrDefault();
            if (userType == null) return null;

            sb.Clear();

            //get all roleId
            sb.Append("SELECT DISTINCT [RoleId] as id ");

            sb.Append("FROM [AspNetModuleDecentralizations] ");
            sb.Append("WHERE UserTypeId=@userTypeId ");
            sb.Append("UNION ");

            sb.Append("SELECT DISTINCT [RoleId] as id ");

            sb.Append("FROM [AspNetFunctionDecentralizations] ");
            sb.Append("WHERE UserTypeId=@userTypeId ");
            query = sb.ToString();

            sb.Clear();
            sb.Append("SELECT [Id] as id, [Name] as text ");
            sb.Append("FROM [AspNetRoles] as rl ");

            sb.Append("WHERE rl.Id IN ( " + query + " ) ");

            query = sb.ToString();
            userType.Roles = await connection.QueryAsync<TreeViewNode>(query, new
            {
                userTypeId = userType.Id
            });

            sb.Clear();
            sb.Append("SELECT m.Id , m.[Name],g.Id as GroupId,g.Name as GroupName , mr.RoleId ");

            sb.Append("FROM [AspNetModuleDecentralizations] as mr ");
            sb.Append("INNER JOIN [AspNetModules] as m ON m.Id = mr.ModuleId AND m.IsActivation=1 ");
            sb.Append("INNER JOIN [AspNetModuleGroups] as g ON g.Id = m.ModuleGroupId  ");
            sb.Append("WHERE mr.RoleId=@roleId ");
            sb.Append("AND mr.UserTypeId=@userTypeId ");

            query = sb.ToString();
            //List<ModuleModel> modules = new List<ModuleModel>();
            //// lấy danh sách module
            //foreach (var permission in userType.Roles)
            //{
            //    var rmodules = await connection.QueryAsync<ModuleModel>(query, new { userTypeId = userType.Id, roleId = permission.id });
            //    modules.AddRange(rmodules.ToList());
            //}
            //var moduleGroups = modules.GroupBy(x => x.GroupId).ToList();
            //List<TreeViewNode> groupTreeviews = new List<TreeViewNode>();
            //foreach (var item in moduleGroups)
            //{

            //    var items = item.ToList();
            //    if (items.Count > 0)
            //    {
            //        TreeViewNode group = new TreeViewNode();
            //        group.id = items[0].GroupId;
            //        group.text = items[0].GroupName;
            //        group.key = "Group";
            //        List<TreeViewNode> childLevel = new List<TreeViewNode>();

            //        foreach (var childItem in items)
            //        {
            //            childLevel.Add(new TreeViewNode() { key = "Module", id = childItem.Id, text = childItem.Name });
            //        }
            //        group.children = childLevel;
            //        groupTreeviews.Add(group);
            //    }
            //}

            sb.Clear();
            sb.Append("SELECT fr.FunctionId as id, f.[Name] as text ");

            sb.Append("FROM [AspNetFunctionDecentralizations] as fr ");
            sb.Append("INNER JOIN [AspNetFunctions] as f ON f.Id = fr.FunctionId AND f.IsActivation=1 ");
            sb.Append("WHERE fr.[RoleId]=@roleId ");
            sb.Append("AND f.ModuleId=@moduleId ");
            sb.Append("AND fr.UserTypeId=@userTypeId ");
            sb.Append("ORDER BY f.[Weight] ");
            var queryModule = sb.ToString();
            foreach (var permission in userType.Roles)
            {
                var modules = new List<ModuleModel>();
                // danh sách module từ role

                var rmodules = await connection.QueryAsync<ModuleModel>(query, new
                {
                    userTypeId = userType.Id,
                    roleId     = permission.id
                });

                modules.AddRange(rmodules.ToList());

                var moduleGroups   = modules.GroupBy(x => x.GroupId).ToList();
                var groupTreeviews = new List<TreeViewNode>();
                foreach (var item in moduleGroups)
                {
                    var items = item.ToList();
                    if (items.Count > 0)
                    {
                        var groupTreeView = new TreeViewNode();
                        groupTreeView.id   = items[0].GroupId;
                        groupTreeView.text = items[0].GroupName;
                        groupTreeView.key  = "Group";
                        var moduleTreeviews = new List<TreeViewNode>();

                        foreach (var childItem in items)
                        {
                            var moduleTreeview = new TreeViewNode
                            {
                                key  = "Module",
                                id   = childItem.Id,
                                text = childItem.Name
                            };

                            moduleTreeview.children = await connection.QueryAsync<TreeViewNode>(queryModule, new
                            {
                                userTypeId = userType.Id,
                                roleId     = permission.id,
                                moduleId   = moduleTreeview.id
                            });
                            moduleTreeview.children.ForEach(x => x.key = "Function");
                            moduleTreeviews.Add(moduleTreeview);
                        }

                        groupTreeView.children = moduleTreeviews;

                        groupTreeviews.Add(groupTreeView);
                    }
                }

                permission.children = groupTreeviews;
                // permission.children = await connection.QueryAsync<TreeViewNode>(query, new { userTypeId = userType.Id, roleId = permission.id });
            }

            return userType;
            //foreach (var group in groupTreeviews)
            //{
            //    foreach (var module in group.children)
            //    {
            //        var md = modules.FirstOrDefault(x => x.Id == module.id);
            //            if (md != null)
            //        {
            //            module.children = await connection.QueryAsync<TreeViewNode>(queryModule, new { userTypeId = userType.Id, roleId = md.RoleId, moduleId = module.id });
            //            module.children.ForEach(x => x.key = "Function");
            //        }
            //    }
            //}
            //userType.Roles = groupTreeviews;
            // return userType;
        }
    }

    public async Task<CustomUserTypeModel> GetAccountInfoByUserIdAsync(string userId)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var sb = new StringBuilder();


            sb.Append(
                "SELECT  r.[Name] as RoleIdentity,u.PortalId,u.[FullName],u.Email,pro.Name as Province,pro.Type as ProvinceType," +
                "u.UserTypeId, u.UserName ");

            sb.Append("FROM [AspNetUsers] u ");
            sb.Append("INNER JOIN UserTypes ut ON ut.Id = u.UserTypeId ");
            sb.Append("INNER JOIN UserTypeFeatures UTF ON ut.Id = UTF.UserTypeId ");
            sb.Append("INNER JOIN Features F ON UTF.FeatureId = F.Id ");
            sb.Append("INNER JOIN DefaultRoles r ON F.RoleId = r.Id ");
            sb.Append("LEft JOIN Province pro ON pro.Id = u.ProvinceId ");
            sb.Append("WHERE u.[IsDeleted]=0 AND u.[Id]=@userId ");
            var query = sb.ToString();
            var result = await connection.QueryAsync<CustomUserTypeModel>(query, new
            {
                userId
            });

            var customUserTypeModel = result.FirstOrDefault();
            if (customUserTypeModel == null)
                return null;

            customUserTypeModel.UserId = userId;
            sb.Clear();
            sb.Append("SELECT u.Id as BaseUnitUserId, p.IsDepartmentOfEducation ");
            sb.Append("FROM [AspNetUsers] u ");
            sb.Append("INNER JOIN [DepartmentOfEducation] d ON u.[Id]=d.[UserId] ");
            sb.Append("INNER JOIN [AspNetPortals] p ON u.PortalId=p.Id ");
            sb.Append("WHERE u.[IsDeleted]=0 AND d.[IsDeleted]=0 and u.PortalId=@portalId ");
            query = sb.ToString();
            var baseUnit = await connection.QueryAsync<CustomUserTypeModel>(query, new
            {
                portalId = customUserTypeModel.PortalId
            });
            if (baseUnit.Count() > 0)
            {
                var baseUnitModel = baseUnit.FirstOrDefault();
                customUserTypeModel.BaseUnitUserId = baseUnitModel.BaseUnitUserId;
                if (baseUnitModel.IsDepartmentOfEducation)
                    customUserTypeModel.BaseRoleIdentity = RoleIdentity.DepartmentOfEducation;
                else
                    customUserTypeModel.BaseRoleIdentity = RoleIdentity.EducationDepartment;
            }

            //Lấy năm học
            sb.Clear();
            sb.Append(
                "SELECT * FROM [SchoolYear] WHERE [EndTime]=@finalYear and [IsDeleted]=0 and [PortalId]=@portalId");
            query = sb.ToString();
            var schoolYears = await connection.QueryAsync<dynamic>(query, new
            {
                finalYear = DateTime.Now.Year,
                portalId  = customUserTypeModel.PortalId
            });
            if (schoolYears != null && schoolYears.Count() > 0)
            {
                var schoolYear = schoolYears.FirstOrDefault();
                customUserTypeModel.SchoolYear = schoolYear.Id;
            }

            sb.Clear();
            //Unit
            if (RoleIdentity.Administrator != customUserTypeModel.RoleIdentity)
            {
                sb.Append("SELECT g.[Id],u.[FullName] as ManagementName, g.[UserId] as UserId ");
                switch (customUserTypeModel.RoleIdentity)
                {
                    case RoleIdentity.DepartmentOfEducation:
                        sb.Append("FROM [DepartmentOfEducation] g ");
                        break;
                    case RoleIdentity.EducationDepartment:
                        sb.Append("FROM [EducationDepartment] g ");
                        break;
                    case RoleIdentity.Department:
                        sb.Append("FROM [Department] g ");
                        break;
                    case RoleIdentity.Bussiness:
                        sb.Append("FROM [Bussiness] g ");
                        break;
                    default:
                        //sb.Append(", l.Code as EducationLevelCode ");
                        sb.Append("FROM [Bussiness] g ");
                        //sb.Append("INNER JOIN [EducationLevel] l on l.Id=g.EducationLevelId ");
                        break;
                }

                sb.Append("INNER JOIN [AspNetUsers] u ON (u.[Id]=g.[UserId]) AND u.[Id]=@userId ");
                sb.Append("WHERE u.[IsDeleted]=0 ");

                query = sb.ToString();
                var units = await connection.QueryAsync<CustomManagement>(query, new
                {
                    userId
                });

                // tài khoản cấp dưới
                if (units.Count() == 0)
                {
                    sb.Clear();
                    sb.Append("SELECT g.[Id],u.[FullName] as ManagementName, g.[UserId] as UserId ");
                    switch (customUserTypeModel.RoleIdentity)
                    {
                        case RoleIdentity.DepartmentOfEducation:
                            sb.Append("FROM [DepartmentOfEducation] g ");
                            break;
                        case RoleIdentity.EducationDepartment:
                            sb.Append("FROM [EducationDepartment] g ");
                            break;
                        case RoleIdentity.Department:
                            sb.Append("FROM [Department] g ");
                            break;
                        case RoleIdentity.Bussiness:
                            sb.Append("FROM [Bussiness] g ");
                            break;
                        default:
                            //sb.Append(", l.Code as EducationLevelCode ");
                            sb.Append("FROM [Bussiness] g ");
                            //sb.Append("INNER JOIN [EducationLevel] l on l.Id=g.EducationLevelId ");
                            break;
                    }

                    sb.Append("INNER JOIN [AspNetUsers] u ON (u.[ManagementUnitId]=g.[UserId]) AND u.[Id]=@userId ");
                    sb.Append("WHERE u.[IsDeleted]=0 ");

                    query = sb.ToString();
                    units = await connection.QueryAsync<CustomManagement>(query, new
                    {
                        userId
                    });
                }

                var unit = units.FirstOrDefault();
                if (unit != null)
                {
                    customUserTypeModel.UnitId             = unit.Id.ToString();
                    customUserTypeModel.UnitUserId         = unit.UserId;
                    customUserTypeModel.EducationLevelCode = unit.EducationLevelCode;
                    // Lấy tên đơn vị quản lý
                    sb.Clear();
                    sb.Append("SELECT u.[FullName] as ManagementName From [AspNetUsers] u  where u.Id=@id ");
                    query = sb.ToString();
                    var results = await connection.QueryAsync<CustomManagement>(query, new
                    {
                        id = customUserTypeModel.UnitUserId
                    });
                    if (results.Count() > 0)
                    {
                        var mana = results.FirstOrDefault();
                        customUserTypeModel.ManagementName = mana.ManagementName;
                    }
                }

                sb.Clear();
            }

            sb.Clear();


            sb.Append("SELECT DISTINCT [RoleId] ");

            sb.Append("FROM [AspNetModuleDecentralizations] ");
            sb.Append("WHERE UserTypeId=@userTypeId ");
            sb.Append("UNION ");

            sb.Append("SELECT DISTINCT [RoleId] ");

            sb.Append("FROM [AspNetFunctionDecentralizations] ");
            sb.Append("WHERE UserTypeId=@userTypeId ");

            query = sb.ToString();
            sb.Clear();
            sb.Append("SELECT rl.[Identity] ");

            sb.Append("FROM [AspNetRoles] as rl ");
            sb.Append("WHERE Id IN (" + query + ") ");

            query = sb.ToString();

            customUserTypeModel.Roles = await connection.QueryAsync<string>(query, new
            {
                customUserTypeModel.UserTypeId
            });

            sb.Clear();

            sb.Append("SELECT m.[Identity] as Id, r.[Identity] as RoleIdentity, SUM(fn.[Weight]) as We ");
            sb.Append("FROM [AspNetFunctionDecentralizations] as fnd ");

            sb.Append("INNER JOIN [AspNetFunctions] as fn ON fn.[Id]=fnd.[FunctionId] ");
            sb.Append("INNER JOIN [AspNetModules] as m ON m.[Id]=fn.[ModuleId] ");
            sb.Append("INNER JOIN [AspNetRoles] as r ON r.[Id]=fnd.[RoleId] ");

            sb.Append("WHERE fnd.UserTypeId=@userTypeId ");
            sb.Append("GROUP BY m.[Identity], r.[Identity] ");

            query = sb.ToString();

            customUserTypeModel.Permissions = await connection.QueryAsync<CustomModuleModel>(query, new
            {
                customUserTypeModel.UserTypeId
            });
            return customUserTypeModel;
        }
    }

    public async Task<string> Excecute(string query)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var result = await connection.QueryAsync<dynamic>(query);
            var json   = JsonSerializer.Serialize(result.ToList());
            return json;
        }
    }

#endregion
}

public class ManageRoleQueryViewModel : UrlQuery
{
#region Properties

    public string Description { get; set; }
    public string Name        { get; set; }

#endregion
}