using System.ComponentModel.DataAnnotations;

namespace StudioStatistic.Models.DTO
{
    public class RegisterRequestDto
    {
        [Required] public string Username { get; set; } = null!;
        [Required][EmailAddress] public string Email { get; set; } = null!;
        [Required][MinLength(6)] public string Password { get; set; } = null!;
        public UserRole Role { get; set; } = UserRole.Client;
    }

    public class LoginRequestDto
    {
        [Required] public string Email { get; set; } = null!;
        [Required] public string Password { get; set; } = null!;
    }

    public class AuthResponseDto
    {
        public string Token { get; set; } = null!;
        public DateTime Expires { get; set; }
        public string Username { get; set; } = null!;
        public string Role { get; set; } = null!;
    }
}