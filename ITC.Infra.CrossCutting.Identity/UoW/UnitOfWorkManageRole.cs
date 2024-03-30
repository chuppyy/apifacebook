#region

using System;
using ITC.Infra.CrossCutting.Identity.Models;

#endregion

namespace ITC.Infra.CrossCutting.Identity.UoW;

public class UnitOfWorkIdentity : IUnitOfWorkIdentity
{
#region Fields

    private readonly ApplicationDbContext _context;

#endregion

#region Constructors

    public UnitOfWorkIdentity(ApplicationDbContext context)
    {
        _context = context;
    }

#endregion

#region IUnitOfWorkIdentity Members

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

public interface IUnitOfWorkIdentity : IDisposable
{
#region Methods

    bool Commit();

#endregion
}