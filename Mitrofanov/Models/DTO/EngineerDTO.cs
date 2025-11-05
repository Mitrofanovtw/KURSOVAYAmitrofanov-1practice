namespace StudioStatistic.Models.DTO
{
    public class EngineerDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Adress { get; set; } = null!;
        public string WorkExp { get; set; } = null!;
        public string? AboutHimself { get; set; }
    }
}