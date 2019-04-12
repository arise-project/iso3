using ArangoDB.Client;

namespace AstArangoDbConnector.Syntax
{
    public class CodeSyntax
    {

        [DocumentProperty(Identifier = IdentifierType.Key)]
        public string Key;

        public string Text { get; set; }

        public string TypeName { get; set; }
    }
}