using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FilmStore.DAL.Interfaces
{
  public interface IRepository<T> where T : class
  {
    IEnumerable<T> GetAll();
    Task<T> Get(int id);
    IEnumerable<T> Find(Func<T, bool> predicate);
    Task Create(T item);
    void Update(T item);
    void Delete(int id);
  }
}
