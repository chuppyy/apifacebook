#region

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using ITC.Domain.Core.ModelShare.NewsManagers.NewsDomainManagers;
using ITC.Domain.Interfaces.NewsManagers.NewsDomainManagers;
using NCore.Actions;
using NCore.Helpers;
using NCore.Modals;

#endregion

namespace ITC.Infra.Data.FunctionRepositoryQueries.NewsManagers.NewsDomainManagers;

/// <summary>
///     Phòng ban queries
/// </summary>
public class NewsDomainQueries : INewsDomainQueries
{
#region Fields

    private readonly string _connectionString;

#endregion

#region Constructors

    public NewsDomainQueries(string connectionString)
    {
        _connectionString = connectionString;
    }

#endregion

    /// <inheritdoc cref="GetPaging" />
    public async Task<IEnumerable<NewsDomainPagingDto>> GetPaging(PagingModel model)
    {
        var sBuilder = new StringBuilder();
        sBuilder.Append(SqlHelper.GeneralSqlBuilder(@"SELECT RM.Id, 
                                                                 RM.Name, 
                                                                 RM.Description, 
                                                                 RM.StatusId, 
                                                                 RM.Created, 
                                                                 RM.CreatedBy AS OwnerId
                                                          FROM NewsDomains RM
                                                          WHERE RM.IsDeleted = 0 "));
        sBuilder.Append(SqlHelper.Search(new List<string>
        {
            "Name"
        }, model.Search));
        sBuilder.Append(model.StatusId > 0 ? "AND StatusId = @pStatusId " : " ");
        sBuilder.Append("ORDER BY Created ASC ");
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
        return await SqlHelper.RunDapperQueryAsync<NewsDomainPagingDto>(_connectionString,
                                                                           sBuilder,
                                                                           new DynamicParameters(dictionary));
    }

    /// <inheritdoc cref="DeleteAsync" />
    public async Task<int> DeleteAsync(List<Guid> model)
    {
        return await SqlHelper.RunDapperExecuteAsync(_connectionString,
                                                     SqlHelper.DeleteListAsync("NewsDomains",
                                                                               model));
    }

    /// <inheritdoc cref="GetCombobox" />
    public async Task<IEnumerable<ComboboxModal>> GetCombobox(string vSearch, Guid projectId)
    {
        var sBuilder = new StringBuilder();
        sBuilder.Append(@$"SELECT NGT.Id, NGT.Name FROM NewsDomains NGT 
                                    WHERE NGT.StatusId = {ActionStatusEnum.Active.Id} AND NGT.ProjectId = '{projectId}' ");
        return await SqlHelper
                   .RunDapperQueryAsync<ComboboxModal>(
                       _connectionString,
                       SqlHelper.GetComboboxAsync("NewsDomains",
                                                  vSearch,
                                                  sBuilder.ToString(),
                                                  true));
    }

    /// <inheritdoc cref="GetFirstDomain"/>
    public async Task<IEnumerable<ComboboxModal>> GetFirstDomain()
    {
        var sBuilder = new StringBuilder();
        sBuilder.Append(@$"SELECT TOP 1  ND.Id, ND.Name FROM NewsDomains ND WHERE ND.IsDeleted = 0 ORDER BY ND.Created ");
        return await SqlHelper.RunDapperQueryAsync<ComboboxModal>(_connectionString, sBuilder);
    }

    /// <inheritdoc cref="SaveDomain"/>
    public async Task<int> SaveDomain(StringBuilder sBuilder)
    {
        return await SqlHelper.RunDapperExecuteAsync(_connectionString, sBuilder);
    }
}