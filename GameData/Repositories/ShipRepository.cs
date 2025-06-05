using GameData.Models;
using Microsoft.EntityFrameworkCore;
using GameData.Repositories.Interfaces;
using NLog;

namespace GameData.Repositories
{
    /// <summary>  
    /// Репозиторий для кораблей
    /// </summary>
    public class ShipRepository : IShipRepository
    {
        private readonly DbForGame _dbContext;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public ShipRepository(DbForGame dbContext)
        {
            _dbContext = dbContext;
            Logger.Debug("Репозиторий кораблей инициализирован");
        }

        /// <summary>  
        /// Возвращает список всех кораблей
        /// </summary>
        public async Task<List<Ship>> GetAllAsync()
        {
            Logger.Debug("Запрос всех кораблей");
            return await _dbContext.Ships
                .AsNoTracking()
                .ToListAsync();
        }

        /// <summary>  
        /// Возвращает все корабли из игры по ее ID
        /// </summary>
        public async Task<List<Ship>> GetByGameIdAsync(Guid gameId)
        {
            Logger.Debug($"Запрос кораблей для игры ID: {gameId}");
            return await _dbContext.Ships
                .Where(s => s.GameId == gameId)
                .ToListAsync();
        }

        /// <summary>  
        /// Возвращает корабли по ID
        /// </summary>
        public async Task<Ship?> GetByIdAsync(Guid id)
        {
            Logger.Trace($"Запрос корабля по ID: {id}");
            return await _dbContext.Ships
                .FindAsync(id);
        }

        /// <summary>  
        /// Вовзращает игру и игрока по ID
        /// </summary>
        public async Task<List<Ship>> GetByGameAndPlayerAsync(Guid gameId, Guid playerId)
        {
            Logger.Debug($"Запрос кораблей для игры ID: {gameId} и игрока ID: {playerId}");
            return await _dbContext.Ships
                .Where(s => s.GameId == gameId && s.PlayerId == playerId)
                .ToListAsync();
        }

        /// <summary>  
        /// Добавляет новый корабль
        /// </summary>
        public async Task AddAsync(Ship ship)
        {
            Logger.Info($"Добавление нового корабля ID: {ship.Id} для игры ID: {ship.GameId}");
            await _dbContext.Ships.AddAsync(ship);
        }

        /// <summary>  
        /// Сохраняет все изменения в бд контексте
        /// </summary>
        public async Task SaveChangesAsync()
        {
            Logger.Debug("Сохранение изменений кораблей в базе данных");
            await _dbContext.SaveChangesAsync();
        }
    }
}