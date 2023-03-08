using CoNET.Models;
using Microsoft.EntityFrameworkCore;

namespace CoNET.Repositories;

public class DbRepository<T> : IRepository<T>
    where T : class, IEntity, new()
{

    public DbContext Context { get; }

    public DbRepository(DbContext context)
    {
        Context = context;
    }

    public virtual IEnumerable<T> GetAll()
    {
        var entities = Context.Set<T>();
        return entities;
    }

    public virtual T? GetById(int id)
    {
        var entity = Context.Find<T>(id);
        return entity;
    }


    public virtual void Insert(T entity)
    {
        Context.Add<T>(entity);
        Context.SaveChanges();
    }

    public virtual void Update(T entity)
    {
        Context.Update<T>(entity);
        Context.SaveChanges();
    }

    public virtual void Delete(T entity)
    {
        Context.Remove<T>(entity);
        Context.SaveChanges();
    }

}