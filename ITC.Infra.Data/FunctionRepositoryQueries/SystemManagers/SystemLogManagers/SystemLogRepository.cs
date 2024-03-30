using ITC.Domain.Core.Events;
using ITC.Domain.Interfaces.SystemManagers.SystemLogs;
using ITC.Infra.Data.Context;
using ITC.Infra.Data.Repositories;

namespace ITC.Infra.Data.FunctionRepositoryQueries.SystemManagers.SystemLogManagers;

/// <summary>
///     Class repository Nhật ký hệ thống
/// </summary>
public class SystemLogRepository : Repository<SystemLog>, ISystemLogRepository
{
#region Fields

    private readonly EQMContext _context;

#endregion

#region Constructors

    public SystemLogRepository(EQMContext context) : base(context)
    {
        _context = context;
    }

#endregion

    /// <inheritdoc cref="Store" />
    public void Store(SystemLog model)
    {
        _context.SystemLogs.Add(model);
        _context.SaveChanges();
    }
}