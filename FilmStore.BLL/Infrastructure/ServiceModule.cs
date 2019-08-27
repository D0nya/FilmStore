using System;
using FilmStore.BLL.Interfaces;
using FilmStore.BLL.Services;
using FilmStore.DAL.Interfaces;
using FilmStore.DAL.Repositories;
using FilmStore.DBL.EF;
using FilmStore.DBL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace FilmStore.BLL.Infrastructure
{
  public static class ServiceModule
  {
    public static IServiceCollection Services { get; set; }
    public static IServiceProvider Provider { get; private set; }

    public static void Load(DbContextOptions options)
    {
      Services.AddIdentity<User, IdentityRole>()
        .AddEntityFrameworkStores<FilmStoreContext>()
        .AddDefaultTokenProviders();

      Services.AddSingleton(options);
      Services.AddScoped<IUnitOfWork, EFUnitOfWork>();
      Services.AddScoped<IOrderService, OrderService>();
      Services.AddScoped<IUserService, UserService>();
      Provider = Services.BuildServiceProvider();
    }
  }
}
