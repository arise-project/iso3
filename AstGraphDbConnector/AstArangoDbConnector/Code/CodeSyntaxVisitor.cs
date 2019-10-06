using AstDomain;
using AstShared;
using Microsoft.CodeAnalysis;

namespace AstArangoDbConnector.Syntax
{
    public class CodeSyntaxVisitor : ICodeVisitor
    {
        private readonly IAstConnector _astConnector;

        public CodeSyntaxVisitor(IAstConnector astConnector)
        {
            _astConnector = astConnector;
        }

        public void Visit(Config config, SyntaxNode n)
        {
            CodeSyntaxEntity codeSyntax = new CodeSyntaxEntity { Text = n.GetText().ToString(), TypeName = n.GetType().FullName };
            _astConnector.CreateCodeVertex(config, codeSyntax);
        }
    }
}
