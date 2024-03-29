﻿using Microsoft.AspNetCore.Identity;

namespace FilmStore.WEB.Models
{
  public class UserViewModel
  {
    public string Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string UserName { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string Role { get; set; }
    public CustomerViewModel Customer { get; set; }
  }
}
