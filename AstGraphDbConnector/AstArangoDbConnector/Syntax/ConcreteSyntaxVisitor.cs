using AstDomain;
using AstShared;
using System;

namespace AstArangoDbConnector.Syntax
{
    public class ConcreteSyntaxVisitor : IConcreteSyntaxVisitor
    {
        private readonly IAstConnector _astConnector;

        public ConcreteSyntaxVisitor(IAstConnector astConnector)
        {
            _astConnector = astConnector;
        }

        public void Visit(Config config, Type t)
        {
            ConcreteSyntaxEntity concreteSyntax = new ConcreteSyntaxEntity { Name = t.Name, FullName = t.FullName, ParentFullName = t.BaseType.FullName };
            _astConnector.CreateSyntaxConcreteDefinition(config, concreteSyntax);
            _astConnector.CreateSyntaxCollection(config, concreteSyntax);
        }
    }
}
