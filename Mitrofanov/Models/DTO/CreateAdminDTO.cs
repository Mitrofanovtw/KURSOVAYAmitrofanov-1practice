using System.ComponentModel.DataAnnotations;

namespace StudioStatistic.Models.DTO
{
    public class CreateAdminDto
    {
        [Required(ErrorMessage = "Имя администратора обязательно")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Имя должно быть от 2 до 30 символов")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Email обязателен")]
        [EmailAddress(ErrorMessage = "Некорректный формат email")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Пароль обязателен")]
        [MinLength(6, ErrorMessage = "Пароль должен быть не менее 6 символов")]
        public string Password { get; set; } = null!;
    }
}