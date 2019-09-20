using FilmStore.DAL.Entities;
using System;

namespace FilmStore.WEB.Models
{
  public class PurchaseViewModel
  {
    public int Id { get; set; }
    public CustomerViewModel Customer { get; set; }
    public int[] Quantity { get; set; }
    public FilmViewModel[] Films { get; set; }
    public Status Status { get; set; }
    public DateTime Date { get; set; }
  }
}
