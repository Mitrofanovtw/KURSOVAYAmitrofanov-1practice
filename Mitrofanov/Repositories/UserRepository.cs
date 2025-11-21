using Microsoft.EntityFrameworkCore;
using StudioStatistic.Models;

namespace StudioStatistic.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly APIDBContext _context;

        public UserRepository(APIDBContext context)
        {
            _context = context;
        }

        public User? GetById(int id) => _context.Users.Find(id);

        public IEnumerable<User> GetAll() => _context.Users.ToList();

        public void Update(User user) => _context.Users.Update(user);

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}