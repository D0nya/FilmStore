using Microsoft.EntityFrameworkCore;

namespace FilmStore.Models
{
  public class FilmStoreContext : DbContext
  {
    public DbSet<Film> Films { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<Producer> Producers { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Purchase> Purchases { get; set; }

    public FilmStoreContext(DbContextOptions options) : base(options)
    {
      Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<FilmCategory>()
        .HasKey(k => new { k.CategoryId, k.FilmId });

      modelBuilder.Entity<FilmCountry>()
        .HasKey(k => new { k.CountryId, k.FilmId });
    }
  }
}