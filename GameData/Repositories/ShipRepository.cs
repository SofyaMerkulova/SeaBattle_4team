using GameData.Models;
using Microsoft.EntityFrameworkCore;
using GameData.Repositories.Interfaces;

namespace GameData.Repositories
{
    /// <summary>  
    /// Репозиторий для кораблей
    /// </summary>
    public class ShipRepository : IShipRepository
    {
        private readonly DbForGame _dbContext;

        public ShipRepository(DbForGame dbContext)
        {
            _dbContext = dbContext;
        }
        /// <summary>  
        /// Возвращает список всех кораблей
        /// </summary>

        public async Task<List<Ship>> GetAllAsync()
        {
            return await _dbContext.Ships
                .AsNoTracking()
                .ToListAsync();
        }
        /// <summary>  
        /// Возвращает все корабли из игры по ее ID
        /// </summary>
        public async Task<List<Ship>> GetByGameIdAsync(Guid gameId)
        {
            return await _dbContext.Ships
                .Where(s => s.GameId == gameId)
                .ToListAsync();
        }
        /// <summary>  
        /// Возвращает корабли по ID
        /// </summary>
        public async Task<Ship?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Ships
                .FindAsync(id);
        }
        /// <summary>  
        /// Вовзращает игру и игрока по ID
        /// </summary>
        public async Task<List<Ship>> GetByGameAndPlayerAsync(Guid gameId, Guid playerId)
        {
            return await _dbContext.Ships
                .Where(s => s.GameId == gameId && s.PlayerId == playerId)
                .ToListAsync();
        }
        /// <summary>  
        /// Добавляет новый корабль
        /// </summary>
        public async Task AddAsync(Ship ship)
        {
            await _dbContext.Ships.AddAsync(ship);
        }
        /// <summary>  
        /// Сохраняет все изменения в бд контексте
        /// </summary>
        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}