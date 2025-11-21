using System.ComponentModel.DataAnnotations;

namespace StudioStatistic.Models.DTO
{
    public class CreateEngineerDto
    {
        [Required(ErrorMessage = "Имя звукоинженера обязательно")]
        [StringLength(25, MinimumLength = 2, ErrorMessage = "Имя должно быть от 2 до 25 символов")]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "Фамилия звукоинженера обязательна")]
        [StringLength(25, MinimumLength = 2, ErrorMessage = "Фамилия должна быть от 2 до 30 символов")]
        public string LastName { get; set; } = null!;

        [Required(ErrorMessage = "Адрес обязателен")]
        [StringLength(150, MinimumLength = 5, ErrorMessage = "Адрес должен быть от 5 до 150 символов")]
        public string Adress { get; set; } = null!;

        [Required(ErrorMessage = "Опыт работы обязателен")]
        [StringLength(35, ErrorMessage = "Опыт работы не более 35 символов")]
        public string WorkExp { get; set; } = null!;

        [StringLength(300, ErrorMessage = "Описание не более 200 символов")]
        public string? AboutHimself { get; set; }
    }
}