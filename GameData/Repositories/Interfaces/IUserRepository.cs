using GameData.Models;

namespace GameData.Repositories.Interfaces
{
    /// <summary>
    /// Интерфейс для репозитория пользователя
    /// </summary>
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(Guid id);
        Task<User> GetByLoginAsync(string username);
        Task<List<User>> GetAllAsync();
        Task AddAsync(User user);
        Task SaveAsync();
    }
}
