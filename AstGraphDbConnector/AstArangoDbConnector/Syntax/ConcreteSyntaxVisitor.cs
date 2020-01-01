using AstDomain;
using AstShared;
using System;

namespace AstArangoDbConnector.Syntax
{
    public class ConcreteSyntaxVisitor : IConcreteSyntaxVisitor
    {
        private readonly IRepository<ConcreteSyntaxEntity> _concreteSyntaxRepository;
        private readonly IRepository<SyntaxCoollectionEntity> _syntaxRepository;

        public ConcreteSyntaxVisitor(
            IRepository<ConcreteSyntaxEntity> concreteSyntaxRepository,
            IRepository<SyntaxCoollectionEntity> syntaxRepository)
        {
            _concreteSyntaxRepository = concreteSyntaxRepository;
            _syntaxRepository = syntaxRepository;
        }

        public void Visit(Config config, Type t)
        {
            ConcreteSyntaxEntity concreteSyntax = new ConcreteSyntaxEntity { Name = t.Name, FullName = t.FullName, ParentFullName = t.BaseType.FullName };
            _concreteSyntaxRepository.Init(config);
            _concreteSyntaxRepository.Create(concreteSyntax);
            SyntaxCoollectionEntity syntax = new SyntaxCoollectionEntity { Name = t.Name, FullName = t.FullName };
            _syntaxRepository.Init(config);
            _syntaxRepository.Create(syntax);
        }
    }
}
