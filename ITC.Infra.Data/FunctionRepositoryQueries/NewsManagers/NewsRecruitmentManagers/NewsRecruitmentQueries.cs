#region

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using ITC.Domain.Core.ModelShare.NewsManagers.NewsContentManagers;
using ITC.Domain.Core.ModelShare.NewsManagers.NewsRecruitmentManagers;
using ITC.Domain.Interfaces.NewsManagers.NewsRecruitmentManagers;
using NCore.Actions;
using NCore.Helpers;
using NCore.Modals;

#endregion

namespace ITC.Infra.Data.FunctionRepositoryQueries.NewsManagers.NewsRecruitmentManagers;

/// <summary>
///     Bài viết queries
/// </summary>
public class NewsRecruitmentQueries : INewsRecruitmentQueries
{
#region Fields

    private readonly string _connectionString;

#endregion

#region Constructors

    public NewsRecruitmentQueries(string connectionString)
    {
        _connectionString = connectionString;
    }

#endregion

    /// <inheritdoc cref="GetPaging" />
    public async Task<IEnumerable<NewsRecruitmentPagingDto>> GetPaging(PagingModel model, int type)
    {
        var sBuilderSql = new StringBuilder();
        sBuilderSql.Append(@"SELECT NC.Id,
                                           NC.StatusId,
                                           NC.Name,
                                           NC.Summary,
                                           NC.DateTimeStart,
                                           NC.Modified,
                                           NC.CreatedBy AS OwnerId,
                                           ANU.FullName AS OwnerName
                                    FROM NewsRecruitments NC
                                             INNER JOIN AspNetUsers ANU ON NC.CreatedBy = ANU.Id
                               WHERE NC.IsDeleted = 0 AND NC.Type = @typeId AND NC.ProjectId = @projectId ");

        sBuilderSql.Append(model.StatusId > 0 ? "AND NC.StatusId = @pStatusId " : " ");

        var sBuilder = new StringBuilder();
        sBuilder.Append(SqlHelper.GeneralSqlBuilder(sBuilderSql.ToString()));
        sBuilder.Append(SqlHelper.Search(new List<string>
        {
            "Name",
            "Author"
        }, model.Search));
        sBuilder.Append("ORDER BY Modified DESC ");
        sBuilder.Append(SqlHelper.Paging(model.PageNumber, model.PageSize));
        var dictionary = new Dictionary<string, object>
        {
            {
                "@typeId", type
            },
            {
                "@pStatusId", model.StatusId
            },
            {
                "@projectId", model.ProjectId
            }
        };
        return await SqlHelper.RunDapperQueryAsync<NewsRecruitmentPagingDto>(_connectionString,
                                                                             sBuilder,
                                                                             new DynamicParameters(dictionary));
    }

    /// <inheritdoc cref="DeleteAsync" />
    public async Task<int> DeleteAsync(List<Guid> model)
    {
        return await SqlHelper.RunDapperExecuteAsync(_connectionString,
                                                     SqlHelper.DeleteListAsync("NewsRecruitments", model));
    }

    /// <inheritdoc cref="GetHomeViewContent" />
    public async Task<HomeNewsContentViewDto> GetHomeViewContent(Guid projectId, string id, int type)
    {
        var sBuilder = new StringBuilder();
        sBuilder.Append(@$"
                                UPDATE NewsRecruitments SET ViewEye = ViewEye + 1 WHERE SecretKey = '{id}';
                                SELECT NC.Name,
                                       NC.SecretKey   AS Id,
                                       NC.Summary,
                                       NC.Content,
                                       NC.SeoKeyword,
                                       NC.DateTimeStart,
                                       NC.ViewEye,
                                       SF.IsLocal,
                                       SF.FilePath,
                                       SF.LinkUrl
                                FROM NewsRecruitments NC
                                         INNER JOIN ServerFiles SF ON NC.AvatarId = SF.Id
                                WHERE NC.IsDeleted = 0 AND NC.StatusId = {ActionStatusEnum.Active.Id} 
                                  AND NC.SecretKey = '{id}' AND NC.ProjectId = '{projectId}' AND NC.Type = {type} ");
        return await SqlHelper.RunDapperQueryFirstOrDefaultAsync<HomeNewsContentViewDto>(_connectionString,
                   sBuilder);
    }
}