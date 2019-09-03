using AutoMapper;
using FilmStore.BLL.DTO;
using FilmStore.BLL.Infrastructure;
using FilmStore.BLL.Interfaces;
using FilmStore.DAL.Entities;
using FilmStore.DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace FilmStore.BLL.Services
{
  public class OrderService : IOrderService
  {
    IUnitOfWork Database { get; set; }
    public OrderService(IUnitOfWork unitOfWork)
    {
      Database = unitOfWork;
    }

    public FilmDTO GetFilm(int id)
    {
      var film = Database.Films.Get(id);
      if (film == null)
        throw new ValidationException("Film not found", $"Id: {id}");
      var mapper = FilmToFilmDTOMapper();
      return mapper.Map<Film, FilmDTO>(film);
    }
    public IEnumerable<FilmDTO> GetFilms()
    {
      var mapper = FilmToFilmDTOMapper();
      return mapper.Map<IEnumerable<Film>, List<FilmDTO>>(Database.Films.GetAll()); 
    }
    private IMapper FilmToFilmDTOMapper()
    {
      var mapper = new MapperConfiguration(cfg =>
      {
        cfg.CreateMap<Country, CountryDTO>()
        .ForMember(dst => dst.Films, opt => opt.MapFrom(src => src.Films.Select(f => f.Film)));
        cfg.CreateMap<Genre, GenreDTO>()
        .ForMember(dst => dst.Films, opt => opt.MapFrom(src => src.Films.Select(f => f.Film)));
        cfg.CreateMap<Purchase, PurchaseDTO>()
        .ForMember(dst => dst.Films, opt => opt.MapFrom(src => src.Films.Select(f => f.Film)));

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
        .ForMember(dst => dst.Films, opt => opt.Ignore());
        cfg.CreateMap<Producer, ProducerDTO>()
        .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
        .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name))
        .ForMember(dst => dst.Films, opt => opt.MapFrom(src => src.Films));
        cfg.CreateMap<Film, FilmDTO>()
        .ForMember(dst => dst.Countries, opt => opt.MapFrom(src => src.Countries.Select(c => c.Country).ToList()))
        .ForMember(dst => dst.Genres, opt => opt.MapFrom(src => src.Genres.Select(g => g.Genre).ToList()))
        .ForMember(dst => dst.Purchases, opt => opt.MapFrom(src => src.Purchases.Select(p => p.Purchase).ToList()));
      }).CreateMapper();
      return mapper;
    }
    public void Dispose()
    {
      Database.Dispose();
    }
  }
}
