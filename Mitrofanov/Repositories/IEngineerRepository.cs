using StudioStatistic.Models;

namespace StudioStatistic.Repositories
{
    public interface IEngineersRepository : IRepository<Engineers>
    {
        Task<Engineers?> GetByIdAsync(int id);
        Task CreateAsync(Engineers engineer);
        Task DeleteAsync(int id);
    }
}