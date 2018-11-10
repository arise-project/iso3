using AstNeo4jConnector;
using System;

namespace AstTests
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var greeter = new AstConnector("bolt://localhost:7687", "user", "password"))
            {
                
            }

        }
    }
}
