#region

using ITC.Domain.Interfaces;
using ITC.Infra.Data.Context;

#endregion

namespace ITC.Infra.Data.UoW;

public class UnitOfWork : IUnitOfWork
{
#region Fields

    private readonly EQMContext _context;

#endregion

#region Constructors

    public UnitOfWork(EQMContext context)
    {
        _context = context;
    }

#endregion

#region IUnitOfWork Members

    public bool Commit()
    {
        return _context.SaveChanges() > 0;
    }

    public void Dispose()
    {
        _context.Dispose();
    }

#endregion
}