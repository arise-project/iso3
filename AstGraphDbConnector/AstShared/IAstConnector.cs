using AstArangoDbConnector.Syntax;
using System;
using System.Linq;
using System.Reflection;

namespace AstArangoDbConnector
{
    public interface IAstConnector
    {
        void CreateCodeVertex(CodeSyntax code);

        void CreateSyntaxCollection(BaseSyntax syntax);

        void CreateSyntaxAbstractDefinition(BaseSyntax syntax);

        void CreateSyntaxConcreteDefinition(ConcreteSyntax syntax);
    }
}