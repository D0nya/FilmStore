using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmStore.Models
{
  public class User : IdentityUser
  {
    public virtual Customer Customer { get; set; }
  }
}
