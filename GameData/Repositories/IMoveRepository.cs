using GameData.Models;

namespace GameData.Repositories
{
    public interface IMoveRepository
    {
        Task<Move> GetByIdAsync(int id);
        Task<List<Move>> GetByGameIdAsync(int gameId);
        Task<bool> CheckHitAsync(int gameId, int x, int y);
        Task<bool> WasHit(int gameId, int x, int y);
        Task AddAsync(Move move);
        Task SaveAsync();
    }
}