using AstDomain;

namespace AstShared
{
    public interface IConnectionFactory
    {
        Config CreateConfig(string path);
    }
}
