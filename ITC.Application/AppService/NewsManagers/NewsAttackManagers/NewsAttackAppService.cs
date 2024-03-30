#region

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ITC.Domain.Core.ModelShare.NewsManagers.NewsAttackManagers;
using ITC.Domain.Interfaces.NewsManagers.NewsAttackManagers;

#endregion

namespace ITC.Application.AppService.NewsManagers.NewsAttackManagers;

/// <summary>
///     Class service nhóm tin
/// </summary>
public class NewsAttackAppService : INewsAttackAppService
{
#region Fields

    private readonly INewsAttackQueries _queries;

#endregion

#region Constructors

    /// <summary>
    ///     Hàm dựng có tham số
    /// </summary>
    /// <param name="queries"></param>
    public NewsAttackAppService(INewsAttackQueries queries)
    {
        _queries = queries;
    }

#endregion

    /// <inheritdoc cref="GetPaging" />
    public Task<IEnumerable<NewsAttackPagingDto>> GetPaging(Guid newsContentId)
    {
        return _queries.GetPaging(newsContentId);
    }
}