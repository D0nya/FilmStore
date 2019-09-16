using FilmStore.DAL.Entities;
using System;
using System.Threading.Tasks;

namespace FilmStore.DAL.Interfaces
{
  public interface IClientManager : IDisposable
  {
    void Create(Customer item);
  }
}
