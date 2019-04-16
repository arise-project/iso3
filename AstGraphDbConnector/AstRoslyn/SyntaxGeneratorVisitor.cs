using System;
using AstShared;

namespace AstRoslyn
{
    public class SyntaxGeneratorVisitor : ISyntaxVisitor
    {
        private readonly ClassGenerator _classGenerator = new ClassGenerator();

        public void Visit(Type t)
        {
            _classGenerator.CreateClassInFolder(
                @"/home/eugene/Projects/iso3/AstGraphDbConnector/AstArangoDbConnector/SyntaxCollections",
                t.FullName.Replace("Microsoft.CodeAnalysis.CSharp.","").Replace("Microsoft.CodeAnalysis.","").Replace(".","Dot"),
                "BaseSyntaxCollection");
        }
    }
}
