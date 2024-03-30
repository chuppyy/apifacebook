using System;
using System.Linq;
using System.Threading.Tasks;
using ITC.Domain.Interfaces.NewsManagers.NewsGithubManagers;
using ITC.Domain.Models.NewsManagers;
using ITC.Infra.Data.Context;
using ITC.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ITC.Infra.Data.FunctionRepositoryQueries.NewsManagers.NewsGithubManagers;

/// <summary>
///     Class repository phòng ban
/// </summary>
public class NewsGithubRepository : Repository<NewsGithub>, INewsGithubRepository
{
#region Fields

    private readonly EQMContext _context;

#endregion

#region Constructors

    public NewsGithubRepository(EQMContext context) : base(context)
    {
        _context = context;
    }

#endregion

    /// <inheritdoc cref="GetMaxPosition" />
    public async Task<int> GetMaxPosition()
    {
        return 1;
        var lData = await _context.NewsGithubs.MaxAsync(x => x.Position);
        return lData + 1;
    }
}