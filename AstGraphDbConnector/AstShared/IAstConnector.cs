using AstDomain;

namespace AstShared
{
    public interface IAstConnector
    {
        void CreateCodeVertex(Config config, CodeSyntaxEntity code);

        void CreateSyntaxCollection(Config config, BaseSyntaxEntity syntax);

        void CreateSyntaxAbstractDefinition(Config config, BaseSyntaxEntity syntax);

        void CreateSyntaxConcreteDefinition(Config config, ConcreteSyntaxEntity syntax);
    }
}