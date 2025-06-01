using GameData.Models;

namespace GameData.Repositories
{
    public interface IShipRepository
    {
        Task<List<Ship>> GetAllAsync();
        Task<List<Ship>> GetByGameIdAsync(int gameId);  
        Task<Ship?> GetByIdAsync(int id);
        Task AddAsync(Ship ship);
        Task SaveChangesAsync();
    }
}