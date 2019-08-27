using System.Collections.Generic;

namespace FilmStore.BLL.DTO
{
  public class CustomerDTO
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string UserRef { get; set; }
    public UserDTO User { get; set; }
    public List<PurchaseDTO> Purchases { get; set; }
  }
}
