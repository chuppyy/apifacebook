#region

using System;

#endregion

namespace ITC.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
#region Methods

    bool Commit();

#endregion
}