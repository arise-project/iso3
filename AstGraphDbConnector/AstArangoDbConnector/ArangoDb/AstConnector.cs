using ArangoDB.Client;
using AstArangoDbConnector.Syntax;
using AstDomain;
using AstShared;
using AutoMapper;
using System;
using System.Linq;
using System.Reflection;

namespace AstArangoDbConnector
{
    public class AstConnector : IAstConnector
    {
        private readonly IMapper _mapper;

        public AstConnector(IMapper mapper)
        {
            _mapper = mapper;
        }

        private ArangoDatabase CreateDatabase(Config config)
        {
            return new ArangoDatabase(new DatabaseSharedSetting
            {
                Url = config.ArangoDbServer,
                Database = config.ArangoDbDatabse,
                Credential = new System.Net.NetworkCredential(config.ArangoDbUser, config.ArangoDbPassword)
            });
        }

        public void CreateCodeVertex(Config config, CodeSyntaxEntity codeEntity)
        {
            var code = _mapper.Map<CodeSyntax>(codeEntity);
            using (var db = CreateDatabase(config))
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

        public void CreateSyntaxCollection(Config config, BaseSyntaxEntity syntaxEntity)
        {
            var syntax = _mapper.Map<BaseSyntax>(syntaxEntity);
            using (var db = CreateDatabase(config))
            {
                //error codes https://docs.arangodb.com/3.3/Manual/Appendix/ErrorCodes.html
                string name = syntax.FullName.Replace("Microsoft.CodeAnalysis.CSharp.", "").Replace("Microsoft.CodeAnalysis.", "").Replace(".", "Dot");
                if (!db.ListCollections().Any(c => c.Name == name))
                {
                    db.CreateCollection(name);
                }
            }
        }

        public void CreateSyntaxAbstractDefinition(Config config, BaseSyntaxEntity syntaxEntity)
        {
            var syntax = _mapper.Map<BaseSyntax>(syntaxEntity);
            using (var db = CreateDatabase(config))
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

        public void CreateSyntaxConcreteDefinition(Config config, ConcreteSyntaxEntity syntaxEntity)
        {
            var syntax = _mapper.Map<ConcreteSyntax>(syntaxEntity);
            using (var db = CreateDatabase(config))
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
