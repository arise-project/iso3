using Microsoft.CodeAnalysis;

namespace AstShared
{
    public interface ICodeVisitor
    {
        void Visit(SyntaxNode t);
    }
}
