using AstArangoDbConnector;
using AstShared;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace AstTests
{
    class Program
    {
        static void Main(string[] args)
        {
            var sc = new ServiceCollection();
            sc.AddLogging(lb => lb.AddConsole())
                .Configure<LoggerFilterOptions>(options => options.MinLevel = LogLevel.Information);

            InfrastructureAstArangoDbConnector.Init(sc);
            var sp = InfrastructureShared.Init(sc);
            var logger = sp.GetService<ILogger<Program>>();
            var app = sp.GetService<IApp>();

            logger.Log(LogLevel.Information, "Starting application");

            app.PrintConfig();
            Console.WriteLine("database|manage|analyse");
            string commad = Console.ReadLine();
            switch (commad.Trim().ToLower())
            {
                case "database":
                    DatabaseCommands(app);
                    break;
                case "manage":
                    ManageCommands(app);
                    break;
                case "analyse":
                    AnalyseCommads(app);
                    break;
            }

            logger.LogDebug("All done!");
        }


        private static void DatabaseCommands(IApp app)
        {
            Console.WriteLine("configure|create|statistics|delete");
            string commad = Console.ReadLine();
            switch (commad.Trim().ToLower())
            {
                case "configure":
                    app.ConfigureArabgoDbDatabase();
                    break;
                case "create":
                    app.CreateDatabase();
                    break;
                case "statistics":
                    app.DatabaseStatistics();
                    break;
                case "delete":
                    app.DeleteDatabase();
                    break;
            }
        }

        private static void AnalyseCommads(IApp app)
        {
            Console.WriteLine("csharp-file");
            string commad = Console.ReadLine();
            switch (commad.Trim().ToLower())
            {
                case "csharp-file":
                    app.AnalyseCSharpFile();
                    break;
            }
        }

        private static void ManageCommands(IApp app)
        {
            Console.WriteLine("gen-syntax-csv|create-collection-classes|create-collections");
            string commad = Console.ReadLine();
            switch (commad.Trim().ToLower())
            {
                case "gen-syntax-csv":
                    app.GenSyntaxCsv();
                    break;
                case "create-collection-classes":
                    app.CreateArangoDbSyntaxClasses();
                    break;
                case "create-collections":
                    app.CreateArangoDbSyntaxCollections();
                    break;
            }
        }
    }
}
