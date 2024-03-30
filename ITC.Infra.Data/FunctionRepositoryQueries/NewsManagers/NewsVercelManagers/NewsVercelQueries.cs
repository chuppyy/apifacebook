#region

using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ITC.Domain.Interfaces.NewsManagers.NewsVercelManagers;
using NCore.Helpers;
using NCore.Modals;

#endregion

namespace ITC.Infra.Data.FunctionRepositoryQueries.NewsManagers.NewsVercelManagers;

/// <summary>
///     Phòng ban queries
/// </summary>
public class NewsVercelQueries : INewsVercelQueries
{
#region Fields

    private readonly string _connectionString;

#endregion

#region Constructors

    public NewsVercelQueries(string connectionString)
    {
        _connectionString = connectionString;
    }

#endregion

    /// <inheritdoc cref="ListVercel" />
    public async Task<IEnumerable<ComboboxModalInt>> ListVercel()
    {
        var sBuilder = new StringBuilder();
        sBuilder.Append("select NV.Id, NV.Name from NewsVercels NV WHERE NV.IsDeleted = 0 ");
        return await SqlHelper
                   .RunDapperQueryAsync<ComboboxModalInt>(
                       _connectionString,
                       SqlHelper.GetComboboxAsync("NewsVercels",
                                                  "",
                                                  sBuilder.ToString(),
                                                  true));
    }
}