using System;
using System.Collections.Generic;

namespace FilmStore.WEB.Models
{
  public class PurchaseViewModel
  {
    public int Id { get; set; }
    public CustomerViewModel Customer { get; set; }
    public List<FilmViewModel> Films { get; set; }
    public DateTime Date { get; set; }
  }
}
