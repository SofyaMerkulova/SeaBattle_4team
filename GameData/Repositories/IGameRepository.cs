using GameData.Models;

namespace GameData.Repositories
{
    public interface IGameRepository
    {
        Task<List<Game>> GetAllAsync();
        Task<Game> GetByIdAsync(int id);
        Task AddAsync(Game game);
        Task SaveAsync();
        Task<Game> GetActiveGameAsync();
    }

}
