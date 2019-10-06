using AstDomain;
using AstShared;
using Microsoft.CodeAnalysis;
using System;
using System.Linq;

namespace AstRoslyn
{
    public class ConsoleDumpWalker : SyntaxWalker, ISyntaxWalker
    {
        private ICodeVisitor _codeVisitor;

        public ConsoleDumpWalker(ICodeVisitor codeVisitor)
        {
            _codeVisitor = codeVisitor;
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
            System.Console.WriteLine(line);

            _codeVisitor.Visit(config, node);

            base.Visit(node);
        }
    }
}