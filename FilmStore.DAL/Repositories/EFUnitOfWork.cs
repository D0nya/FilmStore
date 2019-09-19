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

    private CountryRepository  countryRepository;
    private CustomerRepository customerRepository;
    private FilmRepository filmRepository;
    private GenreRepository genreRepository;
    private ProducerRepository producerRepository;
    private PurchaseRepository purchaseRepository;
    private UserRepository userRepository;
    private NewsRepository newsRepository;

    private readonly IClientManager clientManager;
    private readonly UserManager<User> userManager;
    private readonly RoleManager<IdentityRole> roleManager;
    private readonly SignInManager<User> signInManager;

    public EFUnitOfWork(DbContextOptions options, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, SignInManager<User> signInManager)
    {
      db = new FilmStoreContext(options);
      this.userManager = userManager;
      this.roleManager = roleManager;
      this.signInManager = signInManager;
      clientManager = new ClientManager(db);
    }

    public IRepository<Country> Countries
    {
      get
      {
        if (countryRepository == null)
          countryRepository = new CountryRepository(db);
        return countryRepository;
      }
    }
    public IRepository<Customer> Customers
    {
      get
      {
        if (customerRepository == null)
          customerRepository = new CustomerRepository(db);
        return customerRepository;
      }
    }
    public IRepository<Film> Films
    {
      get
      {
        if (filmRepository == null)
          filmRepository = new FilmRepository(db);
        return filmRepository;
      }
    }
    public IRepository<Genre> Genres
    {
      get
      {
        if (genreRepository == null)
          genreRepository = new GenreRepository(db);
        return genreRepository;
      }
    }
    public IRepository<Producer> Producers
    {
      get
      {
        if (producerRepository == null)
          producerRepository = new ProducerRepository(db);
        return producerRepository;
      }
    }
    public IRepository<Purchase> Purchases
    {
      get
      {
        if (purchaseRepository == null)
          purchaseRepository = new PurchaseRepository(db);
        return purchaseRepository;
      }
    }
    public IRepository<User> Users
    {
      get
      {
        if (userRepository == null)
          userRepository = new UserRepository(db);
        return userRepository;
      }
    }
    public IRepository<News> News
    {
      get
      {
        if (newsRepository == null)
          newsRepository = new NewsRepository(db);
        return newsRepository;
      }
    }

    public IClientManager ClientManager { get { return clientManager; } }
    public UserManager<User> UserManager { get { return userManager; } }
    public RoleManager<IdentityRole> RoleManager { get { return roleManager; } }
    public SignInManager<User> SignInManager { get { return signInManager; } }

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
      clientManager.Dispose();
      GC.SuppressFinalize(true);
    }
  }
}
