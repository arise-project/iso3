using AstDomain;

namespace AstShared
{
    public interface IAstConnector
    {
        void CreateCodeVertex(CodeSyntaxEntity code);

        void CreateSyntaxCollection(BaseSyntaxEntity syntax);

        void CreateSyntaxAbstractDefinition(BaseSyntaxEntity syntax);

        void CreateSyntaxConcreteDefinition(ConcreteSyntaxEntity syntax);
    }
}