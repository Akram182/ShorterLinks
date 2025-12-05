using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using ShorterLinks.Server.DTOs;
using ShorterLinks.Server.Repositories;
using ShorterLinks.Server.Repositories.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ShorterLinks.Server.Services
{
    public class AuthService : IAuthService
    {

        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthService> _logger;

        public AuthService(
            ApplicationDbContext context, 
            IConfiguration configuration, 
            ILogger<AuthService> logger
        ){
            _context = context;
            _configuration = configuration;
            _logger = logger;
        }


        
        public async Task<AuthResultDto> Login(LoginDto loginDto)
        {
            User? user = null;
            string token = string.Empty;

            try
            {
                user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email);

                if (user == null || !VerifyPassword(loginDto.Password, user.PasswordHash))
                {
                    throw new Exception("Неверный email или пароль.");
                }

                token = GenerateJwtToken(user);

                if (token == string.Empty) {
                    throw new Exception("Не получилось создать JWT token.");
                }

               
            }
            catch (Exception ex) {
                _logger.LogError($"{nameof(Login)} => {ex.Message}");
            }


            _logger.LogInformation($"{user.Email} - Успешно залогинился.");

            return new AuthResultDto { 
                Token = token,
                User = new UserDto
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    CreateAt = user.CreateAt
                }
            };
        }

        public async Task<AuthResultDto> Register(RegisterDto registerDto)
        {
            if(await  _context.Users.AnyAsync(u => u.Email == registerDto.Email))
            {
                 throw new Exception("Пользователь с таким email уже существует!");
            }

            if (await _context.Users.AnyAsync(u => u.UserName == registerDto.UserName))
            {
                throw new Exception("Пользователь с таким именем уже существует!");
            }

            var passwordHash = HashPassword(registerDto.Password);

            var user = new User
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email,
                PasswordHash = passwordHash,
                CreateAt = DateTime.UtcNow
            };

            await _context.Users.AddAsync(user);

            await _context.SaveChangesAsync();

            var token = GenerateJwtToken(user);

            return new AuthResultDto
            {
                Token = token,
                User = new UserDto
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    CreateAt = user.CreateAt
                }
            };

        }


        private bool VerifyPassword(string password, string storedHash)
        {
            var parts = storedHash.Split(':');
            if (parts.Length != 2) return false;

            var passwordSalt = Convert.FromBase64String(parts[0]);
            var storedPasswordHash = Convert.FromBase64String(parts[1]);

            using var hmac = new HMACSHA512(passwordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            return computedHash.SequenceEqual(storedPasswordHash);
        }

        private string GenerateJwtToken(User user) {

            var jwtSettings = _configuration.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
            };


            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string HashPassword(string password)
        {
            using var hmac = new HMACSHA512();
            var passwordSalt = hmac.Key;
            var passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            return Convert.ToBase64String(passwordSalt) + ":" + Convert.ToBase64String(passwordHash);
        }

    }
}
