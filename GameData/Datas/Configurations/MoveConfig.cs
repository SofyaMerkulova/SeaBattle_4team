using GameData.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameData.Datas.Configurations
{
    /// <summary>
    /// Конфигурация ходов
    /// </summary>
    public class MoveConfig :IEntityTypeConfiguration<Move>
    {
        public void Configure(EntityTypeBuilder<Move> builder)
        {
            

            builder.HasKey(m => m.Id);

            builder.Property(m => m.X).IsRequired();
            builder.Property(m => m.Y).IsRequired();
            builder.Property(m => m.IsHit).IsRequired();


            builder.HasOne(m => m.Player)
                   .WithMany(u => u.Moves)
                   .HasForeignKey(m => m.PlayerId);

            builder.HasOne(m => m.Game)
                   .WithMany(g => g.Moves)
                   .HasForeignKey(m => m.GameId);
        }
    }
}
