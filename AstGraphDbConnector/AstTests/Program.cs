using AstArangoDbConnector;
using System;
using System.Threading.Tasks;

namespace AstTests
{
    class Program
    {
        static void Main(string[] args)
        {
            new AstConnector().CreatePerson().ConfigureAwait(false).GetAwaiter().GetResult();
        }
    }
}
