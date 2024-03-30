#region

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using ITC.Domain.Core.ModelShare.StudyManagers.MinusWord;
using ITC.Domain.Interfaces.StudyManagers.MinusWord;
using NCore.Actions;
using NCore.Helpers;
using NCore.Modals;

#endregion

namespace ITC.Infra.Data.FunctionRepositoryQueries.StudyManagers.MinusWord;

/// <summary>
///     Loại môn học queries
/// </summary>
public class MinusWordQueries : IMinusWordQueries
{
#region Fields

    private readonly string _connectionString;

#endregion

#region Constructors

    public MinusWordQueries(string connectionString)
    {
        _connectionString = connectionString;
    }

#endregion

    /// <inheritdoc cref="GetPaging" />
    public async Task<IEnumerable<MinusWordPagingDto>> GetPaging(PagingModel model)
    {
        var sBuilder = new StringBuilder();
        sBuilder.Append(SqlHelper.GeneralSqlBuilder(@"SELECT RM.Id, 
                                                                 RM.Root, 
                                                                 RM.Replace, 
                                                                 RM.Description, 
                                                                 RM.StatusId, 
                                                                 RM.Position, 
                                                                 RM.CreatedBy AS OwnerId
                                                          FROM MinusWords RM
                                                          WHERE RM.IsDeleted = 0 "));
        sBuilder.Append(SqlHelper.Search(new List<string>
        {
            "Name"
        }, model.Search));
        sBuilder.Append(model.StatusId > 0 ? "AND StatusId = @pStatusId " : " ");
        sBuilder.Append("ORDER BY Position ");
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
        return await SqlHelper.RunDapperQueryAsync<MinusWordPagingDto>(_connectionString,
                                                                       sBuilder,
                                                                       new DynamicParameters(dictionary));
    }

    /// <inheritdoc cref="DeleteAsync" />
    public async Task<int> DeleteAsync(List<Guid> model)
    {
        return await SqlHelper.RunDapperExecuteAsync(_connectionString,
                                                     SqlHelper.DeleteListAsync("MinusWords",
                                                                               model));
    }

    /// <inheritdoc cref="GetCombobox" />
    public async Task<IEnumerable<ComboboxModal>> GetCombobox(string vSearch, int groupTable, Guid projectId)
    {
        var sBuilder = new StringBuilder();
        sBuilder.Append(@$"SELECT NGT.Id, NGT.Name 
                                FROM MinusWords NGT 
                                WHERE NGT.StatusId = {ActionStatusEnum.Active.Id} 
                                  AND NGT.TypeId = @pTypeId AND NGT.ProjectId = @pProjectId 
                                ORDER BY NGT.Position ");
        var dictionary = new Dictionary<string, object>
        {
            {
                "@pTypeId", groupTable
            },
            {
                "@pProjectId", projectId
            }
        };
        return await SqlHelper.RunDapperQueryAsync<ComboboxModal>(_connectionString,
                                                                  SqlHelper.GetComboboxAsync("MinusWords",
                                                                      vSearch,
                                                                      sBuilder.ToString(),
                                                                      true),
                                                                  new DynamicParameters(dictionary));
    }
}