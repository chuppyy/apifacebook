using System.Linq;
using System.Threading.Tasks;
using ITC.Domain.Interfaces.SaleProductManagers.ImageLibraryManager;
using ITC.Domain.Models.SaleProductManagers;
using ITC.Infra.Data.Context;
using ITC.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ITC.Infra.Data.FunctionRepositoryQueries.SaleProductManagerss.ImageLibraryManagers;

/// <summary>
///     Class repository slide
/// </summary>
public class ImageLibraryDetailManagerRepository : Repository<ImageLibraryDetailManager>,
                                                   IImageLibraryDetailManagerRepository
{
#region Fields

    private readonly EQMContext _context;

#endregion

#region Constructors

    public ImageLibraryDetailManagerRepository(EQMContext context) : base(context)
    {
        _context = context;
    }

#endregion

    /// <inheritdoc cref="GetMaxPosition" />
    public async Task<int> GetMaxPosition()
    {
        return 1;
        var lData = await _context.ImageLibraryDetailManagers.MaxAsync(x => x.Position);
        return lData + 1;
    }
}