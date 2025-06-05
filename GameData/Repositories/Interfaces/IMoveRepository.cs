using GameData.Models;

namespace GameData.Repositories.Interfaces
{
    /// <summary>
    /// Интерфейс для репозитория ходов
    /// </summary>
    public interface IMoveRepository
    {
        Task<Move> GetByIdAsync(Guid id);
        Task<List<Move>> GetByGameIdAsync(Guid gameId);
        Task<bool> CheckHitAsync(Guid gameId, int x, int y);
        Task<bool> WasHit(Guid gameId, int x, int y);
        Task AddAsync(Move move);
        Task SaveAsync();
    }
}