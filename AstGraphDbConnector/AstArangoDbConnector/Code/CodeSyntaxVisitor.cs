using AstShared;
using Microsoft.CodeAnalysis;

namespace AstArangoDbConnector.Syntax
{
    public class CodeSyntaxVisitor : ICodeVisitor
    {
        AstConnector _connector;

        public CodeSyntaxVisitor(AstConnector connector)
        {
            _connector = connector;
        }

        public void Visit(SyntaxNode n)
        {
            CodeSyntax codeSyntax = new CodeSyntax { Text = n.GetText().ToString(), TypeName = n.GetType().FullName };
            _connector.CreateCodeVertex(codeSyntax);
        }
    }
}
