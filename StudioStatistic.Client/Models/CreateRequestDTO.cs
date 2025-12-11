using StudioStatistic.Client.Validation;
using System.ComponentModel.DataAnnotations;

namespace StudioStatistic.Client.Models.DTO
{
    public class CreateRequestDto
    {
        [Required(ErrorMessage = "ID клиента обязателен")]
        [Range(1, int.MaxValue)]
        public int ClientId { get; set; }

        [Required(ErrorMessage = "ID инженера обязателен")]
        [Range(1, int.MaxValue)]
        public int EngineerId { get; set; }

        [Required(ErrorMessage = "ID услуги обязателен")]
        [Range(1, int.MaxValue)]
        public int ServiceId { get; set; }

        [Required(ErrorMessage = "Дата визита обязательна")]
        [FutureDate(ErrorMessage = "Дата визита должна быть в будущем")]
        public DateTime DateOfVisit { get; set; }

        [StringLength(200)]
        public string? Description { get; set; }
    }
}