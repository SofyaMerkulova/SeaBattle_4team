using GameData.Models;
using Microsoft.EntityFrameworkCore;

namespace GameData.Repositories
{
    public class MoveRepository : IMoveRepository
    {
        private readonly DbForGame _dbContext;

        public MoveRepository(DbForGame dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Move> GetByIdAsync(int id)
        {
            return await _dbContext.Moves.FindAsync(id);
        }

        public async Task<List<Move>> GetByGameIdAsync(int gameId)
        {
            return await _dbContext.Moves
                .Where(m => m.GameId == gameId)
                .ToListAsync();
        }

        public async Task<bool> CheckHitAsync(int gameId, int x, int y)
        {
            // Получаем все корабли в указанной игре
            var ships = await _dbContext.Ships
                .Where(s => s.GameId == gameId)
                .ToListAsync();

            // Проверяем каждый корабль
            foreach (var ship in ships)
            {
                // Разбиваем строку с координатами на отдельные точки
                var coordinates = ship.Cells.Split(';');

                // Проверяем есть ли среди них нужные координаты
                if (coordinates.Contains($"{x},{y}"))
                {
                    return true; // Попадание
                }
            }

            return false; // Промах
        }

        public async Task<bool> WasHit(int gameId, int x, int y)
        {
            return await _dbContext.Moves
                .AnyAsync(m => m.GameId == gameId && m.X == x && m.Y == y && m.IsHit);
        }

        public async Task AddAsync(Move move)
        {
            await _dbContext.Moves.AddAsync(move);
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}