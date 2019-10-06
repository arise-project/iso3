using Microsoft.Extensions.DependencyInjection;
using StructureMap;
using System;

namespace AstShared
{
    public class Infrastructure
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
                    _.AddAllTypesOf<ICodeVisitor>();
                    _.AddAllTypesOf<IConcreteSyntaxVisitor>();
                    _.AddAllTypesOf<ISyntaxGeneratorVisitor>();
                    _.AddAllTypesOf<ISyntaxWalker>();
                    _.AddAllTypesOf<IApp>();
                    _.AddAllTypesOf<ISyntaxNodesToClasses>();
                    _.AddAllTypesOf<ISyntaxNodesToCollections>();
                    _.AddAllTypesOf<IAstConnector>();
                });

                //Populate the container using the service collection
                config.Populate(services);
            });

            return container.GetInstance<IServiceProvider>();
        }
    }
}
