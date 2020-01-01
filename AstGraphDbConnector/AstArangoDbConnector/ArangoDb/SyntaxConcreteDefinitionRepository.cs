using ArangoDB.Client;
using AstArangoDbConnector.Syntax;
using AstDomain;
using AstShared;
using AutoMapper;
using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

namespace AstArangoDbConnector
{
    public class SyntaxConcreteDefinitionRepository : AstConnector, IRepository<ConcreteSyntaxEntity>
    {
        private readonly IMapper _mapper;
        private Config _config;

        public SyntaxConcreteDefinitionRepository(IMapper mapper)
        {
            _mapper = mapper;
        }

        public void Init(Config config)
        {
            _config = config;
        }

        public void Update(string id, ConcreteSyntaxEntity element)
        {

        }

        public void Delete (string id)
        {

        }

        public List<ConcreteSyntaxEntity> Select()
        {
            return null;
        }

        public void Create(ConcreteSyntaxEntity syntaxEntity)
        {
            var syntax = _mapper.Map<ConcreteSyntax>(syntaxEntity);
            using (var db = CreateDatabase(_config))
            {
                if (!db.ListCollections().Any(c => c.Name == "ConcreteSyntax"))
                {
                    db.CreateCollection("ConcreteSyntax");
                }

                if (db.Query<ConcreteSyntax>().Where(p => AQL.Contains(p.FullName, syntax.FullName)).Count() == 0)
                {
                    db.Insert<ConcreteSyntax>(syntax);
                }
            }
        }
    }
}
