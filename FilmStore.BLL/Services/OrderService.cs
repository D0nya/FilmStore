using FilmStore.BLL.DTO;
using FilmStore.BLL.Infrastructure;
using FilmStore.BLL.Interfaces;
using FilmStore.DAL.Entities;
using FilmStore.DAL.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
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
      var mapper = MapperService.CreateFilmToFilmDTOMapper();
      var filmDto = mapper.Map<Film, FilmDTO>(film);
      return filmDto;
    }



    public IEnumerable<FilmDTO> GetFilmsFromCart(HttpContext context, string key)
    {
      var items = context.Session.Get<IEnumerable<FilmDTO>>(key);
      return items;
    }
    public void AddFilmsToCart(HttpContext context, string key, IEnumerable<FilmDTO> films)
    {
      List<FilmDTO> res = new List<FilmDTO>();
      if (GetFilmsFromCart(context, key) != null)
        res.AddRange(GetFilmsFromCart(context, key));
      res.AddRange(films);
      context.Session.Set(key, res);
    }
    public void RemoveFromCart(HttpContext context, string key, bool first, Predicate<FilmDTO> predicate)
    {
      List<FilmDTO> films = new List<FilmDTO>();
      if (GetFilmsFromCart(context, key) != null)
        films = GetFilmsFromCart(context, key).ToList();
      else
        return;

      if (first)
        films.Remove(films.Where(new Func<FilmDTO, bool>(predicate)).First());
      else
        films.RemoveAll(predicate);
      context.Session.Set(key, films);
    }

    public void AddPurchase(HttpContext context, string key)
    {
      List<FilmPurchase> films = new List<FilmPurchase>();
      List<FilmDTO> filmDTOs = GetFilmsFromCart(context, key).ToList();

      Customer customer = Database.Customers.Find(c => c.Name == context.User.Identity.Name).First();

      Purchase purchase = new Purchase { Customer = customer, Date = DateTime.Now, Status = Status.Pending };

      foreach (var item in filmDTOs.GroupBy(f=>f.Id))
      {
        films.Add(new FilmPurchase
        {
          FilmId = item.Key,
          Film = Database.Films.Get(item.Key),
          Quantity = item.Count(),
          PurchaseId = purchase.Id,
          Purchase = purchase
        });
      }
      purchase.Films = films;
      Database.Purchases.Create(purchase);

      Database.SaveAsync().Wait();
      context.Session.Clear();
    }

    public IEnumerable<PurchaseDTO> GetPurchases(string name = null)
    {

      var mapper = MapperService.CreateFilmToFilmDTOMapper();
      IEnumerable<PurchaseDTO> purchases = 
        mapper.Map<IEnumerable<Purchase>, IEnumerable<PurchaseDTO>>(Database.Purchases.GetAll());

      if(name != null)
      {
        int userId = Database.Customers.Find(c => c.Name == name).First().Id;
        purchases = purchases.Where(p => p.Customer.Id == userId);
      }
      purchases = purchases.OrderByDescending(p => p.Date);
      return purchases;
    }
    public void Dispose()
    {
      Database.Dispose();
    }

    public IEnumerable<FilmDTO> GetFilms(string searchString = null, string genre = null, 
      string country = null, string producer = null, string yearFrom = null, string yearTo = null, 
      int page = 1, int pageSize = 3, SortState sortOrder = SortState.NameAsc)
    {
      var mapper = MapperService.CreateFilmToFilmDTOMapper();
      var films = mapper.Map<IEnumerable<Film>, IEnumerable<FilmDTO>>(Database.Films.GetAll());

      if (searchString != null)
        films = films.Where(f => f.Name.ToUpper().Contains(searchString.ToUpper()));
      if (genre != null)
        films = films.Where(f => f.Genres.Any(g => g.Id.ToString() == genre));
      if (country != null)
        films = films.Where(f => f.Countries.Any(c => c.Id.ToString() == country));
      if (producer != null)
        films = films.Where(f => f.Producer.Name.ToUpper().Contains(producer.ToUpper()));
      if (yearFrom != null)
        films = films.Where(f => int.Parse(f.Year) >= int.Parse(yearFrom));
      if (yearTo != null)
        films = films.Where(f => int.Parse(f.Year) <= int.Parse(yearTo));

      switch(sortOrder)
      {
        case SortState.NameAsc:
          films = films.OrderBy(f => f.Name);
          break;
        case SortState.NameDesc:
          films = films.OrderByDescending(f => f.Name);
          break;
        case SortState.PriceAsc:
          films = films.OrderBy(f => f.Price);
          break;
        case SortState.PriceDesc:
          films = films.OrderByDescending(f => f.Price);
          break;
        case SortState.ProducerAsc:
          films = films.OrderBy(f => f.Producer.Name);
          break;
        case SortState.ProducerDesc:
          films = films.OrderByDescending(f => f.Producer.Name);
          break;
        case SortState.QuantityAsc:
          films = films.OrderBy(f => f.QuantityInStock);
          break;
        case SortState.QuantityDesc:
          films = films.OrderByDescending(f => f.QuantityInStock);
          break;
        case SortState.YearAsc:
          films = films.OrderBy(f => f.Year);
          break;
        case SortState.YearDesc:
          films = films.OrderByDescending(f => f.Year);
          break;
        case SortState.RateAsc:
          films = films.OrderBy(f => f.Rate);
          break;
        case SortState.RateDesc:
          films = films.OrderByDescending(f => f.Rate);
          break;
      }
      var count = films.Count();
      var items = films.Skip((page - 1) * pageSize).Take(pageSize).ToList();

      return items;
    }

    public int FilmsCount()
    {
     return  Database.Films.GetAll().Count();
    }
  }
}
