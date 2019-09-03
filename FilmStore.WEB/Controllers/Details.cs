using AutoMapper;
using FilmStore.BLL.DTO;
using FilmStore.BLL.Interfaces;
using FilmStore.WEB.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
      var mapper = CreateFilmDTOToFilmViewModelMapper();
      return View(mapper.Map<FilmDTO, FilmViewModel>(film));
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
}
