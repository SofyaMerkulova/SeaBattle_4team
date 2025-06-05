using GameData.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameData.Datas.Configurations
{
    public class GameConfig : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.HasKey(g => g.Id);

            builder.Property(g => g.Status)
            .HasConversion<string>() 
            .HasDefaultValue(GameStatus.Waiting)
            .IsRequired();

            builder.Property(g => g.CreatedDate)
                   .HasDefaultValueSql("NOW()");

            builder.HasOne(g => g.PlayerFirst)
               .WithMany(u => u.GamesAsPlayerFirst)
               .HasForeignKey(g => g.PlayerFirstId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(g => g.PlayerSecond)
                .WithMany(u => u.GamesAsPlayerSecond)
                .HasForeignKey(g => g.PlayerSecondId)
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