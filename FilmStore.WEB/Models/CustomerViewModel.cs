using System.Collections.Generic;

namespace FilmStore.WEB.Models
{
  public class CustomerViewModel
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string UserRef { get; set; }
    public UserViewModel User { get; set; }
    public List<PurchaseViewModel> Purchases { get; set; }
  }
}
