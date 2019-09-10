using AutoMapper;
using FilmStore.BLL.DTO;
using FilmStore.WEB.Models;

namespace FilmStore.WEB.Services
{
  static class MapperService
  {
    public static IMapper CreateFilmDTOToFilmViewModelMapper()
    {
      var mapper = new MapperConfiguration(cfg =>
      {
        cfg.CreateMap<UserDTO, UserViewModel>();
        cfg.CreateMap<CustomerDTO, CustomerViewModel>();
        cfg.CreateMap<CountryDTO, CountryViewModel>();
        cfg.CreateMap<GenreDTO, GenreViewModel>();
        cfg.CreateMap<PurchaseDTO, PurchaseViewModel>();
        cfg.CreateMap<ProducerDTO, ProducerViewModel>();
        cfg.CreateMap<FilmDTO, FilmViewModel>()
        .ForMember(src => src.Purchases, opt => opt.Ignore());
      }).CreateMapper();
      return mapper;
    }
    public static IMapper CreateFilmViewModelToFilmDTOMapper()
    {
      var mapper = new MapperConfiguration(cfg =>
      {
        cfg.CreateMap<CountryViewModel, CountryDTO>();
        cfg.CreateMap<UserViewModel, UserDTO>();
        cfg.CreateMap<CustomerViewModel, CustomerDTO>();
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
