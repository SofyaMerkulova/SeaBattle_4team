using GameData.Models;

namespace GameData.Repositories.Interfaces
{
    public interface IGameRepository
    {
        Task<List<Game>> GetAllAsync();
        Task<Game> GetByIdAsync(Guid id);
        Task AddAsync(Game game);
        Task SaveAsync();
        Task<Game> GetActiveGameAsync();
    }

}
