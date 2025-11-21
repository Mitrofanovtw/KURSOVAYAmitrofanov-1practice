using System.ComponentModel.DataAnnotations;

namespace StudioStatistic.Models
{
    public class Client
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(25)]
        public string FirstName { get; set; } = null!;

        [Required, MaxLength(25)]
        public string LastName { get; set; } = null!;

        public int QuantityOfVisits { get; set; } = 0;

        public List<Request> Requests { get; set; } = new();
    }
}