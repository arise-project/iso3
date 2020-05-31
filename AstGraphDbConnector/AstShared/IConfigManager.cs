using AstDomain;

namespace AstShared
{
    public interface IConfigManager
    {
        Config ReadConfig(string path);
        void WriteConfig(Config config, string path);
    }
}
