using System;
using System.Collections.Generic;

namespace FilmStore.BLL.DTO
{
  public class PurchaseDTO
  {
    public int Id { get; set; }
    public CustomerDTO Customer { get; set; }
    public List<FilmDTO> Films { get; set; }
    public DateTime Date { get; set; }
  }
}
