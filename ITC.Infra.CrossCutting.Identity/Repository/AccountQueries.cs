#region

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using ITC.Domain.Core.Pagination;
using ITC.Domain.Extensions;
using ITC.Infra.CrossCutting.Identity.Models;
using ITC.Infra.CrossCutting.Identity.Models.QueryModel;
using ITC.Infra.CrossCutting.Identity.Queries;
using Microsoft.Data.SqlClient;

#endregion

namespace ITC.Infra.CrossCutting.Identity.Repository;

public class AccountQueries : IAccountQueries
{
#region Fields

    private readonly string _connectionString;

#endregion

#region Constructors

    public AccountQueries(string connectionString)
    {
        _connectionString = connectionString;
    }

#endregion

#region IAccountQueries Members

    public async Task<List<ApplicationUser>> GetUsersAsync(List<string> users)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var sb = new StringBuilder();
            sb.Append("SELECT * ");
            sb.Append("FROM [AspNetUsers]  ");
            sb.Append("WHERE Id IN (");
            for (var i = 0; i < users.Count; i++)
                if (i != users.Count - 1)
                    sb.Append($"'{users[i]}',");
                else
                    sb.Append($"'{users[i]}') ");
            var query  = sb.ToString();
            var result = await connection.QueryAsync<ApplicationUser>(query);
            return result.ToList();
        }
    }


    // public async Task<List<AccountModel>> GetAccountsAsync(AccountQueryModel urlQuery)
    // {
    //     using (var connection = new SqlConnection(_connectionString))
    //     {
    //         var sb = new StringBuilder();
    //
    //         sb.Append("SELECT f.[Id], f.[UserName], f.[FullName], f.[Email], f.[PhoneNumber], f.[EmailConfirmed], f.[LockoutEnd], f.[PortalId], f.[UserTypeId], ut.[Name] as UserTypeName ");
    //
    //         sb.Append("FROM [AspNetUsers] as f ");
    //         sb.Append("INNER JOIN UserTypes as ut ON ut.[Id]=f.[UserTypeId] ");
    //         sb.Append("WHERE f.IsDeleted=0 ");
    //
    //
    //         if (!string.IsNullOrEmpty(urlQuery.UserName)) sb.Append($"AND f.[UserName] COLLATE Latin1_General_CI_AI LIKE N'%{urlQuery.UserName}%' ");
    //         if (!string.IsNullOrEmpty(urlQuery.FullName)) sb.Append($"AND f.[FullName] COLLATE Latin1_General_CI_AI LIKE N'%{urlQuery.FullName}%' ");
    //         if (!string.IsNullOrEmpty(urlQuery.PhoneNumber)) sb.Append($"AND f.[PhoneNumber] COLLATE Latin1_General_CI_AI LIKE N'%{urlQuery.PhoneNumber}%' ");
    //         if (urlQuery.PortalId > 0) sb.Append($"AND f.[PortalId] ={urlQuery.PortalId} ");
    //         if (!string.IsNullOrEmpty(urlQuery.UserType)) sb.Append($"AND ut.[Name] COLLATE Latin1_General_CI_AI LIKE N'%{urlQuery.UserType}%' ");
    //
    //         switch (urlQuery.Status)
    //         {
    //             case AccountStatus.Activated:
    //                 sb.Append("AND f.[EmailConfirmed]=1 ");
    //                 break;
    //             case AccountStatus.UnActive:
    //                 sb.Append("AND f.[EmailConfirmed]=0 ");
    //                 break;
    //             case AccountStatus.Locked:
    //                 sb.Append("AND f.[LockoutEnd] is not null ");
    //                 break;
    //         }
    //
    //         sb.Append("ORDER BY f.[CreateDate] DESC OFFSET @pageSize  *(@pageNumber - 1) ");
    //         sb.Append("ROWS FETCH NEXT @pageSize ROWS ONLY  ");
    //         var query = sb.ToString();
    //         var result = await connection.QueryAsync<AccountModel>(query, new
    //         {
    //             keyWord    = urlQuery.Keyword,
    //             pageSize   = urlQuery.PageSize,
    //             pageNumber = urlQuery.PageNumber
    //         });
    //         return result.ToList();
    //     }
    // }
    //
    // public async Task<int> CountAccountAsync(AccountQueryModel urlQuery)
    // {
    //     using (var connection = new SqlConnection(_connectionString))
    //     {
    //         var sb = new StringBuilder();
    //
    //         sb.Append("SELECT Count(f.[Id]) ");
    //
    //         sb.Append("FROM [AspNetUsers] as f ");
    //         sb.Append("INNER JOIN UserTypes as ut ON ut.[Id]=f.[UserTypeId] ");
    //         sb.Append("WHERE f.IsDeleted=0 ");
    //
    //         if (!string.IsNullOrEmpty(urlQuery.UserName)) sb.Append($"AND f.[UserName] COLLATE Latin1_General_CI_AI LIKE N'%{urlQuery.UserName}%' ");
    //         if (!string.IsNullOrEmpty(urlQuery.FullName)) sb.Append($"AND f.[FullName] COLLATE Latin1_General_CI_AI LIKE N'%{urlQuery.FullName}%' ");
    //         if (!string.IsNullOrEmpty(urlQuery.PhoneNumber)) sb.Append($"AND f.[PhoneNumber] COLLATE Latin1_General_CI_AI LIKE N'%{urlQuery.PhoneNumber}%' ");
    //         if (urlQuery.PortalId > 0) sb.Append($"AND f.[PortalId] ={urlQuery.PortalId} ");
    //         if (!string.IsNullOrEmpty(urlQuery.UserType)) sb.Append($"AND ut.[Name] COLLATE Latin1_General_CI_AI LIKE N'%{urlQuery.UserType}%' ");
    //         if (!string.IsNullOrEmpty(urlQuery.Active) && urlQuery.Active.ToLower() != "all")
    //         {
    //             if (urlQuery.Active.ToLower() == "true")
    //                 sb.Append("AND f.EmailConfirmed=1 ");
    //             else if (urlQuery.Active.ToLower() == "false") sb.Append("AND f.EmailConfirmed=0 ");
    //         }
    //
    //
    //         var query = sb.ToString();
    //         var result = await connection.QueryAsync<int>(query);
    //         return result.FirstOrDefault();
    //     }
    // }
    //
    // public async Task<List<PersonalAccountModel>> GetPersonnelAccountsAsync(UrlQuery urlQuery, string managementUnitId)
    // {
    //     using (var connection = new SqlConnection(_connectionString))
    //     {
    //         var sb = new StringBuilder();
    //
    //         sb.Append("SELECT f.[Id] as UserId, f.[UserName], hs.[Code] as PersonnelCode, f.[FullName],  f.[UserTypeId], ut.[Name] as UserTypeName, cv.[Name] as Position ");
    //
    //         sb.Append("FROM [AspNetUsers] as f ");
    //         sb.Append("INNER JOIN UserTypes as ut ON ut.[Id]=f.[UserTypeId] ");
    //         sb.Append("INNER JOIN [OfficalProfile] as hs ON hs.[Code]=f.[UserName] AND hs.[UserId]=f.[Id] AND hs.[ManagementId]=@managementUnitId ");
    //         sb.Append("INNER JOIN [Position] as cv ON cv.[Id]=hs.[PositionId] AND cv.[ManagementId]=@managementUnitId ");
    //
    //         sb.Append("WHERE f.IsDeleted=0 ");
    //         sb.Append("AND f.[ManagementUnitId]=@managementUnitId ");
    //
    //
    //         if (!string.IsNullOrEmpty(urlQuery.Keyword)) sb.Append("AND ((f.[FullName] COLLATE Latin1_General_CI_AI LIKE N'%'+@keyWord+'%') OR (f.[UserName] COLLATE Latin1_General_CI_AI LIKE N'%'+@keyWord+'%')) ");
    //
    //         sb.Append("ORDER BY f.[CreateDate] DESC OFFSET @pageSize  *(@pageNumber - 1) ");
    //         sb.Append("ROWS FETCH NEXT @pageSize ROWS ONLY  ");
    //         var query = sb.ToString();
    //         var result = await connection.QueryAsync<PersonalAccountModel>(query, new
    //         {
    //             keyWord    = urlQuery.Keyword,
    //             pageSize   = urlQuery.PageSize,
    //             pageNumber = urlQuery.PageNumber,
    //             managementUnitId
    //         });
    //         return result.ToList();
    //     }
    // }

    public async Task<int> CountPersonnelAccountAsync(UrlQuery urlQuery, string managementUnitId)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var sb = new StringBuilder();

            sb.Append("SELECT Count(f.[Id]) ");

            sb.Append("FROM [AspNetUsers] as f ");
            sb.Append("INNER JOIN [AspNetUserTypes] as ut ON ut.[Id]=f.[UserTypeId] ");
            sb.Append(
                "INNER JOIN [HSCanBo] as hs ON hs.[Ma]=f.[UserName] AND hs.[UserId]=f.[Id] AND hs.[Id_DVQuanLy]=@managementUnitId ");
            sb.Append("INNER JOIN [ChucVu] as cv ON cv.[Id]=hs.[Id_ChucVu] AND cv.[Id_DVQuanLy]=@managementUnitId ");

            sb.Append("WHERE f.IsDeleted=0 ");
            sb.Append("AND f.[ManagementUnitId]=@managementUnitId ");


            if (!string.IsNullOrEmpty(urlQuery.Keyword))
                sb.Append(
                    "AND ((f.[FullName] COLLATE Latin1_General_CI_AI LIKE N'%'+@keyWord+'%') OR (f.[UserName] COLLATE Latin1_General_CI_AI LIKE N'%'+@keyWord+'%')) ");


            var query = sb.ToString();
            var result = await connection.QueryAsync<int>(query, new
            {
                keyWord = urlQuery.Keyword,
                managementUnitId
            });
            return result.FirstOrDefault();
        }
    }

    public async Task<List<AccountModel>> GetUsersAsyncByPortalId(int portalId)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var sb = new StringBuilder();

            sb.Append(
                "SELECT f.[Id], f.[UserName], f.[FullName], f.[Email], f.[Avatar], f.[PortalId], f.[UserTypeId] ");

            sb.Append("FROM [AspNetUsers] as f ");
            sb.Append("WHERE f.IsDeleted=0 ");
            sb.Append("AND f.PortalId=@portalId ");

            sb.Append("ORDER BY f.[FullName] ");

            var query  = sb.ToString();
            var result = await connection.QueryAsync<AccountModel>(query, new { portalId });
            return result.ToList();
        }
    }

    public async Task<string> GetUnitIdByUserName(string userName)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var sb         = new StringBuilder();
            var identities = userName.Split("_");

            if (identities.Length == 2)
            {
                //sb.Append("SELECT f.[Id], f.ManagementId, f.[UserId], f.[Identifier] ");
                //sb.Append("FROM [DepartmentOfEducation] as f ");
                //sb.Append("WHERE f.IsDeleted=0 ");
                //sb.Append("AND f.Identifier=@param ");

                //sb.Append("UNION ALL ");

                //sb.Append("SELECT f.[Id], u.[ManagementUnitId] as ManagementId, f.[UserId], f.[Identifier] ");
                //sb.Append("FROM [EducationDepartment] as f ");
                //sb.Append("INNER JOIN [AspNetUsers] as u ON u.[Id]=f.[UserId] ");
                //sb.Append("WHERE f.IsDeleted=0 ");
                //sb.Append("AND f.Identifier=@param ");

                var query = GenerateDepartmentOfEducationAndEducationDepartment(sb);

                var resultCustomDOE =
                    await connection.QueryAsync<CustomDepartmentOfEducationModel>(query,
                        new { param = identities[0] });

                var customDOE = resultCustomDOE.FirstOrDefault();
                if (customDOE != null) return customDOE.UserId;
                return string.Empty;
            }

            if (identities.Length == 3)
            {
                var query = GenerateDepartmentOfEducationAndEducationDepartment(sb);

                var resultCustomDOE =
                    await connection.QueryAsync<CustomDepartmentOfEducationModel>(query,
                        new { param = identities[0] });

                sb.Clear();

                query = GenerateEducationDepartmentAndSchool(sb, resultCustomDOE);
                var resultCustom =
                    await connection.QueryAsync<CustomDepartmentOfEducationModel>(query,
                        new { param = identities[1] });
                var customObject = resultCustom.FirstOrDefault();
                if (customObject != null) return customObject.UserId;
                return string.Empty;
            }

            if (identities.Length == 4)
            {
                var query = GenerateDepartmentOfEducationAndEducationDepartment(sb);

                var resultCustomDOE =
                    await connection.QueryAsync<CustomDepartmentOfEducationModel>(query,
                        new { param = identities[0] });

                sb.Clear();

                query = GenerateEducationDepartmentAndSchool(sb, resultCustomDOE);
                var resultCustom =
                    await connection.QueryAsync<CustomDepartmentOfEducationModel>(query,
                        new { param = identities[1] });

                sb.Clear();
                var countCustom      = resultCustom.Count();
                var resultCustomList = resultCustom.ToList();
                sb.Append("SELECT f.[Id], u.[ManagementUnitId] as ManagementId, f.[UserId], f.[Identifier] ");
                sb.Append("FROM [School] as f ");
                sb.Append("INNER JOIN [AspNetUsers] as u ON u.[Id]=f.[UserId] ");
                sb.Append("WHERE f.IsDeleted=0 ");
                sb.Append("AND f.Identifier=@param ");

                sb.Append("AND u.ManagementUnitId IN (");
                for (var i = 0; i < countCustom; i++)
                    if (i != countCustom - 1)
                        sb.Append($"'{resultCustomList[i].UserId}',");
                    else
                        sb.Append($"'{resultCustomList[i].UserId}') ");

                query = sb.ToString();
                var resultSchool =
                    await connection.QueryAsync<CustomDepartmentOfEducationModel>(query,
                        new { param = identities[2] });
                var customObject = resultSchool.FirstOrDefault();
                if (customObject != null) return customObject.UserId;
                return string.Empty;
            }

            return string.Empty;
        }
    }

    public async Task<AccountInfoModel> GetAccountInfoAsync(string userId)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var sb = new StringBuilder();

            sb.Append(
                "SELECT f.[Id], f.[UserName],f.[PhoneNumber], f.[FullName], f.[Email], f.[Avatar], f.[PortalId], f.[UserTypeId], ");
            sb.Append(
                " (p.Type +' '+p.Name) as Province, (d.Type+' '+d.Name) as District, (w.Type+' '+w.Name) as Ward ");
            sb.Append("FROM [AspNetUsers] as f ");
            sb.Append("LEFT JOIN  [Province] as p on p.Id=f.ProvinceId ");
            sb.Append("LEFT JOIN  [District] as d on d.Id=f.DistrictId ");
            sb.Append("LEFT JOIN  [Ward] as w on w.Id=f.WardId ");
            sb.Append("WHERE f.IsDeleted=0 ");
            sb.Append("AND f.Id=@userId ");

            sb.Append("ORDER BY f.[FullName] ");

            var query   = sb.ToString();
            var result  = await connection.QueryAsync<AccountInfoModel>(query, new { userId });
            var account = result.FirstOrDefault();
            if (account != null)
            {
                if (string.IsNullOrEmpty(account.Province)) account.Province = string.Empty;
                if (string.IsNullOrEmpty(account.District)) account.District = string.Empty;
                if (string.IsNullOrEmpty(account.Ward)) account.Ward         = string.Empty;
            }

            return account;
        }
    }

    public async Task<ApplicationUser> GetManagementByPortalId(int portalId)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var sb = new StringBuilder();
            sb.Append("SELECT u.FullName, u.PhoneNumber, u.Address, u.Email ");
            sb.Append("FROM [AspNetUsers] as u ");
            sb.Append("INNER JOIN DepartmentOfEducation as d on d.UserId=u.Id  ");
            sb.Append("WHERE u.PortalId=@id and u.IsDeleted=0 and d.IsDeleted=0 ");
            var query  = sb.ToString();
            var result = await connection.QueryAsync<ApplicationUser>(query, new { id = portalId });
            if (result.Count() == 0)
            {
                sb = new StringBuilder();
                sb.Append("SELECT u.FullName, u.PhoneNumber, u.Address, u.Email ");
                sb.Append("FROM [AspNetUsers] as u ");
                sb.Append("INNER JOIN EducationDepartment as d on d.UserId=u.Id  ");
                sb.Append("WHERE u.PortalId=@id and u.IsDeleted=0 and d.IsDeleted=0 ");
                query  = sb.ToString();
                result = await connection.QueryAsync<ApplicationUser>(query, new { id = portalId });
            }

            return result.FirstOrDefault();
        }
    }

    public async Task<ApplicationUser> GetUnitUser(string userId)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var sb = new StringBuilder();
            sb.Append("SELECT ut.[RoleId], r.[Identity] as RoleIdentity,u.PortalId,u.[FullName],u.Email ");
            sb.Append("FROM [AspNetUsers] u ");
            sb.Append("INNER JOIN [AspNetUserTypes] ut ON ut.[Id]=u.[UserTypeId] ");
            sb.Append("INNER JOIN [AspNetRoles] as r ON r.[Id]=ut.[RoleId] ");
            sb.Append("WHERE u.[IsDeleted]=0 AND u.[Id]=@userId ");
            var query               = sb.ToString();
            var result              = await connection.QueryAsync<CustomUserTypeModel>(query, new { userId });
            var customUserTypeModel = result.FirstOrDefault();
            if (customUserTypeModel == null)
                return null;

            sb.Clear();

            //Unit
            if (RoleIdentity.Administrator != customUserTypeModel.RoleIdentity)
            {
                sb.Append("SELECT  g.[UserId] as Id ");
                switch (customUserTypeModel.RoleIdentity)
                {
                    case RoleIdentity.DepartmentOfEducation:
                        sb.Append("FROM [DepartmentOfEducation] g ");
                        break;
                    case RoleIdentity.EducationDepartment:
                        sb.Append("FROM [EducationDepartment] g ");
                        break;
                    default:
                        sb.Append("FROM [School] g ");
                        sb.Append("INNER JOIN [EducationLevel] l on l.Id=g.EducationLevelId ");
                        break;
                }

                sb.Append("INNER JOIN [AspNetUsers] u ON (u.[Id]=g.[UserId]) AND u.[Id]=@userId ");
                sb.Append("WHERE u.[IsDeleted]=0 ");

                query = sb.ToString();
                var units = await connection.QueryAsync<ApplicationUser>(query, new { userId });

                // tài khoản cấp dưới
                if (units.Count() == 0)
                {
                    sb.Clear();
                    sb.Append("SELECT  g.[UserId] as Id ");
                    switch (customUserTypeModel.RoleIdentity)
                    {
                        case RoleIdentity.DepartmentOfEducation:
                            sb.Append("FROM [DepartmentOfEducation] g ");
                            break;
                        case RoleIdentity.EducationDepartment:
                            sb.Append("FROM [EducationDepartment] g ");
                            break;
                        default:
                            sb.Append("FROM [School] g ");
                            break;
                    }

                    sb.Append("INNER JOIN [AspNetUsers] u ON (u.[ManagementUnitId]=g.[UserId]) AND u.[Id]=@userId ");
                    sb.Append("WHERE u.[IsDeleted]=0 ");

                    query = sb.ToString();
                    units = await connection.QueryAsync<ApplicationUser>(query, new { userId });
                }

                var unit = units.FirstOrDefault();
                return unit;
            }

            return null;
        }
    }

    public async Task<ApplicationUser> GetByOfficalProfile(string code, string managementId)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var sb = new StringBuilder();

            sb.Append(
                "SELECT f.[Id], f.[UserName], f.[FullName], f.[Email], f.[Avatar], f.[PortalId], f.[UserTypeId] ");

            sb.Append("FROM [AspNetUsers] as f ");
            sb.Append("INNER JOIN OfficalProfile o on o.Id=f.OfficalProfileId ");
            sb.Append("WHERE f.IsDeleted=0  and o.Code=@code and f.ManagementUnitId=@managementId ");
            sb.Append("and f.IsDeleted=0 and o.IsDeleted=0 ");
            var query = sb.ToString();
            var result = await connection.QueryAsync<ApplicationUser>(query, new
            {
                code, managementId
            });
            return result.FirstOrDefault();
        }
    }

