using System.Linq;
using System.Threading.Tasks;
using ITC.Domain.Interfaces.NewsManagers.NewsRecruitmentManagers;
using ITC.Domain.Models.NewsManagers;
using ITC.Infra.Data.Context;
using ITC.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ITC.Infra.Data.FunctionRepositoryQueries.NewsManagers.NewsRecruitmentManagers;

/// <summary>
///     Class repository bài viết
/// </summary>
public class NewsRecruitmentRepository : Repository<NewsRecruitment>, INewsRecruitmentRepository
{
#region Fields

    private readonly EQMContext _context;

#endregion

#region Constructors

    public NewsRecruitmentRepository(EQMContext context) : base(context)
    {
        _context = context;
    }

#endregion

    /// <inheritdoc cref="GetMaxPosition" />
    public async Task<int> GetMaxPosition(int typeId)
    {
        return 1;
        var lData = await _context.NewsRecruitments.MaxAsync(x => x.Position);
        return lData + 1;
    }

    /// <inheritdoc cref="GetBySecretKey" />
    public async Task<NewsRecruitment> GetBySecretKey(string secretKey)
    {
        return await _context.NewsRecruitments.FirstOrDefaultAsync(x => x.SecretKey == secretKey);
    }
}