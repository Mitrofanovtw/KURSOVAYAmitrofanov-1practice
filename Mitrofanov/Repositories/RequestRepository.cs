using Microsoft.EntityFrameworkCore;
using StudioStatistic.Models;

namespace StudioStatistic.Repositories
{
    public class RequestRepository : RepositoryBase<Request>, IRequestRepository
    {
        public RequestRepository(APIDBContext context) : base(context) { }

        public override IEnumerable<Request> GetAll()
        {
            return _dbSet
                .Include(r => r.Client)
                .Include(r => r.Engineer)
                .Include(r => r.Service)
                .ToList();
        }

        public override Request? GetById(int id)
        {
            return _dbSet
                .Include(r => r.Client)
                .Include(r => r.Engineer)
                .Include(r => r.Service)
                .FirstOrDefault(r => r.Id == id);
        }
    }
}