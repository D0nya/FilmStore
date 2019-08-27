using FilmStore.DAL.Interfaces;
using FilmStore.DBL.EF;
using FilmStore.DBL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FilmStore.DAL.Repositories
{
  class FilmRepository : IRepository<Film>
  {
    private FilmStoreContext db;
    public FilmRepository(FilmStoreContext context)
    {
      db = context;
    }
    public void Create(Film item)
    {
      db.Films.Add(item);
    }
    public void Delete(int id)
    {
      Film film = db.Films.Find(id);
      if (film != null)
        db.Films.Remove(film);
    }
    public IEnumerable<Film> Find(Func<Film, bool> predicate)
    {
      return db.Films.Where(predicate).ToList();
    }
    public Film Get(int id)   
    {
      return db.Films.Find(id);
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
