using AstDomain;
using Microsoft.CodeAnalysis;

namespace AstShared
{
    public interface ICodeVisitor
    {
        void Visit(Config config, SyntaxNode t);
    }
}
