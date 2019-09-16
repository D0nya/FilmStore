using FilmStore.DAL.Interfaces;
using FilmStore.DAL.EF;
using FilmStore.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmStore.DAL.Repositories
{
  class CountryRepository : IRepository<Country>
  {
    private readonly FilmStoreContext db;
    public CountryRepository(FilmStoreContext context)
    {
      db = context;
    }

    public async void Create(Country item)
    {
      await db.Countries.AddAsync(item);
    }
    public async void Delete(int id)
    {
      Country country = await db.Countries.FindAsync(id);
      if (country != null)
        db.Countries.Remove(country);
    }
    public void Update(Country item)
    {
      db.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
    }
    public IEnumerable<Country> Find(Func<Country, bool> predicate)
    {
      return db.Countries.Where(predicate).ToList();
    }
    public async Task<Country> Get(int id)
    {
      return await db.Countries.FindAsync(id);
    }
    public IEnumerable<Country> GetAll()
    {
      return db.Countries;
    }
  }
}
