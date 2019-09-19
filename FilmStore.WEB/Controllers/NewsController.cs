using FilmStore.BLL.DTO;
using FilmStore.BLL.Interfaces;
using FilmStore.WEB.Models;
using FilmStore.WEB.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace FilmStore.WEB.Controllers
{
  public class NewsController : Controller
  {
    private readonly INewsService _newsService;
    IHostingEnvironment _appEnvironment;
    public NewsController(INewsService newsService, IHostingEnvironment hostingEnvironment)
    {
      _newsService = newsService;
      _appEnvironment = hostingEnvironment;
    }

    public IActionResult NewsFeed()
    {
      var mapper = MapperService.NewsDTOTONewsViewModelMapper();
      var news = _newsService.GetNews();
      var newsViewModel = mapper.Map<IEnumerable<NewsDTO>, IEnumerable<NewsViewModel>>(news);
      return PartialView(newsViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> NewsFeed(NewsViewModel news)
    {
      var mapper = MapperService.NewsDTOTONewsViewModelMapper();
      var newsDTO = mapper.Map<NewsViewModel, NewsDTO>(news);
      if (news.Image != null)
      {
        string path = "/Files/" + news.Image.FileName;
        using (FileStream fs = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
        {
          await news.Image.CopyToAsync(fs);
        }
        newsDTO.ImagePath = path;
      }
      await _newsService.AddNews(newsDTO);
      return Redirect("~/Home/Index");
    }

    public async Task<IActionResult> DeleteNews(int id)
    {
      await _newsService.DeleteNews(id);
      return Redirect("~/Home/Index");
    }
  }
}
