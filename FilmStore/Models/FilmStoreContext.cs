using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FilmStore.Models
{
  public class FilmStoreContext : IdentityDbContext<User>
  {
    public DbSet<Film> Films { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<Producer> Producers { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Purchase> Purchases { get; set; }

    public FilmStoreContext(DbContextOptions options) : base(options)
    {
      Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
      modelBuilder.Entity<FilmGenre>()
        .HasKey(k => new { k.GenreId, k.FilmId });

      modelBuilder.Entity<FilmCountry>()
        .HasKey(k => new { k.CountryId, k.FilmId });

      modelBuilder.Entity<FilmPurchase>()
        .HasKey(k => new { k.FilmId, k.PurchaseId });

      modelBuilder.Entity<User>()
        .HasOne(user => user.Customer)
        .WithOne(customer => customer.User)
        .HasForeignKey<Customer>(customer => customer.UserRef);
    }
  }
}