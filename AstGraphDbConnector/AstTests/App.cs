using AstArangoDbConnector;
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

        private IAstConnector _astConnector;
        private readonly ISyntaxNodesToClasses _syntaxNodesToClasse;
        private readonly ISyntaxNodesToCollections _syntaxNodesToCollections;
        private readonly ICodeVisitor _codeVisitor;

        public App(IAstConnector astConnector, ISyntaxNodesToClasses syntaxNodesToClasse, ISyntaxNodesToCollections syntaxNodesToCollections, ICodeVisitor codeVisitor)
        {
            _astConnector = astConnector;
            _syntaxNodesToClasse = syntaxNodesToClasse;
            _syntaxNodesToCollections = syntaxNodesToCollections;
            _codeVisitor = codeVisitor;
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
            if (!File.Exists(ConfigPath))
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

        public void CreateArangoDbSyntaxClasses()
        {
            _syntaxNodesToClasse.CreateTypesTree();
        }

        public void CreateArangoDbSyntaxCollections()
        {
            _syntaxNodesToCollections.CreateTypesTree();
        }

        public void AnalyseCSharpFile()
        {
            Console.WriteLine("File:");
            string file = Console.ReadLine();
            string programText = File.ReadAllText(file);
            SyntaxTree tree = CSharpSyntaxTree.ParseText(programText);
            var root = tree.GetCompilationUnitRoot();
            var writer = new ConsoleDumpWalker(_codeVisitor);
            writer.Visit(root);
        }
    }
}
