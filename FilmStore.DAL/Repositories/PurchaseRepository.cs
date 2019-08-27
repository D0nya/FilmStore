using FilmStore.DAL.Interfaces;
using FilmStore.DBL.EF;
using FilmStore.DBL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FilmStore.DAL.Repositories
{
  class PurchaseRepository : IRepository<Purchase>
  {
    private FilmStoreContext db;
    public PurchaseRepository(FilmStoreContext context)
    {
      db = context;
    }
    public void Create(Purchase item)
    {
      db.Purchases.Add(item);
    }
    public void Delete(int id)
    {
      Purchase purchase = db.Purchases.Find(id);
      if (purchase != null)
        db.Purchases.Remove(purchase);
    }
    public IEnumerable<Purchase> Find(Func<Purchase, bool> predicate)
    {
      return db.Purchases.Where(predicate).ToList();
    }
    public Purchase Get(int id)
    {
      return db.Purchases.Find(id);
    }
    public IEnumerable<Purchase> GetAll()
    {
      return db.Purchases;
    }
    public void Update(Purchase item)
    {
      db.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
    }
  }
}
