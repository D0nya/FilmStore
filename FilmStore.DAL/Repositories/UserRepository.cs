using FilmStore.DAL.Interfaces;
using FilmStore.DAL.EF;
using FilmStore.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FilmStore.DAL.Repositories
{
  class UserRepository : IRepository<User>
  {
    private readonly FilmStoreContext db;
    public UserRepository(FilmStoreContext context)
    {
      db = context;
    }
    public void Create(User item)
    {
      db.Users.Add(item);
    }
    public void Delete(int id)
    {
      User user = db.Users.Find(id);
      if (user != null)
        db.Users.Remove(user);
    }
    public IEnumerable<User> Find(Func<User, bool> predicate)
    {
      return db.Users.Where(predicate).ToList();
    }
    public User Get(int id)
    {
      return db.Users.Find(id);
    }
    public IEnumerable<User> GetAll()
    {
      return db.Users;
    }
    public void Update(User item)
    {
      db.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
    }
  }
}
