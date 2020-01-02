using ArangoDB.Client;
using Microsoft.CodeAnalysis;
using AstArangoDbConnector;

namespace AstArangoDbConnector.Syntax
{
    public class CodeSyntax : BaseEntity
    {
        public string Text { get; set; }

        public string TypeName { get; set; }

        public SyntaxNode Node { get; set; }
    }
}