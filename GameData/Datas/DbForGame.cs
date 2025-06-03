using GameData.Datas.Configurations;
using GameData.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

public class DbForGame : DbContext
{
    public DbForGame() { }

    public DbForGame(DbContextOptions<DbForGame> options) : base(options) { }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Game> Games { get; set; } = null!;
    public DbSet<Move> Moves { get; set; } = null!;
    public DbSet<Log> Logs { get; set; } = null!;
    public DbSet<Ship> Ships { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        if (!options.IsConfigured)
        { options.UseNpgsql("User ID=postgres;Password = VOFV2fo2st;Host = localhost;Port = 5432;Database = postgres;");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      
        modelBuilder.ApplyConfiguration(new ShipConfig());
        modelBuilder.ApplyConfiguration(new MoveConfig());
        modelBuilder.ApplyConfiguration(new LogConfig());
        modelBuilder.ApplyConfiguration(new UserConfig());
        modelBuilder.ApplyConfiguration(new GameConfig());

       
    }
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<DateTime>()
            .HaveConversion<DateTimeToUtcConverter>();

    }
    public class DateTimeToUtcConverter : ValueConverter<DateTime, DateTime>
    {
        public DateTimeToUtcConverter()
            : base(v => v.Kind == DateTimeKind.Utc ? v : v.ToUniversalTime(),
                  v => DateTime.SpecifyKind(v, DateTimeKind.Utc))
        { }
    }  
}


