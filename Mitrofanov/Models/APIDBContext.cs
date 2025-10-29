using Microsoft.EntityFrameworkCore;

namespace StudioStatistic.Models
{
    public class APIDBContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Engineers> Engineers { get; set; }

        public APIDBContext(DbContextOptions<APIDBContext>options) : base (options) { }
    }
}
