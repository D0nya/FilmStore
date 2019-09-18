using FilmStore.DAL.Entities;
using System;
using System.Threading.Tasks;

namespace FilmStore.DAL.Interfaces
{
  public interface IClientManager : IDisposable
  {
    Task Create(Customer item);
  }
}