#endregion

#region Methods

    private string GenerateDepartmentOfEducationAndEducationDepartment(StringBuilder sb)
    {
        sb.Append("SELECT f.[Id], f.[UserId], f.[Identifier] ");
        sb.Append("FROM [DepartmentOfEducation] as f ");
        sb.Append("WHERE f.IsDeleted=0 ");
        sb.Append("AND f.Identifier=@param ");

        sb.Append("UNION ALL ");

        sb.Append("SELECT f.[Id], f.[UserId], f.[Identifier] ");
        sb.Append("FROM [EducationDepartment] as f ");
        sb.Append("WHERE f.IsDeleted=0 ");
        sb.Append("AND f.Identifier=@param ");
        return sb.ToString();
    }

    private string GenerateEducationDepartmentAndSchool(StringBuilder sb,
                                                        IEnumerable<CustomDepartmentOfEducationModel>
                                                            resultCustomDOE)
    {
        sb.Append("SELECT f.[Id], f.[UserId], f.[Identifier] ");
        sb.Append("FROM [EducationDepartment] as f ");
        sb.Append("INNER JOIN [AspNetUsers] as u ON u.[Id]=f.[UserId] ");
        sb.Append("WHERE f.IsDeleted=0 ");
        sb.Append("AND f.Identifier=@param ");
        sb.Append("AND u.ManagementUnitId IN (");
        var countCustomDOE      = resultCustomDOE.Count();
        var resultCustomDOEList = resultCustomDOE.ToList();
        for (var i = 0; i < countCustomDOE; i++)
            if (i != countCustomDOE - 1)
                sb.Append($"'{resultCustomDOEList[i].UserId}',");
            else
                sb.Append($"'{resultCustomDOEList[i].UserId}') ");
        sb.Append("UNION ALL ");

        sb.Append("SELECT f.[Id], f.[UserId], f.[Identifier] ");
        sb.Append("FROM [School] as f ");
        sb.Append("INNER JOIN [AspNetUsers] as u ON u.[Id]=f.[UserId] ");
        sb.Append("WHERE f.IsDeleted=0 ");
        sb.Append("AND f.Identifier=@param ");
        sb.Append("AND u.ManagementUnitId IN (");
        for (var i = 0; i < countCustomDOE; i++)
            if (i != countCustomDOE - 1)
                sb.Append($"'{resultCustomDOEList[i].UserId}',");
            else
                sb.Append($"'{resultCustomDOEList[i].UserId}') ");

        return sb.ToString();
    }

#endregion
}