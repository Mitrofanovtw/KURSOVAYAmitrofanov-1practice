namespace StudioStatistic.Web.Models.DTO
{
    public class EngineerDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Adress { get; set; } = null!;
        public string WorkExp { get; set; } = null!;
        public string? AboutHimself { get; set; }
    }
}