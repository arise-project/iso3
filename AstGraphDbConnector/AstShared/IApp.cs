namespace AstShared
{
    public interface IApp
    {
        void ConfigureArabgoDbDatabase();

        void PrintConfig();

        void CreateDatabase();

        void DatabaseStatistics();

        void DeleteDatabase();

        void CreateArangoDbSyntaxClasses();

        void CreateArangoDbSyntaxCollections();

        void AnalyseCSharpFile();
    }
}
