using Microsoft.AspNetCore.Identity;

namespace FilmStore.DAL.Entities
{
  public class User : IdentityUser
  {
    public virtual Customer Customer { get; set; }
  }
}
