using FilmStore.BLL.DTO;
using FilmStore.BLL.Interfaces;
using FilmStore.DAL.Entities;
using FilmStore.WEB.Extensions;
using FilmStore.WEB.Models;
using FilmStore.WEB.Models.TableLogic;
using FilmStore.WEB.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
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
      IEnumerable<FilmViewModel> films = GetFilmsFromCart("CartFilms");
      if (films == null)
        return View(null);

      IEnumerable<FilmViewModel> filmsDistinct = 
        films.GroupBy(film => film.Id)
        .Select(group => group.FirstOrDefault())
        .OrderBy(f => f.Name);
      var mapper = MapperService.CreateFilmDTOToFilmViewModelMapper();

      ViewBag.FilmsDistinctAmount = new Dictionary<string, int>();

      foreach (var film in films)
        sum += film.Price;
      foreach (var film in filmsDistinct)
        ViewBag.FilmsDistinctAmount[film.Name] = films.Where(f => f.Id == film.Id).Count();
      ViewBag.Sum = sum;

      return View(filmsDistinct);
    }

    public async Task<IActionResult> AddToCart(int id, [FromQuery]int count, [FromQuery]string returnUrl)
    {
      FilmDTO filmDTO = await _orderService.GetFilmAsync(id);
      var mapper = MapperService.CreateFilmDTOToFilmViewModelMapper();
      FilmViewModel film = mapper.Map<FilmDTO, FilmViewModel>(filmDTO);
      List<FilmViewModel> films = new List<FilmViewModel>();

      if (returnUrl == null)
        returnUrl = "~/Home/Index";

      for (int i = 0; i < count; i++)
      {
        films.Add(film);
      }

      AddFilmsToCart("CartFilms", films);
      if (returnUrl.Contains("Cart"))
        return RedirectToAction("Cart");
      return Redirect(returnUrl);
    }

    public IActionResult RemoveFromCart(int id, [FromQuery]bool first)
    {
      RemoveFilmFromCart("CartFilms", first, f => f.Id == id);
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
      IEnumerable<FilmViewModel> filmViewModels = GetFilmsFromCart("CartFilms");
      var mapper = MapperService.CreateFilmViewModelToFilmDTOMapper();
      IEnumerable<FilmDTO> filmDTOs = mapper.Map<IEnumerable<FilmViewModel>, IEnumerable<FilmDTO>>(filmViewModels);
      await _orderService.AddPurchaseAsync(filmDTOs, HttpContext.User.Identity.Name);
      HttpContext.Session.Clear();
      return RedirectToAction("Cart");
    }

    private IEnumerable<FilmViewModel> GetFilmsFromCart(string key)
    {
      var items = HttpContext.Session.Get<IEnumerable<FilmViewModel>>(key);
      return items;
    }
    private void AddFilmsToCart(string key, IEnumerable<FilmViewModel> films)
    {
      List<FilmViewModel> res = new List<FilmViewModel>();
      if (GetFilmsFromCart(key) != null)
        res.AddRange(GetFilmsFromCart(key));
      res.AddRange(films);
      HttpContext.Session.Set(key, res);
    }
    private void RemoveFilmFromCart(string key, bool first, Predicate<FilmViewModel> predicate)
    {
      List<FilmViewModel> films = new List<FilmViewModel>();
      if (GetFilmsFromCart(key) != null)
        films = GetFilmsFromCart(key).ToList();
      else
        return;

      if (first)
        films.Remove(films.Where(new Func<FilmViewModel, bool>(predicate)).First());
      else
        films.RemoveAll(predicate);
      HttpContext.Session.Set(key, films);
    }
  }
}
