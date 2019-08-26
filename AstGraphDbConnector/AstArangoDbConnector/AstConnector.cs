using ArangoDB.Client;
using AstArangoDbConnector.Syntax;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Linq;
using System.Reflection;

namespace AstArangoDbConnector
{
    public class AstConnector
    {
        public void CreateCodeVertex(CodeSyntax code)
        {
            using (var db = new ArangoDatabase(new DatabaseSharedSetting
            {
                Url = "http://localhost:8529",
                Database = "AstGraphDBConnector",
                Credential = new System.Net.NetworkCredential("root", "12345")
            }))
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

        public void CreateSyntaxCollection(BaseSyntax syntax)
        {
            using (var db = new ArangoDatabase(new DatabaseSharedSetting
            {
                Url = "http://localhost:8529",
                Database = "AstGraphDBConnector",
                Credential = new System.Net.NetworkCredential("root", "12345")
            }))
            {
                //error codes https://docs.arangodb.com/3.3/Manual/Appendix/ErrorCodes.html
                string name = syntax.FullName.Replace("Microsoft.CodeAnalysis.CSharp.", "").Replace("Microsoft.CodeAnalysis.", "").Replace(".", "Dot");
                if (!db.ListCollections().Any(c => c.Name == name))
                {
                    db.CreateCollection(name);
                }
            }
        }

        public void CreateSyntaxAbstractDefinition(BaseSyntax syntax)
        {
            using (var db = new ArangoDatabase(new DatabaseSharedSetting
            {
                Url = "http://localhost:8529",
                Database = "AstGraphDBConnector",
                Credential = new System.Net.NetworkCredential("root", "12345")
            }))
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

        public void CreateSyntaxConcreteDefinition(ConcreteSyntax syntax)
        {
            using (var db = new ArangoDatabase(new DatabaseSharedSetting
            {
                Url = "http://localhost:8529",
                Database = "AstGraphDBConnector",
                Credential = new System.Net.NetworkCredential("root", "12345")
            }))
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
