using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilmStore.Models
{
  public class Purchase
  {
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public virtual Customer Customer { get; set; }
    public virtual List<FilmPurchase> Films { get; set; }
    public DateTime Date { get; set; }
    public decimal Sum()
    {
      decimal sum = 0M;
      foreach (var film in Films)
      {
        sum += film.Film.Price;
      }
      return sum;
    }
  }
}
