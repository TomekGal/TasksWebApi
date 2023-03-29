
using TasksWebApi.Models;

namespace TasksWebApi.Interfaces
{
   public interface IAccountService
    {
       void RegisterUser(RegisterUserDto userDto);
        string GenerateJwt(LoginDto loginDto);
    }
}
