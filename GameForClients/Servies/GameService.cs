using GameData.Models;
using GameData.Repositories;

namespace GameForClients.Servies
{
    /// <summary>  
    /// Сервис для логики игры
    /// </summary>
    public class GameService
    {
        private readonly IGameRepository _gameRepo;
        private readonly IShipRepository _shipRepo;
        private readonly IMoveRepository _moveRepo;
        public IMoveRepository MoveRepo => _moveRepo;

        public GameService(IGameRepository gameRepo,
                         IShipRepository shipRepo,
                         IMoveRepository moveRepo)
        {
            _gameRepo = gameRepo;
            _shipRepo = shipRepo;
            _moveRepo = moveRepo;
        }
        /// <summary>  
        /// Создает  новую игру с указанным игроком
        /// </summary>
        public async Task<int> CreateGame(int playerId)
        {
            var game = new Game { Player1Id = playerId, Status = "Waiting" };
            await _gameRepo.AddAsync(game);
            await _gameRepo.SaveAsync();
            return game.Id;
        }
        /// <summary>  
        /// Подключает к игре второго игрока
        /// </summary>
        public async Task<bool> JoinGame(int gameId, int secondPlayerId)
        {
            var game = await _gameRepo.GetByIdAsync(gameId);
            if (game == null || game.Player2Id != null)
                return false;

            game.Player2Id = secondPlayerId;
            game.Status = "Active";
            await _gameRepo.SaveAsync();
            return true;
        }
        
        /// <summary>  
        /// Находит и возвращает игры открытые для подключения второго игрока
        /// </summary>
        public async Task<List<Game>> FindOpenGames(int excludePlayerId)
        {
            var allGames = await _gameRepo.GetAllAsync();
            return allGames
                .Where(g => g.Player2Id == null && g.Player1Id != excludePlayerId)
                .ToList();
        }
        /// <summary>  
        /// Прверяет готовы ли оба игрока для начала игры
        /// </summary>
        public async Task<bool> AreBothPlayersReady(int gameId)
        {
            var game = await _gameRepo.GetByIdAsync(gameId);
            if (game?.Player2Id == null) return false;

            var player1Ships = await _shipRepo.GetByGameAndPlayerAsync(gameId, game.Player1Id.Value);
            var player2Ships = await _shipRepo.GetByGameAndPlayerAsync(gameId, game.Player2Id.Value);

            return player1Ships.Any() && player2Ships.Any();
        }
        /// <summary>  
        /// Ожидание, пока второй игрок также расставит корабли
        /// </summary>
        public async Task WaitForOpponentPlacement(int gameId, int currentPlayerId, int timeoutSec = 60)
        {
            var startTime = DateTime.UtcNow;
            var opponentId = currentPlayerId == (await _gameRepo.GetByIdAsync(gameId)).Player1Id
                ? (await _gameRepo.GetByIdAsync(gameId)).Player2Id
                : (await _gameRepo.GetByIdAsync(gameId)).Player1Id;

            while ((DateTime.UtcNow - startTime).TotalSeconds < timeoutSec)
            {
                var ships = await _shipRepo.GetByGameAndPlayerAsync(gameId, opponentId.Value);
                if (ships.Any()) return;
                await Task.Delay(1000);
            }
            throw new TimeoutException("Игрок не расставил корабли");
        }
        /// <summary>  
        /// Выполнение хода игрока и определение попадания или промаха
        /// </summary>
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
        /// <summary>  
        /// Проверяет был ли по кораблю сделан выстрел с попаданием
        /// </summary>
        public async Task<bool> WasHit(int gameId, int x, int y)
        {
            return await _moveRepo.WasHit(gameId, x, y);
        }
        /// <summary>  
        /// Проверяет что все корабли противника закончились
        /// </summary>
        public async Task<bool> IsGameOver(int gameId, int playerId)
        {
            var enemyShips = (await _shipRepo.GetByGameIdAsync(gameId))
                .Where(s => s.PlayerId != playerId);

            foreach (var ship in enemyShips)
            {
                foreach (var cell in ship.Cells.Split(';'))
                {
                    var coords = cell.Split(',');
                    if (!await _moveRepo.WasHit(gameId, int.Parse(coords[0]), int.Parse(coords[1])))
                        return false;
                }
            }
            return true;
        }
    }
}