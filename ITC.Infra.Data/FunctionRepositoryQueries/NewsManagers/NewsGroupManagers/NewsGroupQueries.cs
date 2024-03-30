#region

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using ITC.Domain.Core.ModelShare.NewsManagers.NewsGroupTypeManagers;
using ITC.Domain.Interfaces.NewsManagers.NewsGroupManagers;
using NCore.Actions;
using NCore.Helpers;
using NCore.Modals;

#endregion

namespace ITC.Infra.Data.FunctionRepositoryQueries.NewsManagers.NewsGroupManagers;

public class NewsGroupQueries : INewsGroupQueries
{
#region Fields

    private readonly string _connectionString;

#endregion

#region Constructors

    public NewsGroupQueries(string connectionString)
    {
        _connectionString = connectionString;
    }

#endregion

    /// <inheritdoc cref="DeleteAsync" />
    public async Task<int> DeleteAsync(List<Guid> model)
    {
        return await SqlHelper.RunDapperExecuteAsync(_connectionString,
                                                     SqlHelper.DeleteListAsync("NewsGroups",
                                                                               model));
    }

    /// <inheritdoc cref="GetTreeView" />
    public async Task<IEnumerable<TreeViewProjectModel>> GetTreeView(string vSearch,
                                                                     Guid   newsGroupTypeId,
                                                                     bool   isAll,
                                                                     Guid   projectId)
    {
        var sBuilder = new StringBuilder();
        sBuilder.Append(@"
                SELECT NG.Id, NG.Name AS Text, NG.ParentId, NG.Position, NG.StatusId, NG.CreatedBy AS OwnerId 
                FROM NewsGroups NG
                WHERE NG.IsDeleted = 0 ");
        sBuilder.Append(isAll ? " " : " AND NG.StatusId = @pStatusId ");
        sBuilder.Append(SqlHelper.Search(new List<string>
        {
            "Name"
        }, vSearch));
        sBuilder.Append("ORDER BY Position ");
        var dictionary = new Dictionary<string, object>
        {
            {
                "@pNewsGroupTypeId", newsGroupTypeId
            },
            {
                "@pStatusId", ActionStatusEnum.Active.Id
            },
            {
                "@pProjectId", projectId
            }
        };
        return await SqlHelper.RunDapperQueryAsync<TreeViewProjectModel>(_connectionString,
                                                                         sBuilder,
                                                                         new DynamicParameters(dictionary));
    }

    /// <inheritdoc cref="ListDomainVercel"/>
    public async Task<IEnumerable<ListVercelDto>> ListDomainVercel()
    {
        var sBuilder = new StringBuilder();
        sBuilder.Append(@"
                SELECT NG.Id, NG.Name, NG.ParentId, NG.DomainVercel, NG.Domain
                FROM NewsGroups NG 
                WHERE NG.IsDeleted = 0 ");
        return await SqlHelper.RunDapperQueryAsync<ListVercelDto>(_connectionString, sBuilder);
    }

    /// <inheritdoc cref="SaveDomain"/>
    public async Task<int> SaveDomain(StringBuilder sBuilder)
    {
        return await SqlHelper.RunDapperExecuteAsync(_connectionString, sBuilder);
    }
}