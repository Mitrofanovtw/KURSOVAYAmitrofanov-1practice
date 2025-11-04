using StudioStatistic.Models;

namespace StudioStatistic.Repositories
{
    public class ServiceRepository : RepositoryBase<Service>, IServiceRepository
    {
        public ServiceRepository(APIDBContext context) : base(context) { }
    }
}