using AstDomain;
using AstShared;
using System;

namespace AstRoslyn
{
    public class SyntaxGeneratorVisitor : ISyntaxGeneratorVisitor
    {
        private readonly ClassGenerator _classGenerator = new ClassGenerator();

        public void Visit(Config config, Type t)
        {
            _classGenerator.CreateClassInFolder(
                config.SyntaxCollectionClassesFolder,
                t.FullName
                .Replace("Microsoft.CodeAnalysis.CSharp.", "")
                .Replace("Microsoft.CodeAnalysis.", "")
                .Replace(".", "Dot")
                .Replace("SyntaxDot", ""),
                "BaseSyntaxCollection",
                "AstArangoDbConnector.Syntax");
        }
    }
}
