using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DummyRp.DataContracts
{
    [Serializable]
    public class ArmResourceDefinition
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("tags")]
        public string Tags  { get; set; }

        [JsonProperty("properties")]
        public JToken Properties { get; set; }
    }
}
