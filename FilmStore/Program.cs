using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using FilmStore.Models;
using Microsoft.Extensions.DependencyInjection;

namespace FilmStore
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var host = CreateWebHostBuilder(args).Build();
      using(var scope = host.Services.CreateScope())
      {
        var services = scope.ServiceProvider;

        try
        {
          var context = services.GetRequiredService<FilmStoreContext>();
          SampleData.Initialize(context);
        }
        catch(Exception ex)
        {
          var logger = services.GetRequiredService<ILogger<Program>>();
          logger.LogError(ex, "Error occured while initializing database.");
        }
      }
      host.Run();
    }

    public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>();
  }
  public static class SampleData
  {
    public static void Initialize(FilmStoreContext context)
    {
      Country c1 = new Country() { Name = "USA" };
      Country c2 = new Country() { Name = "Britain" };
      Producer p1 = new Producer() { Name = "Guy Ritchie" };
      Producer p2 = new Producer() { Name = "Quntin Tarantino" };

      Genre cat1 = new Genre() { Name = "Crime" };
      Genre cat2 = new Genre() { Name = "Comedy" };
      Genre cat3 = new Genre() { Name = "War" };

      if (!context.Countries.Any())
      {
        context.Countries.AddRange(c1, c2);
        context.Producers.AddRange(p1, p2);
        context.Genres.AddRange(cat1, cat2, cat3);
        context.SaveChanges();

        Film film1 = new Film() { Name = "Lock, Stock and Two Smoking Barrels",
                                  Countries = new List<FilmCountry>(),
                                  Genres = new List<FilmGenre>(),
                                  Producer = p1, Year = "1998",
                                  Rate = 8.2f, Price = 10M };

        film1.Genres.Add(new FilmGenre { FilmId = film1.Id, GenreId = cat1.Id });
        film1.Genres.Add(new FilmGenre { FilmId = film1.Id, GenreId = cat2.Id });
        film1.Countries.Add(new FilmCountry { FilmId = film1.Id, CountryId = c1.Id });

        Film film2 = new Film() { Name = "Inglourious Basterds",
                                  Countries = new List<FilmCountry>(), Genres = new List<FilmGenre>(),
                                  Producer = p2, Year = "2009",
                                  Rate = 8.3f, Price = 9.5M };
        film2.Genres.Add(new FilmGenre { FilmId = film2.Id, GenreId = cat3.Id });
        film2.Countries.Add(new FilmCountry { FilmId = film2.Id, CountryId = c1.Id });
        
        Film film3 = new Film() { Name = "Snatch.",
                                  Countries = new List<FilmCountry>(), Genres = new List<FilmGenre>(),
                                  Producer = p1, Year = "2000",
                                  Rate = 8.3f, Price = 11.33M };
        film3.Genres.Add(new FilmGenre { FilmId = film3.Id, GenreId = cat1.Id });
        film3.Genres.Add(new FilmGenre { FilmId = film3.Id, GenreId = cat2.Id });
        film3.Countries.Add(new FilmCountry { FilmId = film3.Id, CountryId = c2.Id });

        Film film4 = new Film() { Name = "Pulp Fiction",
                                  Countries = new List<FilmCountry>(), Genres = new List<FilmGenre>(),
                                  Producer = p2, Year = "1994",
                                  Rate = 8.9f, Price = 15M };
        film4.Genres.Add(new FilmGenre { FilmId = film4.Id, GenreId = cat1.Id });
        film4.Countries.Add(new FilmCountry { FilmId = film4.Id, CountryId = c1.Id });

        context.Films.AddRange(film1, film2, film3, film4);

        context.SaveChanges();
      }
    }
  }
}
