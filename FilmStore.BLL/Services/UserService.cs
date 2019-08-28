using FilmStore.BLL.DTO;
using FilmStore.BLL.Interfaces;
using FilmStore.DAL.Entities;
using FilmStore.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FilmStore.BLL.Services
{
  public class UserService : IUserService
  {

    IUnitOfWork Database { get; set; }
    public UserService(IUnitOfWork uow)
    {
      Database = uow;
    }

    public async Task<OperationDetails> Create(UserDTO userDto)
    {
      User user = await Database.UserManager.FindByEmailAsync(userDto.Email);
      if (user == null)
      {
        user = new User { Email = userDto.Email, UserName = userDto.UserName };
        var result = await Database.UserManager.CreateAsync(user, userDto.Password);
        if (result.Errors.Count() > 0)
          return new OperationDetails(false, result.Errors.FirstOrDefault().Description, "");

        await Database.UserManager.AddToRoleAsync(user, userDto.Role);

        Customer customer = new Customer { Name = userDto.UserName, UserRef = user.Id, Purchases = new List<Purchase>() };
        Database.ClientManager.Create(customer);

        await Database.SignInManager.SignInAsync(user, isPersistent: false);
        return new OperationDetails(true, "Registration finished successfully", "");
      }
      else
      {
        return new OperationDetails(false, "User with this login already exists", "Email");
      }
    }
    public async Task<string> CreateEmailToken(UserDTO userDTO)
    {
      User user = await Database.UserManager.FindByEmailAsync(userDTO.Email);
      if(user != null)
      {
        var token = await Database.UserManager.GenerateEmailConfirmationTokenAsync(user);
        return token;
      } else {
        throw new ArgumentNullException("user");
      }
    }

    public Task<ClaimsIdentity> Authenticate(UserDTO userDto)
    {
      throw new KeyNotFoundException();
    }

    public async Task<string> GetUserIdAsync(UserDTO userDTO)
    {
      User user = await Database.UserManager.FindByEmailAsync(userDTO.Email);
      if (user == null)
        return null;
      return user.Id;
    }
  }
}
