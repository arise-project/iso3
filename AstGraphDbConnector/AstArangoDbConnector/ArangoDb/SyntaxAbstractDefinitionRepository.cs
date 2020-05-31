using ArangoDB.Client;
using AstArangoDbConnector.Syntax;
using AstDomain;
using AstShared;
using AutoMapper;
using System.Linq;
using System.Collections.Generic;

namespace AstArangoDbConnector
{
    public class SyntaxAbstractDefinitionRepository : ArangoDbConnector, IRepository<BaseSyntaxEntity>
    {
        private readonly IMapper _mapper;
        private Config _config;

        public SyntaxAbstractDefinitionRepository(IMapper mapper)
        {
            _mapper = mapper;
        }

        public void Init(Config config)
        {
            _config = config;
        }

        public void Update(string id, BaseSyntaxEntity element)
        {

        }

        public void Delete (string id)
        {

        }

        public List<BaseSyntaxEntity> Select()
        {
            return null;
        }
        
        public void Create(BaseSyntaxEntity syntaxEntity)
        {
            var syntax = _mapper.Map<BaseSyntax>(syntaxEntity);
            using (var db = GetDatabase(_config))
            {
                if (!db.ListCollections().Any(c => c.Name == "BaseSyntax"))
                {
                    db.CreateCollection("BaseSyntax");
                }


                if (db.Query<BaseSyntax>().Where(p => AQL.Contains(p.FullName, syntax.FullName)).Count() == 0)
                {
                    db.Insert<BaseSyntax>(syntax);
                }
            }
        }
    }
}
