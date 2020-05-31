using AstDomain;
using AstShared;
using System;
using System.IO;
using YamlDotNet.Serialization;

namespace AstArangoDbConnector
{
    public class ConfigManager : IConfigManager
    {
        public Config ReadConfig(string path)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("WARN: Empaty configuration created in memory");
                return new Config();
            }

            var d = new Deserializer();
            using (var s = new StreamReader(path))
            {
                return d.Deserialize<Config>(s);
            }
        }

        public void WriteConfig(Config config, string path)
        {
            var s = new Serializer();
            File.WriteAllText(path, s.Serialize(config));
        }
    }
}
