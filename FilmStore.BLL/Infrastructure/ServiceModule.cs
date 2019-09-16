using System;
using FilmStore.BLL.Interfaces;
using FilmStore.BLL.Services;
using FilmStore.DAL.EF;
using FilmStore.DAL.Entities;
using FilmStore.DAL.Interfaces;
using FilmStore.DAL.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;


namespace FilmStore.BLL.Infrastructure
{
  public static class ServiceModule
  {
    public static IServiceCollection Services { get; set; }
    public static IServiceProvider Provider { get; private set; }

    public static void Load()
    {
      Services.AddIdentity<User, IdentityRole>()
        .AddEntityFrameworkStores<FilmStoreContext>()
        .AddDefaultTokenProviders();

      // Ошибка, если менять на AddSingleton
      Services.AddScoped<IUnitOfWork, EFUnitOfWork>();
      Services.AddScoped<IOrderService, OrderService>();
      Services.AddScoped<IUserService, UserService>();
      Services.AddScoped<IAdminService, AdminService>();
      Provider = Services.BuildServiceProvider();
    }
  }
}
