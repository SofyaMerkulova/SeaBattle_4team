using GameData.Models;

namespace GameData.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(Guid id);
        Task<User> GetByLoginAsync(string username);
        Task<List<User>> GetAllAsync();
        Task AddAsync(User user);
        Task SaveAsync();
    }
}
