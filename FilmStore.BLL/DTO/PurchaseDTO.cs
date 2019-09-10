using FilmStore.DAL.Entities;
using System;
using System.Collections.Generic;

namespace FilmStore.BLL.DTO
{
  public class PurchaseDTO
  {
    public int Id { get; set; }
    public CustomerDTO Customer { get; set; }
    public Status Status { get; set; }
    public int[] Quantity { get; set; }
    public FilmDTO[] Films { get; set; }
    public DateTime Date { get; set; }
  }
}
