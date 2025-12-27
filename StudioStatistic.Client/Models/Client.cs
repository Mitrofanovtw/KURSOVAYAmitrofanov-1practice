using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioStatistic.Client.Models
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
