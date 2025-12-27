using StudioStatistic.Models;

namespace StudioStatistic.Repositories
{
    public interface IAdminRepository : IRepository<Admin>
    {
        Task DeleteAsync(int id);
        Task<Admin> CreateAsync(Admin admin);
        Task<Admin?> GetByIdAsync(int id);
    }
}