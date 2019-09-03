using FilmStore.BLL.DTO;
using System.Threading.Tasks;

namespace FilmStore.BLL.Interfaces
{
  public interface IUserService
  {
    Task<OperationDetails> Create(UserDTO userDto);
    Task<string> CreateEmailToken(UserDTO userDTO);
    Task<string> GetUserIdAsync(UserDTO userDTO);
    Task<OperationDetails> EditUserAsync(UserDTO userDTO);
    Task<UserDTO> GetUserByNameAsync(string userName);
  }
}
