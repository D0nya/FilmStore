using FilmStore.BLL.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FilmStore.BLL.Interfaces
{
  public interface INewsService
  {
    IEnumerable<NewsDTO> GetNews();
    Task AddNewsAsync(NewsDTO newsDTO);
    Task DeleteNewsAsync(int id);
  }
}
