using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using FilmStore.Models;

namespace FilmStore.Controllers
{
  public class HomeController : Controller
  {
    readonly FilmStoreContext db;
    readonly IEnumerable<Film> films;
    public HomeController(FilmStoreContext context)
    {
      db = context;
      films = db.Films.OrderBy(film => film.Name).ToList();
    }

    public IActionResult Index()
    {
      return View(films);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }
}