using GameData.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameData.Datas.Configurations
{
    public class LogConfig : IEntityTypeConfiguration<Log>
    {
        public void Configure(EntityTypeBuilder<Log> builder)
        {
            
            builder.HasKey(l => l.Id);

            builder.Property(l => l.Action).IsRequired(false);
           

            builder.HasOne(l => l.User)
                   .WithMany(u => u.Logs)
                   .HasForeignKey(l => l.UserId);
        }
    }
}