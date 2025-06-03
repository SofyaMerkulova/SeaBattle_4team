using Microsoft.EntityFrameworkCore;
using GameData.Models;


namespace GameData.Repositories
{
    /// <summary>  
    /// Репозиторий для пользователя
    /// </summary>
    public class UserRepository : IUserRepository
    {

        private readonly DbForGame _context;
        
        public UserRepository(DbForGame context)
        {
            _context = context;
        }
        /// <summary>  
        /// Возвращает пользователя по его ID
        /// </summary>
        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.Users
                .FindAsync(id);
        }
        /// <summary>  
        /// Возвращает пользователя по его логину
        /// </summary>
        public async Task<User> GetByLoginAsync(string username)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Username == username);
        }
        /// <summary>  
        /// Вовзвращает список всех пользователей 
        /// </summary>
        public async Task<List<User>> GetAllAsync()
        {
            return await _context.Users
                .ToListAsync();
        }
        /// <summary>  
        /// Добавляет нового пользователя
        /// </summary>
        public async Task AddAsync(User user)
        {
            await _context.Users
                .AddAsync(user);
        }
        /// <summary>  
        /// Сохраняет все изменения в бд контексте
        /// </summary>
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
