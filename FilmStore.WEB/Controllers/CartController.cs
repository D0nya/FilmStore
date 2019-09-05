using AutoMapper;
using FilmStore.BLL.DTO;
using FilmStore.BLL.Interfaces;
using FilmStore.WEB.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace FilmStore.WEB.Controllers
{
  [Authorize]
  public class CartController : Controller
  {
    private readonly IOrderService orderService;
    public CartController(IOrderService orderService)
    {
      this.orderService = orderService;
    }

    public IActionResult Cart()
    {
      decimal sum = 0;
      var films = HttpContext.Session.Get<IEnumerable<FilmDTO>>("CartFilms");
      var mapper = CreateFilmDTOToFilmViewModelMapper();

      if(films != null)
      {
        foreach (var film in films)
          sum += film.Price;
      }
      ViewBag.Sum = sum;
      return View(mapper.Map<IEnumerable<FilmDTO>, IEnumerable<FilmViewModel>>(films));
    }
    public IActionResult AddToCart(int id, string returnUrl)
    {
      var film = orderService.GetFilm(id);
      List<FilmDTO> films = new List<FilmDTO>(); ;

      if(returnUrl == null)
        returnUrl = "~/Home/Index";

      if (HttpContext.Session.GetInt32("CartCount") == null)
      {
        HttpContext.Session.SetInt32("CartCount", 0);
      }
      HttpContext.Session.SetInt32("CartCount", (int)HttpContext.Session.GetInt32("CartCount") + 1);

      if(HttpContext.Session.Get<IEnumerable<FilmDTO>>("CartFilms") == null)
      {
        HttpContext.Session.Set<IEnumerable<FilmDTO>>("CartFilms", new List<FilmDTO> { film });
        return Redirect(returnUrl);
      }
      films = HttpContext.Session.Get<IEnumerable<FilmDTO>>("CartFilms").ToList();

      films.Add(film);

      HttpContext.Session.Set<IEnumerable<FilmDTO>>("CartFilms", films);
      return Redirect(returnUrl);
    }
    private IMapper CreateFilmDTOToFilmViewModelMapper()
    {
      var mapper = new MapperConfiguration(cfg =>
      {
        cfg.CreateMap<CountryDTO, CountryViewModel>();
        cfg.CreateMap<GenreDTO, GenreViewModel>();
        cfg.CreateMap<PurchaseDTO, PurchaseViewModel>();
        cfg.CreateMap<ProducerDTO, ProducerViewModel>();
        cfg.CreateMap<FilmDTO, FilmViewModel>()
        .ForMember(src => src.Purchases, opt => opt.Ignore());
      }).CreateMapper();
      return mapper;
    }
  }

  public static class SessionExtensions
  {
    public static void Set<T>(this ISession session, string key, T value)
    {
      session.SetString(key, JsonConvert.SerializeObject(value));
    }
    public static T Get<T>(this ISession session, string key)
    {
      var value = session.GetString(key);
      return value == null ? default : JsonConvert.DeserializeObject<T>(value);
    }
  }
}
