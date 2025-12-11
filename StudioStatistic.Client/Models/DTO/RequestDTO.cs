using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioStatistic.Client.Models.DTO
{
    public class RequestDto
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; } = null!;
        public int EngineerId { get; set; }
        public string EngineerName { get; set; } = null!;
        public int ServiceId { get; set; }
        public string ServiceName { get; set; } = null!;
        public decimal Cost { get; set; }
        public DateTime DateOfVisit { get; set; }
        public string? Description { get; set; }
    }
}
