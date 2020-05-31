using AstDomain;
using AstShared;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using YamlDotNet.Serialization;

namespace AstTests
{
    public class App : IApp
    {
        private const string ConfigPath = @"AstTests.yaml";

        private readonly ISyntaxNodesToClasses _syntaxNodesToClasses;
        private readonly ISyntaxNodesToCollections _syntaxNodesToCollections;
        private readonly ISyntaxNodesToCsv _syntaxNodesToCsv;
        private readonly ICodeVisitor _codeVisitor;
        private readonly IConfigManager _configManager;
        private readonly ISyntaxWalker _syntaxWalker;
        private readonly IDatabaseManager _databaseManager;

        public App(
            ISyntaxNodesToClasses syntaxNodesToClasse, 
            ISyntaxNodesToCollections syntaxNodesToCollections, 
            ISyntaxNodesToCsv syntaxNodesToCsv,
            ICodeVisitor codeVisitor,
            IConfigManager configManager,
            ISyntaxWalker syntaxWalker,
            IDatabaseManager databaseManager)
        {
            _syntaxNodesToClasses = syntaxNodesToClasse;
            _syntaxNodesToCollections = syntaxNodesToCollections;
            _syntaxNodesToCsv = syntaxNodesToCsv;
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

            c.SyntaxCollectionClassesFolder = Path.Combine(Environment.CurrentDirectory, "../AstArangoDbConnector/SyntaxCollections");
            Console.WriteLine($"Destination folder: {c.SyntaxCollectionClassesFolder}");

            if(!Directory.Exists(c.SyntaxCollectionClassesFolder))
            {
                Console.WriteLine("NOT FOUND");
                return;
            }

            _syntaxNodesToClasses.Perform(c);
            _configManager.WriteConfig(c, ConfigPath);
        }

        public void CreateArangoDbSyntaxCollections()
        {
            Config c = _configManager.ReadConfig(ConfigPath);

            _syntaxNodesToCollections.Perform(c);
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

        public void GenSyntaxCsv()
        {
            Config c = _configManager.ReadConfig(ConfigPath);
            
            c.SyntaxCsvFile = Path.Combine(Environment.CurrentDirectory, "../AstRoslyn/SyntaxCollections/predefined_syntax.csv");

            if(!Directory.Exists(Path.GetDirectoryName(c.SyntaxCsvFile)))
            {
                Console.WriteLine($"ERROR: Directory not exists for {c.SyntaxCsvFile}");
                return;
            }

            c.SyntaxCsvHeader = string.Empty;

            if(File.Exists(c.SyntaxCsvFile))
            {
                Console.Write("Extend (y/n)?");
                if(Console.ReadKey().Key != ConsoleKey.Y) 
                {
                    Console.WriteLine($"ERROR: File exists {c.SyntaxCsvFile}");
                    return;
                }
                c.SyntaxCsvHeader = File.ReadLines(c.SyntaxCsvFile).First();
                Console.Write("Header extend:");
                string extend = Console.ReadLine();
                c.SyntaxCsvHeader += "," + extend;
            }
            else
            {
                Console.Write("Header:");
                c.SyntaxCsvHeader = Console.ReadLine();
                File.WriteAllLines(c.SyntaxCsvFile, new List<string> { c.SyntaxCsvHeader });
            }

            _syntaxNodesToCsv.Perform(c);
            _configManager.WriteConfig(c, ConfigPath);
        }
    }
}
