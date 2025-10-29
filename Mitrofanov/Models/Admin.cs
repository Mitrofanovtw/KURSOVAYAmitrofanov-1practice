using System.ComponentModel.DataAnnotations;

namespace StudioStatistic.Models
{
    public class Admin
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; } = null!;

    }
}
