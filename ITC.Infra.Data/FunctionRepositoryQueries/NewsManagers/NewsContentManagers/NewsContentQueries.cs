#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using ITC.Domain.Core.ModelShare.HomeManager;
using ITC.Domain.Core.ModelShare.NewsManagers.NewsContentManagers;
using ITC.Domain.Interfaces.NewsManagers.NewsContentManagers;
using NCore.Actions;
using NCore.Helpers;
using NCore.Modals;

#endregion

namespace ITC.Infra.Data.FunctionRepositoryQueries.NewsManagers.NewsContentManagers;

/// <summary>
///     Bài viết queries
/// </summary>
public class NewsContentQueries : INewsContentQueries
{
    #region Fields

    private readonly string _connectionString;

    #endregion

    #region Constructors

    public NewsContentQueries(string connectionString)
    {
        _connectionString = connectionString;
    }

    #endregion

    /// <inheritdoc/>
    public async Task<IEnumerable<NewsContentPagingDto>> GetPaging(NewsContentPagingModel model,
                                                                   List<Guid> newsGroupId, List<string> userIds = null)
    {
        var sBuilderSql = new StringBuilder();

        sBuilderSql.Append(@"SELECT NC.Id,
                                           NC.StatusId,
                                           NC.Name,
                                           NC.Author AS OwnerName,
                                           NG.Name      AS NewsGroupName,
                                           NC.DateTimeStart,
                                           NC.Modified,
                                           NC.CreatedBy AS OwnerId,
                                           NC.AvatarLink,
                                           NC.AvatarLocal,
                                           NC.AvatarId,
                                           NC.LinkTree,
                                           NC.TimeAutoPost,
                                            NG.TypeId
                                FROM NewsContents NC
                                             INNER JOIN NewsGroups NG ON NC.NewsGroupId = NG.Id                          
                               WHERE NC.IsDeleted = 0 ");
        //Nếu có bộ lọc tác giả
        if (model.Author.CompareTo(Guid.Empty) != 0)
        {
            sBuilderSql.Append(" AND NC.CreatedBy = @author ");
        }
        else
        {
            //Phân quyền cấp dưới
            if (userIds != null && userIds.Any())
            {
                var listOfJoin = new NCoreHelper().convert_list_to_string(userIds);
                sBuilderSql.Append($" AND nc.CreatedBy in {listOfJoin} ");
            }
        }
        if (newsGroupId.Count > 0)
        {
            var listOfIdsJoined = new NCoreHelper().convert_list_to_string(newsGroupId);
            sBuilderSql.Append("AND NC.NewsGroupId IN " + listOfIdsJoined + " ");
        }

        sBuilderSql.Append(model.StatusId > 0 ? "AND NC.StatusId = @pStatusId " : " ");
        sBuilderSql.Append(SqlHelper.Search(new List<string>
        {
            "NC.Name",
            "NC.Author",
            "NG.Name"
        }, model.Search));
        sBuilderSql.Append(
            @" GROUP BY NC.Id, NC.StatusId, NC.Name, NC.Author, NG.Name, NC.DateTimeStart, NC.Modified, NC.CreatedBy, 
                                        NC.AvatarLink, NC.AvatarLocal, NC.AvatarId, NC.LinkTree, NC.TimeAutoPost,NG.TypeId ");
        var sBuilder = new StringBuilder();
        sBuilder.Append(SqlHelper.GeneralSqlBuilder(sBuilderSql.ToString()));
        sBuilder.Append("ORDER BY Modified DESC ");
        sBuilder.Append(SqlHelper.Paging(model.PageNumber, model.PageSize));
        var dictionary = new Dictionary<string, object>
        {
            {
                "@author", model.Author
            },
            {
                "@pStatusId", model.StatusId
            }
        };
        return await SqlHelper.RunDapperQueryAsync<NewsContentPagingDto>(_connectionString,
                                                                         sBuilder,
                                                                         new DynamicParameters(dictionary));
    }

    /// <inheritdoc cref="GetPagingAuto"/>
    public async Task<IEnumerable<NewsContentPagingDto>> GetPagingAuto(NewsContentPagingModel model, List<Guid> newsGroupId)
    {
        var sBuilderSql = new StringBuilder();
        // sBuilderSql.Append(@"SELECT NC.Id,
        //                                    NC.StatusId,
        //                                    NC.Name,
        //                                    NC.Summary,
        //                                    NC.Author,
        //                                    NC.UrlRootLink,
        //                                    NG.Name      AS NewsGroupName,
        //                                    NC.DateTimeStart,
        //                                    NC.Modified,
        //                                    NC.CreatedBy AS OwnerId,
        //                                    ANU.FullName AS OwnerName,
        //                                    SM.UserCode,
        //                                    NC.AvatarLink,
        //                                    NC.AvatarLocal,
        //                                    NC.AvatarId,
        //                                    NG.MetaTitle AS MetaGroup,
        //                                    NC.MetaTitle AS MetaName,
        //                                    NC.SecretKey AS MetaKey,
        //                                    NG.Domain,
        //                                    NC.LinkTree,
        //                                    NC.TimeAutoPost
        //                         FROM NewsContents NC
        //                                      INNER JOIN NewsGroups NG ON NC.NewsGroupId = NG.Id
        //                                      INNER JOIN AspNetUsers ANU ON NC.CreatedBy = ANU.Id                                
        //                                      INNER JOIN StaffManagers SM ON NC.CreatedBy = SM.UserId                            
        //                        WHERE NC.IsDeleted = 0 AND NC.LinkTree != '' AND NC.StatusId != @pStatusId ");
        sBuilderSql.Append(@"SELECT NC.Id,
                                           NC.StatusId,
                                           NC.Name,
                                           NC.Author,
                                           NG.Name      AS NewsGroupName,
                                           NC.DateTimeStart,
                                           NC.Modified,
                                           NC.CreatedBy AS OwnerId,
                                           NC.AvatarLink,
                                           NC.AvatarLocal,
                                            NC.Created,
                                           NC.AvatarId,
                                           NC.LinkTree,
                                           NC.TimeAutoPost,
                                            ANU.FullName AS OwnerName
                                FROM NewsContents NC
                                             INNER JOIN NewsGroups NG ON NC.NewsGroupId = NG.Id
                                             INNER JOIN AspNetUsers ANU ON NC.CreatedBy = ANU.Id                                
                                             INNER JOIN StaffManagers SM ON NC.CreatedBy = SM.UserId                            
                               WHERE NC.IsDeleted = 0 AND NC.LinkTree != '' AND NC.StatusId != @pStatusId ");
        if (model.Author.CompareTo(Guid.Empty) != 0) sBuilderSql.Append(" AND NC.CreatedBy = @author ");
        if (newsGroupId.Count > 0)
        {
            var listOfIdsJoined = new NCoreHelper().convert_list_to_string(newsGroupId);
            sBuilderSql.Append("AND NC.NewsGroupId IN " + listOfIdsJoined + " ");
        }

        sBuilderSql.Append(SqlHelper.Search(new List<string>
        {
            "NC.Name",
            "NC.Author",
            "NG.Name"
        }, model.Search));
        sBuilderSql.Append(
            @" GROUP BY NC.Id, NC.StatusId, NC.Name, NC.Author, NG.Name, NC.DateTimeStart, NC.Modified, NC.CreatedBy, NC.Created,
                                       NC.AvatarLink, NC.AvatarLocal, NC.AvatarId, NC.LinkTree, NC.TimeAutoPost,FullName ");
        var sBuilder = new StringBuilder();
        sBuilder.Append(SqlHelper.GeneralSqlBuilder(sBuilderSql.ToString()));
        sBuilder.Append($@"ORDER BY CASE
                                         WHEN TimeAutoPost IS NULL THEN 1
                                         ELSE 0
                                         END,
                                     TimeAutoPost ASC, Created DESC ");
        sBuilder.Append(SqlHelper.Paging(model.PageNumber, model.PageSize));
        var dictionary = new Dictionary<string, object>
        {
            {
                "@author", model.Author
            },
            {
                "@pStatusId", model.StatusId
            }
        };
        return await SqlHelper.RunDapperQueryAsync<NewsContentPagingDto>(_connectionString,
                                                                         sBuilder,
                                                                         new DynamicParameters(dictionary));
    }

    /// <inheritdoc/>
    public async Task<NewsContentPagingByIdDto> GetPagingById(Guid id)
    {
        var sBuilder = new StringBuilder();
        sBuilder.Append(@"SELECT NC.Id,
                                           NC.Name,
                                           SM.UserCode,
                                           NG.MetaTitle AS MetaGroup,
                                           NC.MetaTitle AS MetaName,
                                           NC.SecretKey AS MetaKey,
                                           NG.Domain, NG.TypeId
                                FROM NewsContents NC
                                             INNER JOIN NewsGroups NG ON NC.NewsGroupId = NG.Id 
                                             LEFT JOIN StaffManagers SM ON NC.CreatedBy = SM.UserId
                               WHERE NC.IsDeleted = 0 AND NC.Id = @id ");
        sBuilder.Append(
            @" GROUP BY NC.Id, NC.Name, NC.Author, NG.Name, NC.DateTimeStart, NC.Modified, NC.CreatedBy, 
                                        NC.AvatarLink, NC.AvatarLocal, NC.AvatarId, NG.MetaTitle, NC.MetaTitle, NC.SecretKey, NG.Domain, SM.UserCode, NG.TypeId ");
        var dictionary = new Dictionary<string, object>
        {
            {
                "@id", id
            }
        };
        return await SqlHelper.RunDapperQueryFirstOrDefaultAsync<NewsContentPagingByIdDto>(_connectionString,
                                                                                           sBuilder,
                                                                                           new DynamicParameters(dictionary));
    }

    /// <inheritdoc cref="GetPagingCombobox" />
    public async Task<IEnumerable<NewsContentPagingComboboxDto>> GetPagingCombobox(
        NewsContentPagingModel model, List<Guid> newsGroupId)
    {
        var sBuilderSql = new StringBuilder();
        sBuilderSql.Append(@"SELECT NC.SecretKey AS Id,
                                           NC.Name,
                                           NC.Summary,
                                           NC.Author,
                                           NG.Name      AS NewsGroupName,
                                           NC.DateTimeStart,
                                           NC.Modified
                                    FROM NewsContents NC
                                             INNER JOIN NewsGroups NG ON NC.NewsGroupId = NG.Id
                               WHERE NC.IsDeleted = 0 AND NC.NewsGroupTypeId = @typeId ");
        if (newsGroupId.Count > 0)
        {
            var listOfIdsJoined = new NCoreHelper().convert_list_to_string(newsGroupId);
            sBuilderSql.Append("AND NC.NewsGroupId IN " + listOfIdsJoined + " ");
        }

        sBuilderSql.Append(@$"AND NC.StatusId = {ActionStatusEnum.Active.Id} ");

        var sBuilder = new StringBuilder();
        sBuilder.Append(SqlHelper.GeneralSqlBuilder(sBuilderSql.ToString()));
        sBuilder.Append(SqlHelper.Search(new List<string>
        {
            "Name",
            "Author",
            "NewsGroupName"
        }, model.Search));
        sBuilder.Append("ORDER BY Modified DESC ");
        sBuilder.Append(SqlHelper.Paging(model.PageNumber, model.PageSize));
        var dictionary = new Dictionary<string, object>
        {
            {
                "@typeId", Guid.Empty //model.NewsGroupTypeId
            }
        };
        return await SqlHelper.RunDapperQueryAsync<NewsContentPagingComboboxDto>(_connectionString,
                                                                                 sBuilder,
                                                                                 new DynamicParameters(dictionary));
    }

    /// <inheritdoc cref="DeleteAsync" />
    public async Task<int> DeleteAsync(List<Guid> model)
    {
        return await SqlHelper.RunDapperExecuteAsync(_connectionString,
                                                     SqlHelper.DeleteListAsync("NewsContents", model));
    }

    /// <inheritdoc cref="NewsAuthor" />
    public async Task<IEnumerable<ComboboxModalString>> NewsAuthor()
    {
        var sBuilder = new StringBuilder();
        sBuilder.Append(@"select Distinct NC.CreatedBy AS Id, ANU.FullName AS Name
                            from NewsContents NC
                            inner join AspNetUsers ANU ON ANU.Id = NC.CreatedBy
                            where nc.IsDeleted = 0 ");
        return await SqlHelper.RunDapperQueryAsync<ComboboxModalString>(_connectionString,
                                                                        sBuilder);
    }

    /// <inheritdoc cref="AutoPostFaceList"/>
    public async Task<IEnumerable<AutoPostFaceModel>> AutoPostFaceList()
    {
        var sBuilder = new StringBuilder();
        sBuilder.Append(@$"SELECT NC.Id,
                                   NC.Name,
                                   NV.IdTkQc   AS TkqcId,
                                   NV.Token    AS Token,
                                   NG.LinkTree AS PageId,
                                   NC.AvatarLocal,
                                   NC.AvatarLink,
                                   NC.LinkTree AS LinkUrl,
                                   NG.TypeId
                            FROM NewsContents NC
                                     INNER JOIN NewsGroups NG ON NC.NewsGroupId = NG.Id
                                     INNER JOIN NewsVias NV ON NG.IdViaQc = NV.Id
                            WHERE NC.StatusId != {ActionStatusEnum.Active.Id} 
                                    AND DATEDIFF(minute , NC.TimeAutoPost, getdate()) > -10 AND DATEDIFF(minute , NC.TimeAutoPost, getdate()) < 10;
                            Update NewsContents  set StatusId = {ActionStatusEnum.Active.Id} WHERE StatusId != {ActionStatusEnum.Active.Id} 
                                    AND DATEDIFF(minute , TimeAutoPost, getdate()) > -10 AND DATEDIFF(minute , TimeAutoPost, getdate()) < 10 ");
        // => tối đa 12 phút
        return await SqlHelper.RunDapperQueryAsync<AutoPostFaceModel>(_connectionString, sBuilder);
    }

    /// <inheritdoc cref="SaveDomain"/>
    public async Task<int> SaveDomain(StringBuilder sBuilder)
    {
        return await SqlHelper.RunDapperExecuteAsync(_connectionString, sBuilder);
    }

    /// <inheritdoc cref="GetDetail"/>
    public async Task<NewsMainModel> GetDetail(string id)
    {
        var sBuilder = new StringBuilder();
        sBuilder.Append(@"SELECT s.UserCode,NC.Name, NC.Content, NC.AvatarLink,NC.UrlRootLink, NC.AvatarLocal, NC.DateTimeStart,NC.Summary, NC.IsDeleted 
                            FROM NewsContents NC
							left join StaffManagers s ON s.UserId=nc.CreatedBy
                            WHERE NC.SecretKey = @id ");
        var dictionary = new Dictionary<string, object>
        {
            {
                "@id", id
            }
        };
        return await SqlHelper.RunDapperQueryFirstOrDefaultAsync<NewsMainModel>(_connectionString,
                                                                                sBuilder,
                                                                                new DynamicParameters(dictionary));
    }

    public async Task<NewsThreadModel> GetDetailThread(string categoryId, int position)
    {
        var sBuilder = new StringBuilder();
        sBuilder.Append(@$"	SELECT  
                            NC.Name,NC.AvatarLink,NC.LinkTree,
                            SM.UserCode,
                            NG.MetaTitle AS MetaGroup,
                            NC.MetaTitle AS MetaName,
                            NC.SecretKey AS MetaKey,
                            NG.Domain		
                            FROM NewsContents NC
                                INNER JOIN NewsGroups NG ON NC.NewsGroupId = NG.Id 
                                LEFT JOIN StaffManagers SM ON NC.CreatedBy = SM.UserId
                            WHERE NC.IsDeleted = 0 
                                AND NG.Id = '{categoryId}'
                            ORDER BY NC.Created DESC
                            OFFSET {position} ROWS FETCH NEXT 1 ROWS ONLY; ");
        
        return await SqlHelper.RunDapperQueryFirstOrDefaultAsync<NewsThreadModel>(_connectionString,sBuilder);
    }

    /// <inheritdoc/>
    public async Task<List<NewsGroupMainModel>> ListContentByGroup(List<Guid> groupModel, int numberOf)
    {
        var listReturn = new List<NewsGroupMainModel>();

        // Convert List<Guid> to List<string>
        var stringGroupModel = groupModel.Select(guid => $@"'{guid}'").ToList();

        // Join the List<string> into a single string
        var map = string.Join(", ", stringGroupModel);

        var sBuilder = new StringBuilder();
        sBuilder.Append(@$"WITH RankedNews AS (
                                        SELECT NC.SecretKey AS Id, NC.Name,
                                            NC.AvatarLink, NC.AvatarLocal,
                                            NC.DateTimeStart, NG.Name AS GroupName,
                                            ROW_NUMBER() OVER (PARTITION BY NG.Name ORDER BY NC.DateTimeStart DESC) AS RowNum
                                        FROM
                                            NewsContents NC
                                            INNER JOIN NewsGroups NG ON NC.NewsGroupId = NG.Id
                                        WHERE
                                            NC.IsDeleted = 0 and  NC.Created < DATEADD(day, -15, GETDATE()) AND NG.Id IN ({map}))
                            SELECT Id, Name, AvatarLink, AvatarLocal, DateTimeStart, GroupName
                            FROM
                                RankedNews
                            WHERE
                                RowNum <= {numberOf}; ");
        var result = await SqlHelper.RunDapperQueryAsync<NewsGroupMainModel>(_connectionString, sBuilder);
        listReturn.AddRange(result);

        /*foreach (var items in groupModel)
        {
            var sBuilder = new StringBuilder();
            sBuilder.Append(@$"SELECT TOP {numberOf} NC.SecretKey AS Id,
                                         NC.Name,
                                         NC.AvatarLink,
                                         NC.AvatarLocal,
                                         NC.DateTimeStart,
                                         NG.Name      AS GroupName
                            FROM NewsContents NC
                                     INNER JOIN NewsGroups NG ON NC.NewsGroupId = NG.Id
                            WHERE NC.IsDeleted = 0 AND NG.Id = '{items}' ");
            var result =
                await SqlHelper.RunDapperQueryAsync<NewsGroupMainModel>(_connectionString, sBuilder);
            listReturn.AddRange(result);
        }*/

        return listReturn;
    }

    /// <inheritdoc cref="HomeNewsLifeModel"/>
    public async Task<HomeNewsLifeModel> HomeNewsLifeModel(string id)
    {
        var newsLifeConnect = "Server=103.149.87.30\\SQLEXPRESS,1433;Initial Catalog=News;User Id=user_news;Password=thien@123456@;TrustServerCertificate=True;";
        var sBuilder = new StringBuilder();
        sBuilder.Append(
            @$"select  a.TenBaiDang,a.AnhDaiDien,t.TenTheLoai,t.IDPage,v.IdQC,v.Token 
                from BAIDANG a join THELOAI t ON a.IDTheLoai=t.IDTheLoai join VIA v ON t.IDVia = v.Id 
                WHERE a.IDBaiDang = '{id}' ");
        return
            await SqlHelper.RunDapperQueryFirstOrDefaultAsync<HomeNewsLifeModel>(newsLifeConnect, sBuilder);
    }

    /// <inheritdoc cref="GetHomeNewsContent" />
    public async Task<IEnumerable<HomeNewsContentDto>> GetHomeNewsContent(int statusId)
    {
        var sBuilder = new StringBuilder();
        sBuilder.Append(@"WITH dTemp AS (SELECT HNGVD.NewsGroupId,
                                                      NG.Name      AS NewsGroupName,
                                                      NG.SecretKey AS NewsGroupKey,
                                                      NGT.Name     AS NewsGroupTypeName,
                                                      NGT.Name     AS NewsGroupTypeKey
                                               FROM HomeNewsGroupViews HNGV
                                                        INNER JOIN HomeNewsGroupViewDetails HNGVD ON HNGV.Id = HNGVD.HomeNewsGroupViewId
                                                        INNER JOIN NewsGroups NG ON HNGVD.NewsGroupId = NG.Id
                                                        INNER JOIN NewsGroupTypes NGT ON NGT.Id = NG.NewsGroupTypeId
                                               WHERE HNGV.StatusId = @pStatusId AND HNGV.IsDeleted = 0
                                               GROUP BY HNGVD.NewsGroupId, NG.Name, NG.SecretKey, NGT.Name)
                                SELECT TOP 8 NC.SecretKey   AS Id,
                                       NC.Name,
                                       NC.Author,
                                       NC.UrlRootLink AS Link,
                                       dT.NewsGroupTypeName,
                                       dT.NewsGroupTypeKey,
                                       dT.NewsGroupName,
                                       dT.NewsGroupKey,
                                       NC.DateTimeStart,
                                       NC.ViewEye,
                                       NC.AvatarLink,
                                       NC.AvatarLocal
                                FROM NewsContents NC
                                         INNER JOIN dTemp dT ON dT.NewsGroupId = NC.NewsGroupId
                                WHERE NC.IsDeleted = 0 AND NC.StatusId = @pStatusId ");
        sBuilder.Append("ORDER BY DateTimeStart DESC ");
        var dictionary = new Dictionary<string, object>
        {
            {
                "@pStatusId", statusId
            }
        };
        return await SqlHelper.RunDapperQueryAsync<HomeNewsContentDto>(_connectionString,
                                                                       sBuilder,
                                                                       new DynamicParameters(dictionary));
    }

    /// <inheritdoc cref="GetHomeViewContent" />
    public async Task<HomeNewsContentViewDto> GetHomeViewContent(Guid projectId, string id)
    {
        var sBuilder = new StringBuilder();
        sBuilder.Append(@$"
                                UPDATE NewsContents SET ViewEye = ViewEye + 1 WHERE SecretKey = '{id}';
                                SELECT NC.Name,
                                       NC.SecretKey   AS Id,
                                       NC.Summary,
                                       NC.Content,
                                       NC.Author,
                                       NC.UrlRootLink AS Link,
                                       NC.SeoKeyword,
                                       NC.DateTimeStart,
                                       NC.ViewEye,
                                       SF.IsLocal,
                                       SF.FilePath,
                                       SF.LinkUrl,
                                       NG.SecretKey   AS NewsGroupId,
                                       NG.Name        AS NewsGroupName,
                                       NGT.SecretKey  AS NewsGroupTypeId,
                                       NGT.Name       AS NewsGroupTypeName,
                                       COUNT(CM.Id)   AS Review
                                FROM NewsContents NC
                                         LEFT JOIN ServerFiles SF ON NC.AvatarId = SF.Id
                                         INNER JOIN NewsGroups NG ON NC.NewsGroupId = NG.Id
                                         INNER JOIN NewsGroupTypes NGT ON NC.NewsGroupTypeId = NGT.Id
                                         LEFT JOIN CommentManagers CM ON CM.IsDeleted = 0 AND CM.StatusId = 3 AND CM.ProductId = NC.Id
                                WHERE NC.IsDeleted = 0 AND NC.StatusId = {ActionStatusEnum.Active.Id} 
                                  AND NC.SecretKey = '{id}' AND NC.ProjectId = '{projectId}' 
                                GROUP BY NC.Name, NC.SecretKey, NC.Summary, NC.Content, NC.Author, NC.UrlRootLink, NC.SeoKeyword, NC.DateTimeStart, NC.ViewEye, SF.IsLocal, SF.FilePath, SF.LinkUrl, NG.SecretKey, NG.Name, NGT.SecretKey, NGT.Name ");
        return await SqlHelper.RunDapperQueryFirstOrDefaultAsync<HomeNewsContentViewDto>(_connectionString,
                                                                                         sBuilder);
    }

    /// <inheritdoc cref="GetHomeViewContentTop5" />
    public async Task<IEnumerable<HomeNewsContentTop5Dto>> GetHomeViewContentTop5(
        string id, int statusId, Guid newGroupId)
    {
        var sBuilder = new StringBuilder();
        sBuilder.Append(@"SELECT TOP 5 NC.SecretKey AS Id,
                                             NC.Name,
                                             NC.DateTimeStart,
                                             NC.Author
                                FROM NewsContents NC
                                WHERE NC.IsDeleted = 0 AND NC.StatusId = @pStatusId 
                                  AND NC.SecretKey != @pId ");
        if (string.Compare(newGroupId.ToString(), Guid.Empty.ToString(), StringComparison.Ordinal) != 0)
            sBuilder.Append(" AND NC.NewsGroupId = @pNewsGroupId ");

        sBuilder.Append(" ORDER BY NC.DateTimeStart DESC ");
        var dictionary = new Dictionary<string, object>
        {
            {
                "@pId", id
            },
            {
                "@pStatusId", statusId
            },
            {
                "@pNewsGroupId", newGroupId
            }
        };
        return await SqlHelper.RunDapperQueryAsync<HomeNewsContentTop5Dto>(_connectionString,
                                                                           sBuilder,
                                                                           new DynamicParameters(dictionary));
    }

    /// <inheritdoc cref="Top5ViewEye" />
    public async Task<IEnumerable<Top5ViewEye>> Top5ViewEye(Guid projectId)
    {
        var sBuilder = new StringBuilder();
        sBuilder.Append(@"SELECT TOP 5 NC.SecretKey AS Id,
                                             NC.Name,
                                             NC.Author,
                                             NC.ViewEye,
                                             SF.IsLocal,
                                             SF.FilePath,
                                             SF.LinkUrl
                                FROM NewsContents NC
                                    LEFT JOIN ServerFiles SF ON NC.AvatarId = SF.Id
                                WHERE NC.IsDeleted = 0 AND NC.StatusId = @pStatusId AND NC.ProjectId = @projectId ");
        sBuilder.Append(" ORDER BY NC.ViewEye DESC ");
        var dictionary = new Dictionary<string, object>
        {
            {
                "@pStatusId", ActionStatusEnum.Active.Id
            },
            {
                "@projectId", projectId
            }
        };
        return await SqlHelper.RunDapperQueryAsync<Top5ViewEye>(_connectionString,
                                                                sBuilder,
                                                                new DynamicParameters(dictionary));
    }

    public async Task<bool> UpdateThread(string profile)
    {
        var sBuilder = new StringBuilder();
        sBuilder.Append(@$"Update ProfileThread set Position=Position+1,ModifiedDate='{DateTime.Now}' where Profile='{profile}'");
        
        return await SqlHelper.RunDapperQueryFirstOrDefaultAsync<bool>(_connectionString,sBuilder);
    }

    public async Task<int> GetPositionThread(string profile)
    {
        var sBuilder = new StringBuilder();
        sBuilder.Append(@$"Select top(1) Position from ProfileThread  where Profile='{profile}'");

        return await SqlHelper.RunDapperQueryFirstOrDefaultAsync<int>(_connectionString, sBuilder);
    }
}