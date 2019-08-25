using ArangoDB.Client;
using Microsoft.CodeAnalysis;

namespace AstArangoDbConnector.Syntax
{
    public class CodeSyntax
    {

        [DocumentProperty(Identifier = IdentifierType.Key)]
        public string Key;

        public string Text { get; set; }

        public string TypeName { get; set; }

        public SyntaxNode Node { get; set; }
    }
}