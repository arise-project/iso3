using System;
using System.Collections.Generic;
using System.Text;

namespace AstShared
{
    public interface ISyntaxVisitor
    {
        void Visit(Type t);
    }
}
