#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITC.Domain.Interfaces.NewsManagers.NewsGroupManagers;
using ITC.Domain.Models.NewsManagers;
using ITC.Infra.Data.Context;
using ITC.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;

#endregion

namespace ITC.Infra.Data.FunctionRepositoryQueries.NewsManagers.NewsGroupManagers;

/// <summary>
///     Lớp repository nhóm tin
/// </summary>
public class NewsGroupRepository : Repository<NewsGroup>, INewsGroupRepository
{
    private readonly EQMContext _context;

#region Constructors

    /// <summary>
    ///     Hàm dựng
    /// </summary>
    /// <param name="context"></param>
    public NewsGroupRepository(EQMContext context) : base(context)
    {
        _context = context;
    }

#endregion

    /// <inheritdoc cref="DeQuyLeftRight" />
    public int DeQuyLeftRight(List<NewsGroup> model, int left, string parentId)
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

    /// <inheritdoc cref="GetMaxPosition" />
    public async Task<int> GetMaxPosition(Guid typeId)
    {
        return 1;
        var lData = await _context.NewsGroups.Where(x => x.NewsGroupTypeId == typeId).MaxAsync(x => x.Position);
        return lData + 1;
    }

    /// <inheritdoc cref="GetListIdFromLeftRight" />
    public async Task<List<Guid>> GetListIdFromLeftRight(int vLeft, int vRight)
    {
        return await _context.NewsGroups
                             .Where(x => x.PLeft > vLeft && x.PRight < vRight)
                             .Select(x => x.Id)
                             .ToListAsync();
    }

    /// <inheritdoc cref="GetBySecrectKey" />
    public Task<NewsGroup> GetBySecrectKey(Guid projectId, string secrectKey)
    {
        return _context.NewsGroups.FirstOrDefaultAsync(x => x.SecretKey == secrectKey && x.ProjectId == projectId);
    }

    public async Task<NewsGroup> GetByIdAsync(Guid id)
    {
        return await _context.NewsGroups.FirstOrDefaultAsync(x => x.Id == id);
    }
}