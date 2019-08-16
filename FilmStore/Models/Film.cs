using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilmStore.Models
{
  public class Film
  {
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Year { get; set; }
    public decimal Price { get; set; }
    public float Rate { get; set; }
    public virtual List<FilmCountry> Countries { get; set; }
    public virtual Producer Producer { get; set; }
    public virtual List<FilmCategory> Categories { get; set; }
  }
}
