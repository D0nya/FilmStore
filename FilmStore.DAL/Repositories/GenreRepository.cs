using FilmStore.DAL.Interfaces;
using FilmStore.DAL.EF;
using FilmStore.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmStore.DAL.Repositories
{
  class GenreRepository : IRepository<Genre>
  {
    private readonly FilmStoreContext db;
    public GenreRepository(FilmStoreContext context)
    {
      db = context;
    }
    public async Task Create(Genre item)
    {
      await db.Genres.AddAsync(item);
    }
    public async void Delete(int id)
    {
      Genre genre = await db.Genres.FindAsync(id);
      if (genre != null)
        db.Genres.Remove(genre);
    }
    public IEnumerable<Genre> Find(Func<Genre, bool> predicate)
    {
      return db.Genres.Where(predicate).ToList();
    }
    public async Task<Genre> Get(int id)
    {
      return await db.Genres.FindAsync(id);
    }
    public IEnumerable<Genre> GetAll()
    {
      return db.Genres;
    }
    public void Update(Genre item)
    {
      db.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
    }
  }
}
