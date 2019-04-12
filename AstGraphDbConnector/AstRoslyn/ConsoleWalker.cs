using System;
using System.Linq;
using AstShared;
using Microsoft.CodeAnalysis;

namespace AstRoslyn
{
    public class ConsoleDumpWalker : SyntaxWalker
    {
        private ICodeVisitor _codeVisitor;

        public ConsoleDumpWalker(ICodeVisitor codeVisitor)
        {
            _codeVisitor = codeVisitor;
        }
        
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

            _codeVisitor.Visit(node);

            base.Visit(node);
        }

    }
}