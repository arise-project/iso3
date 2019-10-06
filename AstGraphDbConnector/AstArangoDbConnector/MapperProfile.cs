using AstArangoDbConnector.Syntax;
using AstDomain;
using AutoMapper;

namespace AstArangoDbConnector
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<ConcreteSyntax, ConcreteSyntaxEntity>();
            CreateMap<ConcreteSyntaxEntity, ConcreteSyntax>();

            CreateMap<CodeSyntax, CodeSyntaxEntity>();
            CreateMap<CodeSyntaxEntity, CodeSyntax>();

            CreateMap<BaseSyntax, BaseSyntaxEntity>();
            CreateMap<BaseSyntaxEntity, BaseSyntax>();

            CreateMap<BaseSyntaxCollection, BaseSyntaxCollectionEntity>();
            CreateMap<BaseSyntaxCollectionEntity, BaseSyntaxCollection>();
        }
    }
}
