using AstDomain;
using AstRoslyn;
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
        private readonly IConnectionFactory _connectionFactory;

        public App(
            ISyntaxNodesToClasses syntaxNodesToClasse, 
            ISyntaxNodesToCollections syntaxNodesToCollections, 
            ICodeVisitor codeVisitor,
            IConnectionFactory connectionFactory)
        {
            _syntaxNodesToClasses = syntaxNodesToClasse;
            _syntaxNodesToCollections = syntaxNodesToCollections;
            _codeVisitor = codeVisitor;
            _connectionFactory = connectionFactory;
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
            var s = new Serializer();
            File.WriteAllText(ConfigPath, s.Serialize(c));
        }

        public void PrintConfig()
        {
            Config c = _connectionFactory.CreateConfig(ConfigPath);

            Console.WriteLine("==================================");
            Console.WriteLine("Server:{0}", c.ArangoDbServer);
            Console.WriteLine("Database:{0}", c.ArangoDbDatabse);
            Console.WriteLine("==================================");
        }

        public void CreateArangoDbSyntaxClasses()
        {
            Config c = _connectionFactory.CreateConfig(ConfigPath);

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
            Config c = _connectionFactory.CreateConfig(ConfigPath);

            _syntaxNodesToCollections.CreateTypesTree(c);
        }

        public void AnalyseCSharpFile()
        {
            Config c = _connectionFactory.CreateConfig(ConfigPath);

            Console.WriteLine("File:");
            string file = Console.ReadLine();
            string programText = File.ReadAllText(file);
            SyntaxTree tree = CSharpSyntaxTree.ParseText(programText);
            var root = tree.GetCompilationUnitRoot();
            var writer = new ConsoleDumpWalker(_codeVisitor);
            writer.Visit(c, root);
        }
    }
}
