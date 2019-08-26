﻿using AstShared;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AstRoslyn
{
    public class SyntaxNodesToCollections : ISyntaxNodesToCollections
    {
        IBaseSyntaxVisitor _baseSyntaxVisitor;
        IConcreteSyntaxVisitor _concreteSyntaxVisitor;

        public SyntaxNodesToCollections(IBaseSyntaxVisitor baseSyntaxVisitor,
            IConcreteSyntaxVisitor concreteSyntaxVisitor)
        {
            _baseSyntaxVisitor = baseSyntaxVisitor;
            _concreteSyntaxVisitor = concreteSyntaxVisitor;
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

                foreach (Type d in derived)
                {
                    pull.Push(new TypeIntent { Type = d, Intent = t.Intent + 1 });
                }
            }

        }
    }
}