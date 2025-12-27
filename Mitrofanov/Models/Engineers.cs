using System.ComponentModel.DataAnnotations;

namespace StudioStatistic.Models
{
    public class Engineers
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(25)]
        public string FirstName { get; set; } = null!;

        [Required, MaxLength(25)]
        public string LastName { get; set; } = null!;

        [Required, MaxLength(150)]
        public string Adress { get; set; } = null!;
        public string WorkExp { get; set; } = null!;

        [MaxLength(200)]
        public string AboutHimself { get; set; } = string.Empty;

        public List<Request> Requests { get; set; } = new();
    }
}