using GameData.Models;

namespace GameData.Repositories.Interfaces
{ 
  /// <summary>
  /// Интерфейс для репозитория игры
  /// </summary>
    public interface IGameRepository
    {
        Task<List<Game>> GetAllAsync();
        Task<Game> GetByIdAsync(Guid id);
        Task AddAsync(Game game);
        Task SaveAsync();
        Task<Game> GetActiveGameAsync();
    }

}
