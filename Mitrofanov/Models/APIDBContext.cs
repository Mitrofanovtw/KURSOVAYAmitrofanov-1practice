using Microsoft.EntityFrameworkCore;

namespace StudioStatistic.Models
{
    public class APIDBContext : DbContext
    {
        public DbSet<Client> Clients { get; set; } = null!;
        public DbSet<Request> Requests { get; set; } = null!;
        public DbSet<Admin> Admins { get; set; } = null!;
        public DbSet<Engineers> Engineers { get; set; } = null!;
        public DbSet<Service> Services { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;

        public APIDBContext(DbContextOptions<APIDBContext> options) : base(options) { }
    }
}