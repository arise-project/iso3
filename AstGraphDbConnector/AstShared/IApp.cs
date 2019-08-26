namespace AstShared
{
    public interface IApp
    {
        void ConfigureArabgoDbDatabase();

        void PrintConfig();

        void CreateArangoDbSyntaxClasses();

        void CreateArangoDbSyntaxCollections();

        void AnalyseCSharpFile();
    }
}
