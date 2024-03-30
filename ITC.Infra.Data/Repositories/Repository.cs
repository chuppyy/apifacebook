#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITC.Domain.Interfaces;
using ITC.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

#endregion

namespace ITC.Infra.Data.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
#region Constructors

    public Repository(EQMContext context)
    {
        Db    = context;
        DbSet = Db.Set<TEntity>();
    }

#endregion

#region Fields

    protected readonly EQMContext     Db;
    protected readonly DbSet<TEntity> DbSet;

#endregion

#region IRepository<TEntity> Members

    public virtual void Add(TEntity obj)
    {
        DbSet.Add(obj);
    }

    public async Task AddAsync(TEntity obj)
    {
        await DbSet.AddAsync(obj);
    }

    public void Dispose()
    {
        Db.Dispose();
        GC.SuppressFinalize(this);
    }

    public virtual TEntity Get(Guid id)
    {
        return DbSet.Find(id);
    }


    public virtual IQueryable<TEntity> GetAll()
    {
        return DbSet;
    }

    public async Task<List<TEntity>> GetAllAsync()
    {
        return await DbSet.ToListAsync();
    }

    public async Task<TEntity> GetAsync(Guid id)
    {
        return await DbSet.FindAsync(id);
    }

    public async Task<TEntity> GetByIdIntAsync(int id)
    {
        return await DbSet.FindAsync(id);
    }

    public virtual void Remove(Guid id)
    {
        DbSet.Remove(DbSet.Find(id));
    }

    public int SaveChanges()
    {
        return Db.SaveChanges();
    }

    public virtual void Update(TEntity obj)
    {
        DbSet.Update(obj);
    }

#endregion
}