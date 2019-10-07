using FilmStore.DAL.Interfaces;
using FilmStore.DAL.EF;
using FilmStore.DAL.Entities;

namespace FilmStore.DAL.Repositories
{
  class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
  {
    public CustomerRepository(FilmStoreContext context) : base(context)
    {
    }
  }
}
