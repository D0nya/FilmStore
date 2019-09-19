using FilmStore.DAL.Interfaces;
using FilmStore.DAL.EF;
using FilmStore.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmStore.DAL.Repositories
{
  class FilmRepository : IRepository<Film>
  {
    private readonly FilmStoreContext db;
    public FilmRepository(FilmStoreContext context)
    {
      db = context;
    }
    public async Task Create(Film item)
    {
      await db.Films.AddAsync(item);
    }
    public async Task Delete(int id)
    {
      Film film = await db.Films.FindAsync(id);
      if (film != null)
        db.Films.Remove(film);
    }
    public IEnumerable<Film> Find(Func<Film, bool> predicate)
    {
      return db.Films.Where(predicate).ToList();
    }
    public async Task<Film> Get(int id)   
    {
      return await db.Films.FindAsync(id);
    }
    public IEnumerable<Film> GetAll()
    {
      return db.Films;
    }
    public void Update(Film item)
    {
      db.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
    }
  }
}
