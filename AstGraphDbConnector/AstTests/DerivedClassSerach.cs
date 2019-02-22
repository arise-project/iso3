using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.CodeAnalysis.CSharp;

namespace AstTests
{
    //https://stackoverflow.com/questions/2362580/discovering-derived-types-using-reflection
    public static class DerivedClassSearch
    {
        public static List<Type> FindAllDerivedTypes<T>()
        {
            return FindAllDerivedTypes<T>(Assembly.GetAssembly(typeof(T)));
        }

        public static List<Type> FindAllDerivedTypes<T>(Assembly assembly)
        {
            var derivedType = typeof(T);
            return assembly
                .GetTypes()
                .Where(t =>
                    t != derivedType &&
                    derivedType.IsAssignableFrom(t)
                    ).ToList();

        }

        public static List<Type> FindAllDerivedTypes(Type type)
        {
            List<Type> result = new List<Type>();
            foreach(Assembly a in AppDomain.CurrentDomain.GetAssemblies())
            {
                var d = FindAllDerivedTypes(a, type);                
                result.AddRange(d);
            }

            return result;
        }

        public static List<Type> FindAllDerivedTypes(Assembly assembly, Type type)
        {
            var baseType = type;
            return assembly
                .GetTypes()
                .Where(t =>
                    t != baseType &&
                    t.IsSubclassOf(baseType) && t != typeof(System.Object)
                    ).ToList();

        }
    }

    public class TypeIntent
    {
        public Type Type {get;set;}

        public int Intent {get;set;}
    }

    public static class SyntaxNodesTree{
        public static void Print()
        {
            var types = DerivedClassSearch.FindAllDerivedTypes<CSharpSyntaxNode>();            

            string intent = "\t";
            Stack<TypeIntent> pull = new Stack<TypeIntent>();
            foreach(Type t in types)
            {
                pull.Push(new TypeIntent { Type = t, Intent = 0} );
            }
            while(pull.Count>0)
            {
                TypeIntent t = pull.Pop();
                for(int i= 0; i < t.Intent;i++)
                {
                    Console.Write(intent);    
                }
                Console.WriteLine(t.Type.FullName);
                var derived = DerivedClassSearch.FindAllDerivedTypes(t.Type);

                foreach(Type d in derived)
                {
                    pull.Push(new TypeIntent { Type = d, Intent = t.Intent+1} );
                }
            }
            
        }
    }
}
