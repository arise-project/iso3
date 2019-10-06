using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace AstArangoDbConnector
{
    public class InfrastructureAstArangoDbConnector
    {
        public static void Init(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(InfrastructureAstArangoDbConnector).Assembly);
        }
    }
}
