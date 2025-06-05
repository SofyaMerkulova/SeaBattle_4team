using GameData.Repositories;
using GameData.Repositories.Interfaces;
using GameForClients.Servies;
using GameForClients.Servies.InferfacesForServ;
using Microsoft.EntityFrameworkCore;
using Castle.Windsor;
using Castle.Windsor.Diagnostics.Extensions;
using Castle.MicroKernel.Registration;
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
            var container = new WindsorContainer();
             container.Register(Component.For<DbForGame>()
                     .UsingFactoryMethod(() =>
                     {
                       var options = new DbContextOptionsBuilder<DbForGame>()
                       .UseNpgsql("User ID=postgres;Password = VOFV2fo2st;Host = localhost;Port = 5432;Database = postgres;")
                       .Options;
                        return new DbForGame(options);
                     })
             .LifestyleTransient());
            container.Register(Component.For<IGameRepository>().ImplementedBy<GameRepository>().LifestyleTransient());
            container.Register(Component.For<IUserRepository>().ImplementedBy<UserRepository>().LifestyleTransient());
            container.Register(Component.For<IShipRepository>().ImplementedBy<ShipRepository>().LifestyleTransient());
            container.Register(Component.For<IMoveRepository>().ImplementedBy<MoveRepository>().LifestyleTransient());
            container.Register(Component.For<IGameService>().ImplementedBy<GameService>().LifestyleTransient());
            var dbContext = container.Resolve<DbForGame>();
            var gameRepo = container.Resolve<IGameRepository>();
            var shipRepo = container.Resolve<IShipRepository>();
            var moveRepo = container.Resolve<IMoveRepository>();
            var userRepo = container.Resolve<IUserRepository>();
            var gameService = container.Resolve<IGameService>();

            Application.Run(new Login(dbContext, gameService, gameRepo, userRepo));

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

        }
    }
}