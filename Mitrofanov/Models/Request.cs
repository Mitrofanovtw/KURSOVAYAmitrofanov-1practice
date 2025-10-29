using System.ComponentModel.DataAnnotations;

namespace StudioStatistic.Models
{
    public class Request
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public List<Client> Client { get; set; } = null!;
        [Required]
        public List<Services> Service { get; set; } = null!;

        [MaxLength(200)]
        public string Description { get; set; } = string.Empty;
        [Required]
        public decimal Cost { get; set; }
        [Required]
        public DateTime DateOfVisit { get; set; } = CreateRequest.RequestDateTime;
    }
}
