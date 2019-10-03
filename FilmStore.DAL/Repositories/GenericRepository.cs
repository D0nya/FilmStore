using FilmStore.DAL.EF;
using FilmStore.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmStore.DAL.Repositories
{
  class GenericRepository<T> : IGenericRepository<T> where T : class
  {
    private readonly FilmStoreContext db;
    private readonly DbSet<T> dbSet;
    public GenericRepository(FilmStoreContext context)
    {
      db = context;
      dbSet = db.Set<T>();
    }

    public async Task Create(T item)
    {
      await dbSet.AddAsync(item);
    }

    public async Task Delete(int id)
    {
      T item = await dbSet.FindAsync(id);
      if (item != null)
        dbSet.Remove(item);
    }

    public IEnumerable<T> Find(Func<T, bool> predicate)
    {
      return dbSet.Where(predicate).ToList();
    }

    public async Task<T> Get(int id)
    {
      return await dbSet.FindAsync(id);
    }

    public IEnumerable<T> GetAll()
    {
      return dbSet;
    }

    public void Update(T item)
    {
      db.Entry(item).State = EntityState.Modified;
    }
  }
}
