using StudioStatistic.Models;

namespace StudioStatistic.Repositories
{
    public interface IUserRepository
    {
        User? GetById(int id);
        IEnumerable<User> GetAll();
        void Update(User user);
        Task SaveChangesAsync();
    }
}