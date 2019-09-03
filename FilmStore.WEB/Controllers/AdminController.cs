using AutoMapper;
using FilmStore.BLL.DTO;
using FilmStore.BLL.Interfaces;
using FilmStore.WEB.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

      var mapper = CreateFilmDTOToFilmViewModelMapper();
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
      var mapper = CreateFilmDTOToFilmViewModelMapper();

      var filmViewModel = mapper.Map<FilmDTO, FilmViewModel>(film);

      ViewBag.Genres = _adminService.GetGenres().Select(g => new SelectListItem(g.Name, g.Id.ToString()));
      ViewBag.Producers = _adminService.GetProducers().Select(p => new SelectListItem(p.Name, p.Id.ToString()));
      ViewBag.Countries = _adminService.GetCountries().Select(c => new SelectListItem(c.Name, c.Id.ToString()));
      return View(filmViewModel);
    }

    [HttpPost]
    public IActionResult Edit(FilmViewModel filmViewModel)
    {
      var mapper = CreateFilmViewModelToFilmDTOMapper();
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
        var mapper = CreateFilmViewModelToFilmDTOMapper();
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
    private IMapper CreateFilmViewModelToFilmDTOMapper()
    {
      var mapper = new MapperConfiguration(cfg =>
      {
        cfg.CreateMap<CountryViewModel, CountryDTO>();
        cfg.CreateMap<GenreViewModel, GenreDTO>();
        cfg.CreateMap<PurchaseViewModel, PurchaseDTO>();
        cfg.CreateMap<ProducerViewModel, ProducerDTO>();
        cfg.CreateMap<FilmViewModel, FilmDTO>()
        .ForMember(src => src.Purchases, opt => opt.Ignore());
      }).CreateMapper();
      return mapper;
    }
  }
}
