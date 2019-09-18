using FilmStore.DAL.Interfaces;
using FilmStore.DAL.EF;
using FilmStore.DAL.Entities;
using System.Threading.Tasks;

namespace FilmStore.DAL.Repositories
{
  class ClientManager : IClientManager
  {
    public FilmStoreContext Database { get; set; }
    public ClientManager(FilmStoreContext context)
    {
      Database = context;
    }

    public async Task Create(Customer item)
    {
      await Database.Customers.AddAsync(item);
      await Database.SaveChangesAsync();
    }

    public void Dispose()
    {
      Database.Dispose();
    }
  }
}
