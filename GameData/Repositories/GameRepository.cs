using Microsoft.EntityFrameworkCore;
using GameData.Models;
using GameData.Repositories.Interfaces;

namespace GameData.Repositories
{
    /// <summary>  
    /// Репозиторий для игр
    /// </summary>  
    public class GameRepository : IGameRepository
    {
        private readonly DbForGame _dbContext;

        public GameRepository(DbForGame dbContext)
        {
            _dbContext = dbContext;
        }
        /// <summary>  
        /// Возвращает список всех игр включая игроков
        /// </summary>  
        public async Task<List<Game>> GetAllAsync()
        {
            return await _dbContext.Games
                .Include(g => g.PlayerFirst)
                .Include(g => g.PlayerSecond)
                .AsNoTracking()
                .ToListAsync();
        }
        /// <summary>  
        /// Получение игры по ID
        /// </summary>  
        public async Task<Game> GetByIdAsync(Guid id)
        {
            using var context = new DbForGame();
            return await context.Games
                .Include(g => g.PlayerFirst)
                .Include(g => g.PlayerSecond)
                .AsNoTracking()
                .FirstOrDefaultAsync(g => g.Id == id);
        }
        /// <summary>  
        /// Вовзращает активную игру 
        /// </summary>  
        public async Task<Game> GetActiveGameAsync()
        {
            using var context = new DbForGame();
            return await context.Games
                .Where(g => g.PlayerSecondId == null && g.Status == GameStatus.Waiting)
                .OrderByDescending(g => g.CreatedDate)
                .FirstOrDefaultAsync();
        }
        /// <summary>  
        /// Добавляет новую игру
        /// </summary>  
        public async Task AddAsync(Game game)
        {
            await _dbContext.Games.AddAsync(game);
        }
        /// <summary>  
        /// Обновляет информацию об игре
        /// </summary>  
        public async Task UpdateAsync(Game game)
        {
            _dbContext.Games.Update(game);
        }
        /// <summary>  
        /// Сохранение всех изменений в бд контексте
        /// </summary>  
        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
