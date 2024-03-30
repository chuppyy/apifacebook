#region

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using ITC.Domain.Core.ModelShare.NewsManagers.NewsGithubManagers;
using ITC.Domain.Interfaces.NewsManagers.NewsGithubManagers;
using NCore.Actions;
using NCore.Helpers;
using NCore.Modals;

#endregion

namespace ITC.Infra.Data.FunctionRepositoryQueries.NewsManagers.NewsGithubManagers;

/// <summary>
///     Phòng ban queries
/// </summary>
public class NewsGithubQueries : INewsGithubQueries
{
#region Fields

    private readonly string _connectionString;

#endregion

#region Constructors

    public NewsGithubQueries(string connectionString)
    {
        _connectionString = connectionString;
    }

#endregion

    /// <inheritdoc cref="GetPaging" />
    public async Task<IEnumerable<NewsGithubPagingDto>> GetPaging(PagingModel model)
    {
        var sBuilder = new StringBuilder();
        sBuilder.Append(SqlHelper.GeneralSqlBuilder(@"SELECT RM.Id, 
                                                                 RM.Name, 
                                                                 RM.Description, 
                                                                 RM.StatusId, 
                                                                 RM.CreatedBy AS OwnerId
                                                          FROM NewsGithubs RM
                                                          WHERE RM.IsDeleted = 0 "));
        sBuilder.Append(SqlHelper.Search(new List<string>
        {
            "Name"
        }, model.Search));
        sBuilder.Append(model.StatusId > 0 ? "AND StatusId = @pStatusId " : " ");
        sBuilder.Append("ORDER BY Id ");
        sBuilder.Append(SqlHelper.Paging(model.PageNumber, model.PageSize));
        var dictionary = new Dictionary<string, object>
        {
            {
                "@pStatusId", model.StatusId
            },
            {
                "@pProjectId", model.ProjectId
            }
        };
        return await SqlHelper.RunDapperQueryAsync<NewsGithubPagingDto>(_connectionString,
                                                                           sBuilder,
                                                                           new DynamicParameters(dictionary));
    }

    /// <inheritdoc cref="DeleteAsync" />
    public async Task<int> DeleteAsync(List<Guid> model)
    {
        return await SqlHelper.RunDapperExecuteAsync(_connectionString,
                                                     SqlHelper.DeleteListAsync("NewsGithubs",
                                                                               model));
    }

    /// <inheritdoc cref="GetCombobox" />
    public async Task<IEnumerable<ComboboxModal>> GetCombobox(string vSearch, Guid projectId)
    {
        var sBuilder = new StringBuilder();
        sBuilder.Append(@$"SELECT NGT.Id, NGT.Name FROM NewsGithubs NGT 
                                    WHERE NGT.StatusId = {ActionStatusEnum.Active.Id} ");
        return await SqlHelper
                   .RunDapperQueryAsync<ComboboxModal>(
                       _connectionString,
                       SqlHelper.GetComboboxAsync("NewsGithubs",
                                                  vSearch,
                                                  sBuilder.ToString(),
                                                  true));
    }
}