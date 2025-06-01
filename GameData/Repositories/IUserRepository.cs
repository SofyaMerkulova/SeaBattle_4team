using GameData.Models;

namespace GameData.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(int id);
        Task<User> GetByLoginAsync(string username);
        Task<List<User>> GetAllAsync();
        Task AddAsync(User user);
        Task SaveAsync();
    }
}
