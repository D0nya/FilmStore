using FilmStore.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FilmStore.DAL.Interfaces
{
  public interface IClientManager : IDisposable
  {
    void Create(Customer item);
  }
}
