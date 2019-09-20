using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FilmStore.WEB.Models;
using FilmStore.BLL.Interfaces;
using FilmStore.WEB.Services;
using FilmStore.BLL.DTO;
using System.Collections.Generic;
using System.Linq;

namespace FilmStore.WEB.Controllers
{
  public class HomeController : Controller
  {
    private readonly IOrderService _orderService;
    public HomeController(IOrderService orderService)
    {
      _orderService = orderService;
    }
    public IActionResult Index()
    {
      var mapper = MapperService.CreateFilmDTOToFilmViewModelMapper();
      var latestFilms = mapper.Map<IEnumerable<FilmDTO>, IEnumerable<FilmViewModel>>(_orderService.GetFilms(page: 0, filmStatus: DAL.Entities.FilmStatus.CameOut));
      latestFilms = latestFilms.OrderByDescending(f => f.Id).Take(5).ToList();
      ViewBag.Latest = latestFilms;

      var comingSoon = mapper.Map<IEnumerable<FilmDTO>, IEnumerable<FilmViewModel>>(_orderService.GetFilms(page: 0, filmStatus: DAL.Entities.FilmStatus.ComingSoon));
      comingSoon = comingSoon.OrderByDescending(f => f.Id).Take(5).ToList();
      ViewBag.ComingSoon = comingSoon;

      return View();
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