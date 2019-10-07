using FilmStore.DAL.Interfaces;
using FilmStore.DAL.EF;
using FilmStore.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace FilmStore.DAL.Repositories
{
  public class EFUnitOfWork : IUnitOfWork
  {
    private readonly FilmStoreContext db;

    private ICountryRepository  countryRepository;
    private ICustomerRepository customerRepository;
    private IFilmRepository     filmRepository;
    private IGenreRepository    genreRepository;
    private IProducerRepository producerRepository;
    private IPurchaseRepository purchaseRepository;
    private IUserRepository     userRepository;
    private INewsRepository     newsRepository;

    private readonly UserManager<User> userManager;
    private readonly RoleManager<IdentityRole> roleManager;
    private readonly SignInManager<User> signInManager;

    public EFUnitOfWork(DbContextOptions options, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, SignInManager<User> signInManager)
    {
      db = new FilmStoreContext(options);
      this.userManager = userManager;
      this.roleManager = roleManager;
      this.signInManager = signInManager;
    }

    public ICountryRepository Countries
    {
      get
      {
        if (countryRepository == null)
          countryRepository = new CountryRepository(db);
        return countryRepository;
      }
    }
    public ICustomerRepository Customers
    {
      get
      {
        if (customerRepository == null)
          customerRepository = new CustomerRepository(db);
        return customerRepository;
      }
    }
    public IFilmRepository Films
    {
      get
      {
        if (filmRepository == null)
          filmRepository = new FilmRepository(db);
        return filmRepository;
      }
    }
    public IGenreRepository Genres
    {
      get
      {
        if (genreRepository == null)
          genreRepository = new GenreRepository(db);
        return genreRepository;
      }
    }
    public IProducerRepository Producers
    {
      get
      {
        if (producerRepository == null)
          producerRepository = new ProducerRepository(db);
        return producerRepository;
      }
    }
    public IPurchaseRepository Purchases
    {
      get
      {
        if (purchaseRepository == null)
          purchaseRepository = new PurchaseRepository(db);
        return purchaseRepository;
      }
    }
    public IUserRepository Users
    {
      get
      {
        if (userRepository == null)
          userRepository = new UserRepository(db);
        return userRepository;
      }
    }
    public INewsRepository News
    {
      get
      {
        if (newsRepository == null)
          newsRepository = new NewsRepository(db);
        return newsRepository;
      }
    }

    public UserManager<User> UserManager => userManager;
    public RoleManager<IdentityRole> RoleManager => roleManager; 
    public SignInManager<User> SignInManager => signInManager; 

    public async Task SaveAsync()
    {
      await db.SaveChangesAsync();
    }

    private bool disposed = false;
    public virtual void Dispose(bool disposing)
    {
      if(!disposed)
      {
        if (disposing)
          db.Dispose();
        disposed = true;
      }
    }

    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(true);
    }
  }
}
