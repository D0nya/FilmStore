using AutoMapper;
using FilmStore.BLL.DTO;
using FilmStore.BLL.Interfaces;
using FilmStore.DAL.Entities;
using FilmStore.DAL.Interfaces;
using System.Collections.Generic;
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
      var countries = mapper.Map<IEnumerable<Country>, IEnumerable<CountryDTO>>(Database.Countries.GetAll());
      return countries;
    }

    public IEnumerable<GenreDTO> GetGenres()
    {
      var mapper = new MapperConfiguration(cfg =>
      {
        cfg.CreateMap<Genre, GenreDTO>()
        .ForMember(src => src.Films, opt => opt.Ignore());
      }).CreateMapper();
      var  genres = mapper.Map<IEnumerable<Genre>, IEnumerable<GenreDTO>>(Database.Genres.GetAll());
      return genres;
    }

    public IEnumerable<ProducerDTO> GetProducers()
    {
      var mapper = new MapperConfiguration(cfg =>
      {
        cfg.CreateMap<Producer, ProducerDTO>()
        .ForMember(src => src.Films, opt => opt.Ignore());
      }).CreateMapper();
      var producers = mapper.Map<IEnumerable<Producer>, IEnumerable<ProducerDTO>>(Database.Producers.GetAll());
      return producers;
    }

    public async void ChangeQuantityInStock(FilmDTO filmDTO)
    {
      Film film = await Database.Films.Get(filmDTO.Id);
      if(film != null)
      {
        film.QuantityInStock = filmDTO.QuantityInStock;
        Database.Films.Update(film);
        await Database.SaveAsync();
      }
    }

    public async void SaveFilm(FilmDTO filmDTO)
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

        film.Producer = await Database.Producers.Get(filmDTO.ProducerId);
        foreach (int countryId in filmDTO.CountriesId)
        {
          film.Countries.Add(new FilmCountry
          {
            Film = film,
            FilmId = film.Id,
            Country = await Database.Countries.Get(countryId),
            CountryId = countryId
          });
        }
        foreach (int genreId in filmDTO.GenresId)
        {
          film.Genres.Add(new FilmGenre
          {
            Film = film,
            FilmId = film.Id,
            Genre = await Database.Genres.Get(genreId),
            GenreId = genreId
          });
        }
        Database.Films.Create(film);
      }
      else
      {
        film = await Database.Films.Get(filmDTO.Id);
        if(film != null)
        {
          film.Name = filmDTO.Name;
          film.Price = filmDTO.Price;
          film.Year = filmDTO.Year;
          film.Rate = filmDTO.Rate;
          film.QuantityInStock = filmDTO.QuantityInStock;
          film.Producer = await Database.Producers.Get(filmDTO.ProducerId);

          film.Countries.Clear();
          foreach (int countryId in filmDTO.CountriesId)
          {
            film.Countries.Add(new FilmCountry
            {
              Film = film,
              FilmId = film.Id,
              Country = await Database.Countries.Get(countryId),
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
              Genre = await Database.Genres.Get(genreId),
              GenreId = genreId
            });
          }
        }
        Database.Films.Update(film);
      }
      await Database.SaveAsync();
    }

    public async void SavePurchase(PurchaseDTO purchaseDTO)
    {
      Purchase purchase = await Database.Purchases.Get(purchaseDTO.Id);
      if(purchase != null)
      {
        purchase.Status = purchaseDTO.Status;
        Database.Purchases.Update(purchase);

        if (purchase.Status == Status.Confirmed)
        {
          foreach (var film in purchase.Films)
          {
            (await Database.Films.Get(film.FilmId)).QuantityInStock -= film.Quantity;
            Database.Films.Update(film.Film);
          }
        }

        await Database.SaveAsync();
      }
    }

    public async void DeleteFilm(int id)
    {
      Database.Films.Delete(id);
      await Database.SaveAsync();
    }

    public async Task<FilmDTO> GetFilm(int id)
    {
      var film = await Database.Films.Get(id);
      var mapper = MapperService.CreateFilmToFilmDTOMapper();
      var films = mapper.Map<Film, FilmDTO>(film);
      return films;
    }
  }
}
