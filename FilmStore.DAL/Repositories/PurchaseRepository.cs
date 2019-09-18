using FilmStore.DAL.Interfaces;
using FilmStore.DAL.EF;
using FilmStore.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmStore.DAL.Repositories
{
  class PurchaseRepository : IRepository<Purchase>
  {
    private readonly FilmStoreContext db;
    public PurchaseRepository(FilmStoreContext context)
    {
      db = context;
    }
    public async Task Create(Purchase item)
    {
      await db.Purchases.AddAsync(item);
    }
    public async void Delete(int id)
    {
      Purchase purchase = await db.Purchases.FindAsync(id);
      if (purchase != null)
        db.Purchases.Remove(purchase);
    }
    public IEnumerable<Purchase> Find(Func<Purchase, bool> predicate)
    {
      return db.Purchases.Where(predicate).ToList();
    }
    public async Task<Purchase> Get(int id)
    {
      return await db.Purchases.FindAsync(id);
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
