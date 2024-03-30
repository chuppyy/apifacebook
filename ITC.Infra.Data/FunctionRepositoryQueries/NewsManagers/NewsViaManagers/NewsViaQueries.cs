#region

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using ITC.Domain.Core.ModelShare.StudyManagers.NewsVia;
using ITC.Domain.Interfaces.NewsManagers.NewsViaManagers;
using NCore.Actions;
using NCore.Helpers;
using NCore.Modals;

#endregion

namespace ITC.Infra.Data.FunctionRepositoryQueries.NewsManagers.NewsViaManagers;

/// <summary>
///     Phòng ban queries
/// </summary>
public class NewsViaQueries : INewsViaQueries
{
#region Fields

    private readonly string _connectionString;

#endregion

#region Constructors

    public NewsViaQueries(string connectionString)
    {
        _connectionString = connectionString;
    }

#endregion

    /// <inheritdoc cref="GetPaging" />
    public async Task<IEnumerable<NewsViaPagingDto>> GetPaging(PagingModel model)
    {
        var sBuilder = new StringBuilder();
        sBuilder.Append(SqlHelper.GeneralSqlBuilder(@"SELECT RM.Id, 
                                                                RM.Code,
                                                                RM.Content AS Description,
                                                                RM.Token,
                                                                RM.IdTkQc,
                                                                RM.StaffId,
                                                                 RM.StatusId, 
                                                                 RM.CreatedBy AS OwnerId,
                                                                 SM.Name      AS StaffName
                                                          FROM NewsVias RM 
                                                            LEFT JOIN StaffManagers SM ON RM.StaffId = SM.Id
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
        return await SqlHelper.RunDapperQueryAsync<NewsViaPagingDto>(_connectionString,
                                                                           sBuilder,
                                                                           new DynamicParameters(dictionary));
    }

    /// <inheritdoc cref="DeleteAsync" />
    public async Task<int> DeleteAsync(List<Guid> model)
    {
        return await SqlHelper.RunDapperExecuteAsync(_connectionString,
                                                     SqlHelper.DeleteListAsync("NewsVias",
                                                                               model));
    }

    /// <inheritdoc cref="GetCombobox" />
    public async Task<IEnumerable<ComboboxModal>> GetCombobox(string vSearch, Guid projectId)
    {
        var sBuilder = new StringBuilder();
        sBuilder.Append(@$"SELECT NGT.Id, NGT.Code AS Name FROM NewsVias NGT 
                                    WHERE NGT.StatusId = {ActionStatusEnum.Active.Id} ");
        if (projectId.CompareTo(Guid.Empty) != 0)
        {
            sBuilder.Append($" AND NGT.ProjectId = '{projectId}' ");
        }
        return await SqlHelper
                   .RunDapperQueryAsync<ComboboxModal>(
                       _connectionString,
                       SqlHelper.GetComboboxAsync("NewsVias",
                                                  vSearch,
                                                  sBuilder.ToString(),
                                                  true));
    }
}