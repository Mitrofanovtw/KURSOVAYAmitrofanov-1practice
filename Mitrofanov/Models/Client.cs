using System.ComponentModel.DataAnnotations;

namespace StudioStatistic.Models
{
    public class Client
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;

        public int QuantityOfVisits { get; set; }
    }
}
