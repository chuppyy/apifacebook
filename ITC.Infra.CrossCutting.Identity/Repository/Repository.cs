#region

using System;
using System.Linq;
using ITC.Infra.CrossCutting.Identity.Interfaces;
using ITC.Infra.CrossCutting.Identity.Models;
using Microsoft.EntityFrameworkCore;

#endregion

namespace ITC.Infra.CrossCutting.Identity.Repository;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
#region Constructors

    public Repository(ApplicationDbContext context)
    {
        Db    = context;
        DbSet = Db.Set<TEntity>();
    }

#endregion

#region Fields

    protected readonly ApplicationDbContext Db;
    protected readonly DbSet<TEntity>       DbSet;

#endregion

#region IRepository<TEntity> Members

    public virtual void Add(TEntity obj)
    {
        DbSet.Add(obj);
    }

    public virtual TEntity GetById(string id)
    {
        return DbSet.Find(id);
    }

    public virtual IQueryable<TEntity> GetAll()
    {
        return DbSet;
    }

    public virtual void Update(TEntity obj)
    {
        DbSet.Update(obj);
    }

    public virtual void Remove(string id)
    {
        DbSet.Remove(DbSet.Find(id));
    }

    public int SaveChanges()
    {
        return Db.SaveChanges();
    }

    public void Dispose()
    {
        Db.Dispose();
        GC.SuppressFinalize(this);
    }

#endregion
}