using AstDomain;
using AstShared;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;

namespace AstRoslyn
{
    public class SyntaxNodesToClasses : ISyntaxNodesToClasses
    {
        ISyntaxGeneratorVisitor _syntaxGeneratorVisitor;

        public SyntaxNodesToClasses(ISyntaxGeneratorVisitor syntaxGeneratorVisitor)
        {
            _syntaxGeneratorVisitor = syntaxGeneratorVisitor;
        }

        public void CreateTypesTree(Config config)
        {
            var types = DerivedClassSearchHelper.FindAllDerivedTypes<CSharpSyntaxNode>();

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
                var derived = DerivedClassSearchHelper.FindAllDerivedTypes(t.Type);

                if (_syntaxGeneratorVisitor != null)
                {
                    _syntaxGeneratorVisitor.Visit(config, t.Type);
                }

                foreach (Type d in derived)
                {
                    pull.Push(new TypeIntent { Type = d, Intent = t.Intent + 1 });
                }
            }
        }
    }
}
