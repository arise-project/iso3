using AstShared;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Text;

namespace AstArangoDbConnector.Syntax
{
    public class CodeSyntaxVisitor : ICodeVisitor
    {
        AstConnector _connector;

        public CodeSyntaxVisitor(AstConnector connector)
        {
            _connector = connector;
        }

        public void Visit(SyntaxNode n)
        {
            CodeSyntax codeSyntax = new CodeSyntax { Text = n.GetText().ToString(), TypeName = n.GetType().FullName };
            _connector.CreateCodeVertex(codeSyntax);
        }
    }
}
