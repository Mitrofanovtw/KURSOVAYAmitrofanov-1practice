namespace StudioStatistic.Client.Models.DTO
{
    public class RequestDto
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int EngineerId { get; set; }
        public int ServiceId { get; set; }
        public DateTime DateOfVisit { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Status { get; set; } = "New";
    }

    public class CreateRequestDto
    {
        public int EngineerId { get; set; }
        public int ServiceId { get; set; }
        public DateTime DateOfVisit { get; set; }
        public string Description { get; set; } = string.Empty;
    }

    public class UpdateStatusDto
    {
        public string Status { get; set; } = "New";
    }
}