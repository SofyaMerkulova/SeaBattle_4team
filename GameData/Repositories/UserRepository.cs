using Microsoft.EntityFrameworkCore;
using GameData.Models;


namespace GameData.Repositories
{
    public class UserRepository : IUserRepository
    {

        private readonly DbForGame _context;

        public UserRepository(DbForGame context)
        {
            _context = context;
        }
        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.Users
                .FindAsync(id);
        }

        public async Task<User> GetByLoginAsync(string username)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _context.Users
                .ToListAsync();
        }

        public async Task AddAsync(User user)
        {
            await _context.Users
                .AddAsync(user);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
