using System;
using System.Threading.Tasks;
using ITC.Domain.Interfaces.AuthorityManager.AuthorityManagerSystem;
using ITC.Domain.Models.AuthorityManager;
using ITC.Infra.Data.Context;
using ITC.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ITC.Infra.Data.FunctionRepositoryQueries.AuthorityManager.AuthorityManagerSystem;

/// <summary>
///     Class repository phân quyền
/// </summary>
public class AuthorityManagerRepository : Repository<Authority>, IAuthorityManagerRepository
{
#region Fields

    private readonly EQMContext _context;

#endregion

#region Constructors

    public AuthorityManagerRepository(EQMContext context) : base(context)
    {
        _context = context;
    }

#endregion

    /// <inheritdoc cref="LoadAsync" />
    public async Task<Authority> LoadAsync(Guid id)
    {
        var s = GetAsync(id).Result;
        if (s == null) return null;

        await _context.Entry(s).Collection(x => x.AuthorityDetails).LoadAsync();
        _context.Entry(s).State = EntityState.Modified;
        return s;
    }
}