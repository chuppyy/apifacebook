#region

using System;
using System.Linq;

#endregion

namespace ITC.Infra.CrossCutting.Identity.Interfaces;

public interface IRepository<TEntity> : IDisposable where TEntity : class
{
#region Methods

    void                Add(TEntity obj);
    IQueryable<TEntity> GetAll();
    TEntity             GetById(string id);
    void                Remove(string  id);
    int                 SaveChanges();
    void                Update(TEntity obj);

#endregion
}