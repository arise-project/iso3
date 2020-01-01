using AstDomain;
using AstShared;
using System;

namespace AstArangoDbConnector.Syntax
{
    public class BaseSyntaxVisitor : IBaseSyntaxVisitor
    {
        private readonly IRepository<BaseSyntaxEntity> _baseSyntaxRepository;
        private readonly IRepository<SyntaxCoollectionEntity> _syntaxRepository;


        public BaseSyntaxVisitor(IRepository<BaseSyntaxEntity> baseSyntaxRepository,
            IRepository<SyntaxCoollectionEntity> syntaxRepository)
        {
            _baseSyntaxRepository = baseSyntaxRepository;
            _syntaxRepository = syntaxRepository;
        }
        public void Visit(Config config, Type t)
        {
            BaseSyntaxEntity baseSyntax = new BaseSyntaxEntity { Name = t.Name, FullName = t.FullName };
            _baseSyntaxRepository.Init(config);
            _baseSyntaxRepository.Create(baseSyntax);
            SyntaxCoollectionEntity syntax = new SyntaxCoollectionEntity { Name = t.Name, FullName = t.FullName };
            _syntaxRepository.Init(config);
            _syntaxRepository.Create(syntax);
        }
    }
}
