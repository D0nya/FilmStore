using AutoMapper;
using FilmStore.BLL.DTO;
using FilmStore.DAL.Entities;
using System.Linq;

namespace FilmStore.BLL.Services
{
  static class MapperService
  {
    public static IMapper CreateFilmToFilmDTOMapper()
    {
      var mapper = new MapperConfiguration(cfg =>
      {
        cfg.CreateMap<Country, CountryDTO>()
        .ForMember(dst => dst.Films, opt => opt.MapFrom(src => src.Films.Select(f => f.Film)));
        cfg.CreateMap<Genre, GenreDTO>()
        .ForMember(dst => dst.Films, opt => opt.MapFrom(src => src.Films.Select(f => f.Film)));
        cfg.CreateMap<Purchase, PurchaseDTO>()
        .ForMember(dst => dst.Films, opt => opt.MapFrom(src => src.Films.Select(fp => fp.Film)))
        .ForMember(dst => dst.Quantity, opt => opt.MapFrom(src => src.Films.Select(fp => fp.Quantity)));
        cfg.CreateMap<User, UserDTO>();
        cfg.CreateMap<Customer, CustomerDTO>()
        .ForMember(dst => dst.Purchases, opt => opt.Ignore());

        cfg.CreateMap<FilmCountry, CountryDTO>()
        .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Country.Id))
        .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Country.Name))
        .ForMember(dst => dst.Films, opt => opt.Ignore());
        cfg.CreateMap<FilmGenre, GenreDTO>()
        .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Genre.Id))
        .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Genre.Name))
        .ForMember(dst => dst.Films, opt => opt.Ignore());
        cfg.CreateMap<FilmPurchase, PurchaseDTO>()
        .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Purchase.Id))
        .ForMember(dst => dst.Date, opt => opt.MapFrom(src => src.Purchase.Date))
        .ForMember(dst => dst.Customer, opt => opt.MapFrom(src => src.Purchase.Customer))
        .ForMember(dst => dst.Status, opt => opt.MapFrom(src => src.Purchase.Status))
        .ForMember(dst => dst.Films, opt => opt.Ignore());
        cfg.CreateMap<Producer, ProducerDTO>()
        .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
        .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name))
        .ForMember(dst => dst.Films, opt => opt.MapFrom(src => src.Films));
        cfg.CreateMap<Film, FilmDTO>()
        .ForMember(dst => dst.Countries, opt => opt.MapFrom(src => src.Countries.Select(c => c.Country).ToList()))
        .ForMember(dst => dst.Genres, opt => opt.MapFrom(src => src.Genres.Select(g => g.Genre).ToList()))
        .ForMember(dst => dst.Purchases, opt => opt.Ignore());
      }).CreateMapper();
      return mapper;
    }
    public static IMapper CreatePurchaseDTOToPurchaseMapper()
    {
      var mapper = new MapperConfiguration(cfg =>
      {
        cfg.CreateMap<UserDTO, User>();
        cfg.CreateMap<CustomerDTO, Customer>()
        .ForMember(src => src.Purchases, opt => opt.Ignore());
        cfg.CreateMap<PurchaseDTO, Purchase>()
        .ForMember(src => src.Films, opt => opt.Ignore());
      }).CreateMapper();
      return mapper;
    }

    public static IMapper NewsToNewsDTOMapper()
    {
      var mapper = new MapperConfiguration(cfg =>
      {
        cfg.CreateMap<News, NewsDTO>();
        cfg.CreateMap<NewsDTO, News>();
      }).CreateMapper();
      return mapper;
    }
  }
}
