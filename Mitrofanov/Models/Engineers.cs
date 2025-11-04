using System.ComponentModel.DataAnnotations;

namespace StudioStatistic.Models
{
    public class Engineers
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(35)]
        public string Name { get; set; } = null!;

        [Required, MaxLength(150)]
        public string Adress { get; set; } = null!;

        [Required, MaxLength(35)]
        public string WorkExp { get; set; } = null!;

        [MaxLength(300)]
        public string AboutHimself { get; set; } = string.Empty;

        public List<Request> Requests { get; set; } = new();
    }
}