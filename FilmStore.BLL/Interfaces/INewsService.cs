using FilmStore.BLL.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FilmStore.BLL.Interfaces
{
  public interface INewsService
  {
    IEnumerable<NewsDTO> GetNews();
    Task AddNews(NewsDTO newsDTO);
    Task DeleteNews(int id);
  }
}
