using System;

namespace AstShared
{
    public interface ISyntaxVisitor
    {
        void Visit(Type t);
    }
}
