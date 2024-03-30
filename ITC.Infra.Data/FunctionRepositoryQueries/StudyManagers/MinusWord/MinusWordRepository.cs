using System.Linq;
using System.Threading.Tasks;
using ITC.Domain.Interfaces.StudyManagers.MinusWord;
using ITC.Infra.Data.Context;
using ITC.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ITC.Infra.Data.FunctionRepositoryQueries.StudyManagers.MinusWord;

/// <summary>
///     Class repository môn học
/// </summary>
public class MinusWordRepository : Repository<Domain.Models.MenuManager.MinusWord>, IMinusWordRepository
{
#region Fields

    private readonly EQMContext _context;

#endregion

#region Constructors

    public MinusWordRepository(EQMContext context) : base(context)
    {
        _context = context;
    }

#endregion

    /// <inheritdoc cref="GetMaxPosition" />
    public async Task<int> GetMaxPosition()
    {
        return 1;
        var lData = await _context.MinusWords.MaxAsync(x => x.Position);
        return lData + 1;
    }
}