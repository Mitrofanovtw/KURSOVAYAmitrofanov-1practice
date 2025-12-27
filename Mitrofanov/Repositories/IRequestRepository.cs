using StudioStatistic.Models;
using StudioStatistic.Repositories;

namespace StudioStatistic
{
    public interface IRequestRepository : IRepository<Request>
    {
        Task<List<Request>> GetByClientIdAsync(int clientId);
    }
}