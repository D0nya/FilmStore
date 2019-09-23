using FilmStore.BLL.DTO;
using FilmStore.BLL.Interfaces;
using FilmStore.DAL.Entities;
using FilmStore.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmStore.BLL.Services
{
  class NewsService : INewsService
  {
    IUnitOfWork Database { get; set; }
    public NewsService(IUnitOfWork db)
    {
      Database = db;
    }

    public IEnumerable<NewsDTO> GetNews()
    {
      var mapper = MapperService.NewsToNewsDTOMapper();
      var news = Database.News.GetAll();
      var newsDTO = mapper.Map<IEnumerable<News>, IEnumerable<NewsDTO>>(news);

      newsDTO = newsDTO.OrderByDescending(n => n.Date);
      return newsDTO;
    }
    public async Task AddNewsAsync(NewsDTO newsDTO)
    {
      News news = await Database.News.Get(newsDTO.Id);
      if(news == null)
      {
        news = new News
        {
          Header = newsDTO.Header,
          Body = newsDTO.Body,
          ImagePath = newsDTO.ImagePath,
          Date = DateTime.Now
        };
        await Database.News.Create(news);
        await Database.SaveAsync();
      }
    }
    public async Task DeleteNewsAsync(int id)
    {
      await Database.News.Delete(id);
      await Database.SaveAsync();
    }
  }
}
