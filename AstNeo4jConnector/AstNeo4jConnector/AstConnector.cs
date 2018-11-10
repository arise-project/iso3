using Neo4j.Driver.V1;
using System;

namespace AstNeo4jConnector
{
    public class AstConnector : IDisposable
    {
        private readonly IDriver _driver;

        public AstConnector(string uri, string user, string password)
        {
            _driver = GraphDatabase.Driver(uri, AuthTokens.Basic(user, password));
        }


        public void Dispose()
        {
            _driver?.Dispose();
        }
    }
}
