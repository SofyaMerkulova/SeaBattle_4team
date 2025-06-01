using GameData.Datas.Configurations;
using GameData.Models;
using Microsoft.EntityFrameworkCore;
using GameData;

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

        var primData = new PrimaryData();
       
        modelBuilder.ApplyConfiguration<User>(primData);
        modelBuilder.ApplyConfiguration<Game>(primData);
        modelBuilder.ApplyConfiguration<Move>(primData);
        modelBuilder.ApplyConfiguration<Ship>(primData);
        modelBuilder.ApplyConfiguration<Log>(primData);
        base.OnModelCreating(modelBuilder);
    }
}

