using ArangoDB.Client;
using AstArangoDbConnector.Syntax;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AstArangoDbConnector
{
    public class AstConnector
    {
        class Person
        {
            [DocumentProperty(Identifier = IdentifierType.Key)]
            public string Key;
            public string Name;
            public int Age;
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
                if(!db.ListCollections().Any(c => c.Name == "BaseSyntax")){
                    db.CreateCollection("BaseSyntax");
                }


                if(db.Query<BaseSyntax>().Where(p => AQL.Contains(p.FullName, syntax.FullName)).Count() == 0)
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
                if(!db.ListCollections().Any(c => c.Name == "ConcreteSyntax")){
                    db.CreateCollection("ConcreteSyntax");
                }

                if(db.Query<ConcreteSyntax>().Where(p => AQL.Contains(p.FullName, syntax.FullName)).Count() == 0)
                {
                    db.Insert<ConcreteSyntax>(syntax);
                }
            }
        }

        public async Task CreatePerson()
        {
            using (var db = new ArangoDatabase(new DatabaseSharedSetting
            {
                Url = "http://localhost:8529",
                Database = "AstGraphDBConnector",
                Credential = new System.Net.NetworkCredential("root", "12345")
            }))
            {
                if(!db.ListCollections().Any(c => c.Name == "Person")){
                    db.CreateCollection("Person");
                }
                
                ///////////////////// insert and update documents /////////////////////////
                var person = new Person { Name = "raoof hojat", Age = 26 };

                // insert new document and creates 'Person' collection on the fly
                db.Insert<Person>(person);

                person.Age = 27;

                // partially updates person, only 'Age' attribute will be updated
                await db.UpdateAsync<Person>(person);

                // returns 27
                int age = db.Query<Person>()
                                  .Where(p => AQL.Contains(p.Name, "raoof"))
                                  .Select(p => p.Age)
                                  .FirstOrDefault();
            }
        }
    }
}
