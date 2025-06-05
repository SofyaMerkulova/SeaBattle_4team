using GameData.Models;
using GameData.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using NLog;


namespace GameData.Repositories
{
    /// <summary>  
    /// Репозиторий для игр
    /// </summary>  
    public class GameRepository : IGameRepository
    {
        private readonly DbForGame _dbContext;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        public GameRepository(DbForGame dbContext)
        {
            _dbContext = dbContext;
            Logger.Debug("Инициализация репозитория игр");
        }
        /// <summary>  
        /// Возвращает список всех игр включая игроков
        /// </summary>  
        public async Task<List<Game>> GetAllAsync()
        {
            Logger.Trace("Запрос на получение всех игр с игроками");

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
            Logger.Debug($"Запрос игры по ID: {id}");
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
            Logger.Trace("Поиск активной игры");
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
            Logger.Info($"Добавление игры: {game?.Id}");
            await _dbContext.Games.AddAsync(game);
        }
        /// <summary>  
        /// Обновляет информацию об игре
        /// </summary>  
        public async Task UpdateAsync(Game game)
        {
            Logger.Info($"Обновление информации об игре: {game?.Id}");
            _dbContext.Games.Update(game);
        }
        /// <summary>  
        /// Сохранение всех изменений в бд контексте
        /// </summary>  
        public async Task SaveAsync()
        {
            Logger.Debug("Сохранение всех изменений в базу данных");
            await _dbContext.SaveChangesAsync();
        }
    }
}
