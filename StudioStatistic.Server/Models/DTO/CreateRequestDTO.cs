using System.ComponentModel.DataAnnotations;

namespace StudioStatistic.Web.Models.DTO
{
    public class CreateRequestDto
    {
        [Required(ErrorMessage = "ID клиента обязателен")]
        [Range(1, int.MaxValue, ErrorMessage = "ID клиента должен быть положительным")]
        public int ClientId { get; set; }

        [Required(ErrorMessage = "ID инженера обязателен")]
        [Range(1, int.MaxValue, ErrorMessage = "ID инженера должен быть положительным")]
        public int EngineerId { get; set; }

        [Required(ErrorMessage = "ID услуги обязателен")]
        [Range(1, int.MaxValue, ErrorMessage = "ID услуги должен быть положительным")]
        public int ServiceId { get; set; }

        [Required(ErrorMessage = "Дата визита обязательна")]
        public DateTime DateOfVisit { get; set; }

        [StringLength(200, ErrorMessage = "Описание не более 200 символов")]
        public string? Description { get; set; }
    }
}