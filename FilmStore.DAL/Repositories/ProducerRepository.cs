using FilmStore.DAL.Interfaces;
using FilmStore.DAL.EF;
using FilmStore.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmStore.DAL.Repositories
{
  class ProducerRepository : IRepository<Producer>
  {
    private readonly FilmStoreContext db;
    public ProducerRepository(FilmStoreContext context)
    {
      db = context;
    }

    public async void Create(Producer item)
    {
      await db.Producers.AddAsync(item);
    }
    public async void Delete(int id)
    {
      Producer producer = await db.Producers.FindAsync(id);
      if (producer != null)
        db.Producers.Remove(producer);
    }
    public IEnumerable<Producer> Find(Func<Producer, bool> predicate)
    {
      return db.Producers.Where(predicate).ToList();
    }
    public async Task<Producer> Get(int id)
    {
      return await db.Producers.FindAsync(id);
    }
    public IEnumerable<Producer> GetAll()
    {
      return db.Producers;
    }
    public void Update(Producer item)
    {
      db.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
    }
  }
}
