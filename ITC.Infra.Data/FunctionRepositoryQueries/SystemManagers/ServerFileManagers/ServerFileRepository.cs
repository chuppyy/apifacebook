using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITC.Domain.Interfaces.SystemManagers.ServerFiles;
using ITC.Domain.Models.SystemManagers;
using ITC.Infra.Data.Context;
using ITC.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ITC.Infra.Data.FunctionRepositoryQueries.SystemManagers.ServerFileManagers;

/// <summary>
///     Class repository Server File
/// </summary>
public class ServerFileRepository : Repository<ServerFile>, IServerFileRepository
{
#region Fields

    private readonly EQMContext _context;

#endregion

#region Constructors

    public ServerFileRepository(EQMContext context) : base(context)
    {
        _context = context;
    }

#endregion

    /// <inheritdoc/>
    public int DeQuyLeftRight(List<ServerFile> model, int left, string parentId)
    {
        var dsItem = model.Where(x => x.ParentId == parentId);
        foreach (var item in dsItem)
        {
            item.PLeft  = left;
            left        = DeQuyLeftRight(model, left + 1, item.Id.ToString());
            item.PRight = left;
            left++;
        }

        return left;
    }

    /// <inheritdoc/>
    public async Task<int> GetMaxPosition(string userId)
    {
        /*return await Task.Run(async () =>
        {
            var iValue = await _context.ServerFiles.Where(x => x.CreatedBy == userId).ToListAsync();
            var abc = iValue.Count > 0 ? iValue.Max(x => x.Position) + 1 : 1;
            return abc;
        });*/
        return 1;
        var result = await _context.ServerFiles.Where(x => x.CreatedBy == userId).MaxAsync(x => x.Position);
        return result + 1;
    }

    /// <inheritdoc/>
    public async Task<ServerFile> GetByParentId(Guid value)
    {
        return await _context.ServerFiles.FirstOrDefaultAsync(x => x.Id == value);
    }

    /// <inheritdoc/>
    public async Task<ServerFile> GetDataRoot()
    {
        var result = await _context.ServerFiles.FirstOrDefaultAsync(x => x.IsRoot);
        return result;
    }

    public async Task<List<ServerFile>> GetListAsync()
    {
        var result = await _context.ServerFiles.Where(x => !x.IsDeleted).ToListAsync();
        return result;
    }
}