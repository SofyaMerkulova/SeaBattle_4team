using Microsoft.EntityFrameworkCore;
using GameData.Models;

namespace GameData.Repositories
{
    public class GameRepository: IGameRepository
    {
        private readonly DbForGame _dbContext;

        public GameRepository(DbForGame dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Game>> GetAllAsync()
        {
            return await _dbContext.Games
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Game> GetByIdAsync(int id)
        {
            return await _dbContext.Games
                .AsNoTracking()
                .FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task AddAsync(Game game)
        {
            await _dbContext.Games.AddAsync(game);
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
   

