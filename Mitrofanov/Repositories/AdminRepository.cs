using StudioStatistic.Models;

namespace StudioStatistic.Repositories
{
    public class AdminRepository : RepositoryBase<Admin>, IAdminRepository
    {
        public AdminRepository(APIDBContext context) : base(context) { }
    }
}