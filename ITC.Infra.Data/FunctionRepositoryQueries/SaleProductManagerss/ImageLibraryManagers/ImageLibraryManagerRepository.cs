using System;
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
public class ImageLibraryManagerRepository : Repository<ImageLibraryManager>, IImageLibraryManagerRepository
{
#region Fields

    private readonly EQMContext _context;

#endregion

#region Constructors

    public ImageLibraryManagerRepository(EQMContext context) : base(context)
    {
        _context = context;
    }

#endregion

    /// <inheritdoc cref="GetMaxPosition" />
    public async Task<int> GetMaxPosition()
    {
        return 1;
        var lData = await _context.ImageLibraryManagers.MaxAsync(x => x.Position);
        return lData + 1;
    }

    /// <inheritdoc cref="GetBySecrectKey" />
    public Task<ImageLibraryManager> GetBySecrectKey(Guid projectId, string secrectKey)
    {
        return _context.ImageLibraryManagers.FirstOrDefaultAsync(x => x.SecretKey == secrectKey &&
                                                                      x.ProjectId == projectId && x.IsDeleted == false);
    }
}