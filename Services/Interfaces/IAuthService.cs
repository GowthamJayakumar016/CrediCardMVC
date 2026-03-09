using CreditCardAppMvc.DTOs;


namespace CreditCardAppMvc.Services.Interfaces
{
    public interface IAuthService
    {
        Task Register(RegisterDto dto);

        Task<(string token, string role)> Login(LoginDto dto);
    }
}