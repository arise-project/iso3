using AstShared;
using System;
using System.Collections.Generic;
using System.Text;

namespace AstArangoDbConnector.Syntax
{
    public class BaseSyntaxVisitor : ISyntaxVisitor
    {
        AstConnector _connector;

        public BaseSyntaxVisitor(AstConnector connector)
        {
            _connector = connector;
        }
        public void Visit(Type t)
        {
            BaseSyntax baseSyntax = new BaseSyntax { Name = t.Name, FullName = t.FullName };
            _connector.CreateSyntaxAbstractDefinition(baseSyntax);
        }
    }
}
