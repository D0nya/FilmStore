using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilmStore.DAL.Entities
{
  public class Film
  {
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Name { get; set; }
    public int QuantityInStock { get; set; }
    public string Year { get; set; }
    public decimal Price { get; set; }
    public float Rate { get; set; }
    public virtual Producer Producer { get; set; }
    public virtual List<FilmCountry> Countries { get; set; }
    public virtual List<FilmGenre> Genres { get; set; }
    public virtual List<FilmPurchase> Purchases { get; set; }
  }
}
