using StudioStatistic.Models;

namespace StudioStatistic.Repositories
{
    public class ClientRepository : RepositoryBase<Client>, IClientRepository
    {
        public ClientRepository(APIDBContext context) : base(context) { }
    }
}