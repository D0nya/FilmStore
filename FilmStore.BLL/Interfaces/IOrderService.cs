using FilmStore.BLL.DTO;
using System.Collections.Generic;

namespace FilmStore.BLL.Interfaces
{
  public interface IOrderService
  {
    FilmDTO GetFilm(int id);
    IEnumerable<FilmDTO> GetFilms();

    void Dispose();
  }
}
