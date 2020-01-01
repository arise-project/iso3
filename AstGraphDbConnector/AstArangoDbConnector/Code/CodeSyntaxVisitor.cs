using AstDomain;
using AstShared;
using Microsoft.CodeAnalysis;

namespace AstArangoDbConnector.Syntax
{
    public class CodeSyntaxVisitor : ICodeVisitor
    {
        private readonly IRepository<CodeSyntaxEntity> _codeSyntaxRepository;

        public CodeSyntaxVisitor(IRepository<CodeSyntaxEntity> codeSyntaxRepository)
        {
            _codeSyntaxRepository = codeSyntaxRepository;
        }

        public void Visit(Config config, SyntaxNode n)
        {
            CodeSyntaxEntity codeSyntax = new CodeSyntaxEntity { Text = n.GetText().ToString(), TypeName = n.GetType().FullName };
            _codeSyntaxRepository.Init(config);
            _codeSyntaxRepository.Create(codeSyntax);
        }
    }
}
