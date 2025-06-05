using GameData.Models;
using Microsoft.EntityFrameworkCore;
using GameData.Repositories.Interfaces;
using NLog;

namespace GameData.Repositories
{
    /// <summary>  
    /// Репозиторий для ходов
    /// </summary> 
    public class MoveRepository : IMoveRepository
    {
        private readonly DbForGame _dbContext;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public MoveRepository(DbForGame dbContext)
        {
            _dbContext = dbContext;
            Logger.Debug("Инициализация репозитория ходов");
        }

        /// <summary>  
        /// Возвращает ход по ID
        /// </summary>  
        public async Task<Move> GetByIdAsync(Guid id)
        {
            Logger.Trace($"Запрос хода по ID: {id}");
            return await _dbContext.Moves.FindAsync(id);
        }

        /// <summary>  
        /// Возвращает список ходов по ID игры
        /// </summary> 
        public async Task<List<Move>> GetByGameIdAsync(Guid gameId)
        {
            Logger.Debug($"Получение ходов для игры ID: {gameId}");
            return await _dbContext.Moves
                .Where(m => m.GameId == gameId)
                .ToListAsync();
        }

        /// <summary>  
        /// Проверяет попадает ли выстрел в корабль 
        /// </summary>  
        public async Task<bool> CheckHitAsync(Guid gameId, int x, int y)
        {
            Logger.Debug($"Проверка попадания в координаты ({x},{y}) для игры ID: {gameId}");

            var ships = await _dbContext.Ships
                .Where(s => s.GameId == gameId)
                .ToListAsync();

            bool isHit = ships.Any(ship =>
                ship.Cells.Split(';').Contains($"{x},{y}"));

            Logger.Trace($"Результат проверки попадания в ({x},{y}): {(isHit ? "попадание" : "промах")}");
            return isHit;
        }

        /// <summary>  
        /// Определяет было ли по клетке в определенной игре по ее ID
        /// </summary>  
        public async Task<bool> WasHit(Guid gameId, int x, int y)
        {
            Logger.Trace($"Проверка, была ли клетка ({x},{y}) поражена в игре ID: {gameId}");
            return await _dbContext.Moves
                .AnyAsync(m => m.GameId == gameId && m.X == x && m.Y == y && m.IsHit);
        }

        public async Task AddAsync(Move move)
        {
            Logger.Info($"Добавление нового хода для игры ID: {move.GameId} в координаты ({move.X},{move.Y})");
            await _dbContext.Moves.AddAsync(move);
        }

        /// <summary>  
        /// Сохранение всех изменений в бд контексте
        /// </summary>  
        public async Task SaveAsync()
        {
            Logger.Debug("Сохранение изменений в базе данных");
            await _dbContext.SaveChangesAsync();
        }
    }
}