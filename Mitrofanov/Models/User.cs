using System.ComponentModel.DataAnnotations;

namespace StudioStatistic.Models
{
    public enum UserRole
    {
        Admin,
        Engineer,
        Client
    }

    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        public string PasswordHash { get; set; } = null!;

        public UserRole Role { get; set; } = UserRole.Client;
    }
}