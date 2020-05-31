using ArangoDB.Client;

namespace AstArangoDbConnector
{
    public class BaseEntity
    {
        [DocumentProperty(Identifier = IdentifierType.Key)]
        public string Key;

    }
}