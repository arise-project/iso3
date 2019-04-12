using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis;

namespace AstShared
{
    public interface ICodeVisitor
    {
        void Visit(SyntaxNode t);
    }
}
