using Microsoft.EntityFrameworkCore;
using GameData.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameData.Repositories
{
    public class ShipRepository : IShipRepository
    {
        private readonly DbForGame _dbContext;

        public ShipRepository(DbForGame dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Ship>> GetAllAsync()
        {
            return await _dbContext.Ships
                .AsNoTracking()
                .ToListAsync();
        }

        // Получаем корабли по ID игры
        public async Task<List<Ship>> GetByGameIdAsync(int gameId)
        {
            return await _dbContext.Ships
                .Where(s => s.GameId == gameId)
                .ToListAsync();
        }

        // Получаем один корабль по его ID
        public async Task<Ship?> GetByIdAsync(int id)
        {
            return await _dbContext.Ships
                .FindAsync(id);
        }

        public async Task AddAsync(Ship ship)
        {
            await _dbContext.Ships.AddAsync(ship);
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}