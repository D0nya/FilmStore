using FilmStore.DAL.Interfaces;
using FilmStore.DAL.EF;
using FilmStore.DAL.Entities;

namespace FilmStore.DAL.Repositories
{
  class ClientManager : IClientManager
  {
    public FilmStoreContext Database { get; set; }
    public ClientManager(FilmStoreContext context)
    {
      Database = context;
    }

    public void Create(Customer item)
    {
      Database.Customers.Add(item);
      Database.SaveChanges();
    }

    public void Dispose()
    {
      Database.Dispose();
    }
  }
}
