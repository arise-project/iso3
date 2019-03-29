using AstShared;
using System;
using System.Collections.Generic;
using System.Text;

namespace AstArangoDbConnector.Syntax
{
    public class ConcreteSyntaxVisitor : ISyntaxVisitor
    {
        AstConnector _connector;

        public ConcreteSyntaxVisitor(AstConnector connector)
        {
            _connector = connector;
        }

        public void Visit(Type t)
        {
            ConcreteSyntax concreteSyntax = new ConcreteSyntax { Name = t.Name, FullName = t.FullName, ParentFullName = t.BaseType.FullName };
            _connector.CreateSyntaxConcreteDefinition(concreteSyntax);
        }
    }
}
