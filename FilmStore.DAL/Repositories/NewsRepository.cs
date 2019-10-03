using FilmStore.DAL.EF;
using FilmStore.DAL.Entities;
using FilmStore.DAL.Interfaces;

namespace FilmStore.DAL.Repositories
{
  class NewsRepository : GenericRepository<News>
  {
    public NewsRepository(FilmStoreContext context) : base(context)
    {
    }
  }
}
