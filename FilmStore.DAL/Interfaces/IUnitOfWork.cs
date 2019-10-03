using FilmStore.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace FilmStore.DAL.Interfaces
{
  public interface IUnitOfWork : IDisposable
  {
    IGenericRepository<Country> Countries { get; }
    IGenericRepository<Customer> Customers { get; }
    IGenericRepository<Film> Films { get; }
    IGenericRepository<Genre> Genres { get; }
    IGenericRepository<Producer> Producers { get; }
    IGenericRepository<Purchase> Purchases { get; }
    IGenericRepository<User> Users { get; }
    IGenericRepository<News> News { get; }

    UserManager<User> UserManager { get; }
    RoleManager<IdentityRole> RoleManager { get; }
    SignInManager<User> SignInManager { get; }

    Task SaveAsync();
  }
}
