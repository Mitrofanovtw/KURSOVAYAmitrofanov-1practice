using StudioStatistic.Models;

namespace StudioStatistic.Repositories
{
    public class EngineersRepository : RepositoryBase<Engineers>, IEngineersRepository
    {
        public EngineersRepository(APIDBContext context) : base(context) { }
    }
}