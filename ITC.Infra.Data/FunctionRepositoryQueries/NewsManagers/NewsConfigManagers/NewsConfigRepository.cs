using ITC.Domain.Interfaces.NewsManagers.NewsConfigManagers;
using ITC.Domain.Models.NewsManagers;
using ITC.Infra.Data.Context;
using ITC.Infra.Data.Repositories;

namespace ITC.Infra.Data.FunctionRepositoryQueries.NewsManagers.NewsConfigManagers;

/// <summary>
///     Class repository phòng ban
/// </summary>
public class NewsConfigRepository : Repository<NewsConfig>, INewsConfigRepository
{
#region Fields

    private readonly EQMContext _context;

#endregion

#region Constructors

    public NewsConfigRepository(EQMContext context) : base(context)
    {
        _context = context;
    }

#endregion
}