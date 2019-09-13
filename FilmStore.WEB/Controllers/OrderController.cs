using FilmStore.BLL.DTO;
using FilmStore.BLL.Interfaces;
using FilmStore.WEB.Models;
using FilmStore.WEB.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace FilmStore.WEB.Controllers
{
  [Authorize]
  public class OrderController : Controller
  {
    private readonly IOrderService _orderService;
    public OrderController(IOrderService orderService)
    {
      this._orderService = orderService;
    }

    public IActionResult Cart()
    {
      decimal sum = 0;
      IEnumerable<FilmDTO> films = _orderService.GetFilmsFromCart(HttpContext, "CartFilms");
      if (films == null)
        return View(null);
      IEnumerable<FilmDTO> filmsDistinct = films.GroupBy(film => film.Id).Select(group => group.FirstOrDefault());
      var mapper = MapperService.CreateFilmDTOToFilmViewModelMapper();

      ViewBag.FilmsDistinctAmount = new Dictionary<string, int>();

      foreach (var film in films)
        sum += film.Price;
      foreach (var film in filmsDistinct)
        ViewBag.FilmsDistinctAmount[film.Name] = films.Where(f => f.Id == film.Id).Count();
      ViewBag.Sum = sum;

      var viewFilms = mapper.Map<IEnumerable<FilmDTO>, IEnumerable<FilmViewModel>>(filmsDistinct).OrderBy(f => f.Name);
      return View(viewFilms);
    }

    public IActionResult AddToCart(int id, [FromQuery]int count, [FromQuery]string returnUrl)
    {
      FilmDTO film = _orderService.GetFilm(id);
      List<FilmDTO> films = new List<FilmDTO>();

      if (returnUrl == null)
        returnUrl = "~/Home/Index";

      for (int i = 0; i < count; i++)
      {
        films.Add(film);
      }

      _orderService.AddFilmsToCart(HttpContext, "CartFilms", films);
      if (returnUrl.Contains("Cart"))
        return RedirectToAction("Cart");
      return Redirect(returnUrl);
    }

    public IActionResult RemoveFromCart(int id, [FromQuery]bool first)
    {
      _orderService.RemoveFromCart(HttpContext, "CartFilms", first, f => f.Id == id);
      return RedirectToAction("Cart");
    }

    public IActionResult Products()
    {
      IEnumerable<FilmDTO> filmDTOs = _orderService.GetFilms();
      var mapper = MapperService.CreateFilmDTOToFilmViewModelMapper();
      var films = mapper.Map<IEnumerable<FilmDTO>, List<FilmViewModel>>(filmDTOs);
      return View(films);
    }
      
    public IActionResult Search(string searchString)
    {
      var mapper = MapperService.CreateFilmDTOToFilmViewModelMapper();
      var filmDTOs = _orderService.GetFilms();
      var filmViewModels = mapper.Map<IEnumerable<FilmDTO>, IEnumerable<FilmViewModel>>(filmDTOs);
      return PartialView(filmViewModels);
    }

    public IActionResult MakeOrder()
    {
      TempData["message"] = $"Your order has been sent for review.";
      _orderService.AddPurchase(HttpContext, "CartFilms");
      return RedirectToAction("Cart");
    }
  }
}
