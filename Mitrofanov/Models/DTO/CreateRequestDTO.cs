namespace StudioStatistic.DTO
{
    public class CreateRequestDto
    {
        public int ClientId { get; set; }
        public int EngineerId { get; set; }
        public int ServiceId { get; set; }
        public string? Description { get; set; }
        public DateTime DateOfVisit { get; set; }
    }
}