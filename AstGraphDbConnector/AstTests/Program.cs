using AstArangoDbConnector;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using AstRoslyn;
using AstArangoDbConnector.Syntax;

namespace AstTests
{
    class Program
    {
        private const string Path = "Program.cs";

        static void Main(string[] args)
        {
            //new AstConnector().CreatePerson().ConfigureAwait(false).GetAwaiter().GetResult();

            //roslyn
            //https://github.com/dotnet/roslyn/blob/master/src/Compilers/CSharp/Portable/Syntax/SyntaxKind.cs

            

            var connector = new AstConnector();

            var typesTree = new SyntaxNodesTree();
            typesTree.AcceptBaseSyntaxWritter(new BaseSyntaxVisitor(connector));
            typesTree.AcceptConcreteSyntaxWritter(new ConcreteSyntaxVisitor(connector));
            typesTree.AcceptSyntaxGenerator(new SyntaxGeneratorVisitor());
            typesTree.CreateTypesTree();

            // string programText = File.ReadAllText(path: Path);
            // SyntaxTree tree = CSharpSyntaxTree.ParseText(programText);
            // var root = tree.GetCompilationUnitRoot();
            // var writer = new ConsoleDumpWalker(new CodeSyntaxVisitor(connector));
            // writer.Visit(root);
        }
    }
}
