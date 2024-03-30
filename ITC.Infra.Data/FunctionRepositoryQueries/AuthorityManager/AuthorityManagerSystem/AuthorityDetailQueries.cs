#region

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using ITC.Domain.Interfaces.AuthorityManager.AuthorityManagerSystem;
using NCore.Helpers;

#endregion

namespace ITC.Infra.Data.FunctionRepositoryQueries.AuthorityManager.AuthorityManagerSystem;

/// <summary>
///     Phân quyền queries
/// </summary>
public class AuthorityDetailQueries : IAuthorityDetailQueries
{
#region Fields

    private readonly string _connectionString;

#endregion

#region Constructors

    public AuthorityDetailQueries(string connectionString)
    {
        _connectionString = connectionString;
    }

#endregion

    /// <inheritdoc cref="DeleteAsync" />
    public async Task<int> DeleteAsync(Guid authoritiesId, int historyPosition)
    {
        var sBuilder = new StringBuilder();
        sBuilder.Append(
            "UPDATE AuthorityDetail SET IsDeleted = 1 WHERE AuthorityId = @pId AND HistoryPosition = @pHistoryPosition ");
        var dictionary = new Dictionary<string, object>
        {
            {
                "@pId", authoritiesId
            },
            {
                "@pHistoryPosition", historyPosition
            }
        };
        return await SqlHelper.RunDapperExecuteAsync(_connectionString,
                                                     sBuilder,
                                                     new DynamicParameters(dictionary));
    }
}