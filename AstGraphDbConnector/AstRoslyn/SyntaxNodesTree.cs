using AstShared;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AstRoslyn
{
    public class SyntaxNodesTree
    {
        ISyntaxVisitor _baseSyntaxVisitor;
        ISyntaxVisitor _concreteSyntaxVisitor;
        ISyntaxVisitor _syntaxGeneratorVisitor;

        public void AcceptBaseSyntaxWritter(ISyntaxVisitor visitor)
        {
            _baseSyntaxVisitor = visitor;
        }

        public void AcceptConcreteSyntaxWritter(ISyntaxVisitor visitor)
        {
            _concreteSyntaxVisitor = visitor;
        }

        public void AcceptSyntaxGenerator(ISyntaxVisitor visitor)
        {
            _syntaxGeneratorVisitor = visitor;
        }

        public void CreateTypesTree()
        {
            var types = DerivedClassSearch.FindAllDerivedTypes<CSharpSyntaxNode>();

            string intent = "\t";
            Stack<TypeIntent> pull = new Stack<TypeIntent>();
            foreach (Type t in types)
            {
                pull.Push(new TypeIntent { Type = t, Intent = 0 });
            }
            while (pull.Count > 0)
            {
                TypeIntent t = pull.Pop();
                for (int i = 0; i < t.Intent; i++)
                {
                    Console.Write(intent);
                }
                Console.WriteLine(t.Type.FullName);
                var derived = DerivedClassSearch.FindAllDerivedTypes(t.Type);

                if (derived.Any())
                {
                    _baseSyntaxVisitor.Visit(t.Type);
                }
                else
                {
                    _concreteSyntaxVisitor.Visit(t.Type);
                }

                _syntaxGeneratorVisitor.Visit(t.Type);

                foreach (Type d in derived)
                {
                    pull.Push(new TypeIntent { Type = d, Intent = t.Intent + 1 });
                }
            }

        }
    }
}
