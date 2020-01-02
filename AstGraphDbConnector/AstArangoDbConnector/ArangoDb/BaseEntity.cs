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
    public class BaseEntity
    {
        [DocumentProperty(Identifier = IdentifierType.Key)]
        public string Key;

    }
}