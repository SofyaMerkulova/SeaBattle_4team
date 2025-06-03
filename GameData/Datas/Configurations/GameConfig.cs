using GameData.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GameData;
using GameData.Repositories;

namespace GameData.Datas.Configurations
{
    public class GameConfig : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.HasKey(g => g.Id);

            builder.Property(g => g.Status)
                   .HasDefaultValue("Waiting")
                   .IsRequired();

            builder.Property(g => g.CreatedDate)
                   .HasDefaultValueSql("NOW()");

            builder.HasOne(g => g.Player1)
                   .WithMany(u => u.GamesAsPlayer1)
                   .HasForeignKey(g => g.Player1Id)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(g => g.Player2)
                   .WithMany(u => u.GamesAsPlayer2)
                   .HasForeignKey(g => g.Player2Id)
                   .IsRequired(false)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(g => g.Winner)
                   .WithMany(u => u.GamesWon)
                   .HasForeignKey(g => g.WinnerId)
                   .IsRequired(false)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}