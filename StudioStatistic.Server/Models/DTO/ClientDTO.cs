namespace StudioStatistic.Web.Models.DTO
{
    public class ClientDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public int QuantityOfVisits { get; set; }
    }
}