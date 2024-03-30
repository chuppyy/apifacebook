#region

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using ITC.Domain.Core.ModelShare.NewsManagers.NewsAttackManagers;
using ITC.Domain.Interfaces.NewsManagers.NewsAttackManagers;
using NCore.Helpers;

#endregion

namespace ITC.Infra.Data.FunctionRepositoryQueries.NewsManagers.NewsAttackManagers;

/// <summary>
///     Bài viết queries
/// </summary>
public class NewsAttackQueries : INewsAttackQueries
{
#region Fields

    private readonly string _connectionString;

#endregion

#region Constructors

    public NewsAttackQueries(string connectionString)
    {
        _connectionString = connectionString;
    }

#endregion

    /// <inheritdoc cref="DeleteAsync" />
    public async Task<int> DeleteAsync(Guid newsContentId)
    {
        var sBuilder = new StringBuilder();
        sBuilder.Append("UPDATE NewsAttacks SET IsDeleted = 1 WHERE NewsContentId = @pNewsContentId ");
        var dictionary = new Dictionary<string, object>
        {
            {
                "@pNewsContentId", newsContentId
            }
        };
        return await SqlHelper.RunDapperExecuteAsync(_connectionString, sBuilder,
                                                     new DynamicParameters(dictionary));
    }

    /// <inheritdoc cref="GetPaging" />
    public async Task<IEnumerable<NewsAttackPagingDto>> GetPaging(Guid newsContentId)
    {
        var sBuilder = new StringBuilder();
        sBuilder.Append(@"SELECT NA.Id, NA.AttackDateTime, NA.IsDownload, SF.FileName, SF.IsLocal
                                FROM NewsAttacks NA
                                         INNER JOIN ServerFiles SF ON NA.FileId = SF.Id
                                WHERE NA.IsDeleted = 0 AND NA.NewsContentId = @pNewsContentId ");
        var dictionary = new Dictionary<string, object>
        {
            {
                "@pNewsContentId", newsContentId
            }
        };
        return await SqlHelper.RunDapperQueryAsync<NewsAttackPagingDto>(_connectionString, sBuilder,
                                                                        new DynamicParameters(dictionary));
    }
}