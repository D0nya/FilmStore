using FilmStore.DAL.Interfaces;
using FilmStore.DAL.EF;
using FilmStore.DAL.Entities;

namespace FilmStore.DAL.Repositories
{
  class FilmRepository : GenericRepository<Film>, IFilmRepository
  {
    public FilmRepository(FilmStoreContext context) : base(context)
    {
    }
  }
}
