using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilmStore.DAL.Entities
{
  public class Purchase
  {

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public Status Status { get; set; }
    public virtual Customer Customer { get; set; }
    public virtual List<FilmPurchase> Films { get; set; }
    public DateTime Date { get; set; }
  }
}
