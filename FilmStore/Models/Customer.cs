using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilmStore.Models
{
  public class Customer
  {
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Name { get; set; }
    public string UserRef { get; set; }
    public virtual User User { get; set; }
    public virtual List<Purchase> Purchases { get; set; }
  }
}
