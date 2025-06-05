using Microsoft.EntityFrameworkCore;
using GameData.Models;
using GameData.Repositories.Interfaces;
using NLog;

namespace GameData.Repositories
{
    /// <summary>  
    /// Репозиторий для пользователя
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly DbForGame _context;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public UserRepository(DbForGame context)
        {
            _context = context;
            Logger.Debug("Репозиторий пользователей инициализирован");
        }

        /// <summary>  
        /// Возвращает пользователя по его ID
        /// </summary>
        public async Task<User> GetByIdAsync(Guid id)
        {
            Logger.Trace($"Запрос пользователя по ID: {id}");
            return await _context.Users.FindAsync(id);
        }

        /// <summary>  
        /// Возвращает пользователя по его логину
        /// </summary>
        public async Task<User> GetByLoginAsync(string username)
        {
            Logger.Debug($"Поиск пользователя по логину: {username}");
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Username == username);
        }

        /// <summary>  
        /// Возвращает список всех пользователей 
        /// </summary>
        public async Task<List<User>> GetAllAsync()
        {
            Logger.Debug("Запрос всех пользователей");
            return await _context.Users.ToListAsync();
        }

        /// <summary>  
        /// Добавляет нового пользователя
        /// </summary>
        public async Task AddAsync(User user)
        {
            Logger.Info($"Добавление нового пользователя: {user.Username} (ID: {user.Id})");
            await _context.Users.AddAsync(user);
        }

        /// <summary>  
        /// Сохраняет все изменения в бд контексте
        /// </summary>
        public async Task SaveAsync()
        {
            Logger.Debug("Сохранение изменений пользователей в базе данных");
            await _context.SaveChangesAsync();
        }
    }
}