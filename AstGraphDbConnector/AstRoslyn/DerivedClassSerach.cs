using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AstRoslyn
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
                    t.IsSubclassOf(baseType) && t != typeof(System.Object) //https://theburningmonk.com/2011/03/type-issubclssof-and-type-isassignablefrom/
                    ).ToList();

        }
    }
}
