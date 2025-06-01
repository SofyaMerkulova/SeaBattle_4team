using GameData.Models;
using GameData.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace GameForClients.Servies
{
    public class GameService
    {
        private readonly IGameRepository _gameRepo;
        private readonly IShipRepository _shipRepo;
        private readonly IMoveRepository _moveRepo;

        public GameService(IGameRepository gameRepo,
                         IShipRepository shipRepo,
                         IMoveRepository moveRepo)
        {
            _gameRepo = gameRepo;
            _shipRepo = shipRepo;
            _moveRepo = moveRepo;
        }

        public async Task<int> CreateGame(int playerId)
        {
            var game = new Game { Player1Id = playerId };
            await _gameRepo.AddAsync(game);
            await _gameRepo.SaveAsync();
            return game.Id;
        }
        public async Task<List<Game>> FindOpenGames(int excludePlayerId)
        {
            return (await _gameRepo.GetAllAsync())
                .Where(g => g.Player2Id == null && g.Player1Id != excludePlayerId)
                .ToList();
        }

        // Новый метод для присоединения к игре
        public async Task<bool> JoinGame(int gameId, int player2Id)
        {
            var game = await _gameRepo.GetByIdAsync(gameId);
            if (game == null || game.Player2Id != null)
                return false;

            game.Player2Id = player2Id;
            await _gameRepo.SaveAsync();
            return true;
        }
        public async Task<bool> MakeMove(int gameId, int playerId, int x, int y)
        {
            var enemyShips = (await _shipRepo.GetByGameIdAsync(gameId))
                .Where(s => s.PlayerId != playerId);

            bool isHit = enemyShips.Any(s => s.Cells.Split(';').Contains($"{x},{y}"));

            await _moveRepo.AddAsync(new Move
            {
                GameId = gameId,
                PlayerId = playerId,
                X = x,
                Y = y,
                IsHit = isHit,
                MoveTime = DateTime.Now
            });
            await _moveRepo.SaveAsync();

            return isHit;
        }

        public async Task<bool> WasHit(int gameId, int x, int y)
        {
            return await _moveRepo.WasHit(gameId, x, y);
        }
    }
}