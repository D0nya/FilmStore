using FilmStore.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace FilmStore.DAL.Interfaces
{
  public interface IUnitOfWork : IDisposable
  {
    IRepository<Country> Countries { get; }
    IRepository<Customer> Customers { get; }
    IRepository<Film> Films { get; }
    IRepository<Genre> Genres { get; }
    IRepository<Producer> Producers { get; }
    IRepository<Purchase> Purchases { get; }
    IRepository<User> Users { get; }
    IRepository<News> News { get; }

    IClientManager ClientManager { get; }
    UserManager<User> UserManager { get; }
    RoleManager<IdentityRole> RoleManager { get; }
    SignInManager<User> SignInManager { get; }

    Task SaveAsync();
  }
}
