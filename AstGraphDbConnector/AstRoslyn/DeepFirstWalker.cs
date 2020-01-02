using AstDomain;
using AstShared;
using Microsoft.CodeAnalysis;
using System;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace AstRoslyn
{
    public class DeepFirstWalker : SyntaxWalker, ISyntaxWalker
    {
        private readonly ICodeVisitor _codeVisitor;
        private readonly ILogger<DeepFirstWalker> _logger;

        public DeepFirstWalker(ICodeVisitor codeVisitor, ILogger<DeepFirstWalker> logger)
        {
            _codeVisitor = codeVisitor;
            _logger = logger;
        }

        public void Visit(Config config, SyntaxNode node)
        {
            int padding = node.Ancestors().Count();
            //To identify leaf nodes vs nodes with children
            string prepend = node.ChildNodes().Any() ? "[-]" : "[.]";
            //Get the type of the node
            string line = new string(' ', padding) + prepend +
                                    " " + node.GetType().ToString() + " " + (node as SyntaxNode).GetText();
            //Write the line
            _logger.LogDebug(line);

            _codeVisitor.Visit(config, node);

            base.Visit(node);
        }
    }
}