using BCrypt.Net;
using CreditCardAppMvc.Data;
using CreditCardAppMvc.DTOs;
using CreditCardAppMvc.Models;
using CreditCardAppMvc.Services.Interfaces;

using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CreditCardAppMvc.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;

        public AuthService(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
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

        public async Task<(string token, string role)> Login(LoginDto dto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Email == dto.Email);

            if (user == null)
                throw new Exception("User not found");

            bool valid = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);

            if (!valid)
                throw new Exception("Invalid password");

            var key = Encoding.UTF8.GetBytes(
                _config["Jwt:Key"] ?? throw new Exception("JWT key missing")
            );

            var claims = new[]
            {
        new Claim("UserId", user.Id.ToString()),
        new Claim("Role", user.Role)
    };

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials:
                new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256)
            );

            string jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return (jwt, user.Role);
        }
    }
}