using AstDomain;
using Microsoft.CodeAnalysis;

namespace AstShared
{
    public interface ISyntaxWalker
    {
        void Visit(Config config, SyntaxNode node);
    }
}
