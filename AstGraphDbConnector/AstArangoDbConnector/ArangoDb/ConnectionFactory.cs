using AstDomain;
using AstShared;
using System.IO;
using YamlDotNet.Serialization;

namespace AstArangoDbConnector
{
    public class ConnectionFactory : IConnectionFactory
    {
        public Config CreateConfig(string path)
        {
            if (!File.Exists(path))
            {
                return null;
            }

            var d = new Deserializer();
            using (var s = new StreamReader(path))
            {
                return d.Deserialize<Config>(s);
            }
        }
    }
}
