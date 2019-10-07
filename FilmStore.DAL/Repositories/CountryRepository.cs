using FilmStore.DAL.Interfaces;
using FilmStore.DAL.EF;
using FilmStore.DAL.Entities;

namespace FilmStore.DAL.Repositories
{
  class CountryRepository : GenericRepository<Country>, ICountryRepository
  {
    public CountryRepository(FilmStoreContext context) : base(context)
    {
    }
  }
}
