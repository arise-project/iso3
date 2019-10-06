using AstDomain;
using System;

namespace AstShared
{
    public interface ISyntaxVisitor
    {
        void Visit(Config config, Type t);
    }
}
