using ITC.Domain.Interfaces.NewsManagers.NewsSeoKeyWordManagers;
using ITC.Domain.Models.NewsManagers;
using ITC.Infra.Data.Context;
using ITC.Infra.Data.Repositories;

namespace ITC.Infra.Data.FunctionRepositoryQueries.NewsManagers.NewsSeoKeyWordManagers;

/// <summary>
///     Class repository phòng ban
/// </summary>
public class NewsSeoKeyWordRepository : Repository<NewsSeoKeyWord>, INewsSeoKeyWordRepository
{
#region Fields

    private readonly EQMContext _context;

#endregion

#region Constructors

    public NewsSeoKeyWordRepository(EQMContext context) : base(context)
    {
        _context = context;
    }

#endregion
}