using GameData.Models;
using GameData.Repositories.Interfaces;
using GameForClients.Servies.InferfacesForServ;
using Microsoft.EntityFrameworkCore;
using NLog;

namespace GameForClients.Servies
{
    /// <summary>  
    /// Сервис для логики игры
    /// </summary>
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepo;
        private readonly IShipRepository _shipRepo;
        private readonly IMoveRepository _moveRepo;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        /// <summary>
        /// Получение репозитория ходов
        /// </summary>
        public IMoveRepository MoveRepo => _moveRepo;

        public GameService(IGameRepository gameRepo,
                         IShipRepository shipRepo,
                         IMoveRepository moveRepo)
        {
            _gameRepo = gameRepo;
            _shipRepo = shipRepo;
            _moveRepo = moveRepo;
            Logger.Debug("GameService инициализирован");
        }
        /// <summary>
        /// Создание новой игры
        /// </summary>
        public async Task<Guid> CreateGame(Guid playerId)
        {
            Logger.Info($"Создание новой игры для игрока {playerId}");
            var game = new Game { PlayerFirstId = playerId, Status = GameStatus.Waiting };
            await _gameRepo.AddAsync(game);
            await _gameRepo.SaveAsync();
            Logger.Debug($"Игра создана, ID: {game.Id}");
            return game.Id;
        }
        /// <summary>
        /// Присоединение к игре 
        /// </summary>
        public async Task<bool> JoinGame(Guid gameId, Guid secondPlayerId)
        {
            Logger.Debug($"Попытка присоединения игрока {secondPlayerId} к игре {gameId}");
            var game = await _gameRepo.GetByIdAsync(gameId);

            if (game == null || game.PlayerSecondId != null)
            {
                Logger.Warn($"Не удалось присоединиться к игре {gameId}");
                return false;
            }

            game.PlayerSecondId = secondPlayerId;
            game.Status = GameStatus.PlacingShips;
            await _gameRepo.SaveAsync();
            Logger.Info($"Игрок {secondPlayerId} присоединился к игре {gameId}");
            return true;
        }
        /// <summary>
        /// Совершение хода в игре
        /// </summary>
        public async Task<bool> MakeMove(Guid gameId, Guid playerId, int x, int y)
        {
            Logger.Debug($"Ход игрока {playerId} в игре {gameId} на [{x},{y}]");

            if (await _moveRepo.WasHit(gameId, x, y))
            {
                Logger.Trace($"Клетка [{x},{y}] уже была поражена ранее");
                return false;
            }

            var isHit = (await _shipRepo.GetByGameIdAsync(gameId))
                .Where(s => s.PlayerId != playerId)
                .Any(s => s.Cells.Split(';').Contains($"{x},{y}"));

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

            Logger.Info($"Ход: {(isHit ? "попадение" : "промах")} в [{x},{y}]");
            return isHit;
        }
        /// <summary>
        /// Проверка было ли попадение в клетку
        /// </summary>
        public async Task<bool> WasHit(Guid gameId, int x, int y)
        {
            Logger.Trace($"Проверка клетки на попадение [{x},{y}] в игре {gameId}");
            return await _moveRepo.WasHit(gameId, x, y);
        }
        /// <summary>
        /// Проверка готовности противника
        /// </summary>
        public async Task<bool> IsOpponentReady(Guid gameId)
        {
            Logger.Debug($"Проверка готовности противника в игре {gameId}");
            var game = await _gameRepo.GetByIdAsync(gameId);
            if (game.PlayerSecondId == null) return false;

            var shipsCount = (await _shipRepo.GetByGameIdAsync(gameId))
                .Count(s => s.PlayerId == game.PlayerSecondId);

            Logger.Trace($"Кораблей противника: {shipsCount}");
            return shipsCount >= 20;
        }
        /// <summary>
        /// Проверка на присоединение к игре 
        /// </summary>
        public async Task<bool> IsGameAvailableForJoin(Guid gameId)
        {
            Logger.Trace($"Проверка игры {gameId} для присоединения");
            var game = await _gameRepo.GetByIdAsync(gameId);
            return game != null &&
                   game.PlayerSecondId == null &&
                   game.Status == GameStatus.Waiting;
        }
        /// <summary>
        /// Установка победителя в игре
        /// </summary>
        public async Task SetWinner(Guid gameId, Guid winnerId)
        {
            Logger.Info($"Установлен победитель {winnerId} в игре {gameId}");
            var game = await _gameRepo.GetByIdAsync(gameId);
            if (game != null && game.WinnerId == null)
            {
                game.WinnerId = winnerId;
                game.Status = GameStatus.Finished;
                await _gameRepo.SaveAsync();
                Logger.Debug($"Игра {gameId} завершена, победил: {winnerId}");
            }
        }
        /// <summary>
        /// Проверка завершена ли игра для игрока по ID
        /// </summary>
        public async Task<bool> IsGameOver(Guid gameId, Guid playerId)
        {
            Logger.Debug($"Проверка завершения игры {gameId} для игрока {playerId}");

            var enemyShips = (await _shipRepo.GetByGameIdAsync(gameId))
                .Where(s => s.PlayerId != playerId);

            foreach (var ship in enemyShips)
            {
                foreach (var cell in ship.Cells.Split(';'))
                {
                    var coords = cell.Split(',');
                    if (!await _moveRepo.WasHit(gameId, int.Parse(coords[0]), int.Parse(coords[1])))
                    {
                        Logger.Trace("Не все корабли противника уничтожены");
                        return false;
                    }
                }
            }
            Logger.Info($"Все корабли противника уничтожены в игре {gameId}");
            return true;
        }
    }
}