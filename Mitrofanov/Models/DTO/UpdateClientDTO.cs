using System.ComponentModel.DataAnnotations;

namespace StudioStatistic.DTO
{
    public class UpdateClientDto
    {
        [Required(ErrorMessage = "Имя клиента обязательно")]
        [StringLength(25, MinimumLength = 2, ErrorMessage = "Имя должно быть от 2 до 25 символов")]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "Фамилия клиента обязательна")]
        [StringLength(25, MinimumLength = 2, ErrorMessage = "Фамилия должна быть от 2 до 30 символов")]
        public string LastName { get; set; } = null!;

        [Range(0, 100000, ErrorMessage = "Количество посещений должно быть от 0 до 100000")]
        public int QuantityOfVisits { get; set; }
    }
}