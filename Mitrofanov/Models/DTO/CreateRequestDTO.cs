using System.ComponentModel.DataAnnotations;

namespace StudioStatistic.Models.DTO
{
    public class CreateRequestDto
    {
        [Required] public int ClientId { get; set; }
        [Required] public int EngineerId { get; set; }
        [Required] public int ServiceId { get; set; }
        [Required] public DateTime DateOfVisit { get; set; }
        public string? Description { get; set; }
    }
}