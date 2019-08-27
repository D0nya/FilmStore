using FilmStore.DAL.Interfaces;
using FilmStore.DBL.EF;
using FilmStore.DBL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FilmStore.DAL.Repositories
{
  class CustomerRepository : IRepository<Customer>
  {
    private FilmStoreContext db;
    public CustomerRepository(FilmStoreContext context)
    {
      db = context;
    }

    public void Create(Customer item)
    {
      db.Customers.Add(item);
    }
    public void Delete(int id)
    {
      Customer customer = db.Customers.Find(id);
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
    public Customer Get(int id)
    {
      return db.Customers.Find(id);
    }
    public IEnumerable<Customer> GetAll()
    {
      return db.Customers;
    }
  }
}
