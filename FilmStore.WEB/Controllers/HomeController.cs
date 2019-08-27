using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FilmStore.WEB.Models;
using FilmStore.BLL.Interfaces;
using AutoMapper;
using FilmStore.BLL.DTO;

namespace FilmStore.WEB.Controllers
{
  public class HomeController : Controller
  {
    IOrderService orderService;
    public HomeController(IOrderService serv)
    {
      orderService = serv;
    }
    public IActionResult Index()
    {
      IEnumerable<FilmDTO> filmDTOs = orderService.GetFilms();
      var mapper = new MapperConfiguration(cfg =>
       {
         cfg.CreateMap<CountryDTO, CountryViewModel>();
         cfg.CreateMap<GenreDTO, GenreViewModel>();
         cfg.CreateMap<PurchaseDTO, PurchaseViewModel>();
         cfg.CreateMap<ProducerDTO, ProducerViewModel>();
         cfg.CreateMap<FilmDTO, FilmViewModel>();
       }).CreateMapper();
      var films = mapper.Map<IEnumerable<FilmDTO>, List<FilmViewModel>>(filmDTOs);
      return View(films);
    }

    public IActionResult Privacy()
    {
      return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }
}