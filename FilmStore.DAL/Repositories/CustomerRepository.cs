using FilmStore.DAL.Interfaces;
using FilmStore.DAL.EF;
using FilmStore.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmStore.DAL.Repositories
{
  class CustomerRepository : IRepository<Customer>
  {
    private readonly FilmStoreContext db;
    public CustomerRepository(FilmStoreContext context)
    {
      db = context;
    }

    public async void Create(Customer item)
    {
      await db.Customers.AddAsync(item);
    }
    public async void Delete(int id)
    {
      Customer customer = await db.Customers.FindAsync(id);
      if (customer != null)
        db.Customers.Remove(customer);
    }
    public void Update(Customer item)
    {
      db.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
    }
    public IEnumerable<Customer> Find(Func<Customer, bool> predicate)
    {
      return db.Customers.Where(predicate).ToList();
    }
    public async Task<Customer> Get(int id)
    {
      return await db.Customers.FindAsync(id);
    }
    public IEnumerable<Customer> GetAll()
    {
      return db.Customers;
    }
  }
}
