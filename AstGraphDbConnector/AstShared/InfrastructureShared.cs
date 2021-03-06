﻿using Microsoft.Extensions.DependencyInjection;
using StructureMap;
using System;
using AstDomain;

namespace AstShared
{
    public class InfrastructureShared
    {
        //https://andrewlock.net/using-dependency-injection-in-a-net-core-console-application/
        //https://andrewlock.net/getting-started-with-structuremap-in-asp-net-core/
        public static IServiceProvider Init(IServiceCollection services)
        {
            var container = new Container();

            container.Configure(config =>
            {
                // Register stuff in container, using the StructureMap APIs...
                config.Scan(_ =>
                {
                    _.AssembliesFromApplicationBaseDirectory();
                    _.WithDefaultConventions();
                    _.AddAllTypesOf<IBaseSyntaxVisitor>();
                    _.AddAllTypesOf<IClassGenerator>();
                    _.AddAllTypesOf<ICsvGenerator>();
                    _.AddAllTypesOf<ICodeVisitor>();
                    _.AddAllTypesOf<IConcreteSyntaxVisitor>();
                    _.AddAllTypesOf<ISyntaxGeneratorVisitor>();
                    _.AddAllTypesOf<ISyntaxCsvVisitor>();
                    _.AddAllTypesOf<ISyntaxWalker>();
                    _.AddAllTypesOf<IApp>();
                    _.AddAllTypesOf<ISyntaxNodesToClasses>();
                    _.AddAllTypesOf<ISyntaxNodesToCollections>();
                    _.AddAllTypesOf<ISyntaxNodesToCsv>();
                    _.AddAllTypesOf<IArangoDbConnector>();
                    _.AddAllTypesOf<IConfigManager>();
                    _.AddAllTypesOf<IDatabaseManager>();
                    _.AddAllTypesOf<IRepository<SyntaxCoollectionEntity>>();
                    _.AddAllTypesOf<IRepository<CodeSyntaxEntity>>();
                    _.AddAllTypesOf<IRepository<BaseSyntaxEntity>>();
                    _.AddAllTypesOf<IRepository<ConcreteSyntaxEntity>>();
                });

                //Populate the container using the service collection
                config.Populate(services);
            });

            return container.GetInstance<IServiceProvider>();
        }
    }
}
