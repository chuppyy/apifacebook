#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#endregion

namespace ITC.Domain.Interfaces;

public interface IRepository<TEntity> : IDisposable where TEntity : class
{
#region Methods

    void Add(TEntity obj);

    Task AddAsync(TEntity obj);

    TEntity Get(Guid id);

    IQueryable<TEntity> GetAll();

    Task<List<TEntity>> GetAllAsync();

    Task<TEntity> GetAsync(Guid       id);
    Task<TEntity> GetByIdIntAsync(int id);

    void Remove(Guid id);

    int SaveChanges();

    void Update(TEntity obj);

#endregion
}