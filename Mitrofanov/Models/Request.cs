using System.ComponentModel.DataAnnotations;

namespace StudioStatistic.Models
{
    public enum RequestStatus
    {
        New,
        Accepted,
        Completed,
        Cancelled
    }

    public class Request
    {
        [Key]
        public int Id { get; set; }

        public int ClientId { get; set; }
        public Client Client { get; set; } = null!;

        public int EngineerId { get; set; }
        public Engineers Engineer { get; set; } = null!;

        public int ServiceId { get; set; }
        public Service Service { get; set; } = null!;

        [MaxLength(200)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public decimal Cost { get; set; }

        [Required]
        public DateTime DateOfVisit { get; set; }
        public RequestStatus Status { get; set; } = RequestStatus.New;
    }
}