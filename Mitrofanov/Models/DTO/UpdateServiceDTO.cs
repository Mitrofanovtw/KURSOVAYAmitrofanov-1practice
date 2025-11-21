using System.ComponentModel.DataAnnotations;

namespace StudioStatistic.Models.DTO
{
    public class UpdateServiceDto
    {
        [Required(ErrorMessage = "Название услуги обязательно")]
        [StringLength(100, MinimumLength = 7, ErrorMessage = "Название должно быть от 7 до 100 символов")]
        public string Name { get; set; } = null!;

        [Range(500, 100000, ErrorMessage = "Цена должна быть от 500 до 100,000")]
        public decimal Price { get; set; }
    }
}