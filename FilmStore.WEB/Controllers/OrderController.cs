﻿using FilmStore.BLL.DTO;
using FilmStore.BLL.Interfaces;
using FilmStore.DAL.Entities;
using FilmStore.WEB.Models;
using FilmStore.WEB.Models.TableLogic;
using FilmStore.WEB.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmStore.WEB.Controllers
{
  [Authorize]
  public class OrderController : Controller
  {
    private readonly IOrderService _orderService;
    private readonly IAdminService _adminService;
    private readonly IEnumerable<SelectListItem> genres;
    private readonly IEnumerable<SelectListItem> producers;
    private readonly IEnumerable<SelectListItem> countries;
    public OrderController(IOrderService orderService, IAdminService adminService)
    {
      _orderService = orderService;
      _adminService = adminService;
      genres = _adminService.GetGenres().Select(g => new SelectListItem(g.Name, g.Id.ToString()));
      producers = _adminService.GetProducers().Select(p => new SelectListItem(p.Name, p.Id.ToString()));
      countries = _adminService.GetCountries().Select(c => new SelectListItem(c.Name, c.Id.ToString()));
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

    public async Task<IActionResult> AddToCart(int id, [FromQuery]int count, [FromQuery]string returnUrl)
    {
      FilmDTO film = await _orderService.GetFilmAsync(id);
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
      ViewBag.Genres = genres;
      ViewBag.Countries = countries;
      return View();
    }
      
    public IActionResult Search(string searchString, string genre, string country, string producer, 
      string yearFrom, string yearTo, string returnUrl, int page = 1, SortState sortOrder = SortState.NameAsc)
    {
      int pageSize = 5;
      FilmStatus status;
      if (returnUrl == "/Admin/Admin")
        status = 0;
      else
        status = FilmStatus.CameOut;

      var mapper = MapperService.CreateFilmDTOToFilmViewModelMapper();
      var filmDTOs = _orderService.GetFilms(searchString, genre, country, producer,yearFrom,yearTo, page,pageSize,sortOrder, status);
      var filmViewModels = mapper.Map<IEnumerable<FilmDTO>, IEnumerable<FilmViewModel>>(filmDTOs);

      TableViewModel<FilmViewModel> res = new TableViewModel<FilmViewModel>
      {
        PageViewModel = new PageViewModel(_orderService.FilmsCount(), page, pageSize),
        SortViewModel = new SortViewModel(sortOrder),
        Items = filmViewModels
      };

      switch (returnUrl)
      {
        case "/Admin/Admin":
          return PartialView("Search", res);
        case "/Order/Products":
          return PartialView("ProductsSearch", res);
        default:
          return Redirect("~/Home/Index");
      }
    }

    public async Task<IActionResult> MakeOrder()
    {
      TempData["message"] = $"Your order has been sent for review.";
      await _orderService.AddPurchaseAsync(HttpContext, "CartFilms");
      return RedirectToAction("Cart");
    }
  }
}
