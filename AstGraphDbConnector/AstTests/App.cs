using AstDomain;
using AstShared;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.IO;
using YamlDotNet.Serialization;

namespace AstTests
{
    public class App : IApp
    {
        private const string ConfigPath = @"AstTests.yaml";

        private readonly ISyntaxNodesToClasses _syntaxNodesToClasses;
        private readonly ISyntaxNodesToCollections _syntaxNodesToCollections;
        private readonly ICodeVisitor _codeVisitor;
        private readonly IConfigManager _configManager;
        private readonly ISyntaxWalker _syntaxWalker;
        private readonly IDatabaseManager _databaseManager;

        public App(
            ISyntaxNodesToClasses syntaxNodesToClasse, 
            ISyntaxNodesToCollections syntaxNodesToCollections, 
            ICodeVisitor codeVisitor,
            IConfigManager configManager,
            ISyntaxWalker syntaxWalker,
            IDatabaseManager databaseManager)
        {
            _syntaxNodesToClasses = syntaxNodesToClasse;
            _syntaxNodesToCollections = syntaxNodesToCollections;
            _codeVisitor = codeVisitor;
            _configManager = configManager;
            _syntaxWalker = syntaxWalker;
            _databaseManager = databaseManager;
        }

        public void ConfigureArabgoDbDatabase()
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
            _configManager.WriteConfig(c, ConfigPath);
        }

        public void PrintConfig()
        {
            Config c = _configManager.ReadConfig(ConfigPath);

            Console.WriteLine("==================================");
            Console.WriteLine("Server:{0}", c.ArangoDbServer);
            Console.WriteLine("Database:{0}", c.ArangoDbDatabse);
            Console.WriteLine("DbUser:{0}", c.ArangoDbUser);
            Console.WriteLine("DbPassword:{0}", c.ArangoDbPassword);
            Console.WriteLine("==================================");
        }

        public void CreateDatabase()
        {
            Console.Write("Database name:");
            string dbName = Console.ReadLine();
            Config c = _configManager.ReadConfig(ConfigPath);
            if(_databaseManager.CreateDatabase(c, dbName))
            {
                c.ArangoDbDatabse = dbName;
                _configManager.WriteConfig(c, ConfigPath);

                Console.WriteLine("INFO Database created");
                if(_databaseManager.CheckDatabase(c))
                {
                    Console.WriteLine("INFO Database checked");
                }
            }
        }

        public void DatabaseStatistics()
        {
            Config c = _configManager.ReadConfig(ConfigPath);
            _databaseManager.CheckDatabase(c);
        }

        public void DeleteDatabase()
        {
            Config c = _configManager.ReadConfig(ConfigPath);
            _databaseManager.DeleteDatabase(c);
        }

        public void CreateArangoDbSyntaxClasses()
        {
            Config c = _configManager.ReadConfig(ConfigPath);

            Console.WriteLine("Destination folder:");
            c.SyntaxCollectionClassesFolder = Console.ReadLine();

            if(Directory.Exists(c.SyntaxCollectionClassesFolder))
            {
                Console.WriteLine("NOT FOUND");
                return;
            }

            _syntaxNodesToClasses.CreateTypesTree(c);
        }

        public void CreateArangoDbSyntaxCollections()
        {
            Config c = _configManager.ReadConfig(ConfigPath);

            _syntaxNodesToCollections.CreateTypesTree(c);
        }

        public void AnalyseCSharpFile()
        {
            Config c = _configManager.ReadConfig(ConfigPath);

            Console.WriteLine("File:");
            string file = Console.ReadLine();
            string programText = File.ReadAllText(file);
            SyntaxTree tree = CSharpSyntaxTree.ParseText(programText);
            var root = tree.GetCompilationUnitRoot();
            _syntaxWalker.Visit(c, root);
        }
    }
}
