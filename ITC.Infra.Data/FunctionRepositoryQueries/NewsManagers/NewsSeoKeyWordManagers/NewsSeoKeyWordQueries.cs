#region

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using ITC.Domain.Core.ModelShare.NewsManagers.NewsSeoKeyWordManagers;
using ITC.Domain.Interfaces.NewsManagers.NewsSeoKeyWordManagers;
using NCore.Actions;
using NCore.Helpers;
using NCore.Modals;

#endregion

namespace ITC.Infra.Data.FunctionRepositoryQueries.NewsManagers.NewsSeoKeyWordManagers;

/// <summary>
///     Phòng ban queries
/// </summary>
public class NewsSeoKeyWordQueries : INewsSeoKeyWordQueries
{
#region Fields

    private readonly string _connectionString;

#endregion

#region Constructors

    public NewsSeoKeyWordQueries(string connectionString)
    {
        _connectionString = connectionString;
    }

#endregion

    /// <inheritdoc cref="GetPaging" />
    public async Task<IEnumerable<NewsSeoKeyWordPagingDto>> GetPaging(PagingModel model, Guid projectId)
    {
        var sBuilder = new StringBuilder();
        sBuilder.Append(SqlHelper.GeneralSqlBuilder(@"SELECT RM.Id, 
                                                                 RM.Name, 
                                                                 RM.Description, 
                                                                 RM.StatusId, 
                                                                 RM.CreatedBy AS OwnerId
                                                          FROM NewsSeoKeyWords RM
                                                          WHERE RM.IsDeleted = 0 AND RM.ProjectId = @pProjectId "));
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
                "@pProjectId", projectId
            }
        };
        return await SqlHelper.RunDapperQueryAsync<NewsSeoKeyWordPagingDto>(_connectionString,
                                                                            sBuilder,
                                                                            new DynamicParameters(dictionary));
    }

    /// <inheritdoc cref="DeleteAsync" />
    public async Task<int> DeleteAsync(List<Guid> model)
    {
        return await SqlHelper.RunDapperExecuteAsync(_connectionString,
                                                     SqlHelper.DeleteListAsync("NewsSeoKeyWords",
                                                                               model));
    }

    /// <inheritdoc cref="GetCombobox" />
    public async Task<IEnumerable<ComboboxModal>> GetCombobox(string vSearch, Guid projectId)
    {
        var sBuilder = new StringBuilder();
        sBuilder.Append(@$"SELECT NGT.Id, NGT.Name FROM NewsSeoKeyWords NGT 
                                WHERE NGT.StatusId = {ActionStatusEnum.Active.Id} AND NGT.ProjectId = '{projectId}' ");
        return await SqlHelper
                   .RunDapperQueryAsync<ComboboxModal>(
                       _connectionString,
                       SqlHelper.GetComboboxAsync("NewsSeoKeyWords",
                                                  vSearch,
                                                  sBuilder.ToString(),
                                                  true));
    }
}