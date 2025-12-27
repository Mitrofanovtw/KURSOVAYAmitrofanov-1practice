using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StudioStatistic.Models;
using StudioStatistic.Models.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StudioStatistic.Services
{
    public class AuthService : IAuthService
    {
        private readonly APIDBContext _context;
        private readonly IConfiguration _config;

        public AuthService(APIDBContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterRequestDto dto)
        {
            if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
                throw new InvalidOperationException("Пользователь с таким email уже существует");

            var user = new User
            {
                Username = dto.Username,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Role = UserRole.Client
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            _context.Clients.Add(new Client
            {
                Id = user.Id,
                FirstName = dto.FirstName ?? "Новый",
                LastName = dto.LastName ?? "Клиент",
                QuantityOfVisits = 0
            });
            await _context.SaveChangesAsync();

            var token = GenerateJwtToken(user.Username, user.Email, user.Role.ToString());
            var fullName = $"{dto.FirstName ?? "Новый"} {dto.LastName ?? "Клиент"}".Trim();

            return new AuthResponseDto
            {
                Token = token,
                Expires = DateTime.UtcNow.AddMinutes(60),
                Username = user.Username,
                Role = user.Role.ToString(),
                FullName = fullName
            };
        }

        public async Task<AuthResponseDto?> LoginAsync(LoginRequestDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                return null;

            var token = GenerateJwtToken(user.Username, user.Email, user.Role.ToString());
            var fullName = await GetFullNameAsync(user);

            return new AuthResponseDto
            {
                Token = token,
                Expires = DateTime.UtcNow.AddMinutes(60),
                Username = user.Username,
                Role = user.Role.ToString(),
                FullName = fullName
            };
        }

        private async Task<string> GetFullNameAsync(User user)
        {
            return user.Role switch
            {
                UserRole.Admin => await GetAdminName(user.Id),
                UserRole.Engineer => await GetEngineerName(user.Id),
                UserRole.Client => await GetClientName(user.Id),
                _ => user.Username
            };
        }

        private async Task<string> GetAdminName(int userId)
        {
            var admin = await _context.Admins.FirstOrDefaultAsync(a => a.Id == userId);
            return admin?.Name ?? "Админ";
        }

        private async Task<string> GetEngineerName(int userId)
        {
            var engineer = await _context.Engineers.FirstOrDefaultAsync(e => e.Id == userId);
            return engineer != null
                ? $"{engineer.FirstName} {engineer.LastName}".Trim()
                : "Инженер";
        }

        private async Task<string> GetClientName(int userId)
        {
            var client = await _context.Clients.FirstOrDefaultAsync(c => c.Id == userId);
            return client != null
                ? $"{client.FirstName} {client.LastName}".Trim()
                : "Клиент";
        }

        private string GenerateJwtToken(string username, string email, string role)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(60),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}