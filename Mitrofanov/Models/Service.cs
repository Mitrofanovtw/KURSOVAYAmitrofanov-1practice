using System.ComponentModel.DataAnnotations;

namespace StudioStatistic.Models
{
    public class Service
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; } = null!;

        [Required]
        public decimal Price { get; set; }

        public List<Request> Requests { get; set; } = new();
    }
}