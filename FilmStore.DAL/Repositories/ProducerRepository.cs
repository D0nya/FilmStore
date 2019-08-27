using FilmStore.DAL.Interfaces;
using FilmStore.DBL.EF;
using FilmStore.DBL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FilmStore.DAL.Repositories
{
  class ProducerRepository : IRepository<Producer>
  {
    private FilmStoreContext db;
    public ProducerRepository(FilmStoreContext context)
    {
      db = context;
    }

    public void Create(Producer item)
    {
      db.Producers.Add(item);
    }
    public void Delete(int id)
    {
      Producer producer = db.Producers.Find(id);
      if (producer != null)
        db.Producers.Remove(producer);
    }
    public IEnumerable<Producer> Find(Func<Producer, bool> predicate)
    {
      return db.Producers.Where(predicate).ToList();
    }
    public Producer Get(int id)
    {
      return db.Producers.Find(id);
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
