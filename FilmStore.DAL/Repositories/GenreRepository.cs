using FilmStore.DAL.Interfaces;
using FilmStore.DAL.EF;
using FilmStore.DAL.Entities;

namespace FilmStore.DAL.Repositories
{
  class GenreRepository : GenericRepository<Genre>, IGenreRepository
  {
    public GenreRepository(FilmStoreContext context) : base(context)
    {
    }
  }
}
