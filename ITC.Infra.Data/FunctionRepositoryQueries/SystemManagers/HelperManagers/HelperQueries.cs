#region

using System;
using System.Text;
using System.Threading.Tasks;
using ITC.Domain.Interfaces.SystemManagers.Helpers;
using NCore.Helpers;

#endregion

namespace ITC.Infra.Data.FunctionRepositoryQueries.SystemManagers.HelperManagers;

/// <summary>
///     Helper queries
/// </summary>
public class HelperQueries : IHelperQueries
{
#region Fields

    private readonly string _connectionString;

#endregion

#region Constructors

    public HelperQueries(string connectionString)
    {
        _connectionString = connectionString;
    }

#endregion

    /// <inheritdoc cref="UpdateStatus" />
    public async Task<int> UpdateStatus(Guid contentId, string tableName, int statusId)
    {
        var sBuilder = new StringBuilder();
        sBuilder.Append(statusId == 1000
                            ? $"UPDATE {tableName} SET IsDeleted = 1 WHERE Id = '{contentId}' "
                            : $"UPDATE {tableName} SET StatusId = {statusId} WHERE Id = '{contentId}' ");

        return await SqlHelper.RunDapperExecuteAsync(_connectionString, sBuilder);
    }
}