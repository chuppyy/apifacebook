using System.Linq;
using System.Threading.Tasks;
using ITC.Domain.Interfaces.NewsManagers.NewsViaManagers;
using ITC.Domain.Models.NewsManagers;
using ITC.Infra.Data.Context;
using ITC.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ITC.Infra.Data.FunctionRepositoryQueries.NewsManagers.NewsViaManagers;

/// <summary>
///     Class repository phòng ban
/// </summary>
public class NewsViaRepository : Repository<NewsVia>, INewsViaRepository
{
#region Fields

    private readonly EQMContext _context;

#endregion

#region Constructors

    public NewsViaRepository(EQMContext context) : base(context)
    {
        _context = context;
    }

#endregion

    /// <inheritdoc cref="GetMaxPosition" />
    public async Task<int> GetMaxPosition()
    {
        return 1;
        var lData = await _context.NewsVias.MaxAsync(x => x.Position);
        return lData + 1;
    }

    /// <inheritdoc cref="GetByIdQc"/>
    public async Task<NewsVia> GetByIdQc(string idQc)
    {
        var lData = await _context.NewsVias.FirstOrDefaultAsync(x => x.IdTkQc == idQc);
        return lData;
    }
}