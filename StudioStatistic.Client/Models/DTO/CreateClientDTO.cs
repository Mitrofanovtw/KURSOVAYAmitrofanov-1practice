using System.ComponentModel.DataAnnotations;

namespace StudioStatistic.Client.Models.DTO
{
    public class CreateClientDto
    {
        [Required] public string FirstName { get; set; } = null!;
        [Required] public string LastName { get; set; } = null!;
        public int QuantityOfVisits { get; set; } = 0;
    }
}