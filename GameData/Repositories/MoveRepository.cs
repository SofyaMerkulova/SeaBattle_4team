using GameData.Models;
using Microsoft.EntityFrameworkCore;
using GameData.Repositories.Interfaces;

namespace GameData.Repositories
{
    /// <summary>  
    /// Репозиторий для ходов
    /// </summary> 
    public class MoveRepository : IMoveRepository
    {
        private readonly DbForGame _dbContext;

        public MoveRepository(DbForGame dbContext)
        {
            _dbContext = dbContext;
        }
        /// <summary>  
        /// Возвращает ход по ID
        /// </summary>  
        public async Task<Move> GetByIdAsync(Guid id)
        {
            return await _dbContext.Moves.FindAsync(id);
        }
        /// <summary>  
        /// Возвращает список ходов по ID игры
        /// </summary> 
        public async Task<List<Move>> GetByGameIdAsync(Guid gameId)
        {
            return await _dbContext.Moves
                .Where(m => m.GameId == gameId)
                .ToListAsync();
        }
        /// <summary>  
        /// Проверяет попадает ли выстрел в корабль 
        /// </summary>  
        public async Task<bool> CheckHitAsync(Guid gameId, int x, int y)
        {
            var ships = await _dbContext.Ships
                .Where(s => s.GameId == gameId)
                .ToListAsync();

            foreach (var ship in ships)
            {
                var coordinates = ship.Cells.Split(';');

                if (coordinates.Contains($"{x},{y}"))
                {
                    return true; 
                }
            }

            return false; 
        }
        /// <summary>  
        /// Определяет было ли по клетке в определенной игре по ее ID
        /// </summary>  
        public async Task<bool> WasHit(Guid gameId, int x, int y)
        {
            return await _dbContext.Moves
                .AnyAsync(m => m.GameId == gameId && m.X == x && m.Y == y && m.IsHit);
        }

        public async Task AddAsync(Move move)
        {
            await _dbContext.Moves.AddAsync(move);
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