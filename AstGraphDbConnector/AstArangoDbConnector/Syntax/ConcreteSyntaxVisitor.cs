using AstDomain;
using AstShared;
using System;

namespace AstArangoDbConnector.Syntax
{
    public class ConcreteSyntaxVisitor : IConcreteSyntaxVisitor
    {
        AstConnector _connector;

        public ConcreteSyntaxVisitor(AstConnector connector)
        {
            _connector = connector;
        }

        public void Visit(Type t)
        {
            ConcreteSyntaxEntity concreteSyntax = new ConcreteSyntaxEntity { Name = t.Name, FullName = t.FullName, ParentFullName = t.BaseType.FullName };
            _connector.CreateSyntaxConcreteDefinition(concreteSyntax);
            _connector.CreateSyntaxCollection(concreteSyntax);
        }
    }
}
