using AutoMapper;
using FilmStore.BLL.DTO;
using FilmStore.BLL.Interfaces;
using FilmStore.DAL.Entities;
using FilmStore.DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmStore.BLL.Services
{
  class AdminService : IAdminService
  {
    IUnitOfWork Database { get; set; }
    public AdminService(IUnitOfWork db)
    {
      Database = db;
    }

    public IEnumerable<CountryDTO> GetCountries()
    {
      var mapper = new MapperConfiguration(cfg =>
      {
        cfg.CreateMap<Country, CountryDTO>()
        .ForMember(src => src.Films, opt => opt.Ignore());
      }).CreateMapper();
      return mapper.Map<IEnumerable<Country>, IEnumerable<CountryDTO>>(Database.Countries.GetAll());
    }

    public IEnumerable<GenreDTO> GetGenres()
    {
      var mapper = new MapperConfiguration(cfg =>
      {
        cfg.CreateMap<Genre, GenreDTO>()
        .ForMember(src => src.Films, opt => opt.Ignore());
      }).CreateMapper();
      return mapper.Map<IEnumerable<Genre>, IEnumerable<GenreDTO>>(Database.Genres.GetAll());
    }

    public IEnumerable<ProducerDTO> GetProducers()
    {
      var mapper = new MapperConfiguration(cfg =>
      {
        cfg.CreateMap<Producer, ProducerDTO>()
        .ForMember(src => src.Films, opt => opt.Ignore());
      }).CreateMapper();
      return mapper.Map<IEnumerable<Producer>, IEnumerable<ProducerDTO>>(Database.Producers.GetAll());
    }

    public void SaveFilm(FilmDTO filmDTO)
    {
      Film film;

      if (filmDTO.Id == 0)
      {
        film = new Film
        {
          Id = 0,
          Name = filmDTO.Name,
          Price = filmDTO.Price,
          Rate = filmDTO.Rate,
          Year = filmDTO.Year,
          Purchases = new List<FilmPurchase>(),
          Countries = new List<FilmCountry>(),
          Genres = new List<FilmGenre>()
        };

        film.Producer = Database.Producers.Get(filmDTO.ProducerId);
        foreach (int countryId in filmDTO.CountriesId)
        {
          film.Countries.Add(new FilmCountry
          {
            Film = film,
            FilmId = film.Id,
            Country = Database.Countries.Get(countryId),
            CountryId = countryId
          });
        }
        foreach (int genreId in filmDTO.GenresId)
        {
          film.Genres.Add(new FilmGenre
          {
            Film = film,
            FilmId = film.Id,
            Genre = Database.Genres.Get(genreId),
            GenreId = genreId
          });
        }
        Database.Films.Create(film);
      }
      else
      {
        film = Database.Films.Get(filmDTO.Id);
        if(film != null)
        {
          film.Name = filmDTO.Name;
          film.Price = filmDTO.Price;
          film.Year = filmDTO.Year;
          film.Rate = filmDTO.Rate;

          film.Producer = Database.Producers.Get(filmDTO.ProducerId);

          film.Countries.Clear();
          foreach (int countryId in filmDTO.CountriesId)
          {
            film.Countries.Add(new FilmCountry
            {
              Film = film,
              FilmId = film.Id,
              Country = Database.Countries.Get(countryId),
              CountryId = countryId
            });
          }

          film.Genres.Clear();
          foreach (int genreId in filmDTO.GenresId)
          {
            film.Genres.Add(new FilmGenre
            {
              Film = film,
              FilmId = film.Id,
              Genre = Database.Genres.Get(genreId),
              GenreId = genreId
            });
          }
        }
        Database.Films.Update(film);
      }
      Database.SaveAsync().Wait();
    }

    public void DeleteFilm(int id)
    {
      Database.Films.Delete(id);
      Database.SaveAsync().Wait();
    }

    public FilmDTO GetFilm(int id)
    {
      var film = Database.Films.Get(id);
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
      return mapper.Map<Film, FilmDTO>(film);
    }
  }
}
