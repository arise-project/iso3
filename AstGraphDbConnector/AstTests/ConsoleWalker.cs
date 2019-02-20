using System;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace AstTests
{
    class ConsoleDumpWalker : SyntaxWalker
    {
        public override void Visit(SyntaxNode node)
        {
            int padding = node.Ancestors().Count();
            //To identify leaf nodes vs nodes with children
            string prepend = node.ChildNodes().Any() ? "[-]" : "[.]";
            //Get the type of the node
            string line = new String(' ', padding) + prepend +
                                    " " + node.GetType().ToString() + " " + (node as  SyntaxNode).GetText();
            //Write the line
            System.Console.WriteLine(line);
            base.Visit(node);
        }

    }
}