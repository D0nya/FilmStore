using FilmStore.BLL.DTO;
using FilmStore.BLL.Interfaces;
using FilmStore.WEB.Models;
using FilmStore.WEB.Services;
using Microsoft.AspNetCore.Mvc;

namespace FilmStore.WEB.Controllers
{
  public class Details : Controller
  {
    private readonly IOrderService _orderService;
    public Details(IOrderService orderService)
    {
      _orderService = orderService;
    }
    public ViewResult FilmDetails(int id)
    {
      var film = _orderService.GetFilm(id);
      var mapper = MapperService.CreateFilmDTOToFilmViewModelMapper();
      var viewFilm = mapper.Map<FilmDTO, FilmViewModel>(film);
      return View(viewFilm);
    }
  }
}
