using AstDomain;
using AstShared;
using System;

namespace AstArangoDbConnector.Syntax
{
    public class BaseSyntaxVisitor : IBaseSyntaxVisitor
    {
        private readonly IAstConnector _astConnector;

        public BaseSyntaxVisitor(IAstConnector astConnector)
        {
            _astConnector = astConnector;
        }
        public void Visit(Config config, Type t)
        {
            BaseSyntaxEntity baseSyntax = new BaseSyntaxEntity { Name = t.Name, FullName = t.FullName };
            _astConnector.CreateSyntaxAbstractDefinition(config, baseSyntax);
            _astConnector.CreateSyntaxCollection(config, baseSyntax);
        }
    }
}
