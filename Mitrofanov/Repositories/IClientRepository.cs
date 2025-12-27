using StudioStatistic.Models;

namespace StudioStatistic.Repositories
{
    public interface IClientRepository : IRepository<Client>
    {
        Task<Client?> GetByIdAsync(int id);
        Task CreateAsync(Client client);
        Task DeleteAsync(int id);
    }
}