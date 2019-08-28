using FilmStore.DAL.Interfaces;
using FilmStore.DAL.EF;
using FilmStore.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FilmStore.DAL.Repositories
{
  class GenreRepository : IRepository<Genre>
  {
    private readonly FilmStoreContext db;
    public GenreRepository(FilmStoreContext context)
    {
      db = context;
    }
    public void Create(Genre item)
    {
      db.Genres.Add(item);
    }
    public void Delete(int id)
    {
      Genre genre = db.Genres.Find(id);
      if (genre != null)
        db.Genres.Remove(genre);
    }
    public IEnumerable<Genre> Find(Func<Genre, bool> predicate)
    {
      return db.Genres.Where(predicate).ToList();
    }
    public Genre Get(int id)
    {
      return db.Genres.Find(id);
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
