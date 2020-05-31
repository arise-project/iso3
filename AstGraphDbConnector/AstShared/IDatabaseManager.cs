using AstDomain;

namespace AstShared
{
    public interface IDatabaseManager
    {
        bool CreateDatabase(Config config, string dbName);
        bool CheckDatabase(Config config);
        bool BackupDatabase(Config config, string backupFolder, string backupFileName);
        bool DeleteDatabase(Config config);
    }
}
