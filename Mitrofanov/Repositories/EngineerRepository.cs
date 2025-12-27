using StudioStatistic.Models;

namespace StudioStatistic.Repositories
{
    public class EngineersRepository : RepositoryBase<Engineers>, IEngineersRepository
    {
        public EngineersRepository(APIDBContext context) : base(context) { }

        public async Task<Engineers?> GetByIdAsync(int id)
        {
            return await _context.Engineers.FindAsync(id);
        }

        public async Task CreateAsync(Engineers engineer)
        {
            _context.Engineers.Add(engineer);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var engineer = await _context.Engineers.FindAsync(id);
            if (engineer != null)
            {
                _context.Engineers.Remove(engineer);
                await _context.SaveChangesAsync();
            }
        }
    }
}