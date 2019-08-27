using FilmStore.BLL.DTO;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FilmStore.BLL.Interfaces
{
  public interface IUserService
  {
    Task<OperationDetails> Create(UserDTO userDto);
    Task<ClaimsIdentity> Authenticate(UserDTO userDto);
    Task<string> CreateEmailToken(UserDTO userDTO);
    Task<string> GetUserIdAsync(UserDTO userDTO);

  }
}
