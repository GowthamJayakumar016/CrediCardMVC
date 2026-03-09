using CreditCardAppMvc.DTOs;
using CreditCardAppMvc.Models;

namespace CreditCardAppMvc.Services.Interfaces
{
    public interface IAuthService
    {
        Task Register(RegisterDto dto);

        Task<User> Login(LoginDto dto);
    }
}