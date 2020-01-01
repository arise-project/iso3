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
    public class SyntaxRepository : AstConnector, IRepository<SyntaxCoollectionEntity>
    {
        private readonly IMapper _mapper;
        private Config _config;

        public SyntaxRepository(IMapper mapper)
        {
            _mapper = mapper;
        }

        public void Init(Config config)
        {
            _config = config;
        }

        public void Update(string id, SyntaxCoollectionEntity element)
        {

        }

        public void Delete (string id)
        {

        }

        public List<SyntaxCoollectionEntity> Select()
        {
            return null;
        }

        public void Create(SyntaxCoollectionEntity syntaxEntity)
        {
            var syntax = _mapper.Map<BaseSyntax>(syntaxEntity);
            using (var db = CreateDatabase(_config))
            {
                //error codes https://docs.arangodb.com/3.3/Manual/Appendix/ErrorCodes.html
                string name = syntax.FullName.Replace("Microsoft.CodeAnalysis.CSharp.", "").Replace("Microsoft.CodeAnalysis.", "").Replace(".", "Dot");
                if (!db.ListCollections().Any(c => c.Name == name))
                {
                    db.CreateCollection(name);
                }
            }
        }
    }
}
