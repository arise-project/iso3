using Newtonsoft.Json;
using System.Collections.Generic;

namespace ArangoDB.Client.Examples.Models
{
    [CollectionProperty(Naming = NamingConvention.ToCamelCase, CollectionName = "hosts")]
    public class Host
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        [DocumentProperty(Identifier = IdentifierType.Key)]
        public string Key { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Ip { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public IList<string> Tags { get; set; }
    }
}