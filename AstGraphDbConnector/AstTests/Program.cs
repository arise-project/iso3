using AstArangoDbConnector;
using AstArangoDbConnector.Syntax;
using AstRoslyn;
using AstShared;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using YamlDotNet.Serialization;

namespace AstTests
{
    class Program
    {
        private const string ConfigPath = @"AstTests.yaml";

        private static AstConnector connector = new AstConnector();

        static void Main(string[] args)
        {
            var sc = new ServiceCollection();
            sc.AddLogging(lb => lb.AddConsole())
                .Configure<LoggerFilterOptions>(options => options.MinLevel = LogLevel.Information);

            var sp = Infrastructure.Init(sc);
            var logger = sp.GetService<ILogger<Program>>();

            logger.Log(LogLevel.Information, "Starting application");

            PrintConfig();
            Console.WriteLine("database|manage|analyse");
            string commad = Console.ReadLine();
            switch (commad.Trim().ToLower())
            {
                case "database":
                    DatabaseCommands();
                    break;
                case "manage":
                    ManageCommands();
                    break;
                case "analyse":
                    AnalyseCommads();
                    break;
            }

            logger.LogDebug("All done!");
        }

        private static void AnalyseCommads()
        {
            Console.WriteLine("csharp-file");
            string commad = Console.ReadLine();
            switch (commad.Trim().ToLower())
            {
                case "csharp-file":
                    AnalyseCSharpFile();
                    break;
            }
        }

        private static void AnalyseCSharpFile()
        {
            Console.WriteLine("File:");
            string file = Console.ReadLine();
            string programText = File.ReadAllText(file);
            SyntaxTree tree = CSharpSyntaxTree.ParseText(programText);
            var root = tree.GetCompilationUnitRoot();
            var writer = new ConsoleDumpWalker(new CodeSyntaxVisitor(connector));
            writer.Visit(root);
        }

        private static void ManageCommands()
        {
            Console.WriteLine("create-collections|create-collection-classes");
            string commad = Console.ReadLine();
            switch (commad.Trim().ToLower())
            {
                case "create-collections":
                    CreateArangoDbSyntaxCollections();
                    break;
                case "create-collection-classes":
                    CreateArangoDbSyntaxClasses();
                    break;
            }
        }

        private static void CreateArangoDbSyntaxClasses()
        {
            var typesTree = new SyntaxNodesTree();
            typesTree.AcceptSyntaxGenerator(new SyntaxGeneratorVisitor());
            typesTree.CreateTypesTree();
        }

        private static void CreateArangoDbSyntaxCollections()
        {
            var typesTree = new SyntaxNodesTree();
            typesTree.AcceptBaseSyntaxWritter(new BaseSyntaxVisitor(connector));
            typesTree.AcceptConcreteSyntaxWritter(new ConcreteSyntaxVisitor(connector));
            typesTree.CreateTypesTree();
        }

        private static void DatabaseCommands()
        {
            Console.WriteLine("configure|create|statistics|delete");
            string commad = Console.ReadLine();
            switch (commad.Trim().ToLower())
            {
                case "configure":
                    ConfigureArabgoDbDatabase();
                    break;
                case "create":
                    break;
                case "statistics":
                    break;
                case "delete":
                    break;
            }
        }

        private static void ConfigureArabgoDbDatabase()
        {
            Config c = new Config();
            Console.WriteLine("Server:");
            c.ArangoDbServer = Console.ReadLine();
            Console.WriteLine("Database:");
            c.ArangoDbDatabse = Console.ReadLine();
            Console.WriteLine("User:");
            c.ArangoDbUser = Console.ReadLine();
            Console.WriteLine("Password:");
            c.ArangoDbPassword = Console.ReadLine();
            var s = new Serializer();
            File.WriteAllText(ConfigPath, s.Serialize(c));
        }

        private static void PrintConfig()
        {
            if(!File.Exists(ConfigPath))
            {
                return;
            }

            var d = new Deserializer();
            Config c;
            using (var s = new StreamReader(ConfigPath))
            {
                c = d.Deserialize<Config>(s);
            }

            Console.WriteLine("==================================");
            Console.WriteLine("Server:{0}", c.ArangoDbServer);
            Console.WriteLine("Database:{0}", c.ArangoDbDatabse);
            Console.WriteLine("==================================");
        }
    }
}
