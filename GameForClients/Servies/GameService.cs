using GameData.Models;
using GameData.Repositories.Interfaces;
using GameForClients.Servies.InferfacesForServ;
using Microsoft.EntityFrameworkCore;

namespace GameForClients.Servies
{
    /// <summary>  
    /// Сервис для логики игры
    /// </summary>
    public class GameService : IGameService
    {
        private readonly DbForGame _dbContext;
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

        public async Task<Guid> CreateGame(Guid playerId)
        {
            var game = new Game { PlayerFirstId = playerId, Status = GameStatus.Waiting };
            await _gameRepo.AddAsync(game);
            await _gameRepo.SaveAsync();
            return game.Id;
        }

        public async Task<bool> JoinGame(Guid gameId, Guid secondPlayerId)
        {
            var game = await _gameRepo.GetByIdAsync(gameId);
            if (game == null || game.PlayerSecondId != null)
                return false;

            game.PlayerSecondId = secondPlayerId;
            game.Status = GameStatus.PlacingShips;
            await _gameRepo.SaveAsync();
            return true;
        }

        public async Task<bool> MakeMove(Guid gameId, Guid playerId, int x, int y)
        {
            if (await _moveRepo.WasHit(gameId, x, y))
                return false;

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

        public async Task<bool> WasHit(Guid gameId, int x, int y)
        {
            return await _moveRepo.WasHit(gameId, x, y);
        }

        public async Task<bool> IsOpponentReady(Guid gameId)
        {
            var game = await _gameRepo.GetByIdAsync(gameId);
            if (game.PlayerSecondId == null) return false;

            var ships = await _shipRepo.GetByGameIdAsync(gameId);
            return ships.Count(s => s.PlayerId == game.PlayerSecondId) >= 20;
        }

        public async Task<bool> IsGameAvailableForJoin(Guid gameId)
        {
            var game = await _gameRepo.GetByIdAsync(gameId);
            return game != null &&
                   game.PlayerSecondId == null &&
                   game.Status == GameStatus.Waiting;
        }

        public async Task SetWinner(Guid gameId, Guid winnerId)
        {
            var game = await _gameRepo.GetByIdAsync(gameId);
            if (game != null && game.WinnerId == null)
            {
                game.WinnerId = winnerId;
                game.Status = GameStatus.Finished;
                await _gameRepo.SaveAsync();
            }
        }

        /// <summary>  
        /// Проверяет окончена ли игра
        /// </summary>
        public async Task<bool> IsGameOver(Guid gameId, Guid playerId)
        {
            var enemyShips = (await _shipRepo.GetByGameIdAsync(gameId))
                .Where(s => s.PlayerId != playerId);

            bool allEnemyShipsDestroyed = true;
            foreach (var ship in enemyShips)
            {
                var cells = ship.Cells.Split(';');
                foreach (var cell in cells)
                {
                    var coords = cell.Split(',');
                    if (!await _moveRepo.WasHit(gameId, int.Parse(coords[0]), int.Parse(coords[1])))
                    {
                        allEnemyShipsDestroyed = false;
                        break;
                    }
                }
                if (!allEnemyShipsDestroyed)
                    break;
            }

            if (allEnemyShipsDestroyed)
                return true;
            var myShips = (await _shipRepo.GetByGameIdAsync(gameId))
                .Where(s => s.PlayerId == playerId);

            foreach (var ship in myShips)
            {
                var cells = ship.Cells.Split(';');
                foreach (var cell in cells)
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