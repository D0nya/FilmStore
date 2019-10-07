using FilmStore.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace FilmStore.DAL.Interfaces
{
  public interface IUnitOfWork : IDisposable
  {
    ICountryRepository Countries { get; }
    ICustomerRepository Customers { get; }
    IFilmRepository Films { get; }
    IGenreRepository Genres { get; }
    IProducerRepository Producers { get; }
    IPurchaseRepository Purchases { get; }
    IUserRepository Users { get; }
    INewsRepository News { get; }

    UserManager<User> UserManager { get; }
    RoleManager<IdentityRole> RoleManager { get; }
    SignInManager<User> SignInManager { get; }

    Task SaveAsync();
  }
}
