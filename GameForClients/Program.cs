using Castle.MicroKernel.Registration;
using Castle.Windsor;
using GameData.Repositories;
using GameData.Repositories.Interfaces;
using GameForClients.Servies;
using GameForClients.Servies.InferfacesForServ;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using NLog;
using NLog.Config;
using NLog.Targets;
using LogLevel = NLog.LogLevel;

namespace GameForClients
{
    internal static class Program
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            var config = new LoggingConfiguration();
            var logfile = new FileTarget("logfile")
            {
                FileName = "GameForClients/logSB.log",

                Layout = "${longdate}|${level:uppercase=true}|${logger}|${message}|${exception:format=ToString}"

            };
            config.AddRule(LogLevel.Info, LogLevel.Fatal, logfile);

            LogManager.Configuration = config;
            Logger.Info("Приложение запущено");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("ru-RU");
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