using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GameData.Models;

namespace GameData
{
    public class PrimaryData : IEntityTypeConfiguration<User>,
                               IEntityTypeConfiguration<Game>,
                               IEntityTypeConfiguration<Move>,
                               IEntityTypeConfiguration<Ship>,
                               IEntityTypeConfiguration<Log>
    {
        public void Configure(EntityTypeBuilder<User> userBuilder)
        {
           
            var user1 = new User
            {
                Id = 1,
                Username = "player1",
                PasswordHash = ForPasswordGeneration.HashPassword("123456"),
                CreatedAt = DateTime.UtcNow
            };

            var user2 = new User
            {
                Id = 2,
                Username = "player2",
                PasswordHash = ForPasswordGeneration.HashPassword("123456"),
                CreatedAt = DateTime.UtcNow
            };
            userBuilder.HasData(user1, user2);
        }
          

        public void Configure(EntityTypeBuilder<Game> gameBuilder)
        {
            gameBuilder.HasData(
                new Game
                {
                    Id = 1,
                    Player1Id = 1,
                    Player2Id = 2,
                    StartedAt = DateTime.UtcNow.AddDays(-1),
                    EndedAt = null,
                    WinnerId = null
                }
            );
        }

        public void Configure(EntityTypeBuilder<Move> moveBuilder)
        {
            moveBuilder.HasData(
                new Move
                {
                    Id = 1,
                    GameId = 1,
                    PlayerId = 1,
                    X = 0,
                    Y = 0,
                    IsHit = false,
                    MoveTime = DateTime.UtcNow.AddHours(-23)
                },
                new Move
                {
                    Id = 2,
                    GameId = 1,
                    PlayerId = 2,
                    X = 1,
                    Y = 0,
                    IsHit = true,
                    MoveTime = DateTime.UtcNow.AddHours(-22)
                }
            );
        }

        public void Configure(EntityTypeBuilder<Ship> shipBuilder)
        {
            shipBuilder.HasData(
                new Ship
                {
                    Id = 1,
                    GameId = 1,
                    PlayerId = 1,
                    ShipType = "Destroyer",
                    Cells = "0,0;0,1;0,2"
                },
                new Ship
                {
                    Id = 2,
                    GameId = 1,
                    PlayerId = 2,
                    ShipType = "Submarine",
                    Cells = "1,0;1,1;1,2"
                }
            );
        }

        public void Configure(EntityTypeBuilder<Log> logBuilder)
        {
            logBuilder.HasData(
                new Log
                {
                    Id = 1,
                    UserId = 1,
                    Action = "User logged in",
                    CreatedAt = DateTime.UtcNow.AddDays(-1).AddHours(-1)
                },
                new Log
                {
                    Id = 2,
                    UserId = 2,
                    Action = "User started game",
                    CreatedAt = DateTime.UtcNow.AddDays(-1).AddMinutes(-30)
                }
            );
        }
    }
}

