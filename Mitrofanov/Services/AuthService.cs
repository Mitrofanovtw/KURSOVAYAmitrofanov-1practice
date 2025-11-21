using Microsoft.IdentityModel.Tokens;
using StudioStatistic.Models;
using StudioStatistic.Models.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;

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
            if (_context.Users.Any(u => u.Email == dto.Email))
                throw new InvalidOperationException("User already exists");

            var user = new User
            {
                Username = dto.Username,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Role = UserRole.Client
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var token = GenerateJwtToken(user.Username, user.Email, user.Role.ToString());
            return new AuthResponseDto
            {
                Token = token,
                Expires = DateTime.UtcNow.AddMinutes(60),
                Username = user.Username,
                Role = user.Role.ToString()
            };
        }

        public async Task<AuthResponseDto?> LoginAsync(LoginRequestDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (user != null)
            {
                var isPasswordValid = await Task.Run(() => BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash));
                if (isPasswordValid)
                {
                    return GenerateAuthResponse(user.Username, user.Email, user.Role.ToString());
                }
            }

            var admin = await _context.Admins.FirstOrDefaultAsync(a => a.Email == dto.Email);
            if (admin != null)
            {
                var isPasswordValid = await Task.Run(() => BCrypt.Net.BCrypt.Verify(dto.Password, admin.PasswordHash));
                if (isPasswordValid)
                {
                    return GenerateAuthResponse(admin.Name, admin.Email, "Admin");
                }
            }

            return null;
        }

        private AuthResponseDto GenerateAuthResponse(string username, string email, string role)
        {
            var token = GenerateJwtToken(username, email, role);
            return new AuthResponseDto
            {
                Token = token,
                Expires = DateTime.UtcNow.AddMinutes(60),
                Username = username,
                Role = role
            };
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