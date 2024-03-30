using System;
using System.Linq;
using System.Threading.Tasks;
using ITC.Domain.Interfaces.NewsManagers.NewsAttackManagers;
using ITC.Domain.Models.NewsManagers;
using ITC.Infra.Data.Context;
using ITC.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ITC.Infra.Data.FunctionRepositoryQueries.NewsManagers.NewsAttackManagers;

/// <summary>
///     Class repository bài viết
/// </summary>
public class NewsAttackRepository : Repository<NewsAttack>, INewsAttackRepository
{
#region Fields

    private readonly EQMContext _context;

#endregion

#region Constructors

    public NewsAttackRepository(EQMContext context) : base(context)
    {
        _context = context;
    }

#endregion

    /// <inheritdoc cref="GetMaxHistoryPosition" />
    public async Task<int> GetMaxHistoryPosition(Guid newsContentId)
    {
        var lData = await _context.NewsAttacks.Where(x => x.NewsContentId == newsContentId).MaxAsync(x => x.HistoryPosition);
        return lData;
    }
}