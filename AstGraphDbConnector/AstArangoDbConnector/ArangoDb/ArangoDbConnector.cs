using System.Net;
using ArangoDB.Client;
using AstDomain;
using AstShared;

namespace AstArangoDbConnector
{
    public class ArangoDbConnector : IArangoDbConnector
    {
        protected ArangoDatabase GetDatabase(Config config) => 
        new ArangoDatabase(new DatabaseSharedSetting
        {
            Url = config.ArangoDbServer,
            Database = config.ArangoDbDatabse,
            Credential = new NetworkCredential(config.ArangoDbUser, config.ArangoDbPassword),
            SystemDatabaseCredential = new NetworkCredential(config.ArangoDbUser, config.ArangoDbPassword),
        });
    }
}
