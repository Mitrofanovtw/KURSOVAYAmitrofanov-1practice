using StudioStatistic.Models;

namespace StudioStatistic.Repositories
{
    public class RequestRepository : RepositoryBase<Request>, IRequestRepository
    {
        public RequestRepository(APIDBContext context) : base(context) { }
    }
}