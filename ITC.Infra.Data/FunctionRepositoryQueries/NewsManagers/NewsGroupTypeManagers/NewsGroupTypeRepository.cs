using System;
using System.Linq;
using System.Threading.Tasks;
using ITC.Domain.Interfaces.NewsManagers.NewsGroupTypeManagers;
using ITC.Domain.Models.NewsManagers;
using ITC.Infra.Data.Context;
using ITC.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ITC.Infra.Data.FunctionRepositoryQueries.NewsManagers.NewsGroupTypeManagers;

/// <summary>
///     Class repository phòng ban
/// </summary>
public class NewsGroupTypeRepository : Repository<NewsGroupType>, INewsGroupTypeRepository
{
#region Fields

    private readonly EQMContext _context;

#endregion

#region Constructors

    public NewsGroupTypeRepository(EQMContext context) : base(context)
    {
        _context = context;
    }

#endregion

    /// <inheritdoc cref="GetMaxPosition" />
    public async Task<int> GetMaxPosition()
    {
        return 1;
        var lData = await _context.NewsGroupTypes.MaxAsync(x => x.Position);
        return lData + 1;
    }

    /// <inheritdoc cref="GetBySecrect" />
    public async Task<NewsGroupType> GetBySecrect(Guid projectId, string secrectKey)
    {
        return await _context.NewsGroupTypes.FirstOrDefaultAsync(
                   x => x.SecretKey == secrectKey && x.ProjectId == projectId);
    }
}