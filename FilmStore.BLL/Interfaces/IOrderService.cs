using FilmStore.BLL.DTO;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FilmStore.BLL.Interfaces
{
  public interface IOrderService
  {
    Task<FilmDTO> GetFilm(int id);
    IEnumerable<FilmDTO> GetFilms(string searchString = null, string genre=null, string country=null, 
      string producer = null, string yearFrom = null, string yearTo = null, int page = 1, int pageSize = 3,
      SortState sortOrder = SortState.NameAsc);

    IEnumerable<FilmDTO> GetFilmsFromCart(HttpContext context, string key);
    void AddFilmsToCart(HttpContext context, string key, IEnumerable<FilmDTO> films);
    void RemoveFromCart(HttpContext context, string key, bool first, Predicate<FilmDTO> predicate);

    Task AddPurchase(HttpContext context, string key);

    IEnumerable<PurchaseDTO> GetPurchases(string name = null);

    int FilmsCount();

    void Dispose();
  }
}
