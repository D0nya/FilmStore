using FilmStore.DAL.Interfaces;
using FilmStore.DAL.EF;
using FilmStore.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmStore.DAL.Repositories
{
  class UserRepository : IRepository<User>
  {
    private readonly FilmStoreContext db;
    public UserRepository(FilmStoreContext context)
    {
      db = context;
    }

    public async Task Create(User item)
    {
      await db.Users.AddAsync(item);
    }
    public async Task Delete(int id)
    {
      User user = await db.Users.FindAsync(id);
      if (user != null)
        db.Users.Remove(user);
    }
    public IEnumerable<User> Find(Func<User, bool> predicate)
    {
      return db.Users.Where(predicate).ToList();
    }
    public async Task<User> Get(int id)
    {
      return await db.Users.FindAsync(id);
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
