﻿using FilmStore.DAL.Interfaces;
using FilmStore.DAL.EF;
using FilmStore.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FilmStore.DAL.Repositories
{
  class CountryRepository : IRepository<Country>
  {
    private readonly FilmStoreContext db;
    public CountryRepository(FilmStoreContext context)
    {
      db = context;
    }

    public void Create(Country item)
    {
      db.Countries.Add(item);
    }
    public void Delete(int id)
    {
      Country country = db.Countries.Find(id);
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
    public Country Get(int id)
    {
      return db.Countries.Find(id);
    }
    public IEnumerable<Country> GetAll()
    {
      return db.Countries;
    }
  }
}
