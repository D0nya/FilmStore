using FilmStore.BLL.DTO;
using FilmStore.BLL.Interfaces;
using FilmStore.WEB.Models;
using FilmStore.WEB.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FilmStore.WEB.Controllers
{
  [Authorize(Roles="admin")]
  public class AdminController : Controller
  {
    private readonly IOrderService _orderService;
    private readonly IAdminService _adminService;
    public AdminController(IOrderService orderService, IAdminService adminService)
    {
      _orderService = orderService;
      _adminService = adminService;
    }
    public IActionResult Admin(string genreId, string searchString)
    {
      IEnumerable<FilmDTO> filmDTOs = _orderService.GetFilms();

      ViewBag.Genres = _adminService.GetGenres().Select(g => new SelectListItem(g.Name, g.Id.ToString()));

      var mapper = MapperService.CreateFilmDTOToFilmViewModelMapper();
      var films = mapper.Map<IEnumerable<FilmDTO>, List<FilmViewModel>>(filmDTOs);
      if(searchString != null)
        films = films.Where(f => f.Name.ToUpper().Contains(searchString.ToUpper())).ToList();
      if (genreId != null)
        films = films.Where(f => f.Genres.Any(g => g.Id.ToString() == genreId)).ToList();
      return View(films);
    }
    public ViewResult Edit(int Id)
    {
      var film = _orderService.GetFilm(Id);
      var mapper = MapperService.CreateFilmDTOToFilmViewModelMapper();

      var filmViewModel = mapper.Map<FilmDTO, FilmViewModel>(film);

      ViewBag.Genres = _adminService.GetGenres().Select(g => new SelectListItem(g.Name, g.Id.ToString()));
      ViewBag.Producers = _adminService.GetProducers().Select(p => new SelectListItem(p.Name, p.Id.ToString()));
      ViewBag.Countries = _adminService.GetCountries().Select(c => new SelectListItem(c.Name, c.Id.ToString()));
      return View(filmViewModel);
    }

    [HttpPost]
    public IActionResult Edit(FilmViewModel filmViewModel)
    {
      var mapper = MapperService.CreateFilmViewModelToFilmDTOMapper();
      if(ModelState.IsValid)
      {
        var filmDTO = mapper.Map<FilmViewModel, FilmDTO>(filmViewModel);
        _adminService.SaveFilm(filmDTO);
        TempData["message"] = $"Changes in film {filmViewModel.Name} were saved successfully.";
        return RedirectToAction("Admin");
      }
      else
      {
        ViewBag.Genres = _adminService.GetGenres().Select(g => new SelectListItem(g.Name, g.Id.ToString()));
        ViewBag.Producers = _adminService.GetProducers().Select(p => new SelectListItem(p.Name, p.Id.ToString()));
        ViewBag.Countries = _adminService.GetCountries().Select(c => new SelectListItem(c.Name, c.Id.ToString()));
        return View(filmViewModel);
      }
    }

    public ViewResult AddFilm()
    {
      ViewBag.Genres = _adminService.GetGenres().Select(g => new SelectListItem(g.Name, g.Id.ToString()));
      ViewBag.Producers = _adminService.GetProducers().Select(p => new SelectListItem(p.Name, p.Id.ToString()));
      ViewBag.Countries = _adminService.GetCountries().Select(c => new SelectListItem(c.Name, c.Id.ToString()));
      return View();
    }

    [HttpPost]
    public IActionResult AddFilm(FilmViewModel filmViewModel)
    {
      if (ModelState.IsValid)
      {
        var mapper = MapperService.CreateFilmViewModelToFilmDTOMapper();
        var filmDTO = mapper.Map<FilmViewModel, FilmDTO>(filmViewModel);
        _adminService.SaveFilm(filmDTO);
        TempData["message"] = $"Film {filmViewModel.Name} was added successfully.";
        return RedirectToAction("Admin");
      }
      else
      {
        ViewBag.Genres = _adminService.GetGenres().Select(g => new SelectListItem(g.Name, g.Id.ToString()));
        ViewBag.Producers = _adminService.GetProducers().Select(p => new SelectListItem(p.Name, p.Id.ToString()));
        ViewBag.Countries = _adminService.GetCountries().Select(c => new SelectListItem(c.Name, c.Id.ToString()));
        return View(filmViewModel);
      }
    }

    public IActionResult DeleteFilm(int id)
    {
      string filmName = _adminService.GetFilm(id).Name;
      _adminService.DeleteFilm(id);
      TempData["message"] = $"Film {filmName} was deleted successfully.";
      return RedirectToAction("Admin");
    }

    public IActionResult Purchases()
    {
      return View();
    }
  }
}
