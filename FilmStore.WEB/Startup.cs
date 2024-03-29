﻿using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using FilmStore.DAL.EF;
using FilmStore.DAL.Entities;
using FilmStore.DAL.Interfaces;
using FilmStore.DAL.Repositories;
using FilmStore.BLL.Interfaces;
using FilmStore.BLL.Services;
using FilmStore.WEB.Areas.Identity;

namespace FilmStore.WEB
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.Configure<CookiePolicyOptions>(options =>
      {
        // This lambda determines whether user consent for non-essential cookies is needed for a given request.
        options.CheckConsentNeeded = context => true;
        options.MinimumSameSitePolicy = SameSiteMode.None;
      });

      string connection = Configuration.GetConnectionString("DefaultConnection");
      services.AddDbContext<FilmStoreContext>(options => options
      .UseSqlServer(connection)
      .UseLazyLoadingProxies());

      services.AddIdentity<User, IdentityRole>()
        .AddEntityFrameworkStores<FilmStoreContext>()
        .AddDefaultTokenProviders();

      services.AddScoped<IUnitOfWork, EFUnitOfWork>();
      services.AddScoped<IOrderService, OrderService>();
      services.AddScoped<IUserService, UserService>();
      services.AddScoped<IAdminService, AdminService>();
      services.AddScoped<INewsService, NewsService>();

      services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
      .AddRazorPagesOptions(options =>
      {
        options.AllowAreas = true;
        options.Conventions.AuthorizeAreaFolder("Identity", "/Account/Manage");
        options.Conventions.AuthorizeAreaPage("Identity", "/Account/Logout");
      });

      services.ConfigureApplicationCookie(options =>
      {
        options.LoginPath = $"/Identity/Account/Login";
        options.LogoutPath = $"/Identity/Account/Logout";
        options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
      });

      services.AddDistributedMemoryCache();
      services.AddSession(options => 
      {
        options.IdleTimeout = TimeSpan.FromMinutes(120);
      });

      services.AddScoped<IEmailSender, EmailSender>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
      }


      app.UseHttpsRedirection();
      app.UseStaticFiles();
      app.UseAuthentication();
      app.UseSession();
      app.UseCookiePolicy();

      app.UseMvc(routes =>
      {
        routes.MapRoute(
                  name: "default",
                  template: "{controller=Home}/{action=Index}/{id?}");
      });
    }
  }
}
