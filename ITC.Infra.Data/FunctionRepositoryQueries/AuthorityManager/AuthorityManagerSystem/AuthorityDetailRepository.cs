#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITC.Domain.Interfaces.AuthorityManager.AuthorityManagerSystem;
using ITC.Domain.Models.AuthorityManager;
using ITC.Domain.Models.NewsManagers;
using ITC.Infra.Data.Context;
using ITC.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;

#endregion

namespace ITC.Infra.Data.FunctionRepositoryQueries.AuthorityManager.AuthorityManagerSystem;

/// <summary>
///     Lớp repository phân quyền chi tiết
/// </summary>
public class AuthorityDetailRepository : Repository<AuthorityDetail>, IAuthorityDetailRepository
{
    private readonly EQMContext _context;

#region Constructors

    /// <summary>
    ///     Hàm dựng
    /// </summary>
    /// <param name="context"></param>
    public AuthorityDetailRepository(EQMContext context) : base(context)
    {
        _context = context;
    }

#endregion

    /// <inheritdoc cref="GetMaxHistoryPosition" />
    public async Task<int> GetMaxHistoryPosition(Guid authoritiesId)
    {
        var lData = await _context.AuthorityDetails.Where(x => x.AuthorityId == authoritiesId).MaxAsync(x => x.HistoryPosition);
        return lData;
    }

    /// <inheritdoc cref="GetListAuthorityDetailChild" />
    public async Task<List<AuthorityDetail>> GetListAuthorityDetailChild(Guid authorityId)
    {
        var author = await _context.AuthorityDetails
            .Where(x => x.AuthorityId == authorityId && x.IsDeleted == false)
            .ToListAsync();
        return author;
    }

    /// <inheritdoc cref="GetByMenuManager" />
    public Task<AuthorityDetail> GetByMenuManager(Guid authorityId, Guid menuManagerId)
    {
        return _context.AuthorityDetails.FirstOrDefaultAsync(x => x.AuthorityId   == authorityId &&
                                                                  x.MenuManagerId == menuManagerId);
    }
}