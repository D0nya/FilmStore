using FilmStore.BLL.DTO;
using FilmStore.DAL.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace FilmStore.BLL.Interfaces
{
  public interface IOrderService
  {
    IEnumerable<FilmDTO> GetFilms(string searchString = null, string genre=null, string country=null, 
      string producer = null, string yearFrom = null, string yearTo = null, int page = 1, int pageSize = 3,
      SortState sortOrder = SortState.NameAsc, FilmStatus filmStatus = 0);
    IEnumerable<PurchaseDTO> GetPurchases(int page = 0, int pageSize = 0, string searchString = null, string name = null);
    IEnumerable<FilmDTO> GetFilmsFromCart(HttpContext context, string key);

    Task AddPurchaseAsync(HttpContext context, string key);
    Task<FilmDTO> GetFilmAsync(int id);
    MemoryStream GetPurchasesStream();
    int FilmsCount();

    void Dispose();
    void AddFilmsToCart(HttpContext context, string key, IEnumerable<FilmDTO> films);
    void RemoveFromCart(HttpContext context, string key, bool first, Predicate<FilmDTO> predicate);
  }
}
