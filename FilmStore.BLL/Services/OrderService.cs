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
    public IEnumerable<FilmDTO> GetFilms()
    {
      var mapper = MapperService.CreateFilmToFilmDTOMapper();
      var films = mapper.Map<IEnumerable<Film>, List<FilmDTO>>(Database.Films.GetAll());
      return films;
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

    public void Dispose()
    {
      Database.Dispose();
    }
  }
}
