using AstDomain;
using AstShared;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;

namespace AstRoslyn
{
    public class SyntaxNodesToCsv : ISyntaxNodesToCsv
    {
        ISyntaxCsvVisitor _syntaxCsvVisitor;

        public SyntaxNodesToCsv(ISyntaxCsvVisitor syntaxCsvVisitor)
        {
            _syntaxCsvVisitor = syntaxCsvVisitor;
        }

        public void Perform(Config config)
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

                _syntaxCsvVisitor.Visit(config, t.Type);

                foreach (Type d in derived)
                {
                    pull.Push(new TypeIntent { Type = d, Intent = t.Intent + 1 });
                }
            }
        }
    }
}
