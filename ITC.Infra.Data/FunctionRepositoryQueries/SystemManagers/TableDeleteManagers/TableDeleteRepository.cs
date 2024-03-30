using System.Threading.Tasks;
using ITC.Domain.Interfaces.SystemManagers.TableDeleteManagers;
using ITC.Domain.Models.SystemManagers;
using ITC.Infra.Data.Context;
using ITC.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ITC.Infra.Data.FunctionRepositoryQueries.SystemManagers.TableDeleteManagers;

/// <summary>
///     Class repository danh sách table cần xóa
/// </summary>
public class TableDeleteManagerRepository : Repository<TableDeleteManager>, ITableDeleteManagerRepository
{
#region Fields

    private readonly EQMContext _context;

#endregion

#region Constructors

    public TableDeleteManagerRepository(EQMContext context) : base(context)
    {
        _context = context;
    }

#endregion

    /// <inheritdoc cref="GetTableNameByCode" />
    public async Task<string> GetTableNameByCode(int code)
    {
        var iValue = await _context.TableDeleteManagers.FirstOrDefaultAsync(x => x.Code == code);
        return iValue?.Name ?? "";
    }
}