using StudioStatistic.Models;

namespace StudioStatistic.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(int id);
        IEnumerable<User> GetAll();
        Task UpdateAsync(User user);
        Task SaveChangesAsync();
        Task<User?> GetByEmailAsync(string email);
        Task<List<User>> GetByRoleAsync(UserRole role);
    }
}