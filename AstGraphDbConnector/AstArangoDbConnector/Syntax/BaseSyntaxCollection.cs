using ArangoDB.Client;

namespace AstArangoDbConnector.Syntax
{
    public class BaseSyntaxCollection
    {

        [DocumentProperty(Identifier = IdentifierType.Key)]
        public string Key;

        public string Text { get; set; }
    }
}