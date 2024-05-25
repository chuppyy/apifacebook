using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITC.Domain.Interfaces.CompanyManagers.StaffManagers;
using ITC.Domain.Models.CompanyManagers;
using ITC.Infra.Data.Context;
using ITC.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ITC.Infra.Data.FunctionRepositoryQueries.CompanyManagers.StaffManagers;

/// <summary>
///     Class repository nhân viên
/// </summary>
public class StaffManagerRepository : Repository<StaffManager>, IStaffManagerRepository
{
#region Fields

    private readonly EQMContext _context;

#endregion

#region Constructors

    public StaffManagerRepository(EQMContext context) : base(context)
    {
        _context = context;
    }

#endregion

    /// <inheritdoc cref="GetByUserId" />
    public async Task<StaffManager> GetByUserId(string id)
    {
        return await _context.StaffManagers.FirstOrDefaultAsync(x => x.UserId == id);
    }

    public async Task<List<string>> GetByOwnerIdAsync(string ownerId)
    {
        var result = await _context.StaffManagers.Where(x =>( x.UserId == ownerId) || (x.OwerId!=null&& x.OwerId.Value.ToString() == ownerId)).Select(x=>x.UserId).ToListAsync();
        return result;
    }
}