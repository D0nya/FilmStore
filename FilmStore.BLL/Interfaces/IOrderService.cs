using FilmStore.BLL.DTO;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace FilmStore.BLL.Interfaces
{
  public interface IOrderService
  {
    FilmDTO GetFilm(int id);
    IEnumerable<FilmDTO> GetFilms();

    IEnumerable<FilmDTO> GetFilmsFromCart(HttpContext context, string key);
    void AddFilmsToCart(HttpContext context, string key, IEnumerable<FilmDTO> films);
    void RemoveFromCart(HttpContext context, string key, bool first, Predicate<FilmDTO> predicate);

    void AddPurchase(HttpContext context, string key);

    void Dispose();
  }
}
