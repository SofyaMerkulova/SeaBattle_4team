using GameData.Repositories;
using GameForClients.Properties;
using GameForClients.Servies;
using Microsoft.EntityFrameworkCore;

namespace GameForClients
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var options = new DbContextOptionsBuilder<DbForGame>()
           .UseNpgsql("User ID=postgres;Password = VOFV2fo2st;Host = localhost;Port = 5432;Database = postgres;")
           .Options;
            
            var dbContext = new DbForGame();
            var gameRepo = new GameRepository(dbContext);
            var shipRepo = new ShipRepository(dbContext);
            var moveRepo = new MoveRepository(dbContext);
            var userRepository = new UserRepository(dbContext);
            var gameService = new GameService(gameRepo, shipRepo, moveRepo);
            Application.Run(new Login(dbContext, gameService));

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
              
        }
        private static GameService CreateGameService(DbForGame dbContext)
        {
            var gameRepo = new GameRepository(dbContext);
            var shipRepo = new ShipRepository(dbContext);
            var moveRepo = new MoveRepository(dbContext);
            return new GameService(gameRepo, shipRepo, moveRepo);
        }
    }
}
