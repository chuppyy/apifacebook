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
}