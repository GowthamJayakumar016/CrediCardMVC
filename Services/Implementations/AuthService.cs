using CreditCardAppMvc.Data;
using CreditCardAppMvc.DTOs;
using CreditCardAppMvc.Models;
using CreditCardAppMvc.Services.Interfaces;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;

namespace CreditCardAppMvc.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;

        public AuthService(AppDbContext context)
        {
            _context = context;
        }

        public async Task Register(RegisterDto dto)
        {
            var existing = await _context.Users
                .FirstOrDefaultAsync(x => x.Email == dto.Email);

            if (existing != null)
                throw new Exception("Email already exists");

            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Role = "User"
            };

            _context.Users.Add(user);

            await _context.SaveChangesAsync();
        }

        public async Task<User> Login(LoginDto dto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Email == dto.Email);

            if (user == null)
                throw new Exception("User not found");

            bool valid = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);

            if (!valid)
                throw new Exception("Invalid password");

            return user;
        }
    }
}