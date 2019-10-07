using FilmStore.DAL.EF;
using FilmStore.DAL.Entities;
using FilmStore.DAL.Interfaces;

namespace FilmStore.DAL.Repositories
{
  class UserRepository : GenericRepository<User>, IUserRepository
  {
    public UserRepository(FilmStoreContext context) : base(context)
    {
    }
  }
}
