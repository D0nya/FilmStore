using FilmStore.DAL.EF;
using FilmStore.DAL.Entities;
using FilmStore.DAL.Interfaces;

namespace FilmStore.DAL.Repositories
{
  class PurchaseRepository : GenericRepository<Purchase>
  {
    public PurchaseRepository(FilmStoreContext context) : base(context)
    {
    }
  }
}
