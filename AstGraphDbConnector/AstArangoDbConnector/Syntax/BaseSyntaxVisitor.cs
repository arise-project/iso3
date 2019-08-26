﻿using AstShared;
using System;

namespace AstArangoDbConnector.Syntax
{
    public class BaseSyntaxVisitor : IBaseSyntaxVisitor
    {
        AstConnector _connector;

        public BaseSyntaxVisitor(AstConnector connector)
        {
            _connector = connector;
        }
        public void Visit(Type t)
        {
            BaseSyntax baseSyntax = new BaseSyntax { Name = t.Name, FullName = t.FullName };
            _connector.CreateSyntaxAbstractDefinition(baseSyntax);
            _connector.CreateSyntaxCollection(baseSyntax);
        }
    }
}
