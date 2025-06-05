using GameData.Models;

namespace GameData.Repositories.Interfaces
{
    public interface IShipRepository
    {
        Task<List<Ship>> GetAllAsync();
        Task<List<Ship>> GetByGameIdAsync(Guid gameId);  
        Task<Ship?> GetByIdAsync(Guid id);
        Task AddAsync(Ship ship);
        Task<List<Ship>> GetByGameAndPlayerAsync(Guid gameId, Guid playerId);
        Task SaveChangesAsync();
    }
}