using Microsoft.CodeAnalysis;

namespace AstShared
{
    public interface ISyntaxWalker
    {
        void Visit(SyntaxNode node);
    }
}
