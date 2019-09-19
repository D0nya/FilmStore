using FilmStore.DAL.EF;
using FilmStore.DAL.Entities;
using FilmStore.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmStore.DAL.Repositories
{
  class NewsRepository : IRepository<News>
  {
    private readonly FilmStoreContext db;
    public NewsRepository(FilmStoreContext context)
    {
      db = context;
    }

    public async Task Create(News item)
    {
      await db.News.AddAsync(item);
    }

    public async Task Delete(int id)
    {
      News news = await db.News.FindAsync(id);
      if (news != null)
        db.News.Remove(news);
    }

    public IEnumerable<News> Find(Func<News, bool> predicate)
    {
      return db.News.Where(predicate).ToList();
    }

    public async Task<News> Get(int id)
    {
      return await db.News.FindAsync(id);
    }

    public IEnumerable<News> GetAll()
    {
      return db.News;
    }

    public void Update(News item)
    {
      db.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
    }
  }
}
