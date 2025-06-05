using GameData.Repositories.Interfaces;

namespace GameForClients.Servies.InferfacesForServ
{
    public interface IGameService
    {
        Task<Guid> CreateGame(Guid playerId);
        Task<bool> JoinGame(Guid gameId, Guid secondPlayerId);
        Task<bool> MakeMove(Guid gameId, Guid playerId, int x, int y);
        Task<bool> WasHit(Guid gameId, int x, int y);
        Task<bool> IsGameOver(Guid gameId, Guid playerId);
        IMoveRepository MoveRepo {  get; }
        Task<bool> IsOpponentReady(Guid gameId);
        Task SetWinner(Guid gameId, Guid winnerId);
        
        Task<bool> IsGameAvailableForJoin(Guid gameId);
    }
}
