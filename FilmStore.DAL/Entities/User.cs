using Microsoft.AspNetCore.Identity;

namespace FilmStore.DBL.Entities
{
  public class User : IdentityUser
  {
    public virtual Customer Customer { get; set; }
  }
}
