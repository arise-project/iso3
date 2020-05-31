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
    public class CodeSyntaxRepository : ArangoDbConnector, IRepository<CodeSyntaxEntity>
    {
        private readonly IMapper _mapper;
        private Config _config;

        public CodeSyntaxRepository(IMapper mapper)
        {
            _mapper = mapper;
        }

        public void Init(Config config)
        {
            _config = config;
        }

        public void Update(string id, CodeSyntaxEntity element)
        {

        }

        public void Delete (string id)
        {

        }

        public List<CodeSyntaxEntity> Select()
        {
            return null;
        }

        public void Create(CodeSyntaxEntity codeEntity)
        {
            var code = _mapper.Map<CodeSyntax>(codeEntity);
            using (var db = GetDatabase(_config))
            {
                //error codes https://docs.arangodb.com/3.3/Manual/Appendix/ErrorCodes.html
                string name = code.TypeName.Replace("Microsoft.CodeAnalysis.CSharp.", "").Replace("Microsoft.CodeAnalysis.", "").Replace(".", "Dot");
                if (db.ListCollections().Any(c => c.Name == name))
                {
                    MethodInfo method = typeof(ArangoDatabase).GetMethod("Insert");
                    var collectionType = Type.GetType($"AstArangoDbConnector.Syntax.{name}");
                    MethodInfo generic = method.MakeGenericMethod(collectionType);
                    Console.WriteLine(string.Join(",", generic.GetParameters().Select(p => p.Name).ToArray()));
                    var item = Activator.CreateInstance(collectionType);

                    var baseItem = (BaseSyntaxCollection)item;

                    baseItem.Text = code.Text;
                    generic.Invoke(db, new[] { item, null, null });
                }
                else
                {
                    throw new NotSupportedException(code.TypeName);
                }
            }
        }
    }
}
