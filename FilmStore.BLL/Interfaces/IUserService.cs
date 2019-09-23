using FilmStore.BLL.DTO;
using System.Threading.Tasks;

namespace FilmStore.BLL.Interfaces
{
  public interface IUserService
  {
    Task<OperationDetails> CreateAsync(UserDTO userDto);
    Task<string> CreateEmailTokenAsync(UserDTO userDTO);
    Task<string> GetUserIdAsync(UserDTO userDTO);
    Task<OperationDetails> EditUserAsync(UserDTO userDTO);
    Task<UserDTO> GetUserByNameAsync(string userName);
  }
}
