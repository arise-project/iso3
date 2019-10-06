using AstDomain;
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
            CodeSyntaxEntity codeSyntax = new CodeSyntaxEntity { Text = n.GetText().ToString(), TypeName = n.GetType().FullName };
            _connector.CreateCodeVertex(codeSyntax);
        }
    }
}
