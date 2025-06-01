using GameData.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameData.Datas.Configurations
{
    public class ShipConfig : IEntityTypeConfiguration<Ship>
    {
        public void Configure(EntityTypeBuilder<Ship> builder)
        {
          

            builder.HasKey(s => s.Id);

            builder.Property(s => s.ShipType).HasMaxLength(20);
            builder.Property(s => s.Cells).IsRequired();

            builder.HasOne(s => s.Player)
                   .WithMany(u => u.Ships)
                   .HasForeignKey(s => s.PlayerId);

            builder.HasOne(s => s.Game)
                   .WithMany(g => g.Ships)
                   .HasForeignKey(s => s.GameId);
        }
    }
}