using ArangoDB.Client;

namespace AstArangoDbConnector.Syntax
{
    public class BaseSyntax
    {

        [DocumentProperty(Identifier = IdentifierType.Key)]
        public string Key;

        public string Name { get; set; }

        public string FullName { get; set; }
    }
}