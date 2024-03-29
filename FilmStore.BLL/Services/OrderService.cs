﻿using FilmStore.BLL.DTO;
using FilmStore.BLL.Infrastructure;
using FilmStore.BLL.Interfaces;
using FilmStore.DAL.Entities;
using FilmStore.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmStore.BLL.Services
{
  public class OrderService : IOrderService
  {
    IUnitOfWork Database { get; set; }
    public OrderService(IUnitOfWork unitOfWork)
    {
      Database = unitOfWork;
    }

    public IEnumerable<FilmDTO> GetFilms(string searchString = null, string genre = null, 
      string country = null, string producer = null, string yearFrom = null, string yearTo = null, 
      int page = 1, int pageSize = 3, SortState sortOrder = SortState.NameAsc, FilmStatus filmStatus = 0)
    {
      var mapper = MapperService.CreateFilmToFilmDTOMapper();
      IEnumerable<Film> films;
      if (filmStatus == 0)
        films = Database.Films.GetAll();
      else
        films = Database.Films.GetAll().Where(f => f.Status == filmStatus);

      var filmsDTO = mapper.Map<IEnumerable<Film>, IEnumerable<FilmDTO>>(films);

      if (searchString != null)
        filmsDTO = filmsDTO.Where(f => f.Name.ToUpper().Contains(searchString.ToUpper()));
      if (genre != null)
        filmsDTO = filmsDTO.Where(f => f.Genres.Any(g => g.Id.ToString() == genre));
      if (country != null)
        filmsDTO = filmsDTO.Where(f => f.Countries.Any(c => c.Id.ToString() == country));
      if (producer != null)
        filmsDTO = filmsDTO.Where(f => f.Producer.Name.ToUpper().Contains(producer.ToUpper()));
      if (yearFrom != null)
        filmsDTO = filmsDTO.Where(f => int.Parse(f.Year) >= int.Parse(yearFrom));
      if (yearTo != null)
        filmsDTO = filmsDTO.Where(f => int.Parse(f.Year) <= int.Parse(yearTo));

      switch(sortOrder)
      {
        case SortState.NameAsc:
          filmsDTO = filmsDTO.OrderBy(f => f.Name);
          break;
        case SortState.NameDesc:
          filmsDTO = filmsDTO.OrderByDescending(f => f.Name);
          break;
        case SortState.PriceAsc:
          filmsDTO = filmsDTO.OrderBy(f => f.Price);
          break;
        case SortState.PriceDesc:
          filmsDTO = filmsDTO.OrderByDescending(f => f.Price);
          break;
        case SortState.ProducerAsc:
          filmsDTO = filmsDTO.OrderBy(f => f.Producer.Name);
          break;
        case SortState.ProducerDesc:
          filmsDTO = filmsDTO.OrderByDescending(f => f.Producer.Name);
          break;
        case SortState.QuantityAsc:
          filmsDTO = filmsDTO.OrderBy(f => f.QuantityInStock);
          break;
        case SortState.QuantityDesc:
          filmsDTO = filmsDTO.OrderByDescending(f => f.QuantityInStock);
          break;
        case SortState.YearAsc:
          filmsDTO = filmsDTO.OrderBy(f => f.Year);
          break;
        case SortState.YearDesc:
          filmsDTO = filmsDTO.OrderByDescending(f => f.Year);
          break;
        case SortState.RateAsc:
          filmsDTO = filmsDTO.OrderBy(f => f.Rate);
          break;
        case SortState.RateDesc:
          filmsDTO = filmsDTO.OrderByDescending(f => f.Rate);
          break;
      }
      if (page != 0)
      {
        var count = filmsDTO.Count();
        filmsDTO = filmsDTO.Skip((page - 1) * pageSize).Take(pageSize).ToList();
      }

      return filmsDTO;
    }

    public IEnumerable<PurchaseDTO> GetPurchases(int page = 0, int pageSize = 0, string searchString = null, string name = null)
    {
      var mapper = MapperService.CreateFilmToFilmDTOMapper();
      IEnumerable<Purchase> purchases = Database.Purchases.GetAll();
      IEnumerable<PurchaseDTO> purchasesDTO = 
        mapper.Map<IEnumerable<Purchase>, IEnumerable<PurchaseDTO>>(purchases);

      if(name != null)
      {
        int userId = Database.Customers.Find(c => c.Name == name).First().Id;
        purchasesDTO = purchasesDTO.Where(p => p.Customer.Id == userId);
      }
      if (searchString != null)
        purchasesDTO = purchasesDTO.Where(f => f.Customer.Name.ToUpper().Contains(searchString.ToUpper()));

      purchasesDTO = purchasesDTO.OrderByDescending(p => p.Date);

      if (page != 0 && pageSize != 0)
      {
        var count = purchasesDTO.Count();
        var items = purchasesDTO.Skip((page - 1) * pageSize).Take(pageSize);
        return items;
      }
      return purchasesDTO;
    }

    public async Task<FilmDTO> GetFilmAsync(int id)
    {
      var film = await Database.Films.Get(id);
      if (film == null)
        throw new ValidationException("Film not found", $"Id: {id}");
      var mapper = MapperService.CreateFilmToFilmDTOMapper();
      var filmDto = mapper.Map<Film, FilmDTO>(film);
      return filmDto;
    }
    public async Task AddPurchaseAsync(IEnumerable<FilmDTO> filmDTOs, string userName)
    {
      List<FilmPurchase> films = new List<FilmPurchase>();

      Customer customer = Database.Customers.Find(c => c.Name == userName).First();

      Purchase purchase = new Purchase { Customer = customer, Date = DateTime.Now, Status = Status.Pending };

      foreach (var item in filmDTOs.GroupBy(f=>f.Id))
      {
        films.Add(new FilmPurchase
        {
          FilmId = item.Key,
          Film = await Database.Films.Get(item.Key),
          Quantity = item.Count(),
          PurchaseId = purchase.Id,
          Purchase = purchase
        });
      }
      purchase.Films = films;
      await Database.Purchases.Create(purchase);

      await Database.SaveAsync();
    }

    public int FilmsCount()
    {
      return  Database.Films.GetAll().Count();
    }
    public void Dispose()
    {
      Database.Dispose();
    }
  }
}
