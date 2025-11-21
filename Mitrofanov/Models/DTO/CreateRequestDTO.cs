using System.ComponentModel.DataAnnotations;
using StudioStatistic.Validation;

namespace StudioStatistic.Models.DTO
{
    public class CreateRequestDto
    {
        [Required(ErrorMessage = "ID клиента обязателен")]
        [Range(1, int.MaxValue, ErrorMessage = "ID клиента должен быть положительным числом")]
        public int ClientId { get; set; }

        [Required(ErrorMessage = "ID инженера обязателен")]
        [Range(1, int.MaxValue, ErrorMessage = "ID инженера должен быть положительным числом")]
        public int EngineerId { get; set; }

        [Required(ErrorMessage = "ID услуги обязателен")]
        [Range(1, int.MaxValue, ErrorMessage = "ID услуги должен быть положительным числом")]
        public int ServiceId { get; set; }

        [Required(ErrorMessage = "Дата визита обязательна")]
        [FutureDate(ErrorMessage = "Дата визита должна быть в будущем")]
        public DateTime DateOfVisit { get; set; }

        [StringLength(200, ErrorMessage = "Описание не более 200 символов")]
        public string? Description { get; set; }
    }
}