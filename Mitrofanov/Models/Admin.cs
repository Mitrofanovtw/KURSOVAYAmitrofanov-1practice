using System.ComponentModel.DataAnnotations;

namespace StudioStatistic.Models
{
    public class Admin
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(30)]
        public string Name { get; set; } = null!;

        [Required, EmailAddress]
        public string Email { get; set; } = null!;

        [Required, MinLength(6)]
        public string PasswordHash { get; set; } = null!;
    }
}