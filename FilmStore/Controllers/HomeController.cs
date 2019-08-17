using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using FilmStore.Models;
using System;

namespace FilmStore.Controllers
{
  public class HomeController : Controller
  {
    readonly FilmStoreContext db;
    readonly IEnumerable<Film> films;
    readonly IEnumerable<Producer> producers;
    readonly IEnumerable<Purchase> purchases;
    public HomeController(FilmStoreContext context)
    {
      db = context;
      films = db.Films.OrderBy(film => film.Name);
      producers = db.Producers.OrderBy(prod => prod.Name);
      purchases = db.Purchases.Where(p => p.Customer.Id == 1);
    }

    public IActionResult Index()
    {
      return View(films.ToList());
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public IActionResult Purchases()
    {
      return View(purchases.ToList());
    }

    [HttpGet("/AddFilm")]
    public IActionResult AddFilm()
    {
      ViewBag.Producers = producers;
      return View();
    }

    [HttpPost("/AddFilm")]
    public string AddFilm(Film film)
    {
      try
      {
        db.Films.Add(film);
        db.SaveChanges();
      }
      catch(Exception ex)
      {
        return ex.Message;
      }
      return "Success!";
    }
  }
}