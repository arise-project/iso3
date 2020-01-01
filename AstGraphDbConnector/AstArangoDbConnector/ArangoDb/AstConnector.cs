using ArangoDB.Client;
using AstArangoDbConnector.Syntax;
using AstDomain;
using AstShared;
using AutoMapper;
using System;
using System.Linq;
using System.Reflection;

namespace AstArangoDbConnector
{
    public class AstConnector : IAstConnector
    {
        protected ArangoDatabase CreateDatabase(Config config)
        {
            return new ArangoDatabase(new DatabaseSharedSetting
            {
                Url = config.ArangoDbServer,
                Database = config.ArangoDbDatabse,
                Credential = new System.Net.NetworkCredential(config.ArangoDbUser, config.ArangoDbPassword)
            });
        }
    }
}
